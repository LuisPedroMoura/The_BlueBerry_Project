using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueBudget_DB
{
    class Stock
    {

        public int? Ticker { get; }
        public int? StockTypeId { get; }
        public string StockType { get; }
        public string Company { get; }
        public double? AskPrice { get; }
        public double? PurchasePrice { get; }
        public int? AccountId { get; }

        public Stock(int? ticker = null, int? stockTypeId = null, string stockType = null, string company = null, double? askPrice = null,
            double? purchasePrice = null, int? accountId = null)
        {
            Ticker = ticker;
            StockTypeId = stockTypeId;
            StockType = stockType;
            Company = company;
            AskPrice = askPrice;
            PurchasePrice = purchasePrice;
            AccountId = accountId;
        }

    }
}
