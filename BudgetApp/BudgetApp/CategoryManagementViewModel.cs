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

        }

        public CategoryManagementViewModel(string name, int id)
        {
            this.CategoryName = name;
            this.CategoryId = id;
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

        ObservableCollection<Category> _categories = new DatabaseAccessor().SelectAllCategories();
        public ObservableCollection<Category> Category
        {
            get { return _categories; }
            set { SetProperty(ref _categories, value); }
        }
    }
}
