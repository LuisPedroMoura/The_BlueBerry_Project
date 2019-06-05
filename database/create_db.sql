
--------------------------------------------------------------------
-- CREATE CLEANING PROCEDURE and CLEAN PROJECT ---------------------
--------------------------------------------------------------------
-- because of permissions we are unable to drop database or schema directly
-- (or because of SQL Server safety rules). The following sequence of 'drops'
-- allows an easy refactor of the database during database debugging.
-- This is only to be used during implementation, and should be commented
-- after 'installation' otherwise all tables data will be lost.

DROP TRIGGER IF EXISTS Project.tr_insert_base_wallets_on_new_money_account_insert;
DROP TRIGGER IF EXISTS Project.tr_insert_base_categories_on_new_money_account_insert;
DROP TRIGGER IF EXISTS Project.tr_insert_new_wallet_on_goal_insert;
DROP TRIGGER IF EXISTS Project.tr_update_goal_completion_with_wallet_transfer;
DROP TRIGGER IF EXISTS Project.tr_update_account_patrimony_on_wallet_balance_update
DROP TRIGGER IF EXISTS Project.tr_update_wallet_balance_on_loans_insert;
DROP TRIGGER IF EXISTS Project.tr_update_account_balance_on_wallet_balance_update;
DROP TRIGGER IF EXISTS Project.tr_update_wallet_balance_on_transaction_insert;
DROP TRIGGER IF EXISTS Project.tr_update_wallet_balance_on_transaction_delete;
DROP TRIGGER IF EXISTS Project.tr_update_wallet_balance_on_transfer_insert;
DROP TRIGGER IF EXISTS Project.tr_insert_new_transaction_on_loan_payment_update;

DROP PROC IF EXISTS pr_insert_user;
DROP PROC IF EXISTS pr_update_user;
DROP PROC IF EXISTS pr_delete_subscription;
DROP PROC IF EXISTS pr_delete_user;
DROP PROC IF EXISTS pr_select_users;
DROP PROC IF EXISTS pr_exists_user;

DROP PROC IF EXISTS pr_select_recurrences;
DROP PROC IF EXISTS pr_select_recurrence_id;

DROP PROC IF EXISTS pr_insert_money_account;
DROP PROC IF EXISTS pr_delete_money_account;
DROP PROC IF EXISTS pr_select_money_accounts;
DROP PROC IF EXISTS pr_select_user_money_accounts;
DROP PROC IF EXISTS pr_money_account_add_user;
DROP PROC IF EXISTS pr_money_account_remove_user;
DROP PROC IF EXISTS pr_exists_money_account;
DROP PROC IF EXISTS pr_recalculate_patrimony;

DROP PROC IF EXISTS pr_insert_wallet;
DROP PROC IF EXISTS pr_select_wallets;
DROP PROC IF EXISTS pr_add_balance_to_wallet;

DROP PROC IF EXISTS pr_select_categories;
DROP PROC IF EXISTS pr_insert_category;
DROP PROC IF EXISTS pr_insert_subcategory;
DROP PROC IF EXISTS pr_delete_category;
DROP PROC IF EXISTS pr_select_category_types;

DROP PROC IF EXISTS pr_select_loans;
DROP PROC IF EXISTS pr_exists_loan;
DROP PROC IF EXISTS pr_insert_loan;
DROP PROC IF EXISTS pr_loan_payment;

DROP PROC IF EXISTS pr_select_budgets;
DROP PROC IF EXISTS pr_insert_budget;
DROP PROC IF EXISTS pr_select_budgets_by_category_id;

DROP PROC IF EXISTS pr_insert_goal;
DROP PROC IF EXISTS pr_select_goals;

DROP PROC IF EXISTS pr_select_transactions;
DROP PROC IF EXISTS pr_insert_transaction;
DROP PROC IF EXISTS pr_select_transaction_types;
DROP PROC IF EXISTS pr_delete_transaction;

DROP PROC IF EXISTS pr_update_stocks_values;
DROP PROC IF EXISTS pr_select_purchased_stocks;
DROP PROC IF EXISTS pr_select_stocks;
DROP PROC IF EXISTS pr_insert_purchased_stock;
DROP PROC IF EXISTS pr_delete_purchased_stocks;

DROP FUNCTION IF EXISTS udf_annual_statistics;
DROP PROC IF EXISTS pr_annual_statistics;
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

		drop schema Project; -- this procedure is droped when droping schema
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

--------------------------------------------------------------------
-- USERS -----------------------------------------------------------
--------------------------------------------------------------------

-- inserts 1 or 2 tables. Into users, and then if @active_subscription
-- into subscription. Atomicity of the two insertions must be garanteed
CREATE PROC pr_insert_user (
	@email varchar(50) = NULL,
	@card_number varchar(20) = NULL,
	@term DATE = NULL,
	@fname VARCHAR(20) = NULL,	
	@lname VARCHAR(20) = NULL,
	@periodicity INT = NULL,
	@user_name varchar(20) = NULL,
	@mname VARCHAR(20) = NULL,
	@active_subscription BIT = 0
) AS
BEGIN
	
	BEGIN TRANSACTION
	SAVE TRANSACTION insert_user_savepoint
	BEGIN TRY
		

			INSERT INTO Project.users(email, [user_name], active_subscription)
			VALUES (@email, @user_name, @active_subscription);

			IF @active_subscription=1
			BEGIN
				INSERT INTO Project.subscriptions(card_number, email, term, fname, mname, lname, periodicity)
				VALUES (@card_number, @email, @term, @fname, @mname, @lname, @periodicity);
			END
		COMMIT
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION insert_user_savepoint
	END CATCH
END
GO

-- deletes a user subscription, does not delete the user.
CREATE PROC pr_delete_subscription (
	@email varchar(50)
) AS
BEGIN

	DELETE FROM Project.subscriptions WHERE email=@email;
END
GO

-- deletes a user and all his money_accounts. This triggers a "cascade"
-- deletion on many tables.
CREATE PROC pr_delete_user (
	@email varchar(50)
) AS
BEGIN
	
	BEGIN TRANSACTION
	SAVE TRANSACTION delete_subscription_savepoint
	BEGIN TRY
			DELETE FROM Project.users_money_accounts
			WHERE user_email=@email;

			EXEC pr_delete_subscription @email;

			DELETE FROM Project.users WHERE email=@email;
		COMMIT
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION delete_subscription_savepoint
	END CATCH
END
GO

-- updates users table and/or subscriptions table
CREATE PROC pr_update_user (
	@email varchar(50),
	@card_number varchar(20) = NULL,
	@term DATE = NULL,
	@fname VARCHAR(20) = NULL,	
	@lname VARCHAR(20) = NULL,
	@periodicity INT = NULL,
	@user_name varchar(20) = NULL,
	@mname VARCHAR(20) = NULL,
	@active_subscription BIT = NULL
) AS
BEGIN
	
	BEGIN TRANSACTION
	SAVE TRANSACTION update_user_savepoint
	BEGIN TRY
	
			-- verify if user is already subscribed
			DECLARE @subscribed BIT;
			SET @subscribed = (SELECT active_subscription FROM Project.users WHERE email=@email);

			-- update users table
			UPDATE Project.users
			SET
				[user_name]=ISNULL(@user_name, [user_name]),
				active_subscription=ISNULL(@active_subscription, active_subscription)
			WHERE email=@email;

			-- update subscription table
			IF @subscribed=1 AND (@active_subscription=1 OR @active_subscription IS NULL)
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

			IF @subscribed=1 AND @active_subscription=0
			BEGIN
				IF @subscribed=1
				BEGIN
					EXEC pr_delete_subscription @email;
				END
			END

			IF @subscribed=0 AND @active_subscription=1
		BEGIN
			INSERT INTO Project.subscriptions (card_number, email,term, fname, mname, lname, periodicity)
			VALUES (@card_number, @email, @term, @fname, @mname, @lname, @periodicity);
		END	
		COMMIT
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION update_user_savepoint
	END CATCH
END
GO

