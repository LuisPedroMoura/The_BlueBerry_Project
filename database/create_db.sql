---------------------------------------------------------------
--- RETURN ERROR RULES ----------------------------------------
---------------------------------------------------------------
/*
-1 : Missing Argument
-2 : Row to insert already exists
-3 : Row to update does not exist
-4 : Row to delete does not exist
-5 : Argument to select does not exist
-6 : Invalid argument
*/

--------------------------------------------------------------------
-- CREATE CLEANING PROCEDURE and CLEAN PROJECT ---------------------
--------------------------------------------------------------------
CREATE PROC clean_procedures_and_functions
AS
BEGIN
	DROP PROC pr_insert_user;
	DROP PROC pr_update_user;
	DROP PROC pr_delete_subscription;
	DROP PROC pr_delete_user;
	DROP PROC pr_select_users;
	DROP PROC pr_exists_user;

	DROP PROC pr_select_recurrences;
	DROP PROC pr_select_recurrence_id;

	DROP PROC pr_insert_money_account;
	DROP PROC pr_delete_money_account;
	DROP PROC pr_select_money_accounts;
	DROP PROC pr_select_user_money_accounts;
	DROP PROC pr_money_account_add_user;
	DROP PROC pr_money_account_remove_user;
	DROP PROC pr_exists_money_account;

	DROP PROC pr_insert_wallet;

	DROP PROC pr_select_categories;
	DROP PROC pr_insert_category;
	DROP PROC pr_insert_subcategory;
	DROP PROC pr_delete_category;
	DROP PROC pr_select_category_type_by_designation;

	DROP PROC pr_select_loans;
	DROP PROC pr_exists_loan;
	DROP PROC pr_insert_loan;

	DROP PROC pr_select_budgets;
	DROP PROC pr_insert_budget;

	DROP TRIGGER Project.create_basic_categories_on_user_insert;
END
GO

CREATE PROC clean_project
AS
BEGIN
	
	IF EXISTS (SELECT * FROM sys.schemas WHERE name = 'Project')
	BEGIN
		alter table Project.wallets drop constraint FK_WALLETS_MONEYACCOUNTS;
		alter table Project.loans drop constraint FK_LOANS_MONEYACCOUNTS;
		alter table Project.purchased_stocks drop constraint FK_PURCHASEDSTOCKS_MONEYACCOUNTS;
		alter table Project.categories drop constraint FK_CATEGORIES_MONEYACCOUNTS;
		alter table Project.users_money_accounts drop constraint FK_USERSMONEYACCOUNTS_MONEYACCOUNTS;
		alter table Project.money_accounts drop constraint PK_MONEYACCOUNTS;
		alter table Project.subscriptions drop constraint FK_SUBSCRIPTIONS_USERS;
		alter table Project.subscriptions drop constraint FK_SUBSCRIPTIONS_RECURRENCE;
		alter table Project.users_money_accounts drop constraint FK_USERSMONEYACCOUNTS_USERS;
		alter table Project.categories drop constraint FK_CATEGORIES_CATEGORYTYPES;
		alter table Project.transactions drop constraint FK_TRANSACTIONS_CATEGORIES;
		alter table Project.transactions drop constraint FK_TRANSACTIONS_TRANSACTIONTYPES;
		alter table Project.transactions drop constraint FK_TRANSACTIONS_WALLETS;
		alter table Project.transfers drop constraint FK_TRANSFERS_TRANSACTIONS;
		alter table Project.transfers drop constraint FK_TRANSFERS_WALLETS;
		alter table Project.budgets drop constraint FK_BUDGETS_RECURRENCE;
		alter table Project.budgets drop constraint FK_BUDGETS_CATEGORIES;
		alter table Project.goals drop constraint FK_GOALS_CATEGORIES;
		alter table Project.stocks drop constraint FK_STOCKS_STOCKTYPES;
		alter table Project.purchased_stocks drop constraint FK_PURCHASEDSTOCKS_STOCKS;
	
		drop table Project.money_accounts;
		drop table Project.wallets;
		drop table Project.recurrence;
		drop table Project.subscriptions;
		drop table Project.users;
		drop table Project.users_money_accounts;
		drop table Project.category_types;
		drop table Project.categories;
		drop table Project.transaction_types;
		drop table Project.transactions;
		drop table Project.transfers;
		drop table Project.budgets;
		drop table Project.goals;
		drop table Project.loans;
		drop table Project.stock_types;
		drop table Project.stocks;
		drop table Project.purchased_stocks;

		EXEC clean_procedures_and_functions;
		DROP PROC clean_procedures_and_functions;

		drop schema Project; -- this procedure is droped when droping schema
	END
	
	ELSE
	BEGIN
		DROP PROC clean_procedures_and_functions;
	END
	
