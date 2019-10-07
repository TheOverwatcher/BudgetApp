using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetApp
{
    class BudgetFormViewModel : BaseViewModel
    {
        public BudgetFormViewModel()
        {

        }

        public BudgetFormViewModel(string name, int id)
        {
            this.BudgetName = name;
            this.BudgetId = id;
            DatabaseAccessor DbAccessor = new DatabaseAccessor();
            this._associatedCategories = DbAccessor.SelectAllCategoriesAssociatedToBudget(this.BudgetId);
            DbAccessor.CloseConnection();
            this._availableCategories = DbAccessor.SelectAllCategoriesDisassociatedToBudget(this.BudgetId);
            DbAccessor.CloseConnection();
        }

        private string _budgetName = "Example Name";
        public string BudgetName
        {
            get { return _budgetName; }
            set { SetProperty(ref _budgetName, value); }
        }

        private int _budgetId = -1;
        public int BudgetId
        {
            get { return _budgetId; }
            set { SetProperty(ref _budgetId, value); }
        }

        ObservableCollection<Category> _associatedCategories = new ObservableCollection<Category>();
        public ObservableCollection<Category> AssociatedCatgories
        {
            get { return _associatedCategories; }
            set { SetProperty(ref _associatedCategories, value); }
        }

        ObservableCollection<Category> _availableCategories = new ObservableCollection<Category>();
        public ObservableCollection<Category> AvailableCategories
        {
            get { return _availableCategories; }
            set { SetProperty(ref _availableCategories, value); }
        }

    }
}
