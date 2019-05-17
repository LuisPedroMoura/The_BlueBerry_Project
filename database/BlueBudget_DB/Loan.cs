using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueBudget_DB
{
    class Loan
    {

        public string Name { get; }
        public int? AccountID { get; }
        public double? InitialAmount { get; }
        public double? CurrentDebt { get; }
        public DateTime? Term { get; }
        public double? Interest { get; }

        public Loan(string name = null, int? accountID = null, double? initialAmount = null, double? currentDebt = null,
            DateTime? term = null, double? interest = null)
        {
            Name = name;
            AccountID = accountID;
            InitialAmount = initialAmount;
            CurrentDebt = currentDebt;
            Term = term;
            Interest = interest;
        }
    }
}