END
GO

EXEC clean_project;
GO

DROP PROC clean_project;
GO

--------------------------------------------------------------------
-- CREATE SCHEMA ---------------------------------------------------
--------------------------------------------------------------------

IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'Project')
BEGIN
	EXEC('CREATE SCHEMA Project')
END
GO

--------------------------------------------------------------------
-- CREATE PROCEDURES AND FUNCTIONS ---------------------------------
--------------------------------------------------------------------

CREATE PROC pr_insert_user (
	@email varchar(50) = NULL,
	@card_number varchar(20) = NULL,
	@term DATETIME = NULL,
	@fname VARCHAR(20) = NULL,	
	@lname VARCHAR(20) = NULL,
	@periodicity INT = NULL,
	@user_name varchar(20) = NULL,
	@mname VARCHAR(20) = NULL,
	@active BIT = 0
) AS
BEGIN

	INSERT INTO Project.users(email, [user_name])
	VALUES (@email, @user_name);

	IF @active=1
	BEGIN
		INSERT INTO Project.subscriptions(card_number, email, term, fname, mname, lname, active, periodicity)
		VALUES (@card_number, @email, @term, @fname, @mname, @lname, @active, @periodicity);
	END

END
GO

CREATE PROC pr_delete_subscription (
	@email varchar(50)
) AS
BEGIN
	DELETE FROM Project.subscriptions WHERE email=@email;
END
GO

CREATE PROC pr_delete_user (
	@email varchar(50)
) AS
BEGIN
	
	EXEC pr_delete_subscription @email;
	DELETE FROM Project.users WHERE email=@email;
END
GO

CREATE PROC pr_update_user (
	@email varchar(50) = NULL,
	@card_number varchar(20) = NULL,
	@term DATETIME = NULL,
	@fname VARCHAR(20) = NULL,	
	@lname VARCHAR(20) = NULL,
	@periodicity INT = NULL,
	@user_name varchar(20) = NULL,
	@mname VARCHAR(20) = NULL,
	@active BIT = NULL
) AS
BEGIN
	
	-- verify that email was given
	IF @email IS NULL
		RETURN -1

	-- verify if user exists
	IF NOT EXISTS (SELECT email FROM Project.users WHERE email=@email)
		RETURN -2

	-- verify if user was subscribed
	DECLARE @subscribed BIT;
	SET @subscribed = ISNULL((SELECT card_number FROM Project.subscriptions WHERE email=@email), 0);

	-- update 'users' table
	UPDATE Project.users
	SET [user_name]=ISNULL(@user_name, [user_name])
	WHERE email=@email;

	IF @subscribed=1 AND (@active=1 OR @active IS NULL)
	BEGIN

		UPDATE Project.subscriptions
		SET
			card_number=ISNULL(@card_number, card_number),
			term=ISNULL(@term, term),
			fname=ISNULL(@fname, fname),
			mname=ISNULL(@mname, mname),
			lname=ISNULL(@lname, lname),
			periodicity=ISNULL(@periodicity, periodicity)
		WHERE email=@email;
	END

	IF @subscribed=1 AND @active=0
	BEGIN
		IF @subscribed=1
		BEGIN
			EXEC pr_delete_subscription @email;
		END
	END

	IF @subscribed=0 AND @active=1
	BEGIN
		INSERT INTO Project.subscriptions (card_number, email,term, fname, mname, lname, periodicity, active)
		VALUES (@card_number, @email, @term, @fname, @mname, @lname, @periodicity, @active);
	END	
END
GO

