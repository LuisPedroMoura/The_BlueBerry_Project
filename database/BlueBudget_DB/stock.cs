using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueBudget_DB
{
    class Stock
    {

        private int? Id { get; set; }
        private int? StockTypeId { get; set; }
        private string Company { get; set; }
        private double? AskPrice { get; set; }
        private double? BidPrice { get; set; }
        private double? PurchasedPrice { get; set; }
        private int? AccountId { get; set; }

        public Stock(int? id = null, int? stockTypeId = null, string company = null, double? askPrice = null,
            double? bidPrice = null, double? purchasedPrice = null, int? accountId = null)
        {
            Id = id;
            StockTypeId = stockTypeId;
            Company = company;
            AskPrice = askPrice;
            BidPrice = bidPrice;
            PurchasedPrice = purchasedPrice;
            AccountId = accountId;
        }

    }
}
