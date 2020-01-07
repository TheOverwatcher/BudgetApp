using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetApp
{
    class BudgetDataStore
    {
        public static BudgetDataStore DataStore = new BudgetDataStore();

        public BudgetDataStore()
        {
            databaseAccessor = new DatabaseAccessor();
            budgets = databaseAccessor.SelectAllBudgets();
        }

        public List<Budget> GetBudgets(Boolean refresh)
        {
            if (refresh) budgets = databaseAccessor.SelectAllBudgets();
            return budgets;
        }

        public Budget GetBudgetById(int id, Boolean refresh)
        {
            if(refresh) budgets = databaseAccessor.SelectAllBudgets();
            return budgets.First(b => b.BudgetId == id);
        }

        private DatabaseAccessor databaseAccessor;
        private List<Budget> budgets;

    }
}