CREATE PROC pr_select_users (
	@email varchar(50) = NULL,
	@card_number varchar(20) = NULL,
	@term DATETIME = NULL,
	@fname VARCHAR(20) = NULL,	
	@lname VARCHAR(20) = NULL,
	@periodicity INT = NULL,
	@user_name varchar(20) = NULL,
	@mname VARCHAR(20) = NULL,
	@active BIT = NULL
) AS
BEGIN
	
	SELECT *
	FROM
		Project.users AS U LEFT JOIN Project.subscriptions AS S
		ON U.email=S.email
	WHERE
			(U.email=@email OR @email IS NULL)
		AND (card_number=@card_number OR @card_number IS NULL)
		AND (term=@term OR @term IS NULL)
		AND (fname=@fname OR @fname IS NULL)
		AND (mname=@mname OR @mname IS NULL)
		AND (lname=@lname OR @lname IS NULL)
		AND (periodicity=@periodicity OR @periodicity IS NULL)
		AND ([user_name]=@user_name OR @user_name IS NULL)
		AND (active=@active OR @active IS NULL);
END
GO

CREATE PROC pr_exists_user (
	@email varchar(50)
) AS
BEGIN

	IF EXISTS (SELECT email FROM Project.users WHERE email=@email)
		SELECT 1;
	ELSE
		SELECT 0;
END
GO

---------------------------------------------------------------
--- RECURRENCE ------------------------------------------------
---------------------------------------------------------------

CREATE PROC pr_select_recurrences (
	@periodicity INT = NULL,
	@designation VARCHAR(20) = NULL
) AS
BEGIN

	SELECT *
	FROM
		Project.recurrence
	WHERE
			(periodicity=@periodicity OR @periodicity IS NULL)
		AND (designation=@designation OR @designation IS NULL);
END
GO

CREATE PROC pr_select_recurrence_id (
	@designation VARCHAR(20)
) AS
BEGIN

	SELECT periodicity
	FROM Project.recurrence
	WHERE designation=@designation;
END
GO
	
---------------------------------------------------------------
--- MONEY ACCOUNTS --------------------------------------------
---------------------------------------------------------------

CREATE PROC pr_insert_money_account (
	@user_email varchar(50),
	@account_name varchar(20),
	@balance MONEY = 0.0,
	@patrimony MONEY = 0.0
) AS
BEGIN
	-- verify that user does not have a money account with same name
	IF EXISTS (
		SELECT *
		FROM
			Project.money_accounts AS M LEFT JOIN Project.users_money_accounts AS U
			ON M.account_id=U.account_id
		WHERE M.account_name=@account_name AND U.user_email=@user_email)
			RETURN -1

	-- insert new money_account
	INSERT INTO Project.money_accounts(account_name, balance, patrimony)
	VALUES (@account_name, @balance, @patrimony);
	-- get new money_account id
	DECLARE @account_id INT;
	SET @account_id = SCOPE_IDENTITY();
	-- create relation between money account and user
	INSERT INTO Project.users_money_accounts([user_email], account_id)
	VALUES (@user_email, @account_id);
END
GO

CREATE PROC pr_delete_money_account (
	@account_id INT
) AS
BEGIN

	-- verify that given account id exists
	IF NOT EXISTS (SELECT account_id FROM Project.money_accounts WHERE id=@account_id)
		RETURN -2

	-- delete money_account
	DELETE FROM Project.users_money_accounts
	WHERE account_id=@account_id;

	DELETE FROM Project.money_accounts
	WHERE account_id=@account_id;
END
GO

CREATE PROC pr_select_money_accounts (
	@account_id INT = NULL,
	@account_name varchar(20) = NULL,
	@balanceMin MONEY = NULL,
	@balanceMax MONEY = NULL,
	@patrimonyMin MONEY = NULL,
	@patrimonyMax MONEY = NULL
) AS
BEGIN
	
	SELECT *
	FROM Project.money_accounts
	WHERE
			(account_id=@account_id OR @account_id IS NULL)
		AND (account_name=@account_name OR @account_name IS NULL)
		AND (balance>=@balanceMin OR @balanceMin IS NULL)
		AND (balance<=@balanceMax OR @balanceMax IS NULL)
		AND (patrimony>=@patrimonyMin OR @patrimonyMin IS NULL)
		AND (patrimony<=@patrimonyMax OR @patrimonyMax IS NULL)
