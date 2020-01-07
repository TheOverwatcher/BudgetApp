using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetApp
{
    class CategoryDataStore
    {
        public static CategoryDataStore DataStore = new CategoryDataStore();

        public CategoryDataStore()
        {
            databaseAccessor = new DatabaseAccessor();
            categories = databaseAccessor.SelectAllCategories();
        }

        public List<Category> GetCategories(Boolean refresh)
        {
            if (refresh) categories = databaseAccessor.SelectAllCategories();
            return categories;
        }

        public Category GetAccountById(int id, Boolean refresh)
        {
            if (refresh) categories = databaseAccessor.SelectAllCategories();
            return categories.First(a => a.CategoryId == id);
        }

        private DatabaseAccessor databaseAccessor;
        private List<Category> categories;
    }
}