-- selects one or more users
CREATE PROC pr_select_users (
	@email varchar(50) = NULL,
	@card_number varchar(20) = NULL,
	@term DATE = NULL,
	@fname VARCHAR(20) = NULL,	
	@lname VARCHAR(20) = NULL,
	@periodicity INT = NULL,
	@user_name varchar(20) = NULL,
	@mname VARCHAR(20) = NULL,
	@active_subscription BIT = NULL
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
		AND (active_subscription=@active_subscription OR @active_subscription IS NULL);
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

-- selects one or more recurrences
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

-- selects a single recurrence using its id
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

-- if user does not have a money account with the same name already,
-- creates one. Affects 2 tables directly so atomicity must be garanteed.
-- After insertion a trigger will create pre-defined categories associated
-- with created account
CREATE PROC pr_insert_money_account (
	@user_email varchar(50),
	@account_name varchar(20),
	@balance MONEY = 0.0,
	@patrimony MONEY = 0.0
) AS
BEGIN
	
	BEGIN TRANSACTION
	SAVE TRANSACTION insert_money_account_savepoint
	BEGIN TRY

		-- verify that user does not have a money account with same name
		IF NOT EXISTS (
			SELECT *
			FROM
				Project.money_accounts AS M LEFT JOIN Project.users_money_accounts AS U
				ON M.account_id=U.account_id
			WHERE M.account_name=@account_name AND U.user_email=@user_email)
		BEGIN
				
			
			

			-- insert new money_account
			INSERT INTO Project.money_accounts(account_name, balance, patrimony)
			VALUES (@account_name, @balance, @patrimony);

			-- get new money_account id
			DECLARE @account_id INT;
			SET @account_id = SCOPE_IDENTITY();

			-- create relation between money account and user
			INSERT INTO Project.users_money_accounts([user_email], account_id)
			VALUES (@user_email, @account_id);

			COMMIT
		END

		-- A trigger (tr_create_basic_categories_on_user_insert) will insert standard categories in the database.
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION insert_money_account_savepoint
	END CATCH
END
GO

-- deletes a user money account. This has repercussions in many tables
-- and deletes all activity associated with the deleted account.
-- To garantee integrity all operations must be atomic
CREATE PROC pr_delete_money_account (
	@account_id INT
) AS
BEGIN
	
	BEGIN TRANSACTION
	SAVE TRANSACTION delete_money_account_savepoint
	BEGIN TRY
		
			DELETE FROM Project.budgets
			WHERE account_id=@account_id;
	
			DELETE FROM Project.goals
			WHERE account_id=@account_id;

			DELETE FROM Project.loans
			WHERE account_id=@account_id;

			DELETE FROM Project.purchased_stocks
			WHERE account_id=@account_id;

			DELETE FROM Project.transactions
			WHERE account_id=@account_id;
	
			DELETE FROM Project.wallets
			WHERE account_id=@account_id;

			DELETE FROM Project.users_money_accounts
			WHERE account_id=@account_id;

			DELETE FROM Project.categories
			WHERE account_id=@account_id;

			DELETE FROM Project.money_accounts
			WHERE account_id=@account_id;
		COMMIT
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION delete_money_account_savepoint
	END CATCH
END
GO

-- selects one or more money accounts
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

-- selects all money account belonging to one user
CREATE PROC pr_select_user_money_accounts (
	@account_id INT = NULL,
	@user_email VARCHAR(50) = NULL
) AS
BEGIN

	SELECT UMA.user_email AS user_email, MA.account_id AS account_id, MA.account_name AS account_name, MA.balance AS balance, MA.patrimony AS patrimony
	FROM
		Project.users_money_accounts AS UMA LEFT JOIN Project.money_accounts AS MA
		ON UMA.account_id=MA.account_id
	WHERE
			(UMA.user_email=@user_email OR @user_email IS NULL)
		AND (MA.account_id=@account_id OR @account_id IS NULL);
END
GO

-- grants an user access to an existing (other users's) money account
CREATE PROC pr_money_account_add_user (
	@account_id INT,
	@user_email VARCHAR(50)
) AS
BEGIN

	INSERT INTO Project.users_money_accounts(account_id, user_email)
	VALUES (@account_id, @user_email)
END
GO

-- ungrants an user access to one of his money account
-- there is no users privileges distintion between account users
CREATE PROC pr_money_account_remove_user (
	@account_id INT,
	@user_email VARCHAR(50)
) AS
BEGIN

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

-- one account's patrimony is affected by multiple tables and actions
-- this procedure should be called whenever a change occurs in one
-- of thoses tables, so the total value may be updated in the money_account table.
-- To garantee integrity, all operations must be atomic
CREATE PROC pr_recalculate_patrimony (
	@account_id MONEY
) AS
BEGIN
	
	BEGIN TRANSACTION
	SAVE TRANSACTION recalculate_patrimony_savepoint
	BEGIN TRY
		
			DECLARE @patrimony MONEY;
			SET @patrimony = COALESCE((SELECT SUM(purchase_price) FROM Project.purchased_stocks WHERE account_id=@account_id), 0.0);
			SET @patrimony = @patrimony - COALESCE((SELECT SUM(current_debt) FROM Project.loans WHERE account_id=@account_id), 0.0);
			SET @patrimony = @patrimony + COALESCE((SELECT SUM(balance) FROM Project.wallets WHERE account_id=@account_id), 0.0);

			UPDATE Project.money_accounts SET patrimony=@patrimony WHERE account_id=@account_id;

		COMMIT
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION recalculate_patrimony_savepoint
	END CATCH
END
GO

---------------------------------------------------------------
--- WALLETS ---------------------------------------------------
---------------------------------------------------------------

-- inserts a new wallet into a money account
CREATE PROC pr_insert_wallet (
	@account_id INT,
	@name varchar(20),
	@balance MONEY = 0.0
) AS
BEGIN
	
	-- verify that wallet with same name does not exist in account
	IF NOT EXISTS (SELECT * FROM Project.wallets WHERE account_id=@account_id AND [name]=@name)
	BEGIN
		INSERT INTO Project.wallets(account_id, [name], balance)
		VALUES (@account_id, @name, @balance);
	END
END
GO

-- selects one or more wallets
CREATE PROC pr_select_wallets (
	@account_id INT = NULL,
	@name VARCHAR(20) = NULL,
	@wallet_id INT = NULL
) AS
BEGIN
	
	SELECT *
	FROM Project.wallets
	WHERE
			(account_id=@account_id OR @account_id IS NULL)
		AND ([name]=@name OR @name IS NULL)
		AND (wallet_id=@wallet_id OR @wallet_id IS NULL)
END
GO

-- add money to wallet balance
CREATE PROC pr_add_balance_to_wallet(
	@amount MONEY,
	@wallet_id INT
) AS
BEGIN

	IF @amount IS NOT NULL
	BEGIN
		UPDATE Project.wallets
		SET balance = balance + @amount
		WHERE wallet_id = @wallet_id
	END
END
GO

---------------------------------------------------------------
--- CATEGORIES ------------------------------------------------
---------------------------------------------------------------
-- Categories are specific to each users's money account. Although
-- some are pre-inserted in account creation.
-- Categories identification is not sequential. In order to distinguish
-- categories from subcategories a code was applied. All categories are
-- coded with an even hundreds number (0, 100, 200, etc), all number after
-- a category id are its subcategories (eg: 101, 102, 103 are subcategories
-- of 100)

-- selects one or more categories
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

-- calculates a new id and inserts a new category in a money account.
-- id calculation and insertion must be an atomic operation
CREATE PROC pr_insert_category (
	@category_type_id INT,
	@account_id INT,
	@name VARCHAR(20)
) AS
BEGIN
	
	BEGIN TRANSACTION
	SAVE TRANSACTION insert_category_savepoint
	
	BEGIN TRY
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
		COMMIT
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION insert_category_savepoint
	END CATCH
END
GO

--  calculates a new id and inserts a new subcategory in a money account.
-- id calculation and insertion must be an atomic operation
CREATE PROC pr_insert_subcategory (
	@category_id INT,
	@category_type_id INT,
	@account_id INT,
	@name VARCHAR(20)
) AS
BEGIN
	
	BEGIN TRANSACTION
	SAVE TRANSACTION insert_subcategory_savepoint
	BEGIN TRY
		
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
		COMMIT
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION insert_subcategory_savepoint
	END CATCH
END
GO

-- deletes one or more categories
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

-- selects a single category type (expense, transfer, income, ...)
CREATE PROC pr_select_category_types (
	@designation VARCHAR(20) = NULL,
	@category_type_id INT = NULL
) AS
BEGIN
	
	IF ((@designation IS NULL) AND (@category_type_id IS NULL))
	BEGIN
		
		SELECT *
		FROM Project.category_types
		WHERE
				(designation=@designation OR @designation IS NULL)
			AND (category_type_id=@category_type_id or @category_type_id IS NULL)
	END

	ELSE
	BEGIN

		IF (@designation IS NULL)
		BEGIN
			SELECT designation
			FROM Project.category_types
			WHERE category_type_id=@category_type_id
		END

		IF (@category_type_id IS NULL)
		BEGIN
			SELECT category_type_id
			FROM Project.category_types
			WHERE designation=@designation
		END
	END
END
GO


---------------------------------------------------------------
--- LOANS -----------------------------------------------------
---------------------------------------------------------------

-- selects one or more loans
CREATE PROC pr_select_loans (
	@name VARCHAR(20) = NULL,
	@initial_amount MONEY = NULL,
	@current_debt MONEY = NULL,
	@term DATE = NULL,
	@interest DECIMAL(5,2) = NULL,
	@account_id INT = NULL
) AS
BEGIN
	
	SELECT *
	FROM Project.loans
	WHERE
			([name]=@name OR @name IS NULL)
		AND (initial_amount=@initial_amount OR @initial_amount IS NULL)
		AND (current_debt=@current_debt OR @current_debt IS NULL)
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

-- inserts one loan into one account
CREATE PROC pr_insert_loan (
	@account_id INT,
	@name VARCHAR(20),
	@initial_amount MONEY,
	@current_debt MONEY = @initial_amount,
	@term DATE = NULL,
	@interest DECIMAL(5,2) = NULL
) AS
BEGIN
	
	-- verify if account exists
	IF EXISTS (SELECT * FROM Project.money_accounts WHERE account_id=@account_id)
	BEGIN
		INSERT INTO Project.loans (account_id, [name], initial_amount, current_debt, term, interest)
		VALUES (@account_id, @name, @initial_amount, @current_debt, @term, @interest)
	END
END
GO

-- updates a loan current debt. Its important to garantee that
-- the read and write operations necessary to update are atomic
CREATE PROC pr_loan_payment (
	@account_id INT,
	@name VARCHAR(20) = NULL,
	@payment MONEY = NULL
) AS
BEGIN
	
	BEGIN TRANSACTION
	SAVE TRANSACTION loan_payment_savepoint
	BEGIN TRY
		
			DECLARE @current_debt MONEY;
			SELECT @current_debt=current_debt FROM Project.loans WHERE account_id=@account_id AND [name]=@name;
			
			SET @current_debt -= @payment;

			IF @current_debt < 0
				SET @current_debt = 0;
				
			UPDATE Project.loans
			SET current_debt=@current_debt
			WHERE account_id=@account_id AND [name]=@name
			;
		COMMIT
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION loan_payment_savepoint
	END CATCH
END
GO

---------------------------------------------------------------
--- BUDGETS -----------------------------------------------------
---------------------------------------------------------------

-- selects one or more budgets
CREATE PROC pr_select_budgets (
	@account_id INT = NULL,
	@category_id INT = NULL,
	@budget_id INT = NULL,
	@amount MONEY = NULL,
	@periodicity INT = NULL,
	@start_date DATE = NULL,
	@end_date DATE = NULL
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

-- selects one or more budgets. If the given category_id is from
-- a subcategory, only that row will be returnd. If th category_id
-- represents a category, all its subcategories and itself will be returned
CREATE PROC pr_select_budgets_by_category_id (
	@account_id INT,
	@category_id INT
) AS
BEGIN
	
	DECLARE @base_id INT = @category_id/100;

	IF (@category_id%100 = 0)
	BEGIN
		SELECT *
		FROM Project.budgets
		WHERE account_id=@account_id AND category_id/100=@base_id;
	END
	
	ELSE
	BEGIN
		SELECT *
		FROM Project.budgets
		WHERE account_id=@account_id AND category_id=@category_id;
	END		
END
GO

-- insert a new budget into one account
CREATE PROC pr_insert_budget (
	@account_id INT,
	@category_id INT,
	@amount MONEY = 0.0,
	@periodicity INT = 1,
	@start_date DATE NULL,
	@end_date DATE = NULL
) AS
BEGIN
	
	IF @start_date IS NULL
		SET @start_date = GETDATE();

	INSERT INTO Project.budgets (account_id, category_id, amount, periodicity, [start_date], end_date)
	VALUES (@account_id, @category_id, @amount, @periodicity, @start_date, @end_date)
END
GO


---------------------------------------------------------------
--- GOALS -----------------------------------------------------
---------------------------------------------------------------

-- inserts a new goal into one account After insertion a trigger
-- tr_new_wallet_on_goal_insert will insert a new wallet with the
-- same name as the goal and 0.0$ balance
CREATE PROC pr_insert_goal (
	@name VARCHAR(20),
	@category_id INT,
	@account_id INT,
	@amount MONEY = 0.0,
	@term DATE = NULL,
	@accomplished MONEY = 0.0
) AS
BEGIN
	
	BEGIN TRANSACTION
	SAVE TRANSACTION insert_goal_savepoint -- stop rollback in case o trigger fail

	BEGIN TRY
		INSERT INTO Project.goals ([name], category_id, account_id, amount, term, accomplished)
		VALUES (@name, @category_id, @account_id, @amount, @term, @accomplished)
		;
		COMMIT
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION  insert_goal_savepoint
	END CATCH
	
END
GO

-- selects one or more goals
CREATE PROC pr_select_goals (
	@name VARCHAR(20) = NULL,
	@category_id INT = NULL,
	@account_id INT = NULL,
	@amount INT = NULL,
	@term DATE = NULL,
	@accomplished BIT = NULL
) AS
BEGIN
	
	SELECT *
	FROM Project.goals
	WHERE
			([name]=@name OR @name IS NULL)
		AND (category_id=@category_id OR @category_id IS NULL)
		AND (account_id=@account_id OR @account_id IS NULL)
		AND (amount=@amount OR @amount IS NULL)
		AND (term=@term OR @term IS NULL)
		AND (accomplished=@accomplished OR @accomplished IS NULL)
END
GO

---------------------------------------------------------------
--- TRANSACTION -----------------------------------------------
---------------------------------------------------------------

-- selects one or more transactions. As transactions belong to a
-- category if the given category_id is from a main category, all
-- subcategory transactions will be return too.
CREATE PROC pr_select_transactions (
	@account_id INT = NULL,
	@category_id INT = NULL,
	@wallet_id INT = NULL,
	@transaction_id INT = NULL,
	@transaction_type_id INT = NULL,
	@min_amount MONEY = NULL,
	@max_amount MONEY = NULL,
	@start_date DATE = NULL,
	@end_date DATE = NULL,
	@location VARCHAR(50) = NULL
) AS
BEGIN
	
	IF @category_id%100 != 0
	BEGIN
	
		SELECT *
		FROM
			Project.transactions AS TR LEFT JOIN Project.transfers AS TF
			ON TR.transaction_id=TF.transaction_id
		WHERE
				(account_id=@account_id OR @account_id IS NULL)
			AND (category_id=@category_id OR @category_id IS NULL)
			AND (wallet_id=@wallet_id OR @wallet_id IS NULL)
			AND (TR.transaction_id=@transaction_id OR @transaction_id IS NULL)
			AND (transaction_type_id=@transaction_type_id OR @transaction_type_id IS NULL)
			AND (amount>=@min_amount OR @min_amount IS NULL)
			AND (amount<=@max_amount OR @max_amount IS NULL)
			AND ([date]>=@start_date OR @start_date IS NULL)
			AND ([date]<=@end_date OR @end_date IS NULL)
			AND ([location]=@location OR @location IS NULL)
		ORDER BY [date]
		;
	END

	ELSE
	BEGIN
	
		DECLARE @cat_id INT;
		SET @cat_id = @category_id/100;
				
		SELECT *
		FROM
			Project.transactions AS TR LEFT JOIN Project.transfers AS TF
			ON TR.transaction_id=TF.transaction_id
		WHERE
				(account_id=@account_id OR @account_id IS NULL)
			AND (category_id/100=@cat_id OR @category_id IS NULL)
			AND (wallet_id=@wallet_id OR @wallet_id IS NULL)
			AND (TR.transaction_id=@transaction_id OR @transaction_id IS NULL)
			AND (transaction_type_id=@transaction_type_id OR @transaction_type_id IS NULL)
			AND (amount>=@min_amount OR @min_amount IS NULL)
			AND (amount<=@max_amount OR @max_amount IS NULL)
			AND ([date]>=@start_date OR @start_date IS NULL)
			AND ([date]<=@end_date OR @end_date IS NULL)
			AND ([location]=@location OR @location IS NULL)
		ORDER BY [date]
		;
	END
END
GO

-- inserts one transaction. If the transaction type is transfer,
-- then it inserts into tranfers table too. Those operations
-- atomicity mus be garanteed.
-- After both insertions a "cascade" of triggers will be activated
-- ensuring that the wallet balance as well as the money account
-- balance and patrimony are updated.
CREATE PROC pr_insert_transaction (
	@account_id INT,
	@category_id INT,
	@from_wallet_id INT,
	@to_wallet_id INT = NULL,
	@transaction_type_id INT,
	@amount MONEY = 0.0,
	@date DATE = NULL,
	@notes VARCHAR(50) = NULL,
	@location VARCHAR(50) = NULL
) AS
BEGIN
	
	BEGIN TRANSACTION
	SAVE TRANSACTION insert_transaction_savepoint
	BEGIN TRY
		
			IF @date IS NULL
				SET @date = GETDATE();

			INSERT INTO Project.transactions (account_id, category_id, wallet_id, transaction_type_id, amount, [date], notes, [location])
			VALUES (@account_id, @category_id, @from_wallet_id, @transaction_type_id, @amount, @date, @notes, @location)

			DECLARE @transaction_id INT;
			SET @transaction_id = SCOPE_IDENTITY();

			IF @transaction_type_id = 0
			BEGIN
				INSERT INTO Project.transfers (transaction_id, recipient_wallet_id)
				VALUES (@transaction_id, @to_wallet_id);
			END
		COMMIT
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION insert_transaction_savepoint
	END CATCH
END
GO

-- selects one or more transaction types
CREATE PROC pr_select_transaction_types (
	@transaction_type_id INT = NULL,
	@designation VARCHAR(20) = NULL
) AS
BEGIN

	IF ((@designation IS NULL) AND (@transaction_type_id IS NULL))
	BEGIN
		
		SELECT *
		FROM Project.transaction_types
		WHERE
				(designation=@designation OR @designation IS NULL)
			AND (transaction_type_id=@transaction_type_id or @transaction_type_id IS NULL)
	END

	ELSE
	BEGIN

		IF (@designation IS NULL)
		BEGIN
			SELECT designation
			FROM Project.transaction_types
			WHERE transaction_type_id=@transaction_type_id
		END

		IF (@transaction_type_id IS NULL)
		BEGIN
			SELECT transaction_type_id
			FROM Project.transaction_types
			WHERE designation=@designation
		END
	END
END
GO

-- deletes one transaction
CREATE PROC pr_delete_transaction (
	@transaction_id INT
) AS
BEGIN
	
	DELETE FROM Project.transactions
	WHERE transaction_id=@transaction_id;
END
GO

---------------------------------------------------------------
--- STOCK -----------------------------------------------------
---------------------------------------------------------------

-- This procedure is here just for demonstration purposes, and
-- should not be considered. Its just used to generate random values
-- to stock prices. In a real situation, this information would be retrieved
-- from an API and not stored in this way in the database.
CREATE PROC pr_update_stocks_values
AS
BEGIN

	UPDATE Project.stocks
	SET ask_price=CAST(ABS(CHECKSUM(NEWID()) % 10000) AS MONEY) / 100
END
GO

-- selects one or more purchase stocks
CREATE PROC pr_select_purchased_stocks (
	@account_id INT = NULL,
	@ticker INT = NULL,
	@company VARCHAR(50) = NULL,
	@purchase_price MONEY = NULL
) AS
BEGIN
	
	SELECT PS.account_id AS account_id, PS.ticker AS ticker, PS.company AS company, PS.purchase_price AS purchase_price,
			S.bid_price AS bid_price, S.stock_type_id AS stock_type_id
	FROM
		Project.purchased_stocks AS PS JOIN Project.stocks AS S
		ON PS.company = S.company
	WHERE
			(account_id=@account_id OR @account_id IS NULL)
		AND (ticker=@ticker OR @ticker IS NULL)
		AND (PS.company=@company OR @company IS NULL)
		AND (purchase_price=@purchase_price OR @purchase_price IS NULL)
END
GO

-- selects one or more stocks
CREATE PROC pr_select_stocks (
	@company VARCHAR(50) = NULL,
	@min_ask_price MONEY = NULL,
	@max_ask_price MONEY = NULL,
	@min_bid_price MONEY = NULL,
	@max_bid_price MONEY = NULL,
	@stock_type_id INT = NULL
) AS
BEGIN
	
	-- update stocks values
	EXEC pr_update_stocks_values;	-- no problem if it fail, its just a stock value generator
									-- for demonstration purposes
	
	-- select stocks
	SELECT S.company, S.ask_price, S.stock_type_id, ST.designation AS stock_type
	FROM
		Project.stocks AS S LEFT JOIN Project.stock_types AS ST
		ON S.stock_type_id=ST.Stock_type_id
	WHERE
			(ask_price>=@min_ask_price OR @min_ask_price IS NULL)
		AND (ask_price<=@max_ask_price OR @max_ask_price IS NULL)
		AND (bid_price>=@min_bid_price OR @min_bid_price IS NULL)
		AND (bid_price<=@max_bid_price OR @max_bid_price IS NULL)
		AND (company=@company OR @company IS NULL)
		AND (S.stock_type_id=@stock_type_id OR @stock_type_id IS NULL)
END
GO

-- inserts a new purchased stock
CREATE PROC pr_insert_purchased_stock (
	@account_id INT,
	@company VARCHAR(50),
	@purchase_price MONEY
) AS
BEGIN

	INSERT INTO Project.purchased_stocks (account_id, company, purchase_price)
	VALUES (@account_id, @company, @purchase_price)

	DECLARE @wall_id INT;
	SELECT @wall_id=min(wallet_id) FROM Project.wallets WHERE account_id=@account_id;

	SET @purchase_price = -@purchase_price;
	EXEC pr_add_balance_to_wallet @amount=@purchase_price, @wallet_id=@wall_id;
END
GO

-- select * from Project.loans;
--select * from Project.purchased_stocks;
--select * from Project.wallets;
--select * from Project.money_accounts;
--exec pr_delete_purchased_stocks @account_id=1, @ticker=1, @ask_price=30;


-- deletes one purchased stock
-- in order to not lose the profit value, the wallet balnce must be updated
-- from within the procedure. Then a trigger will activate and 
-- update the patrimony of the oney acciunt associated with the stock
CREATE PROC pr_delete_purchased_stocks (
	@account_id INT,
	@ticker INT = NULL,
	@company VARCHAR(50) = NULL,
	@purchase_price MONEY = NULL,
	@ask_price MONEY = NULL
) AS
BEGIN
	
	BEGIN TRANSACTION
	SAVE TRANSACTION del_purchased_stocks_savepoint
	BEGIN TRY	

			DELETE FROM Project.purchased_stocks
			WHERE
					(ticker=@ticker OR company=@company)
				AND (purchase_price=@purchase_price OR @purchase_price IS NULL)
			;

			DECLARE @wall_id INT;
			SELECT @wall_id=min(wallet_id) FROM Project.wallets WHERE account_id=@account_id;
			EXEC pr_add_balance_to_wallet @amount=@ask_price, @wallet_id=@wall_id;
		COMMIT
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION del_purchased_stocks_savepoint
	END CATCH
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
CREATE NONCLUSTERED INDEX IX_MONEY_ACCOUNTS_ACCOUNT_NAME ON Project.money_accounts
(account_name) WITH (FILLFACTOR = 75, PAD_INDEX = ON);

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
	active_subscription BIT NOT NULL DEFAULT(0),
	CHECK([user_name] != ''),
	CONSTRAINT PK_USERS PRIMARY KEY (email),
);
CREATE NONCLUSTERED INDEX IX_USERS_USER_NAME ON Project.users
([user_name]) WITH (FILLFACTOR = 75, PAD_INDEX = ON);

CREATE TABLE Project.subscriptions
(
	card_number VARCHAR(20),
	email VARCHAR(50),
	term DATE NOT NULL, --DEFAULT DATEADD(year, 1, GETDATE()),
	fname VARCHAR(20) NOT NULL,
	mname VARCHAR(20),
	lname VARCHAR(20) NOT NULL,
	periodicity INT NOT NULL,
	CHECK(card_number>0),
	CONSTRAINT PK_SUBSCRIPTIONS PRIMARY KEY (card_number),
	CONSTRAINT FK_SUBSCRIPTIONS_USERS FOREIGN KEY (email) REFERENCES Project.users(email)
		ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT FK_SUBSCRIPTIONS_RECURRENCE FOREIGN KEY (periodicity) REFERENCES Project.recurrence(periodicity)
		ON DELETE NO ACTION ON UPDATE CASCADE	
);
CREATE NONCLUSTERED INDEX IX_SUBSCRIPTIONS_EMAIL ON Project.subscriptions
(email) WITH (FILLFACTOR = 75, PAD_INDEX = ON);

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
CREATE NONCLUSTERED INDEX IX_USERS_MONEY_ACCOUNTS_USER_EMAIL ON Project.users_money_accounts
(user_email) WITH (FILLFACTOR = 75, PAD_INDEX = ON);

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
	amount MONEY NOT NULL,
	[date] DATE NOT NULL,
	notes VARCHAR(50),
	[location] VARCHAR(50),
	category_id INT,
	account_id INT,
	transaction_type_id INT,
	wallet_id INT,
	CONSTRAINT PK_TRANSACTIONS PRIMARY KEY (transaction_id),
	CONSTRAINT FK_TRANSACTIONS_CATEGORIES FOREIGN KEY (category_id, account_id) REFERENCES Project.categories(category_id, account_id)
		ON DELETE NO ACTION ON UPDATE CASCADE,
	CONSTRAINT FK_TRANSACTIONS_TRANSACTIONTYPES FOREIGN KEY (transaction_type_id) REFERENCES Project.transaction_types(transaction_type_id)
		ON DELETE NO ACTION ON UPDATE CASCADE,
	CONSTRAINT FK_TRANSACTIONS_WALLETS FOREIGN KEY (wallet_id) REFERENCES Project.wallets(wallet_id)
		ON DELETE NO ACTION ON UPDATE NO ACTION
);
CREATE NONCLUSTERED INDEX IX_TRANSACTIONS_ACCOUNT_ID ON Project.transactions
(account_id) WITH (FILLFACTOR = 75, PAD_INDEX = ON);
CREATE NONCLUSTERED INDEX IX_TRANSACTIONS_CATEGORY_ID ON Project.transactions
(category_id) WITH (FILLFACTOR = 75, PAD_INDEX = ON);
CREATE NONCLUSTERED INDEX IX_TRANSACTIONS_WALLET_ID ON Project.transactions
(wallet_id) WITH (FILLFACTOR = 75, PAD_INDEX = ON);

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
	[start_date] DATE,
	end_date DATE,
	CONSTRAINT PK_BUDGETS PRIMARY KEY (budget_id),
	CONSTRAINT FK_BUDGETS_RECURRENCE FOREIGN KEY (periodicity) REFERENCES Project.recurrence(periodicity)
		ON DELETE NO ACTION ON UPDATE CASCADE,
	CONSTRAINT FK_BUDGETS_CATEGORIES FOREIGN KEY (category_id, account_id) REFERENCES Project.categories(category_id, account_id)
		ON DELETE NO ACTION ON UPDATE CASCADE
);
CREATE NONCLUSTERED INDEX IX_BUDGETS_ACCOUNT_ID ON Project.budgets
(account_id) WITH (FILLFACTOR = 75, PAD_INDEX = ON);
CREATE NONCLUSTERED INDEX IX_BUDGETS_CATEGORY_ID ON Project.budgets
(category_id) WITH (FILLFACTOR = 75, PAD_INDEX = ON);