END
GO

CREATE PROC pr_select_user_money_accounts (
	@account_id INT = NULL,
	@user_email VARCHAR(50) = NULL
) AS
BEGIN
	
	IF @user_email IS NOT NULL
	BEGIN
		IF NOT EXISTS (SELECT * FROM Project.users WHERE email=@user_email)
			RETURN -5
	END

	SELECT UMA.user_email AS user_email, MA.account_id AS account_id, MA.account_name AS account_name, MA.balance AS balance, MA.patrimony AS patrimony
	FROM
		Project.users_money_accounts AS UMA LEFT JOIN Project.money_accounts AS MA
		ON UMA.account_id=MA.account_id
	WHERE
			(UMA.user_email=@user_email OR @user_email IS NULL)
		AND (MA.account_id=@account_id OR @account_id IS NULL);
END
GO

CREATE PROC pr_money_account_add_user (
	@account_id INT,
	@user_email VARCHAR(50)
) AS
BEGIN
	
	IF NOT EXISTS (SELECT * FROM Project.users WHERE email=@user_email)
		RETURN -5

	IF NOT EXISTS (SELECT * FROM Project.money_accounts WHERE account_id=@account_id)
		RETURN -5

	INSERT INTO Project.users_money_accounts(account_id, user_email)
	VALUES (@account_id, @user_email)
END
GO

CREATE PROC pr_money_account_remove_user (
	@account_id INT,
	@user_email VARCHAR(50)
) AS
BEGIN
	
	IF NOT EXISTS (SELECT * FROM Project.users_money_accounts WHERE user_email=@user_email)
		RETURN -5

	IF NOT EXISTS (SELECT * FROM Project.money_accounts WHERE account_id=@account_id)
		RETURN -5

	DELETE FROM Project.users_money_accounts
	WHERE account_id=@account_id AND user_email=@user_email
END
GO

CREATE PROC pr_exists_money_account (
	@account_id varchar(50)
) AS
BEGIN
	
	DECLARE @exists BIT = 1;
	IF EXISTS (SELECT * FROM Project.money_accounts WHERE account_id=@account_id)
		SELECT @exists;
	
	ELSE
	BEGIN
		SET @exists = 0;
		SELECT @exists;
	END
END
GO

---------------------------------------------------------------
--- WALLETS ---------------------------------------------------
---------------------------------------------------------------

CREATE PROC pr_insert_wallet (
	@account_id varchar(50),
	@name varchar(20),
	@balance MONEY = 0.0
) AS
BEGIN
	
	-- verify that wallet with same name does not exist in account
	IF EXISTS (SELECT * FROM Project.wallets WHERE account_id=@account_id AND [name]=@name)
		RETURN -2;

	INSERT INTO Project.wallets(account_id, [name], balance)
	VALUES (@account_id, @name, @balance);
END
GO

---------------------------------------------------------------
--- CATEGORIES ------------------------------------------------
---------------------------------------------------------------

CREATE PROC pr_select_categories (
	@category_id INT = NULL,
	@account_id INT = NULL,
	@name VARCHAR(20) = NULL
) AS
BEGIN
	
	SELECT *
	FROM Project.categories
	WHERE
			(category_id=@category_id OR @category_id IS NULL)
		AND (account_id=@account_id OR @account_id IS NULL)
		AND ([name]=@name OR @name IS NULL)
	ORDER BY category_id;
END
GO

CREATE PROC pr_insert_category (
	@category_type_id INT,
	@account_id INT,
	@name VARCHAR(20)
) AS
BEGIN
	
	-- calculate new category id
	DECLARE @category_id INT;
	SET @category_id = (
		SELECT MAX(category_id)
		FROM Project.categories
		WHERE account_id=@account_id
	);
	SET @category_id = ((@category_id%100)+1)*100;

	INSERT INTO Project.categories (category_id, category_type_id, account_id, [name])
	VALUES (@category_id, @category_type_id, @account_id, @name);
END
GO

