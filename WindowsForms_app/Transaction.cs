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
        public int? RecipientWalletID { get; }

        public Transaction(int? transactionID, double amount, DateTime? date, string notes, string location,
            int? categoryID, int? accountID, int? transactionTypeID, int? walletID, int? recipientWalletID)
        {
            TransactionID = transactionID;
            Amount = amount;
            Date = date;
            Notes = notes;
            Location = location;
            CategoryID = categoryID;
            AccountID = accountID;
            TransactionTypeID = transactionTypeID;
            WalletID = walletID;
            RecipientWalletID = recipientWalletID;
        }

        public override string ToString()
        {
            return "TRANSACTION: id: "+TransactionID+", wallet: "+WalletID+", amount: "+Amount;
        }
    }
}