CREATE TABLE Project.goals
(
	[name] VARCHAR(20),
	category_id INT,
	account_id INT,
	amount MONEY DEFAULT 0,
	term DATE,
	accomplished MONEY DEFAULT 0,
	CHECK([name] != ''),
	CONSTRAINT PK_GOALS PRIMARY KEY ([name], category_id, account_id),
	CONSTRAINT FK_GOALS_CATEGORIES FOREIGN KEY (category_id, account_id) REFERENCES Project.categories(category_id, account_id)
		ON DELETE NO ACTION ON UPDATE CASCADE
);
CREATE NONCLUSTERED INDEX IX_GOALS_CATEGORY_ID ON Project.goals
(category_id) WITH (FILLFACTOR = 75, PAD_INDEX = ON);
CREATE NONCLUSTERED INDEX IX_GOALS_ACCOUNT_ID ON Project.goals
(account_id) WITH (FILLFACTOR = 75, PAD_INDEX = ON);

CREATE TABLE Project.loans
(
	[name] VARCHAR(20),
	account_id INT,
	initial_amount MONEY,
	current_debt MONEY,
	term DATE,
	interest DECIMAL(5,2) DEFAULT(0.0),
	CHECK([name] != ''),
	CHECK(initial_amount>=0),
	CHECK(interest BETWEEN 0 AND 100),
	CONSTRAINT PK_LOANS PRIMARY KEY (account_id, [name]),
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
	bid_price MONEY,
	ask_price MONEY,
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
	purchase_price MONEY,
	CONSTRAINT PK_PURCHASEDSTOCKS PRIMARY KEY (ticker),
	CONSTRAINT FK_PURCHASEDSTOCKS_STOCKS FOREIGN KEY (company) REFERENCES Project.stocks(company)
		ON DELETE NO ACTION ON UPDATE CASCADE,
	CONSTRAINT FK_PURCHASEDSTOCKS_MONEYACCOUNTS FOREIGN KEY (account_id) REFERENCES Project.money_accounts(account_id)
		ON DELETE NO ACTION ON UPDATE CASCADE
);
CREATE NONCLUSTERED INDEX IX_PURCHASED_STOCKS_COMPANY ON Project.purchased_stocks
(company) WITH (FILLFACTOR = 75, PAD_INDEX = ON);

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