CREATE PROC pr_insert_subcategory (
	@category_id INT,
	@category_type_id INT,
	@account_id INT,
	@name VARCHAR(20)
) AS
BEGIN
	
	-- calculate new category id
	DECLARE @subcategory_id INT;
	SET @subcategory_id = (
		SELECT MAX(category_id)
		FROM Project.categories
		WHERE
				account_id=@account_id
			AND category_id>(@category_id%100)*100
			AND category_id<((@category_id%100)+1)*100
	);	
	SET @category_id = @category_id + 1;

	INSERT INTO Project.categories (category_id, category_type_id, account_id, [name])
	VALUES (@category_id, @category_type_id, @account_id, @name);
END
GO

CREATE PROC pr_delete_category (
	@category_id INT,
	@account_id INT
) AS
BEGIN

	DELETE FROM Project.categories
	WHERE
			category_id=@category_id
		AND account_id=@account_id;
END
GO

CREATE PROC pr_select_category_type_by_designation 
	@designation VARCHAR(20),
) AS
BEGIN
	
	SELECT category_type_id
	FROM Project.category_types
	WHERE designation=@designation;
END
GO


---------------------------------------------------------------
--- LOANS -----------------------------------------------------
---------------------------------------------------------------

CREATE PROC pr_select_loans (
	@name VARCHAR(20) = NULL,
	@amount MONEY = NULL,
	@term DATETIME = NULL,
	@interest DECIMAL(5,2) = NULL,
	@account_id INT = NULL
) AS
BEGIN
	
	SELECT *
	FROM Project.loans
	WHERE
			([name]=@name OR @name IS NULL)
		AND (amount=@amount OR @amount IS NULL)
		AND (term=@term OR @term IS NULL)
		AND (interest=@interest OR @interest IS NULL)
		AND (account_id=@account_id OR @account_id IS NULL)
END
GO

CREATE PROC pr_exists_loan (
	@account_id INT,
	@name VARCHAR(20)
) AS
BEGIN
	
	IF EXISTS (SELECT * FROM Project.loans WHERE account_id=@account_id AND [name]=@name)
		SELECT 1;
	ELSE
		SELECT 0;
END
GO

CREATE PROC pr_insert_loan (
	@account_id INT = NULL,
	@name VARCHAR(20) = NULL,
	@amount MONEY = NULL,
	@term DATETIME = NULL,
	@interest DECIMAL(5,2) = NULL
) AS
BEGIN
	
	-- verify if account exists
	IF NOT EXISTS (SELECT * FROM Project.money_accounts WHERE account_id=@account_id)
		RETURN -2

	-- verify if loan already exists
	--IF EXISTS (SELECT * FROM Project.loans WHERE account_id=@account_id AND [name]=@name)
	--	RETURN -2

	INSERT INTO Project.loans (account_id, [name], amount, term, interest)
	VALUES (@account_id, @name, @amount, @term, @interest)
END
GO

---------------------------------------------------------------
--- BUDGETS -----------------------------------------------------
---------------------------------------------------------------

CREATE PROC pr_select_budgets (
	@account_id INT = NULL,
	@category_id INT = NULL,
	@budget_id INT = NULL,
	@amount MONEY = NULL,
	@periodicity INT = NULL,
	@start_date DATETIME = NULL,
	@end_date DATETIME = NULL
) AS
BEGIN
	
	SELECT *
	FROM Project.budgets
	WHERE
			(account_id=@account_id OR @account_id IS NULL)
		AND (category_id=@category_id OR @category_id IS NULL)
		AND (budget_id=@budget_id OR @budget_id IS NULL)
		AND (amount=@amount OR @amount IS NULL)
		AND (periodicity=@periodicity OR @periodicity IS NULL)
		AND ([start_date]=@start_date OR @start_date IS NULL)
		AND (end_date=@end_date OR @end_date IS NULL)
END
GO

CREATE PROC pr_insert_budget (
	@account_id INT,
	@category_id INT,
	@amount MONEY = 0.0,
	@periodicity INT = 1,
	@start_date DATETIME GETDATE(),
	@end_date DATETIME = NULL
) AS
BEGIN
	
	INSERT INTO Project.budgets (account_id, category_id, amount, periodicity, [start_date], end_date)
	VALUES (@account_id, @category_id, @amount, @periodicity, @start_date, @end_date)
END
GO


------------------------------------------------------------------------------------------------------------------------
-- CREATE DATABASE TABLES ----------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------

