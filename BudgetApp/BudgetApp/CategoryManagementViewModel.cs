using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetApp
{
    class CategoryManagementViewModel : BaseViewModel
    {
        public CategoryManagementViewModel()
        {
            AddCategoriesFromStoreNoRefresh();
        }

        public CategoryManagementViewModel(string name, int id)
        {
            CategoryName = name;
            CategoryId = id;
            AddCategoriesFromStoreNoRefresh();
        }

        private void AddCategoriesFromStoreNoRefresh()
        {
            if (Categories == null) Categories = new ObservableCollection<Category>();
            foreach (Category c in categoryDataStore.GetCategories(false))
            {
                Categories.Add(c);
            }
        }

        private string _categoryName = "Example Category Name";
        public string CategoryName
        {
            get { return _categoryName; }
            set { SetProperty(ref _categoryName, value); }
        }

        private int _categoryId = -1;
        public int CategoryId
        {
            get { return _categoryId; }
            set { SetProperty(ref _categoryId, value); }
        }

        private CategoryDataStore categoryDataStore => CategoryDataStore.DataStore;
        public ObservableCollection<Category> Categories { get; set; }
    }
}