INSERT INTO Project.stocks (company, ask_price, stock_type_id)
	VALUES
		( 'Microsoft Corporation', CAST(RAND() * 100 AS MONEY), 0 ),
		( 'Apple Inc.', CAST(RAND() * 100 AS MONEY), 0 ),
		( 'Johnson & Johnson', CAST(RAND() * 100 AS MONEY), 1 ),
		( 'JPMorgan Chase & Co.', CAST(RAND() * 100 AS MONEY), 0 ),
		( 'ExxonMobil Corporation', CAST(RAND() * 100 AS MONEY), 1 ),
		( 'Bank of America Corp.', CAST(RAND() * 100 AS MONEY), 0 ),
		( 'Facebook Inc.', CAST(RAND() * 100 AS MONEY), 1 ),
		( 'Wal-Mart Stores Inc.', CAST(RAND() * 100 AS MONEY), 0 ),
		( 'Amazon.com, Inc.', CAST(RAND() * 100 AS MONEY), 1 ),
		( 'Alphabet Inc.', CAST(RAND() * 100 AS MONEY), 0 ),
		( 'Berkshire Hathaway Inc', CAST(RAND() * 100 AS MONEY), 0 ),
		( 'Alibaba Group Holding Ltd', CAST(RAND() * 100 AS MONEY), 0 ),
		( 'Wells Fargo & Co.', CAST(RAND() * 100 AS MONEY), 0 ),
		( 'Royal Dutch Shell plc', CAST(RAND() * 100 AS MONEY), 1 ),
		( 'Visa Inc.', CAST(RAND() * 100 AS MONEY), 0 ),
		( 'Procter & Gamble Co.', CAST(RAND() * 100 AS MONEY), 0 ),
		( 'Anheuser-Busch Inbev NV', CAST(RAND() * 100 AS MONEY), 1 ),
		( 'AT&T Inc.', CAST(RAND() * 100 AS MONEY), 0 ),
		( 'Chevron Corporation', CAST(RAND() * 100 AS MONEY), 0 ),
		( 'UnitedHealth Group Inc.', CAST(RAND() * 100 AS MONEY), 0 ),
		( 'Pfizer Inc.', CAST(RAND() * 100 AS MONEY), 0 ),
		( 'Roche Holding Ltd.', CAST(RAND() * 100 AS MONEY), 1 )

