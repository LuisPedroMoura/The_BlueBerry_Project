---------------------------------------------------------------
--- RETURN ERROR RULES ----------------------------------------
---------------------------------------------------------------
/*
-1 : Missing Argument
-2 : Row to insert already exists
-3 : Row to update does not exist
-4 : Row to delete does not exist
-5 : Argument to select does not exist
*/

---------------------------------------------------------------
--- CREATE CLEAN PROCEDURE and CLEAN FUNCTIONS AND PROCEDURES -
---------------------------------------------------------------

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
END
GO

EXEC clean_procedures_and_functions;
GO

DROP PROC clean_procedures_and_functions;
GO

---------------------------------------------------------------
--- CREATE PROCEDURES AND FUNCTIONS ---------------------------
---------------------------------------------------------------

---------------------------------------------------------------
--- USERS -----------------------------------------------------
---------------------------------------------------------------





-------------------------------
--- EXECUTED TILL THIS POINT! -------------------------
-------------------------------