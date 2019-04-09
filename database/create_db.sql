CREATE TABLE Project.money_accounts
(
	account_name VARCHAR(20),
	patrimony INT DEFAULT 0,
	id INT IDENTITY(1,1),
	CHECK (account_name!=''),
	CONSTRAINT PK_MONEYACCOUNTS PRIMARY KEY (account_name, id)
);

CREATE TABLE Project.wallets
(
	balance INT DEFAULT 0,
	[name] VARCHAR(20),
	id INT IDENTITY(1,1),
	account_name VARCHAR(20),
	account_id INT,
	CONSTRAINT PK_WALLETS PRIMARY KEY (id),
	CONSTRAINT FK_WALLETS_MONEYACCOUNTS FOREIGN KEY (account_name, account_id) REFERENCES Projetc.money_accounts(account_name, id)
		ON DELETE NO ACTION ON UPDATE CASCADE
);

CREATE TABLE Project.recurrence
(
	designation VARCHAR(20),
	periodicity INT,
	CONSTRAINT PK_RECURRENCE PRIMARY KEY (periodicity)
);

CREATE TABLE Project.subscriptions
(
	term DATE, --DEFAULT DATEADD(year, 1, GETDATE()),
	card_number INT,
	fname VARCHAR(20),
	mname VARCHAR(20),
	lname VARCHAR(20),
	active INT,
	periodicity INT,
	CHECK(card_number>0),
	CONSTRAINT PK_SUBSCRIPTIONS PRIMARY KEY (card_number),
	CONSTRAINT FK_SUBSCRIPTIONS_RECURRENCE FOREIGN KEY (periodicity) REFERENCES Project.recurrence(periodicity)
		ON DELETE NO ACTION ON UPDATE CASCADE
		
);

CREATE TABLE Project.users
(
	[user_name] VARCHAR(20),
	email VARCHAR(50),
	card_number INT,
	CHECK([user_name] != ''),
	CONSTRAINT PK_USERS PRIMARY KEY (email),
	CONSTRAINT FK_USERS_SUBSCRIPTIONS FOREIGN KEY (card_number) REFERENCES Project.subscriptions(card_number)
		ON DELETE NO ACTION ON UPDATE CASCADE
);

CREATE TABLE Project.users_money_accounts
(
	user_email VARCHAR(50),
	account_name VARCHAR(20),
	account_id INT,
	CONSTRAINT PK_USERSMONEYACCOUNTS PRIMARY KEY (user_email, account_name, account_id),
	CONSTRAINT FK_USERSMONEYACCOUNTS_USERS FOREIGN KEY (user_email) REFERENCES Project.users(email)
		ON DELETE NO ACTION ON UPDATE CASCADE,
	CONSTRAINT FK_USERSMONEYACCOUNTS_MONEYACCOUNTS FOREIGN KEY (account_name, account_id) REFERENCES Project.money_accounts(account_name, id)
		ON DELETE NO ACTION ON UPDATE CASCADE
		
);

CREATE TABLE Project.categories
(
	[name] VARCHAR(20),
	id INT,
	account_name VARCHAR(20),
	account_id INT,
	CHECK([name] != ''),
	CONSTRAINT PK_CATEGORIES PRIMARY KEY (id, account_name, account_id),
	CONSTRAINT FK_CATEGORIES_MONEYACCOUNTS FOREIGN KEY (account_name, account_id) REFERENCES Project.money_accounts(account_name, id)
		ON DELETE NO ACTION ON UPDATE CASCADE
);

CREATE TABLE Project.transaction_types
(
	id INT,
	designation VARCHAR(20),
	CONSTRAINT PK_TRANSACTIONTYPES PRIMARY KEY (id)
);

CREATE TABLE Project.transactions
(
	amount INT,
	[date] DATE,
	notes VARCHAR(50),
	id INT IDENTITY(1,1),
	[location] VARCHAR(50),
	category_id INT,
	account_name VARCHAR(20),
	account_id INT,
	transaction_type_id INT,
	wallet_id INT,
	CHECK(amount>0),
	CONSTRAINT PK_TRANSACTIONS PRIMARY KEY (id),
	CONSTRAINT FK_TRANSACTIONS_CATEGORIES FOREIGN KEY (category_id, account_name, account_id) REFERENCES Project.categories(id, account_name, account_id)
		ON DELETE NO ACTION ON UPDATE CASCADE,
	CONSTRAINT FK_TRANSACTIONS_TRANSACTIONTYPES FOREIGN KEY (transaction_type_id) REFERENCES Project.transaction_types(id)
		ON DELETE NO ACTION ON UPDATE CASCADE,
	CONSTRAINT FK_TRANSACTIONS_WALLETS FOREIGN KEY (wallet_id) REFERENCES Project.wallets(id)
		ON DELETE NO ACTION ON UPDATE CASCADE
);