GO

--------------------------------------------------------------------
-- TRIGGERS --------------------------------------------------------
--------------------------------------------------------------------

-- when a new user money account is created, it has no categories.
-- This trigger, activated after a money account insert, inserts
-- a set of pre-defined categories common to all users
CREATE TRIGGER tr_insert_base_categories_on_new_money_account_insert
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
	INSERT INTO Project.categories(category_id, account_id, category_type_id, [name])
	VALUES (400, @account_id, -1, 'Loans');
	INSERT INTO Project.categories(category_id, account_id, category_type_id, [name])
	VALUES (500, @account_id, 1, 'Goals');
END
GO

-- when a new user money account is created, it has no wallets.
-- This trigger, activated after a money account insert, inserts
-- a set of pre-defined wallets common to all users
CREATE TRIGGER tr_insert_base_wallets_on_new_money_account_insert
ON Project.money_accounts
AFTER INSERT
AS
BEGIN
	
	SET NOCOUNT ON

	DECLARE @in_account_id INT;
	SET @in_account_id = (SELECT account_id FROM INSERTED);

	exec pr_insert_wallet @account_id=@in_account_id, @name='Current';
	exec pr_insert_wallet @account_id=@in_account_id, @name='Irregular Expenses';
	exec pr_insert_wallet @account_id=@in_account_id, @name='Savings';