CREATE TABLE Project.money_accounts
(
	account_id INT IDENTITY(1,1),
	account_name VARCHAR(20),
	balance MONEY DEFAULT 0.0,
	patrimony MONEY DEFAULT 0.0,
	CHECK (account_name!=''),
	CONSTRAINT PK_MONEYACCOUNTS PRIMARY KEY (account_id)
);

CREATE TABLE Project.wallets
(
	wallet_id INT IDENTITY(1,1),
	[name] VARCHAR(20),
	account_id INT,
	balance MONEY,	
	CONSTRAINT PK_WALLETS PRIMARY KEY (wallet_id),
	CONSTRAINT FK_WALLETS_MONEYACCOUNTS FOREIGN KEY (account_id) REFERENCES Project.money_accounts(account_id)
		ON DELETE NO ACTION ON UPDATE CASCADE
);

CREATE TABLE Project.recurrence
(
	designation VARCHAR(20),
	periodicity INT,
	CONSTRAINT PK_RECURRENCE PRIMARY KEY (periodicity)
);

CREATE TABLE Project.users
(
	email VARCHAR(50),
	[user_name] VARCHAR(20),
	CHECK([user_name] != ''),
	CONSTRAINT PK_USERS PRIMARY KEY (email),
);

CREATE TABLE Project.subscriptions
(
	card_number VARCHAR(20),
	email VARCHAR(50),
	term DATETIME NOT NULL, --DEFAULT DATEADD(year, 1, GETDATE()),
	fname VARCHAR(20) NOT NULL,
	mname VARCHAR(20),
	lname VARCHAR(20) NOT NULL,
	active BIT NOT NULL DEFAULT(0),
	periodicity INT NOT NULL,
	CHECK(card_number>0),
	CONSTRAINT PK_SUBSCRIPTIONS PRIMARY KEY (card_number),
	CONSTRAINT FK_SUBSCRIPTIONS_USERS FOREIGN KEY (email) REFERENCES Project.users(email)
		ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT FK_SUBSCRIPTIONS_RECURRENCE FOREIGN KEY (periodicity) REFERENCES Project.recurrence(periodicity)
		ON DELETE NO ACTION ON UPDATE CASCADE	
);

CREATE TABLE Project.users_money_accounts
(
	user_email VARCHAR(50),
	account_id INT,
	CONSTRAINT PK_USERSMONEYACCOUNTS PRIMARY KEY (user_email, account_id),
	CONSTRAINT FK_USERSMONEYACCOUNTS_USERS FOREIGN KEY (user_email) REFERENCES Project.users(email)
		ON DELETE NO ACTION ON UPDATE CASCADE,
	CONSTRAINT FK_USERSMONEYACCOUNTS_MONEYACCOUNTS FOREIGN KEY (account_id) REFERENCES Project.money_accounts(account_id)
		ON DELETE NO ACTION ON UPDATE CASCADE		
);

CREATE TABLE Project.category_types
(
	category_type_id INT,
	designation VARCHAR(20)
	CONSTRAINT PK_CATEGORYTYPES PRIMARY KEY (category_type_id)
)

CREATE TABLE Project.categories
(
	category_id INT,
	[name] VARCHAR(20),
	account_id INT,
	category_type_id INT NOT NULL,
	CHECK([name] != ''),
	CONSTRAINT PK_CATEGORIES PRIMARY KEY (category_id, account_id),
	CONSTRAINT FK_CATEGORIES_MONEYACCOUNTS FOREIGN KEY (account_id) REFERENCES Project.money_accounts(account_id)
		ON DELETE NO ACTION ON UPDATE CASCADE,
	CONSTRAINT FK_CATEGORIES_CATEGORYTYPES FOREIGN KEY (category_type_id) REFERENCES Project.category_types(category_type_id)
		ON DELETE NO ACTION ON UPDATE CASCADE
);

CREATE TABLE Project.transaction_types
(
	transaction_type_id INT,
	designation VARCHAR(20),
	CONSTRAINT PK_TRANSACTIONTYPES PRIMARY KEY (transaction_type_id)
);

