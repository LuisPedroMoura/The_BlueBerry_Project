CREATE TABLE Project.recurrence)
	periodicity INT NOT NULL,
	designation VARCHAR(20),
	CHECK (periodicity>0),
	PRIMARY KEY (periodicity)
);

CREATE TABLE Project.subscriptions(
	owner_email VARCHAR(50) NOT NULL,
	owner_fname VARCHAR(15) NOT NULL,
	owner_mname VARCHAR(15) NOT NULL,
	owner_lname VARCHAR(15) NOT NULL,
	card_number INT NOT NULL,
	term DATE NOT NULL,
	active BIT NOT NULL,
	recurrence INT NOT NULL
	PRIMARY KEY (owner_email),
);

CREATE TABLE Project.users(
	email VARCHAR(50) NOT NULL,
	username VARCHAR(20) NOT NULL,
	CONSTRAINT valid_email
		CHECK(email LIKE '%_@__%.__%'
        AND PATINDEX('%[^a-z,0-9,@,.,_,\-]%', email) = 0),
	PRIMARY KEY (email),
);

CREATE TABLE Project.link_users_accounts(
	user_email VARCHAR(50) NOT NULL,
	owner_email VARCHAR(50) NOT NULL,
	account_name VARCHAR(20) NOT NULL
	--PRIMARY KEY (user_email, owner_email, account_name),
	--FOREIGN KEY (user_email) REFERENCES Project.users(email),
	--FOREIGN KEY (account_name, owner_email) REFERENCES Project.accounts(account_name, owner_email)
);


CREATE TABLE Project.accounts(
	subscription_owner_email VARCHAR(50) NOT NULL,
	account_name VARCHAR(20) UNIQUE NOT NULL,
	--PRIMARY KEY(subscription_owner_email, account_name),
	--FOREIGN KEY(user_email) REFERENCES Project.users(email)
);

CREATE TABLE Project.stocks(
	ticker VARCHAR(50) NOT NULL,
	account_name VARCHAR(20) NOT NULL,
	subscription_owner_email VARCHAR(50) NOT NULL,
	company VARCHAR(50) NOT NULL,
	stock_type INT NOT NULL,
	purchase_value FLOAT NOT NULL,
	bid_price FLOAT NOT NULL,
	ask_price FLOAT NOT NULL,
);

CREATE TABLE Project.loans(
	loan_name VARCHAR(20) NOT NULL,
	account_name VARCHAR(20) NOT NULL,
	subscription_owner_email VARCHAR(50) NOT NULL,
	amount FLOAT NOT NULL,
	term DATE NOT NULL,
	monthly_payment FLOAT NOT NULL,
	interest FLOAT NOT NULL,
	CHECK (interest>=0 AND interest<=1),
);

CREATE TABLE Project.budgets(
	budget_name VARCHAR(20) NOT NULL,
	account_name VARCHAR(20) NOT NULL,
	subscription_owner_email VARCHAR(50) NOT NULL,
	recurrence INT NOT NULL DEFAULT 1,
	amount FLOAT NOT NULL DEFAULT 0,
	category_id INT NOT NULL DEFAULT 'miscelaneous',
);

CREATE TABLE Project.goals(
	goal_name VARCHAR(20) NOT NULL,
	account_name VARCHAR(20) NOT NULL,
	subscription_pwner_email VARCHAR(50) NOT NULL,
	amount FLOAT NOT NULL,
	term DATE NOT NULL,
	accomplished BIT NOT NULL DEFAULT 0,
	category_id INT NOT NULL
);

CREATE TABLE Project.wallets(
	wallet_name VARCHAR(20) NOT NULL,
	account_name VARCHAR(20) NOT NULL,
	subscription_owner_email VARCHAR(50) NOT NULL
	balance  FLOAT NOT NULL DEFAULT 0,
	--FOREIGN KEY(account_name) REFERENCES Project.accounts(NAME),
	--PRIMARY KEY(account_name, name)
);

CREATE TABLE Project.transactions(
	id INT NOT NULL,
	category_id INT NOT NULL DEFAULT 'miscelaneous',
	wallet_name VARCHAR(20) NOT NULL,
	account_name VARCHAR(20) NOT NULL,
	subscription_owner_email VARCHAR(50) NOT NULL
	amount FLOAT NOT NULL,
	transaction_date DATE NOT NULL,
	transaction_location VARCHAR(20),
	notes VARCHAR(50),
	paid BIT,
);

CREATE TABLE Project.transfers(
	transaction_id INT NOT NULL,
	recipient_wallet VARCHAR(20) NOT NULL
);

CREATE TABLE Project.expense(
	transaction_id INT NOT NULL
);

CREATE TABLE Project.income(
	transaction_id INT NOT NULL
);







CREATE TABLE Project.stock_types(
	id INT NOT NULL IDENTITY(1,1),
	stock_type_name VARCHAR(20), -- common, preferred, convertible
	PRIMARY KEY (id)
);

CREATE TABLE Project.categories(
	id INT NOT NULL IDENTITY(1,1),
	category_name VARCHAR(20) NOT NULL
);

CREATE TABLE Project.sub_categories(
	id INT NOT NULL IDENTITY(1,1),
	sub_category_name VARCHAR(20) NOT NULL
);




-- boolean type type. Garanties values 0 or 1, and no repetition of rules.
CREATE TABLE Project.boolean(
	id INT NOT NULL,
	boolean_name VARCHAR(5) NOT NULL -- TRUE, FALSE
	check ((id=0 AND boolean_name='FALSE') OR (id=1 AND boolean_name='TRUE')),
	PRIMARY KEY(id),
);
INSERT INTO Project.boolean VALUES (0, 'FALSE'), (1, 'TRUE');