END
GO

-- when a goal is inserted, there is necessaty of creating a separate
-- place where to set money aside for said goal. So this trigger
-- creates a new wallet with the same name as the goal
CREATE TRIGGER tr_insert_new_wallet_on_goal_insert
ON Project.goals
AFTER INSERT
AS
BEGIN

	SET NOCOUNT ON

	DECLARE @wallet_account_id INT;
	SET @wallet_account_id = (SELECT account_id FROM INSERTED);
	DECLARE @wallet_name VARCHAR(20);
	SET @wallet_name = (SELECT [name] FROM INSERTED);

	EXEC pr_insert_wallet @account_id=@wallet_account_id, @name=@wallet_name;
END
GO

-- when a transfer is made to a waalet named equal to a goal,
-- means that money was set aside towards that goal. So goal
-- completion is updated
CREATE TRIGGER tr_update_goal_completion_with_wallet_transfer
ON Project.transactions
AFTER INSERT
AS
BEGIN
	
	SET NOCOUNT ON

	DECLARE @transaction_type_id INT;
	DECLARE @amount MONEY;
	DECLARE @account_id INT;
	DECLARE @wallet_id INT;
	DECLARE @wallet_name VARCHAR(20);

	SELECT
		@transaction_type_id= transaction_type_id,
		@amount=amount,
		@account_id=account_id,
		@wallet_id=wallet_id
	FROM INSERTED

	IF @transaction_type_id=0
	BEGIN
		SELECT @wallet_name=[name] FROM Project.wallets WHERE wallet_id=@wallet_id;

		UPDATE Project.goals SET accomplished=accomplished+@amount WHERE [name]=@wallet_name;
	END
END
GO

-- account patrimony is affected by many tables, so in order to easely
-- access its total value, the same is updated whenever there is an
-- update in one of those tables. 
CREATE TRIGGER tr_update_account_patrimony_on_wallet_balance_update
ON Project.wallets
AFTER UPDATE
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @acct_id INT;
	SELECT @acct_id=account_id FROM INSERTED;
	EXEC pr_recalculate_patrimony @account_id=@acct_id;
END
GO

-- account patrimony is affected by many tables, so in order to easely
-- access its total value, the same is updated whenever there is an
-- update in one of those tables.
CREATE TRIGGER tr_update_wallet_balance_on_loans_insert
ON Project.loans
AFTER INSERT
AS
BEGIN
	
	SET NOCOUNT ON

	DECLARE @acct_id INT;
	DECLARE @loan MONEY;
	SELECT @acct_id=account_id, @loan=current_debt FROM INSERTED;

	DECLARE @wall_id INT;
	SELECT @wall_id=min(wallet_id) FROM Project.wallets WHERE account_id=@acct_id;

	EXEC pr_add_balance_to_wallet @amount=@loan, @wallet_id=@wall_id;
END
GO

-- account_balance is affected by many wallets, so in order to easely
-- access its balance, the same is updated whenever there is an
-- update in one of those walets. 
CREATE TRIGGER tr_update_account_balance_on_wallet_balance_update
ON Project.wallets
AFTER UPDATE
AS
BEGIN

	SET NOCOUNT ON

	DECLARE @account_id INT;
	SELECT @account_id=account_id FROM INSERTED;

	DECLARE @balance MONEY;
	SELECT @balance=SUM(balance) FROM Project.wallets WHERE account_id=@account_id;
	SET @balance = COALESCE(@balance, 0.0);

	UPDATE Project.money_accounts SET balance=@balance WHERE account_id=@account_id;

END
GO

-- after a new transaction is inserted, the wallet balance is updated, in order
-- to avoid future calculation of all transactions just to get the balance
-- this trigger ignores transfers which are deal in a separate trigger
CREATE TRIGGER tr_update_wallet_balance_on_transaction_insert
ON Project.transactions
AFTER INSERT
AS
BEGIN
	
	SET NOCOUNT ON

	DECLARE @value MONEY;
	DECLARE @wallet_id INT;
	DECLARE @transaction_type_id INT;
	SELECT @value=amount, @wallet_id=wallet_id, @transaction_type_id=transaction_type_id FROM INSERTED;

	IF @transaction_type_id = -1
	BEGIN
		UPDATE Project.wallets
		SET balance = balance - COALESCE(@value, 0)
		WHERE wallet_id=@wallet_id;
	END

	ELSE IF @transaction_type_id = 1
	BEGIN
		UPDATE Project.wallets
		SET balance = balance + COALESCE(@value, 0)
		WHERE wallet_id=@wallet_id;
	END
END
GO

-- after a new transaction is deleted, the wallet balance is updated, in order
-- to avoid future calculation of all transactions just to get the balance
CREATE TRIGGER tr_update_wallet_balance_on_transaction_delete
ON Project.transactions
AFTER DELETE
AS
BEGIN

	SET NOCOUNT ON

	DECLARE @value MONEY;
	DECLARE @wallet_id INT;
	SELECT @value=amount, @wallet_id=wallet_id FROM DELETED;

	UPDATE Project.wallets
	SET balance=balance + COALESCE(@value, 0)
	WHERE wallet_id=@wallet_id
	;
END
GO