CREATE TABLE Project.transactions
(
	transaction_id INT IDENTITY(1,1),
	amount INT,
	[date] DATETIME,
	notes VARCHAR(50),
	[location] VARCHAR(50),
	category_id INT,
	account_id INT,
	transaction_type_id INT,
	wallet_id INT,
	CHECK(amount>0),
	CONSTRAINT PK_TRANSACTIONS PRIMARY KEY (transaction_id),
	CONSTRAINT FK_TRANSACTIONS_CATEGORIES FOREIGN KEY (category_id, account_id) REFERENCES Project.categories(category_id, account_id)
		ON DELETE NO ACTION ON UPDATE CASCADE,
	CONSTRAINT FK_TRANSACTIONS_TRANSACTIONTYPES FOREIGN KEY (transaction_type_id) REFERENCES Project.transaction_types(transaction_type_id)
		ON DELETE NO ACTION ON UPDATE CASCADE,
	CONSTRAINT FK_TRANSACTIONS_WALLETS FOREIGN KEY (wallet_id) REFERENCES Project.wallets(wallet_id)
		ON DELETE NO ACTION ON UPDATE NO ACTION
);

CREATE TABLE Project.transfers
(
	transaction_id INT,
	recipient_wallet_id INT,
	CONSTRAINT PK_TRANSFERS PRIMARY KEY (transaction_id),
	CONSTRAINT FK_TRANSFERS_TRANSACTIONS FOREIGN KEY (transaction_id) REFERENCES Project.transactions(transaction_id)
		ON DELETE NO ACTION ON UPDATE CASCADE,
	CONSTRAINT FK_TRANSFERS_WALLETS FOREIGN KEY (recipient_wallet_id) REFERENCES Project.wallets(wallet_id)
		ON DELETE NO ACTION ON UPDATE NO ACTION,
);

CREATE TABLE Project.budgets
(
	budget_id INT IDENTITY(1,1),
	category_id INT,
	account_id INT,
	amount INT DEFAULT 0,
	periodicity INT,
	[start_date] DATETIME,
	end_date DATETIME,
	CONSTRAINT PK_BUDGETS PRIMARY KEY (budget_id, category_id, account_id),
	CONSTRAINT FK_BUDGETS_RECURRENCE FOREIGN KEY (periodicity) REFERENCES Project.recurrence(periodicity)
		ON DELETE NO ACTION ON UPDATE CASCADE,
	CONSTRAINT FK_BUDGETS_CATEGORIES FOREIGN KEY (category_id, account_id) REFERENCES Project.categories(category_id, account_id)
		ON DELETE NO ACTION ON UPDATE CASCADE
);

CREATE TABLE Project.goals
(
	[name] VARCHAR(20),
	category_id INT,
	account_id INT,
	amount INT DEFAULT 0,
	term DATETIME,
	accomplished INT,
	CHECK([name] != ''),
	CONSTRAINT PK_GOALS PRIMARY KEY ([name], category_id, account_id),
	CONSTRAINT FK_GOALS_CATEGORIES FOREIGN KEY (category_id, account_id) REFERENCES Project.categories(category_id, account_id)
		ON DELETE NO ACTION ON UPDATE CASCADE
);

CREATE TABLE Project.loans
(
	[name] VARCHAR(20),
	account_id INT,
	amount MONEY,
	term DATETIME,
	interest DECIMAL(5,2) DEFAULT(0.0),
	CHECK([name] != ''),
	CHECK(amount>=0),
	CHECK(interest BETWEEN 0 AND 100),
	CONSTRAINT PK_LOANS PRIMARY KEY ([name], account_id),
	CONSTRAINT FK_LOANS_MONEYACCOUNTS FOREIGN KEY (account_id) REFERENCES Project.money_accounts(account_id)
		ON DELETE NO ACTION ON UPDATE CASCADE
);

CREATE TABLE Project.stock_types
(
	stock_type_id INT,
	designation VARCHAR(20),
	CONSTRAINT PK_STOCKTYPES PRIMARY KEY (stock_type_id),
	UNIQUE (designation)
);

