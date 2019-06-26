using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueBudget_DB
{
    class User
    {
        public string UserName { get; }
        public string Email { get; }
        public string Fname { get; }
        public string Mname { get; }
        public string Lname { get; }
        public int? Periodicity { get; }
        public int? ActiveSubscription { get; }
        public DateTime? Term { get; }

        public User(string userName = null, string email = null, string fname = null, string mname = null,
            string lname = null, int? periodicity = null, int? activeSubscription = null, DateTime? term = null)
        {
            this.UserName = userName;
            this.Email = email;
            this.Fname = fname;
            this.Mname = mname;
            this.Lname = lname;
            this.Periodicity = periodicity;
            this.ActiveSubscription = activeSubscription;
            this.Term = term;
        }
    }
}