-- after a new transfer, the wallet balance is updated, in order
-- to avoid future calculation of all transactions just to get the balance
CREATE TRIGGER tr_update_wallet_balance_on_transfer_insert
ON Project.transfers
AFTER INSERT
AS
BEGIN
	
	DECLARE @transaction_id INT;
	DECLARE @recipient_wallet_id INT;
	SELECT
		@transaction_id=transaction_id,
		@recipient_wallet_id=recipient_wallet_id
	FROM INSERTED;
	
	DECLARE @value MONEY;
	SELECT @value=amount
	FROM Project.transactions
	WHERE transaction_id=@transaction_id;
	
	UPDATE Project.wallets
	SET balance = balance + COALESCE(@value, 0)
	WHERE wallet_id=@recipient_wallet_id;

	DECLARE @from_wallet_id INT;
	SELECT
		@from_wallet_id=wallet_id,
		@value=amount
	FROM Project.transactions
	WHERE transaction_id=@transaction_id;

	UPDATE Project.wallets
	SET balance = balance - COALESCE(@value, 0)
	WHERE wallet_id=@from_wallet_id;
END
GO

CREATE TRIGGER tr_insert_new_transaction_on_loan_payment_update
ON Project.loans
AFTER UPDATE
AS
BEGIN
	
	DECLARE @old_debt MONEY;
	DECLARE @new_debt MONEY;
	DECLARE @payment MONEY;
	DECLARE @acct_id INT;
	DECLARE @wall_id INT;
	DECLARE @loan_name VARCHAR(20);
	DECLARE @today DATE;
	DECLARE @note VARCHAR(50);

	SELECT @old_debt=current_debt, @loan_name=[name] FROM DELETED;
	SELECT @new_debt=current_debt, @acct_id=account_id FROM INSERTED;
	SELECT @wall_id=MIN(wallet_id) FROM Project.wallets WHERE account_id=@acct_id;
	SET @payment = @old_debt - @new_debt;
	SET @today = CONVERT(DATE, GETDATE());
	SET @note = CONCAT(@loan_name,' Loan Payment')


	EXEC pr_insert_transaction @account_id=@acct_id, @category_id=400, @from_wallet_id=@wall_id, @transaction_type_id=-1, @amount=@payment,
	@date=@today, @notes=@note;
END
GO

CREATE FUNCTION udf_annual_statistics (
	@account_id INT,
	@year INT
)
RETURNS @table TABLE (date_year INT, date_month INT, income MONEY, expenses MONEY, balance MONEY)
AS BEGIN
	
	DECLARE @year_str VARCHAR(4);
	SELECT @year_str=CAST(@year as varchar(10));

	INSERT @table(date_year, date_month, income, expenses, balance)
	SELECT @year, I.mes, inc, expe, (inc-expe)
	FROM (
		(SELECT MONTH([date]) AS mes, SUM(amount) AS inc
		FROM Project.transactions
		WHERE
			transaction_type_id=1 AND YEAR([date])=@year_str AND account_id=@account_id
		GROUP BY MONTH([date])
		) AS I
		
		JOIN
		
		(SELECT MONTH([date]) AS mes, SUM(amount) AS expe
		FROM Project.transactions
		WHERE
			transaction_type_id=-1 AND YEAR([date])=@year_str AND account_id=@account_id
		GROUP BY MONTH([date])
		) AS E

		ON I.mes=E.mes
	);
	RETURN;
END
GO

--------------------------------------------------------------------
-- POPULATE DATABASE -----------------------------------------------
--------------------------------------------------------------------

EXEC pr_insert_user @user_name='user1', @email='user1@ua.pt';
EXEC pr_insert_user @user_name='user2', @email='user2@ua.pt', @fname='user', @lname='two', @active_subscription=1, @card_number='1234567890', @term='2020-11-26', @periodicity=1;

EXEC pr_insert_money_account @user_email='user1@ua.pt', @account_name='u1_personal';
EXEC pr_insert_money_account @user_email='user2@ua.pt', @account_name='u2_personal';
EXEC pr_insert_money_account @user_email='user1@ua.pt', @account_name='u1_shared';
EXEC pr_money_account_add_user @account_id=3, @user_email='user2@ua.pt'

EXEC pr_insert_transaction @account_id=1, @category_id=0, @from_wallet_id=1, @transaction_type_id=1, @amount=1000, @date='01/01/2019';
EXEC pr_insert_transaction @account_id=1, @category_id=0, @from_wallet_id=1, @transaction_type_id=1, @amount=1000, @date='02/01/2019';
EXEC pr_insert_transaction @account_id=1, @category_id=0, @from_wallet_id=1, @transaction_type_id=1, @amount=1000, @date='03/01/2019';
EXEC pr_insert_transaction @account_id=1, @category_id=0, @from_wallet_id=1, @transaction_type_id=1, @amount=1000, @date='04/01/2019';
EXEC pr_insert_transaction @account_id=1, @category_id=0, @from_wallet_id=1, @transaction_type_id=1, @amount=1000, @date='05/01/2019';
EXEC pr_insert_transaction @account_id=1, @category_id=0, @from_wallet_id=1, @transaction_type_id=1, @amount=1000, @date='06/01/2019';
EXEC pr_insert_transaction @account_id=1, @category_id=0, @from_wallet_id=1, @transaction_type_id=1, @amount=1000, @date='07/01/2019';
EXEC pr_insert_transaction @account_id=1, @category_id=0, @from_wallet_id=1, @transaction_type_id=1, @amount=1000, @date='08/01/2019';
EXEC pr_insert_transaction @account_id=1, @category_id=0, @from_wallet_id=1, @transaction_type_id=1, @amount=1000, @date='09/01/2019';
EXEC pr_insert_transaction @account_id=1, @category_id=0, @from_wallet_id=1, @transaction_type_id=1, @amount=1000, @date='10/01/2019';
EXEC pr_insert_transaction @account_id=1, @category_id=0, @from_wallet_id=1, @transaction_type_id=1, @amount=1000, @date='11/01/2019';
EXEC pr_insert_transaction @account_id=1, @category_id=0, @from_wallet_id=1, @transaction_type_id=1, @amount=1000, @date='12/01/2019';

