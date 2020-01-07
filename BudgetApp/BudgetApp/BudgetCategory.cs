using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetApp
{
    class BudgetCategory
    {
        public BudgetCategory (int budgetId, string budgetName, int categoryId, string categoryName, long categoryLimit, long currentAmount)
        {
            this.Budget = new Budget(budgetId, budgetName);
            this.Category = new Category(categoryId, categoryName);
            this.CategoryLimit = categoryLimit;
            this.CurrentAmount = currentAmount;
        }

        public Budget Budget { get; set; }
        public Category Category { get; set; }

        public long CategoryLimit { get; set; }
        public long CurrentAmount { get; set; }
    }
}
