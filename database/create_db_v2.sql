CREATE TABLE money_accounts
(
	account_name VARCHAR(20),
	patrimony INT,
	id INT,
	PRIMARY KEY (account_name, id)
);

CREATE TABLE wallets
(
	balance INT,
	name VARCHAR(20),
	id INT,
	account_name VARCHAR(20),
	account_id INT,
	PRIMARY KEY (id),
	FOREIGN KEY (account_name, account_id) REFERENCES money_accounts(account_name, id)
);

CREATE TABLE loans
(
	amount INT,
	term DATE,
	interest INT,
	name VARCHAR(20),
	montlhy_payment INT,
	account_name VARCHAR(20),
	account_id INT,
	PRIMARY KEY (name, account_name, account_id),
	FOREIGN KEY (account_name, account_id) REFERENCES money_accounts(account_name, id)
);

CREATE TABLE stock_types
(
	id INT,
	designation VARCHAR(20),
	PRIMARY KEY (id),
	UNIQUE (designation)
);

CREATE TABLE recurrence
(
	designation VARCHAR(20),
	periodicity INT,
	PRIMARY KEY (periodicity)
);

CREATE TABLE transaction_types
(
	id INT,
	designation VARCHAR(20),
	PRIMARY KEY (id)
);

CREATE TABLE categories
(
	name VARCHAR(20),
	id INT,
	account_name VARCHAR(20),
	account_id INT,
	PRIMARY KEY (id, account_name, account_id),
	FOREIGN KEY (account_name, account_id) REFERENCES money_accounts(account_name, id)
);

CREATE TABLE subscriptions
(
	term DATE,
	card_number INT,
	fname VARCHAR(20),
	mname VARCHAR(20),
	lname VARCHAR(20),
	active INT,
	periodicity INT,
	PRIMARY KEY (card_number),
	FOREIGN KEY (periodicity) REFERENCES recurrence(periodicity)
);

CREATE TABLE stocks
(
	bid_price INT,
	company VARCHAR(50),
	ask_price INT,
	stock_type_id INT,
	PRIMARY KEY (company),
	FOREIGN KEY (stock_type_id) REFERENCES stock_types(id)
);

CREATE TABLE users
(
	user_name VARCHAR(20),
	email VARCHAR(50),
	card_number INT,
	PRIMARY KEY (email),
	FOREIGN KEY (card_number) REFERENCES subscriptions(card_number)
);

CREATE TABLE purchased_stocks
(
	purchase_value INT,
	ticker_(id) INT,
	quantity INT,
	company VARCHAR(50),
	account_name VARCHAR(20),
	account_id INT,
	PRIMARY KEY (ticker_(id)),
	FOREIGN KEY (company) REFERENCES stocks(company),
	FOREIGN KEY (account_name, account_id) REFERENCES money_accounts(account_name, id)
);

CREATE TABLE users_money_accounts
(
	user_email VARCHAR(50),
	account_name VARCHAR(20),
	account_id INT,
	PRIMARY KEY (user_email, account_name, account_id),
	FOREIGN KEY (user_email) REFERENCES users(email),
	FOREIGN KEY (account_name, account_id) REFERENCES money_accounts(account_name, id)
);

CREATE TABLE transactions
(
	amount INT,
	date DATE,
	notes VARCHAR(50),
	id INT,
	location VARCHAR(50),
	category_id INT,
	account_name VARCHAR(20),
	account_id INT,
	transaction_type_id INT,
	wallet_id INT,
	PRIMARY KEY (id),
	FOREIGN KEY (category_id, account_name, account_id) REFERENCES categories(id, account_name, account_id),
	FOREIGN KEY (transaction_type_id) REFERENCES transaction_types(id),
	FOREIGN KEY (wallet_id) REFERENCES wallets(id)
);

CREATE TABLE budgets
(
	amount INT,
	id INT,
	start_date DATE,
	end_date DATE,
	periodicity INT,
	category_id INT,
	account_name VARCHAR(20),
	account_id INT,
	PRIMARY KEY (id, category_id, account_name, account_id),
	FOREIGN KEY (periodicity) REFERENCES recurrence(periodicity),
	FOREIGN KEY (category_id, account_name, account_id) REFERENCES categories(id, account_name, account_id)
);

CREATE TABLE goals
(
	amount INT,
	term DATE,
	accomplished INT,
	name VARCHAR(20),
	category_id INT,
	account_name VARCHAR(20),
	account_id INT,
	PRIMARY KEY (name, category_id, account_name, account_id),
	FOREIGN KEY (category_id, account_name, account_id) REFERENCES categories(id, account_name, account_id)
);

CREATE TABLE transfers
(
	transaction_id INT,
	recipient_wallet_id INT,
	PRIMARY KEY (transaction_id),
	FOREIGN KEY (transaction_id) REFERENCES transactions(id),
	FOREIGN KEY (recipient_wallet_id) REFERENCES wallets(id)
);
