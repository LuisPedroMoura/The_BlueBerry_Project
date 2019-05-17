using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueBudget_DB
{
    class Transaction
    {
        public int? TransactionID { get; }
        public double Amount { get; }
        public DateTime? Date { get; }
        public string Notes { get; }
        public string Location { get; }
        public int? CategoryID { get; }
        public int? AccountID { get; }
        public int? TransactionTypeID { get; }
        public int? WalletID { get; }

        public Transaction(int? transactionID, double amount, DateTime? date, string notes, string location,
            int? categoryID, int? accountID, int? transactionTypeID, int? WalletID)
        {
            this.TransactionID = transactionID;
            this.Amount = amount;
            this.Date = date;
            this.Notes = notes;
            this.Location = location;
            this.CategoryID = categoryID;
            this.AccountID = accountID;
            this.TransactionTypeID = transactionTypeID;
            this.WalletID = WalletID;
        }

        public override string ToString()
        {
            return "TRANSACTION: id: "+TransactionID+", wallet: "+WalletID+", amount: "+Amount;
        }
    }
}
