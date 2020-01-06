using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetApp
{
    class BudgetCategory
    {
        public BudgetCategory (int budgetId, string budgetName, int categoryId, string categoryName, int categoryLimit, int currentAmount)
        {
            this.Budget = new Budget(budgetId, budgetName);
            this.Category = new Category(categoryId, categoryName);
            this.CategoryLimit = categoryLimit;
            this.CurrentAmount = currentAmount;
        }

        public Budget Budget { get; set; }
        public Category Category { get; set; }

        public int CategoryLimit { get; set; }
        public int CurrentAmount { get; set; }
    }
}
