using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetApp
{
    class Account
    {

        public Account(int id, string name, string code, string type, int groupId, float balance, string condition)
        {
            this.AccountId = id;
            this.AccountName =  name;
            this.AccountCode = code;
            this.AccountType = type;
            this.AccountGroupId = groupId;
            this.Balance = balance;
            this.Condition = condition;
        }

        public int AccountId { get; set; }

        public string AccountName { get; set; }

        public string AccountCode { get; set; }

        public string AccountType { get; set; }

        public int AccountGroupId { get; set; }

        public float Balance { get; set; }

        public string Condition { get; set; }
    }
}