CREATE TABLE Project.transfers
(
	transaction_id INT,
	recipient_wallet_id INT,
	CONSTRAINT PK_TRANSFERS PRIMARY KEY (transaction_id),
	CONSTRAINT FK_TRANSFERS_TRANSACTIONS FOREIGN KEY (transaction_id) REFERENCES Project.transactions(id)
		ON DELETE NO ACTION ON UPDATE CASCADE,
	CONSTRAINT FK_TRANSFERS_WALLETS FOREIGN KEY (recipient_wallet_id) REFERENCES Project.wallets(id)
		ON DELETE NO ACTION ON UPDATE CASCADE,
);

CREATE TABLE Project.budgets
(
	amount INT DEFAULT 0,
	id INT IDENTITY(1,1),
	[start_date] DATE,
	end_date DATE,
	periodicity INT,
	category_id INT,
	account_name VARCHAR(20),
	account_id INT,
	CONSTRAINT PK_BUDGETS PRIMARY KEY (id, category_id, account_name, account_id),
	CONSTRAINT FK_BUDGETS_RECURRENCE FOREIGN KEY (periodicity) REFERENCES Project.recurrence(periodicity)
		ON DELETE NO ACTION ON UPDATE CASCADE,
	CONSTRAINT FK_BUDGETS_CATEGORIES FOREIGN KEY (category_id, account_name, account_id) REFERENCES Project.categories(id, account_name, account_id)
		ON DELETE NO ACTION ON UPDATE CASCADE
);

CREATE TABLE Project.goals
(
	amount INT DEFAULT 0,
	term DATE,
	accomplished INT,
	[name] VARCHAR(20),
	category_id INT,
	account_name VARCHAR(20),
	account_id INT,
	CHECK([name] != ''),
	CONSTRAINT PK_GOALS PRIMARY KEY ([name], category_id, account_name, account_id),
	CONSTRAINT FK_GOALS_CATEGORIES FOREIGN KEY (category_id, account_name, account_id) REFERENCES Project.categories(id, account_name, account_id)
		ON DELETE NO ACTION ON UPDATE CASCADE
);

CREATE TABLE Project.loans
(
	amount INT,
	term DATE,
	interest INT,
	[name] VARCHAR(20),
	montlhy_payment INT,
	account_name VARCHAR(20),
	account_id INT,
	CHECK([name] != ''),
	CONSTRAINT PK_LOANS PRIMARY KEY ([name], account_name, account_id),
	CONSTRAINT FK_LOANS_MONEYACCOUNTS FOREIGN KEY (account_name, account_id) REFERENCES Project.money_accounts(account_name, id)
		ON DELETE NO ACTION ON UPDATE CASCADE
);

CREATE TABLE Project.stock_types
(
	id INT,
	designation VARCHAR(20),
	CONSTRAINT PK_STOCKTYPES PRIMARY KEY (id),
	UNIQUE (designation)
);

CREATE TABLE Project.stocks
(
	bid_price INT,
	company VARCHAR(50),
	ask_price INT,
	stock_type_id INT,
	CHECK(company != ''),
	CONSTRAINT PK_STOCKS PRIMARY KEY (company),
	CONSTRAINT FK_STOCKS_STOCKTYPES FOREIGN KEY (stock_type_id) REFERENCES Project.stock_types(id)
		ON DELETE NO ACTION ON UPDATE CASCADE
);

CREATE TABLE Project.purchased_stocks
(
	purchase_value INT,
	ticker INT IDENTITY(1,1),
	quantity INT,
	company VARCHAR(50),
	account_name VARCHAR(20),
	account_id INT,
	CONSTRAINT PK_PURCHASEDSTOCKS PRIMARY KEY (ticker),
	CONSTRAINT FK_PURCHASEDSTOCKS_STOCKS FOREIGN KEY (company) REFERENCES Project.stocks(company)
		ON DELETE NO ACTION ON UPDATE CASCADE,
	CONSTRAINT FK_PURCHASEDSTOCKS_MONEYACCOUNTS FOREIGN KEY (account_name, account_id) REFERENCES Project.money_accounts(account_name, id)
		ON DELETE NO ACTION ON UPDATE CASCADE
);


INSERT INTO Project.recurrence (designation, periodicity)
VALUES 	
		('monthly', 1),
		('quarterly', 3),
		('anual', 12);


INSERT INTO Project.transaction_types (designation,id)
VALUES 	
		('expense', -1),
		('transfer', 0),
		('income', 1);


INSERT INTO Project.stock_types (designation,id)
VALUES 	
		('common', 0),
		('preferred', 1);
