-- INSERT USERS
CREATE PROC pr_insert_user (
	@email varchar(50),
	@card_number varchar(20),
	@term DATETIME,
	@fname VARCHAR(20),	
	@lname VARCHAR(20),
	@periodicity INT,
	@user_name varchar(20) = NULL,
	@mname VARCHAR(20) = NULL,
	@active BIT = 0
) AS
BEGIN
	
	IF @active=1 AND (ISNULL(@term, '')='' OR ISNULL(@fname, '')='' OR ISNULL(@lname, '')='' OR ISNULL(@periodicity, '')='' OR ISNULL(@card_number, '')='')
		RETURN 1

	INSERT INTO Project.users(email, [user_name])
	VALUES (@email, @user_name);

	IF @active=1
	BEGIN
		INSERT INTO Project.subscriptions(card_number, email, term, fname, mname, lname, active, periodicity)
		VALUES (@card_number, @email, @term, @fname, @mname, @lname, @active, @periodicity);
	END

END
GO


CREATE PROC pr_update_user (
	@email varchar(50),
	@card_number varchar(20),
	@term DATETIME,
	@fname VARCHAR(20),	
	@lname VARCHAR(20),
	@periodicity INT,
	@user_name varchar(20) = NULL,
	@mname VARCHAR(20) = NULL,
	@active BIT = 0
) AS
BEGIN
	
	-- verify if user exists
	IF (SELECT email FROM Project.users WHERE email=@email) IS NULL
		RETURN 1

	-- update 'users' table
	UPDATE Project.users SET [user_name]=@user_name WHERE email=@email;

	-- verify if user was subscribed
	DECLARE @subscribed BIT;
	SET @subscribed = ISNULL((SELECT card_number FROM Project.subscriptions WHERE email=@email), 0);
	
	IF @active=1
	BEGIN
		
		IF @subscribed=1
		BEGIN
			UPDATE Project.subscriptions
			SET card_number=@card_number, term=@term, fname=@fname, mname=@mname, lname=@lname, periodicity=@periodicity, active=@active
			WHERE email=@email;
		END

		ELSE
		BEGIN
			INSERT INTO Project.subscriptions (card_number, term, fname, mname, lname, periodicity, active)
			VALUES (@card_number, @term, @fname, @mname, @lname, @periodicity, @active);
		END
	END

	ELSE
	BEGIN
		IF @subscribed=1
		BEGIN
			EXEC pr_delete_subscription @email;
		END
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


CREATE FUNCTION udf_select_user ()
RETURNS TABLE
AS
	RETURN (
		SELECT * FROM Project.users
	);
GO
	


-- INSERT MONEY ACCOUNTS
CREATE PROC pr_insert_money_account (
	@user_email varchar(50),
	@account_name varchar(20),
	@patrimony MONEY = 0.0
) AS
BEGIN
	-- insert new money_account
	INSERT INTO Project.money_accounts(account_name, patrimony)
	VALUES (@account_name, @patrimony);
	-- get new money_account id
	DECLARE @account_id INT;
	SET @account_id = SCOPE_IDENTITY();
	-- create relation between money account and user
	INSERT INTO Project.users_money_accounts([user_email], account_id)
	VALUES (@user_email, @account_id);
END
GO

-- INSERT WALLETS
CREATE PROC pr_insert_wallets (
	@account_id varchar(50),
	@name varchar(20),
	@balance MONEY = 0.0
) AS
BEGIN

	INSERT INTO Project.wallets(account_id, [name], balance)
	VALUES (@account_id, @name, @balance);
END
GO

-------------------------------
--- EXECUTED TILL THIS POINT! -------------------------
-------------------------------