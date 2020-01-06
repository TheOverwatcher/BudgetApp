using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetApp
{
    class BudgetAppHomeViewModel : BaseViewModel
    {
        public BudgetAppHomeViewModel ()
        {

        }

        public BudgetAppHomeViewModel (string pageName)
        {
            this.PageName = pageName;
        }

        public BudgetAppHomeViewModel(int budgetId)
        {
            this.AssociatedCategoriesInfo = new DatabaseAccessor().SelectAllCategoryInformationAssociatedToBudget(budgetId);
        }

        ObservableCollection<Account> _accounts = new DatabaseAccessor().SelectAllAccounts();
        public ObservableCollection<Account> Accounts
        {
            get { return _accounts; }
            set { SetProperty(ref _accounts, value); }
        }

        ObservableCollection<Budget> _budgets = new DatabaseAccessor().SelectAllBudgets();
        public ObservableCollection<Budget> Budget
        {
            get { return _budgets; }
            set { SetProperty(ref _budgets, value); }
        }

        ObservableCollection<BudgetCategory> _associatedCategoriesInfo = new ObservableCollection<BudgetCategory>();
        public ObservableCollection<BudgetCategory> AssociatedCategoriesInfo
        {
            get { return _associatedCategoriesInfo; }
            set { SetProperty(ref _associatedCategoriesInfo, value); }
        }
    }


}