CREATE TABLE Project.stocks
(
	company VARCHAR(50),
	bid_price INT,
	ask_price INT,
	stock_type_id INT,
	CHECK(company != ''),
	CONSTRAINT PK_STOCKS PRIMARY KEY (company),
	CONSTRAINT FK_STOCKS_STOCKTYPES FOREIGN KEY (stock_type_id) REFERENCES Project.stock_types(stock_type_id)
		ON DELETE NO ACTION ON UPDATE CASCADE
);

CREATE TABLE Project.purchased_stocks
(
	ticker INT IDENTITY(1,1),
	company VARCHAR(50),
	account_id INT,
	purchase_value INT,
	quantity INT,
	CONSTRAINT PK_PURCHASEDSTOCKS PRIMARY KEY (ticker),
	CONSTRAINT FK_PURCHASEDSTOCKS_STOCKS FOREIGN KEY (company) REFERENCES Project.stocks(company)
		ON DELETE NO ACTION ON UPDATE CASCADE,
	CONSTRAINT FK_PURCHASEDSTOCKS_MONEYACCOUNTS FOREIGN KEY (account_id) REFERENCES Project.money_accounts(account_id)
		ON DELETE NO ACTION ON UPDATE CASCADE
);

--------------------------------------------------------------------
-- INSERTION OF BASIC VALUES ---------------------------------------
--------------------------------------------------------------------

INSERT INTO Project.recurrence (designation, periodicity)
VALUES 	
		-- ('personalized', 0),
		('monthly', 1),
		('quarterly', 3),
		('anual', 12);

INSERT INTO Project.transaction_types (designation,transaction_type_id)
VALUES 	
		('expense', -1),
		('transfer', 0),
		('income', 1);

INSERT INTO Project.stock_types (designation,stock_type_id)
VALUES 	
		('common', 0),
		('preferred', 1);

INSERT INTO Project.category_types (designation, category_type_id)
VALUES
		('income', 1),
		('expense', -1);

GO

--------------------------------------------------------------------
-- TRIGGERS --------------------------------------------------------
--------------------------------------------------------------------

CREATE TRIGGER create_basic_categories_on_user_insert
ON Project.money_accounts
AFTER INSERT
AS
BEGIN
	
	SET NOCOUNT ON

	DECLARE @account_id INT;
	SET @account_id = (SELECT account_id FROM INSERTED);

	INSERT INTO Project.categories(category_id, account_id, category_type_id, [name])
	VALUES (0, @account_id, 1, 'Income');
	INSERT INTO Project.categories(category_id, account_id, category_type_id, [name])
	VALUES (100, @account_id, -1, 'Transports');
	INSERT INTO Project.categories(category_id, account_id, category_type_id, [name])
	VALUES (101, @account_id, -1, 'Car gas');
	INSERT INTO Project.categories(category_id, account_id, category_type_id, [name])
	VALUES (102, @account_id, -1, 'Car maintenance');
	INSERT INTO Project.categories(category_id, account_id, category_type_id, [name])
	VALUES (200, @account_id, -1, 'Groceries');
	INSERT INTO Project.categories(category_id, account_id, category_type_id, [name])
	VALUES (300, @account_id, -1, 'Personal');
	INSERT INTO Project.categories(category_id, account_id, category_type_id, [name])
	VALUES (301, @account_id, -1, 'Hobbies');
	INSERT INTO Project.categories(category_id, account_id, category_type_id, [name])
	VALUES (302, @account_id, -1, 'Restaurant');
	INSERT INTO Project.categories(category_id, account_id, category_type_id, [name])
	VALUES (303, @account_id, -1, 'Night out');
END
GO



--------------------------------------------------------------------
-- POPULATE DATABASE -----------------------------------------------
--------------------------------------------------------------------

EXEC pr_insert_user @user_name='user1', @email='user1@ua.pt';
EXEC pr_insert_user @user_name='user2', @email='user2@ua.pt', @fname='user', @lname='two', @active=1, @card_number='1234567890', @term='2020-11-26', @periodicity=1;

EXEC pr_insert_money_account @user_email='user1@ua.pt', @account_name='u1_personal';
EXEC pr_insert_money_account @user_email='user2@ua.pt', @account_name='u2_personal';
EXEC pr_insert_money_account @user_email='user1@ua.pt', @account_name='u1_shared';
EXEC pr_money_account_add_user @account_id=3, @user_email='user2@ua.pt'

select * from Project.loans;
