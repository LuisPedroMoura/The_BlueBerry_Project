CREATE TABLE users
(
  email VARCHAR(50) NOT NULL,
  username INT NOT NULL,
  PRIMARY KEY (email)
);

CREATE TABLE category
(
  name VARCHAR(50) NOT NULL,
  id INT NOT NULL,
  PRIMARY KEY (id),
  UNIQUE (name)
);

CREATE TABLE recurrence
(
  periodicity INT NOT NULL,
  designation INT NOT NULL,
  PRIMARY KEY (periodicity)
);

CREATE TABLE stock_types
(
  id INT NOT NULL,
  stock_type INT NOT NULL,
  PRIMARY KEY (id)
);

CREATE TABLE subscription
(
  term DATE NOT NULL,
  card_number INT NOT NULL,
  owner_email INT NOT NULL,
  owner_fname INT NOT NULL,
  owner_mname INT NOT NULL,
  owner_lname INT NOT NULL,
  active INT NOT NULL,
  recurrence INT NOT NULL,
  PRIMARY KEY (owner_email),
  FOREIGN KEY (recurrence) REFERENCES recurrence(periodicity),
  UNIQUE (card_number)
);

CREATE TABLE account
(
  name VARCHAR(50) NOT NULL,
  owner_email INT NOT NULL,
  PRIMARY KEY (name, owner_email),
  FOREIGN KEY (owner_email) REFERENCES subscription(owner_email)
);

CREATE TABLE wallet
(
  name VARCHAR(50) NOT NULL,
  balance FLOAT NOT NULL,
  account_name VARCHAR(50) NOT NULL,
  owner_email INT NOT NULL,
  PRIMARY KEY (name, account_name, owner_email),
  FOREIGN KEY (account_name, owner_email) REFERENCES account(name, owner_email)
);

CREATE TABLE transactions
(
  paid INT NOT NULL,
  notes VARCHAR(100) NOT NULL,
  transaction_date DATE NOT NULL,
  amount FLOAT NOT NULL,
  transaction_location VARCHAR(50) NOT NULL,
  id INT NOT NULL,
  category_id INT NOT NULL,
  wallet_name VARCHAR(50) NOT NULL,
  account_name VARCHAR(50) NOT NULL,
  owner_email INT NOT NULL,
  PRIMARY KEY (id),
  FOREIGN KEY (category_id) REFERENCES category(id),
  FOREIGN KEY (wallet_name, account_name, owner_email) REFERENCES wallet(name, account_name, owner_email)
);

CREATE TABLE transfer
(
  recipient_wallet_id INT NOT NULL,
  transaction_id INT NOT NULL,
  PRIMARY KEY (transaction_id),
  FOREIGN KEY (transaction_id) REFERENCES transactions(id)
);

CREATE TABLE goal
(
  amount FLOAT NOT NULL,
  term DATE NOT NULL,
  accomplished INT NOT NULL,
  name VARCHAR(50) NOT NULL,
  category_id INT NOT NULL,
  account_name VARCHAR(50) NOT NULL,
  owner_email INT NOT NULL,
  PRIMARY KEY (name, account_name, owner_email),
  FOREIGN KEY (category_id) REFERENCES category(id),
  FOREIGN KEY (account_name, owner_email) REFERENCES account(name, owner_email)
);

CREATE TABLE budget
(
  amount FLOAT NOT NULL,
  id INT NOT NULL,
  category_id INT NOT NULL,
  account_name VARCHAR(50) NOT NULL,
  owner_email INT NOT NULL,
  recurrence INT NOT NULL,
  PRIMARY KEY (id),
  FOREIGN KEY (category_id) REFERENCES category(id),
  FOREIGN KEY (account_name, owner_email) REFERENCES account(name, owner_email),
  FOREIGN KEY (recurrence) REFERENCES recurrence(periodicity)
);

CREATE TABLE loans
(
  value FLOAT NOT NULL,
  term DATE NOT NULL,
  interest FLOAT NOT NULL,
  loan_name INT NOT NULL,
  monthly_payment INT NOT NULL,
  account_name VARCHAR(50) NOT NULL,
  owner_email INT NOT NULL,
  PRIMARY KEY (loan_name, account_name, owner_email),
  FOREIGN KEY (account_name, owner_email) REFERENCES account(name, owner_email)
);

CREATE TABLE stocks
(
  ticker FLOAT NOT NULL,
  company INT NOT NULL,
  purchase_value INT NOT NULL,
  bid_price INT NOT NULL,
  ask_price INT NOT NULL,
  stock_type INT NOT NULL,
  account_name VARCHAR(50) NOT NULL,
  owner_email INT NOT NULL,
  PRIMARY KEY (ticker, account_name, owner_email),
  FOREIGN KEY (stock_type) REFERENCES stock_types(id),
  FOREIGN KEY (account_name, owner_email) REFERENCES account(name, owner_email)
);

CREATE TABLE expense
(
  id INT NOT NULL,
  PRIMARY KEY (id),
  FOREIGN KEY (id) REFERENCES transactions(id)
);

CREATE TABLE income
(
  id INT NOT NULL,
  PRIMARY KEY (id),
  FOREIGN KEY (id) REFERENCES transactions(id)
);

CREATE TABLE link_users_accounts
(
  account_name VARCHAR(50) NOT NULL,
  owner_email INT NOT NULL,
  user_email VARCHAR(50) NOT NULL,
  PRIMARY KEY (account_name, owner_email, user_email),
  FOREIGN KEY (account_name, owner_email) REFERENCES account(name, owner_email),
  FOREIGN KEY (user_email) REFERENCES users(email)
);
