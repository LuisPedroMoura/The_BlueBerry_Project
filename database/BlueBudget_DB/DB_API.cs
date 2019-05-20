using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueBudget_DB
{
    class DB_API
    {

        public enum UserEnt
        {
            user_name,
            email,
            fname,
            mname,
            lname,
            card_number,
            periodicity,
            term,
            active_subscription
        };
        public enum RecurrenceEnt
        {
            periodicity,
            designation
        };
        public enum MoneyAccountEnt
        {
            user_email,
            account_id,
            account_name,
            balance,
            patrimony,
        };
        public enum CategoryEnt
        {
            category_id,
            account_id,
            name,
            category_type_id
        }
        public enum CategoryTypeEnt
        {
            Income,
            Expense,
            designation,
            category_type_id
        }
        public enum LoanEnt
        {
            name,
	        initial_amount,
            current_debt,
            term,
	        interest,
	        monthly_payment,
            account_id,
            payment
        }
        public enum BudgetEnt
        {
            account_id,
            category_id,
            budget_id,
            amount,
            start_date,
            end_date,
            periodicity
        }
        public enum GoalEnt
        {
            name,
            account_id,
            category_id,
            amount,
            term,
            accomplished
        }
        public enum WalletEnt
        {
            name,
            wallet_id,
            account_id,
            balance
        }
        public enum TransactionEnt
        {
            account_id,
            category_id,
            wallet_id,
            from_wallet_id,
            to_wallet_id,
            transaction_id,
            transaction_type_id,
            amount,
            min_amount,
            max_amount,
            date,
            start_date,
            end_date,
            notes,
            location
        }
        public enum TransactionTypeEnt
        {
            transaction_type_id,
            designation
        }
        public enum StockEnt
        {
            ticker,
            company,
            account_id,
            purchase_price,
            bid_price,
            ask_price,
            stock_type_id,
            stock_type
        }

        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // API METHODS ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public static string Moneyfy(string value)
        {
            if (Double.TryParse(value, out double aux))
            {
                return Moneyfy(aux);
            }
            return String.Empty;
        }

        public static string Moneyfy(double value)
        {
            return String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:C2}", value);
        }

        public static double UnMoneyfy(string value)
        {
            double res;
            try
            {
                res = Double.Parse(value.Substring(1));
            }
            catch
            {
                value = value.Substring(2);
                value = value.Remove(value.Length-2);
                res = -Double.Parse(value);
            }

            return res;
        }

            


// ----------------------------------------------------------------------------------------------
// USERS ----------------------------------------------------------------------------------------
// ----------------------------------------------------------------------------------------------

        public static bool ExistsUser(string email)
        {
            var attrValue = DB_IO.AttrValue();
            attrValue[DB_API.UserEnt.email] = email;
            return DB_IO.Exists(DB_IO.DB_Interface.pr_exists_user, attrValue);
        }

        public static void InsertUser(string username, string email, string fname, string mname, string lname,
            string cardNo, int periodicity, DateTime term, bool active)
        {
            var attrValue = new Dictionary<System.Enum, Object>
            {
                { UserEnt.user_name, username },
                { UserEnt.email, email },
                { UserEnt.fname, fname },
                { UserEnt.mname, mname },
                { UserEnt.lname, lname },
                { UserEnt.card_number, cardNo },
                { UserEnt.periodicity, periodicity },
                { UserEnt.term, term },
                { UserEnt.active_subscription, active }
            };
            DB_IO.Insert(DB_IO.DB_Interface.pr_insert_user, attrValue);
        }

        public static void UpdateUser(string username, string email, string fname = null, string mname = null,
            string lname = null, string cardNo = null, int periodicity = 1, DateTime term = new DateTime(), bool? active = null)
        {
            var attrValue = new Dictionary<System.Enum, Object>
            {
                { UserEnt.user_name, username },
                { UserEnt.email, email },
                { UserEnt.fname, fname },
                { UserEnt.mname, mname },
                { UserEnt.lname, lname },
                { UserEnt.card_number, cardNo },
                { UserEnt.periodicity, periodicity },
                { UserEnt.term, term },
                { UserEnt.active_subscription, active }
            };
            DB_IO.Update(DB_IO.DB_Interface.pr_update_user, attrValue);
        }

        public static void DeleteUser(string email)
        {
            var attrValue = DB_IO.AttrValue();
            attrValue[UserEnt.email] = email;
            DB_IO.Delete(DB_IO.DB_Interface.pr_delete_user, attrValue);
        }

        public static DataTableReader SelectUserByEmail(string email)
        {
            var attrValue = DB_IO.AttrValue();
            attrValue[UserEnt.email] = email;
            return DB_IO.SelectReader(DB_IO.DB_Interface.pr_select_users, attrValue);
        }

        public static DataTableReader SelectAllUsers()
        {
            return DB_IO.SelectReader(DB_IO.DB_Interface.pr_select_users, DB_IO.AttrValue());
        }


        // ----------------------------------------------------------------------------------------------
        // RECURRENCE -----------------------------------------------------------------------------------
        // ----------------------------------------------------------------------------------------------

        public static DataTableReader SelectAllRecurrences()
        {
            return DB_IO.SelectReader(DB_IO.DB_Interface.pr_select_recurrences, DB_IO.AttrValue());
        }

        public static String SelectRecurrenceById(int id)
        {
            var attrValue = DB_IO.AttrValue();
            attrValue[RecurrenceEnt.periodicity] = id;
            var rdr = DB_IO.SelectReader(DB_IO.DB_Interface.pr_select_recurrences, attrValue);
            rdr.Read();
            return rdr[RecurrenceEnt.designation.ToString()].ToString();
        }

        public static int SelectRecurenceIdbyDesignation(string designation)
        {
            var attrValue = DB_IO.AttrValue();
            attrValue[RecurrenceEnt.designation] = designation;
            Console.WriteLine(DB_IO.SelectScalar(DB_IO.DB_Interface.pr_select_recurrences, attrValue));
            return (int)DB_IO.SelectScalar(DB_IO.DB_Interface.pr_select_recurrence_id, attrValue);
        }


        // ----------------------------------------------------------------------------------------------
        // MONEY ACCOUNTS -------------------------------------------------------------------------------
        // ----------------------------------------------------------------------------------------------

        public static bool ExistsMoneyAccount(int account_id)
        {
            var attrValue = DB_IO.AttrValue();
            attrValue[DB_API.MoneyAccountEnt.account_id] = account_id;
            return DB_IO.Exists(DB_IO.DB_Interface.pr_exists_money_account, attrValue);
        }

        public static void InsertMoneyAccount(string user_email, string account_name, Decimal? balance = null, Decimal? patrimony = null)
        {
            var attrValue = new Dictionary<System.Enum, Object>
            {
                { MoneyAccountEnt.user_email, user_email },
                { MoneyAccountEnt.account_name, account_name },
                { MoneyAccountEnt.balance, balance },
                { MoneyAccountEnt.patrimony, patrimony }
            };
            DB_IO.Insert(DB_IO.DB_Interface.pr_insert_money_account, attrValue);
        }

        public static void DeleteMoneyAccount(int account_id)
        {
            var attrValue = DB_IO.AttrValue();
            attrValue[MoneyAccountEnt.account_id] = account_id;
            DB_IO.Delete(DB_IO.DB_Interface.pr_delete_money_account, attrValue);
        }

        public static DataTableReader SelectMoneyAccountById(int account_id)
        {
            var attrValue = DB_IO.AttrValue();
            attrValue[MoneyAccountEnt.account_id] = account_id;
            return DB_IO.SelectReader(DB_IO.DB_Interface.pr_select_money_accounts, attrValue);
        }

        public static DataTableReader SelectUserMoneyAccounts(string email)
        {
            var attrValue = DB_IO.AttrValue();
            attrValue[MoneyAccountEnt.user_email] = email;
            return DB_IO.SelectReader(DB_IO.DB_Interface.pr_select_user_money_accounts, attrValue);
        }

        public static DataTableReader SelectMoneyAccountUsers(int account_id)
        {
            var attrValue = DB_IO.AttrValue();
            attrValue[MoneyAccountEnt.account_id] = account_id;
            return DB_IO.SelectReader(DB_IO.DB_Interface.pr_select_user_money_accounts, attrValue);
        }

        public static void MoneyAccountAddUser(int account_id, string user_email)
        {
            var attrValue = DB_IO.AttrValue();
            attrValue[MoneyAccountEnt.account_id] = account_id;
            attrValue[MoneyAccountEnt.user_email] = user_email;
            DB_IO.Insert(DB_IO.DB_Interface.pr_money_account_add_user, attrValue);
        }

        public static void MoneyAccountRemoveUser(int account_id, string user_email)
        {
            var attrValue = DB_IO.AttrValue();
            attrValue[MoneyAccountEnt.account_id] = account_id;
            attrValue[MoneyAccountEnt.user_email] = user_email;
            DB_IO.Delete(DB_IO.DB_Interface.pr_money_account_remove_user, attrValue);
        }

        // ----------------------------------------------------------------------------------------------
        // CATEGORIES -----------------------------------------------------------------------------------
        // ----------------------------------------------------------------------------------------------

        public static DataTableReader SelectAccountCategories(int account_id)
        {
            var attrValue = DB_IO.AttrValue();
            attrValue[CategoryEnt.account_id] = account_id;
            return DB_IO.SelectReader(DB_IO.DB_Interface.pr_select_categories, attrValue);
        }

        public static DataTableReader SelectCategory(int category_id)
        {
            var attrValue = DB_IO.AttrValue();
            attrValue[CategoryEnt.category_id] = category_id;
            return DB_IO.SelectReader(DB_IO.DB_Interface.pr_select_categories, attrValue);
        }

        public static void AddCategoryToAccount(int account_id, string name, int type_id)
        {
            var attrValue = new Dictionary<System.Enum, Object>
            {
                { CategoryEnt.account_id, account_id },
                { CategoryEnt.name, name },
                { CategoryEnt.category_type_id, type_id }
            };
            DB_IO.Insert(DB_IO.DB_Interface.pr_insert_category, attrValue);
        }

        public static void AddSubCategoryToAccount(int parentOrSisterCategory_id, int account_id, string name, int category_type)
        {
            var attrValue = new Dictionary<System.Enum, Object>
            {
                { CategoryEnt.category_id, parentOrSisterCategory_id },
                { CategoryEnt.account_id, account_id },
                { CategoryEnt.name, name },
                { CategoryEnt.category_type_id, category_type }
            };
            DB_IO.Insert(DB_IO.DB_Interface.pr_insert_subcategory, attrValue);
        }

        public static void DeleteCategory(int category_id, int account_id)
        {
            var attrValue = new Dictionary<System.Enum, Object>
            {
                { CategoryEnt.category_id, category_id },
                { CategoryEnt.account_id, account_id }
            };
            DB_IO.Insert(DB_IO.DB_Interface.pr_delete_category, attrValue);
        }

        public static int SelectCategoryTypeIdByDesignation(string designation)
        {
            var attrValue = DB_IO.AttrValue();
            attrValue[CategoryTypeEnt.designation] = designation;
            return (int)DB_IO.SelectScalar(DB_IO.DB_Interface.pr_select_category_types, attrValue);
        }

        public static string SelectCategoryTypeDesignationById(int category_type_id)
        {
            var attrValue = DB_IO.AttrValue();
            attrValue[CategoryTypeEnt.category_type_id] = category_type_id;
            return (string)DB_IO.SelectScalar(DB_IO.DB_Interface.pr_select_category_types, attrValue);
        }

        public static DataTableReader SelectAllCategoryTypes()
        {
            return DB_IO.SelectReader(DB_IO.DB_Interface.pr_select_category_types, DB_IO.AttrValue());
        }

        // ----------------------------------------------------------------------------------------------
        // LOANS ----------------------------------------------------------------------------------------
        // ----------------------------------------------------------------------------------------------

        public static DataTableReader SelectAccountLoans(int account_id)
        {
            var attrValue = DB_IO.AttrValue();
            attrValue[CategoryEnt.account_id] = account_id;
            return DB_IO.SelectReader(DB_IO.DB_Interface.pr_select_loans, attrValue);
        }

        public static DataTableReader SelectLoan(int account_id, string loan_name)
        {
            var attrValue = new Dictionary<System.Enum, Object>
            {
                { LoanEnt.account_id, account_id },
                { LoanEnt.name, loan_name }
            };
            return DB_IO.SelectReader(DB_IO.DB_Interface.pr_select_loans, attrValue);
        }

        public static void InsertLoan(int account_id, string name, double amount, double current_debt, DateTime term, double interest)
        {
            var attrValue = new Dictionary<System.Enum, Object>
            {
                { LoanEnt.account_id, account_id },
                { LoanEnt.name, name },
                { LoanEnt.initial_amount, amount },
                { LoanEnt.current_debt, current_debt },
                { LoanEnt.term, term },
                { LoanEnt.interest, interest }
            };
            DB_IO.Insert(DB_IO.DB_Interface.pr_insert_loan, attrValue);
        }

        public static bool ExistsLoan(int account_id, string name)
        {
            var attrValue = new Dictionary<System.Enum, Object>
            {
                { LoanEnt.account_id, account_id },
                { LoanEnt.name, name }
            };
            return DB_IO.Exists(DB_IO.DB_Interface.pr_select_loans, attrValue);
        }

        public static void LoanPayment(int account_id, string name, double payment)
        {
            var attrValue = new Dictionary<System.Enum, Object>
            {
                { LoanEnt.account_id, account_id },
                { LoanEnt.name, name },
                { LoanEnt.payment, payment }
            };
            DB_IO.Update(DB_IO.DB_Interface.pr_loan_payment, attrValue);
        }


        // ----------------------------------------------------------------------------------------------
        // BUDGETS --------------------------------------------------------------------------------------
        // ----------------------------------------------------------------------------------------------

        public static DataTableReader SelectUserCategoryBudgets(int account_id, int category_id)
        {
            var attrValue = new Dictionary<System.Enum, Object>
            {

                { BudgetEnt.account_id, account_id },
                { BudgetEnt.category_id, category_id }
            };
            return DB_IO.SelectReader(DB_IO.DB_Interface.pr_select_budgets_by_category_id, attrValue);
        }

        public static DataTableReader SelectBudget(int budget_id)
        {
            var attrValue = DB_IO.AttrValue();
            attrValue[DB_API.BudgetEnt.budget_id] = budget_id;
            return DB_IO.SelectReader(DB_IO.DB_Interface.pr_select_budgets, attrValue);
        }

        public static void InsertBudget(int account_id, int category_id, double amount, DateTime startDate,
            DateTime endDate)
        {
            var attrValue = new Dictionary<System.Enum, Object>
            {
                { BudgetEnt.account_id, account_id },
                { BudgetEnt.category_id, category_id },
                { BudgetEnt.amount, amount },
                { BudgetEnt.start_date, startDate },
                { BudgetEnt.end_date, endDate },
            };
            DB_IO.Insert(DB_IO.DB_Interface.pr_insert_budget, attrValue);
        }

        // ----------------------------------------------------------------------------------------------
        // GOALS ----------------------------------------------------------------------------------------
        // ----------------------------------------------------------------------------------------------

        public static void InsertGoal(int account_id, int category_id, string name, double amount, DateTime term)
        {
            var attrValue = new Dictionary<System.Enum, Object>
            {
                { GoalEnt.account_id, account_id },
                { GoalEnt.category_id, category_id },
                { GoalEnt.amount, amount },
                { GoalEnt.term, term },
                { GoalEnt.name, name }
            };
            DB_IO.Insert(DB_IO.DB_Interface.pr_insert_goal, attrValue);
        }

        public static DataTableReader SelectAccountGoals(int account_id)
        {
            var attrValue = DB_IO.AttrValue();
            attrValue[DB_API.GoalEnt.account_id] = account_id;
            return DB_IO.SelectReader(DB_IO.DB_Interface.pr_select_goals, attrValue);
        }

        public static DataTableReader SelectGoal(int account_id, string goal_name)
        {
            var attrValue = new Dictionary<System.Enum, Object>
            {
                { GoalEnt.account_id, account_id },
                { GoalEnt.name, goal_name }
            };
            return DB_IO.SelectReader(DB_IO.DB_Interface.pr_select_goals, attrValue);
        }
        
        // ----------------------------------------------------------------------------------------------
        // WALLETS --------------------------------------------------------------------------------------
        // ----------------------------------------------------------------------------------------------

        public static DataTableReader SelectAccountWallets(int account_id)
        {
            var attrValue = DB_IO.AttrValue();
            attrValue[DB_API.WalletEnt.account_id] = account_id;
            return DB_IO.SelectReader(DB_IO.DB_Interface.pr_select_wallets, attrValue);
        }

        public static DataTableReader SelectWallet(int wallet_id)
        {
            var attrValue = DB_IO.AttrValue();
            attrValue[DB_API.WalletEnt.wallet_id] = wallet_id;
            return DB_IO.SelectReader(DB_IO.DB_Interface.pr_select_wallets, attrValue);
        }

        public static DataTableReader SelectWalletByName(int account_id, string name)
        {
            var attrValue = DB_IO.AttrValue();
            attrValue[DB_API.WalletEnt.account_id] = account_id;
            attrValue[DB_API.WalletEnt.name] = name;
            return DB_IO.SelectReader(DB_IO.DB_Interface.pr_select_wallets, attrValue);
        }

        // ----------------------------------------------------------------------------------------------
        // TRANSACTIONS ---------------------------------------------------------------------------------
        // ----------------------------------------------------------------------------------------------

        public static DataTableReader SelectAccountTransactions(int account_id)
        {
            var attrValue = DB_IO.AttrValue();
            attrValue[DB_API.TransactionEnt.account_id] = account_id;
            return DB_IO.SelectReader(DB_IO.DB_Interface.pr_select_transactions, attrValue);
        }

        public static DataTableReader SelectFilteredTransactions(int? account_id = null, int? category_id = null,
            int? wallet_id = null, int? transaction_id = null, int? transaction_type_id = null, double? min_amount = null,
            double? max_amount = null, DateTime? start_date = null, DateTime? end_date = null, string location = null)
        {
            var attrValue = new Dictionary<System.Enum, Object>
            {
                { DB_API.TransactionEnt.account_id, account_id },
                { DB_API.TransactionEnt.category_id, category_id },
                { DB_API.TransactionEnt.from_wallet_id, wallet_id },
                { DB_API.TransactionEnt.transaction_id, transaction_id },
                { DB_API.TransactionEnt.transaction_type_id, transaction_type_id },
                { DB_API.TransactionEnt.min_amount, min_amount },
                { DB_API.TransactionEnt.max_amount, max_amount },
                { DB_API.TransactionEnt.start_date, start_date },
                { DB_API.TransactionEnt.end_date, end_date },
                { DB_API.TransactionEnt.location, location }
            };
            return DB_IO.SelectReader(DB_IO.DB_Interface.pr_select_transactions, attrValue);
        }

        public static void InsertTransaction(int account_id, int category_id, int from_wallet_id, int to_wallet_id,
            int transaction_type_id, double amount, DateTime date, string location, string notes)
        {
            var attrValue = new Dictionary<System.Enum, Object>
            {
                { TransactionEnt.account_id, account_id },
                { TransactionEnt.category_id, category_id },
                { TransactionEnt.from_wallet_id, from_wallet_id },
                { TransactionEnt.to_wallet_id, to_wallet_id },
                { TransactionEnt.transaction_type_id, transaction_type_id },
                { TransactionEnt.amount, amount },
                { TransactionEnt.date, date },
                { TransactionEnt.location, location },
                { TransactionEnt.notes, notes },
            };
            DB_IO.Insert(DB_IO.DB_Interface.pr_insert_transaction, attrValue);
        }

        public static string SelectTransactionTypeNameById(int transaction_tpe_id)
        {
            var attrValue = DB_IO.AttrValue();
            attrValue[TransactionTypeEnt.transaction_type_id] = transaction_tpe_id;
            return (string)DB_IO.SelectScalar(DB_IO.DB_Interface.pr_select_transaction_types, attrValue);
        }

        public static int SelectTransactionTypeIdByName(string designation)
        {
            var attrValue = DB_IO.AttrValue();
            attrValue[TransactionTypeEnt.designation] = designation;
            return (int)DB_IO.SelectScalar(DB_IO.DB_Interface.pr_select_transaction_types, attrValue);
        }

        public static DataTableReader SelectAllTransactionTypes()
        {
            return DB_IO.SelectReader(DB_IO.DB_Interface.pr_select_transaction_types, DB_IO.AttrValue());
        }

        public static void DeleteTransaction(int transaction_id)
        {
            var attrValue = DB_IO.AttrValue();
            attrValue[DB_API.TransactionEnt.transaction_id] = transaction_id;
            DB_IO.Delete(DB_IO.DB_Interface.pr_delete_transaction, attrValue);
        }

        // ----------------------------------------------------------------------------------------------
        // STOCKS ---------------------------------------------------------------------------------------
        // ----------------------------------------------------------------------------------------------

        public static DataTableReader SelectAllAccountPurchasedStocks(int account_id)
        {
            var attrValue = DB_IO.AttrValue();
            attrValue[DB_API.StockEnt.account_id] = account_id;
            return DB_IO.SelectReader(DB_IO.DB_Interface.pr_select_purchased_stocks, attrValue);
        }

        public static void InsertPurchasedStock(int account_id, string company, double purchase_price)
        {
            var attrValue = new Dictionary<System.Enum, Object>
            {
                { StockEnt.account_id, account_id },
                { StockEnt.company, company },
                { StockEnt.purchase_price, purchase_price },
            };
            DB_IO.Insert(DB_IO.DB_Interface.pr_insert_purchased_stock, attrValue);
        }

        public static DataTableReader SelectAllStocks()
        {
            return DB_IO.SelectReader(DB_IO.DB_Interface.pr_select_stocks, DB_IO.AttrValue());
        }

        public static void DeleteStockByTicker(int accountID, int ticker, double purchasePrice, double askPrice)
        {
            var attrValue = DB_IO.AttrValue();
            attrValue[DB_API.StockEnt.account_id] = accountID;
            attrValue[DB_API.StockEnt.ticker] = ticker;
            attrValue[DB_API.StockEnt.purchase_price] = purchasePrice;
            attrValue[DB_API.StockEnt.ask_price] = askPrice;
            DB_IO.Delete(DB_IO.DB_Interface.pr_delete_purchased_stocks, attrValue);
        }

        public static void DeleteStocksByCompany(int accountID, string company, double purchasePrice, double askPrice)
        {
            var attrValue = DB_IO.AttrValue();
            attrValue[DB_API.StockEnt.account_id] = accountID;
            attrValue[DB_API.StockEnt.company] = company;
            attrValue[DB_API.StockEnt.purchase_price] = purchasePrice;
            attrValue[DB_API.StockEnt.ask_price] = askPrice;
            DB_IO.Delete(DB_IO.DB_Interface.pr_delete_purchased_stocks, attrValue);
        }
    }
}
