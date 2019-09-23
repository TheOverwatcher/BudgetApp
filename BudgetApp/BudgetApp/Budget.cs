using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetApp
{
    public class Budget
    {
        public Budget(int id, string name)
        {
            this.BudgetId = id;
            this.BudgetName = name;
        }

        public string BudgetName { get; set; }

        public int BudgetId { get; set; }

    }
}