EXEC pr_insert_transaction @account_id=1, @category_id=100, @from_wallet_id=1, @transaction_type_id=-1, @amount=250, @date='01/01/2019';
EXEC pr_insert_transaction @account_id=1, @category_id=100, @from_wallet_id=1, @transaction_type_id=-1, @amount=500, @date='01/01/2019';
EXEC pr_insert_transaction @account_id=1, @category_id=100, @from_wallet_id=1, @transaction_type_id=-1, @amount=600, @date='02/01/2019';
EXEC pr_insert_transaction @account_id=1, @category_id=100, @from_wallet_id=1, @transaction_type_id=-1, @amount=500, @date='02/01/2019';
EXEC pr_insert_transaction @account_id=1, @category_id=100, @from_wallet_id=1, @transaction_type_id=-1, @amount=300, @date='03/01/2019';
EXEC pr_insert_transaction @account_id=1, @category_id=100, @from_wallet_id=1, @transaction_type_id=-1, @amount=500, @date='03/01/2019';
EXEC pr_insert_transaction @account_id=1, @category_id=100, @from_wallet_id=1, @transaction_type_id=-1, @amount=350, @date='04/01/2019';
EXEC pr_insert_transaction @account_id=1, @category_id=100, @from_wallet_id=1, @transaction_type_id=-1, @amount=500, @date='04/01/2019';
EXEC pr_insert_transaction @account_id=1, @category_id=100, @from_wallet_id=1, @transaction_type_id=-1, @amount=600, @date='05/01/2019';
EXEC pr_insert_transaction @account_id=1, @category_id=100, @from_wallet_id=1, @transaction_type_id=-1, @amount=600, @date='05/01/2019';
EXEC pr_insert_transaction @account_id=1, @category_id=100, @from_wallet_id=1, @transaction_type_id=-1, @amount=550, @date='06/01/2019';
EXEC pr_insert_transaction @account_id=1, @category_id=100, @from_wallet_id=1, @transaction_type_id=-1, @amount=600, @date='06/01/2019';
EXEC pr_insert_transaction @account_id=1, @category_id=100, @from_wallet_id=1, @transaction_type_id=-1, @amount=250, @date='07/01/2019';
EXEC pr_insert_transaction @account_id=1, @category_id=100, @from_wallet_id=1, @transaction_type_id=-1, @amount=500, @date='07/01/2019';
EXEC pr_insert_transaction @account_id=1, @category_id=100, @from_wallet_id=1, @transaction_type_id=-1, @amount=600, @date='08/01/2019';
EXEC pr_insert_transaction @account_id=1, @category_id=100, @from_wallet_id=1, @transaction_type_id=-1, @amount=500, @date='08/01/2019';
EXEC pr_insert_transaction @account_id=1, @category_id=100, @from_wallet_id=1, @transaction_type_id=-1, @amount=300, @date='09/01/2019';
EXEC pr_insert_transaction @account_id=1, @category_id=100, @from_wallet_id=1, @transaction_type_id=-1, @amount=500, @date='09/01/2019';
EXEC pr_insert_transaction @account_id=1, @category_id=100, @from_wallet_id=1, @transaction_type_id=-1, @amount=350, @date='10/01/2019';
EXEC pr_insert_transaction @account_id=1, @category_id=100, @from_wallet_id=1, @transaction_type_id=-1, @amount=500, @date='10/01/2019';
EXEC pr_insert_transaction @account_id=1, @category_id=100, @from_wallet_id=1, @transaction_type_id=-1, @amount=600, @date='11/01/2019';
EXEC pr_insert_transaction @account_id=1, @category_id=100, @from_wallet_id=1, @transaction_type_id=-1, @amount=600, @date='11/01/2019';
EXEC pr_insert_transaction @account_id=1, @category_id=100, @from_wallet_id=1, @transaction_type_id=-1, @amount=550, @date='12/01/2019';
EXEC pr_insert_transaction @account_id=1, @category_id=100, @from_wallet_id=1, @transaction_type_id=-1, @amount=600, @date='12/01/2019';

EXEC pr_insert_transaction @account_id=1, @category_id=0, @from_wallet_id=1, @transaction_type_id=1, @amount=900, @date='01/01/2018';
EXEC pr_insert_transaction @account_id=1, @category_id=0, @from_wallet_id=1, @transaction_type_id=1, @amount=900, @date='02/01/2018';
EXEC pr_insert_transaction @account_id=1, @category_id=0, @from_wallet_id=1, @transaction_type_id=1, @amount=900, @date='03/01/2018';
EXEC pr_insert_transaction @account_id=1, @category_id=0, @from_wallet_id=1, @transaction_type_id=1, @amount=900, @date='04/01/2018';
EXEC pr_insert_transaction @account_id=1, @category_id=0, @from_wallet_id=1, @transaction_type_id=1, @amount=900, @date='05/01/2018';
EXEC pr_insert_transaction @account_id=1, @category_id=0, @from_wallet_id=1, @transaction_type_id=1, @amount=900, @date='06/01/2018';
EXEC pr_insert_transaction @account_id=1, @category_id=0, @from_wallet_id=1, @transaction_type_id=1, @amount=900, @date='07/01/2018';
EXEC pr_insert_transaction @account_id=1, @category_id=0, @from_wallet_id=1, @transaction_type_id=1, @amount=900, @date='08/01/2018';
EXEC pr_insert_transaction @account_id=1, @category_id=0, @from_wallet_id=1, @transaction_type_id=1, @amount=900, @date='09/01/2018';
EXEC pr_insert_transaction @account_id=1, @category_id=0, @from_wallet_id=1, @transaction_type_id=1, @amount=900, @date='10/01/2018';
EXEC pr_insert_transaction @account_id=1, @category_id=0, @from_wallet_id=1, @transaction_type_id=1, @amount=900, @date='11/01/2018';
EXEC pr_insert_transaction @account_id=1, @category_id=0, @from_wallet_id=1, @transaction_type_id=1, @amount=900, @date='12/01/2018';

EXEC pr_insert_transaction @account_id=1, @category_id=100, @from_wallet_id=1, @transaction_type_id=-1, @amount=200, @date='01/01/2018';
EXEC pr_insert_transaction @account_id=1, @category_id=100, @from_wallet_id=1, @transaction_type_id=-1, @amount=550, @date='01/01/2018';
EXEC pr_insert_transaction @account_id=1, @category_id=100, @from_wallet_id=1, @transaction_type_id=-1, @amount=500, @date='02/01/2018';
EXEC pr_insert_transaction @account_id=1, @category_id=100, @from_wallet_id=1, @transaction_type_id=-1, @amount=450, @date='02/01/2018';
EXEC pr_insert_transaction @account_id=1, @category_id=100, @from_wallet_id=1, @transaction_type_id=-1, @amount=350, @date='03/01/2018';
EXEC pr_insert_transaction @account_id=1, @category_id=100, @from_wallet_id=1, @transaction_type_id=-1, @amount=500, @date='03/01/2018';
EXEC pr_insert_transaction @account_id=1, @category_id=100, @from_wallet_id=1, @transaction_type_id=-1, @amount=350, @date='04/01/2018';
EXEC pr_insert_transaction @account_id=1, @category_id=100, @from_wallet_id=1, @transaction_type_id=-1, @amount=500, @date='04/01/2018';
EXEC pr_insert_transaction @account_id=1, @category_id=100, @from_wallet_id=1, @transaction_type_id=-1, @amount=450, @date='05/01/2018';
EXEC pr_insert_transaction @account_id=1, @category_id=100, @from_wallet_id=1, @transaction_type_id=-1, @amount=550, @date='05/01/2018';
EXEC pr_insert_transaction @account_id=1, @category_id=100, @from_wallet_id=1, @transaction_type_id=-1, @amount=550, @date='06/01/2018';
EXEC pr_insert_transaction @account_id=1, @category_id=100, @from_wallet_id=1, @transaction_type_id=-1, @amount=600, @date='06/01/2018';
EXEC pr_insert_transaction @account_id=1, @category_id=100, @from_wallet_id=1, @transaction_type_id=-1, @amount=300, @date='07/01/2018';
EXEC pr_insert_transaction @account_id=1, @category_id=100, @from_wallet_id=1, @transaction_type_id=-1, @amount=450, @date='07/01/2018';
EXEC pr_insert_transaction @account_id=1, @category_id=100, @from_wallet_id=1, @transaction_type_id=-1, @amount=650, @date='08/01/2018';
EXEC pr_insert_transaction @account_id=1, @category_id=100, @from_wallet_id=1, @transaction_type_id=-1, @amount=550, @date='08/01/2018';
EXEC pr_insert_transaction @account_id=1, @category_id=100, @from_wallet_id=1, @transaction_type_id=-1, @amount=250, @date='09/01/2018';
EXEC pr_insert_transaction @account_id=1, @category_id=100, @from_wallet_id=1, @transaction_type_id=-1, @amount=450, @date='09/01/2018';
EXEC pr_insert_transaction @account_id=1, @category_id=100, @from_wallet_id=1, @transaction_type_id=-1, @amount=400, @date='10/01/2018';
EXEC pr_insert_transaction @account_id=1, @category_id=100, @from_wallet_id=1, @transaction_type_id=-1, @amount=500, @date='10/01/2018';
EXEC pr_insert_transaction @account_id=1, @category_id=100, @from_wallet_id=1, @transaction_type_id=-1, @amount=600, @date='11/01/2018';
EXEC pr_insert_transaction @account_id=1, @category_id=100, @from_wallet_id=1, @transaction_type_id=-1, @amount=650, @date='11/01/2018';
EXEC pr_insert_transaction @account_id=1, @category_id=100, @from_wallet_id=1, @transaction_type_id=-1, @amount=500, @date='12/01/2018';
EXEC pr_insert_transaction @account_id=1, @category_id=100, @from_wallet_id=1, @transaction_type_id=-1, @amount=500, @date='12/01/2018';