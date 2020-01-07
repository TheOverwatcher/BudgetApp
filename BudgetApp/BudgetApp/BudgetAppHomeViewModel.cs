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
            AddAccountsFromStoreNoRefresh();
            AddBudgetsFromStoreNoRefresh();
        }

        private void AddAccountsFromStoreNoRefresh()
        {
            if (Accounts == null) Accounts = new ObservableCollection<Account>();
            foreach (Account a in accountDataStore.GetAccounts(false))
            {
                Accounts.Add(a);
            }
        }

        private void AddBudgetsFromStoreNoRefresh()
        {
            if (Budgets == null) Budgets = new ObservableCollection<Budget>();
            foreach (Budget b in budgetDataStore.GetBudgets(false))
            {
                Budgets.Add(b);
            }
        }

        public BudgetAppHomeViewModel (string pageName)
        {
            this.PageName = pageName;
        }

        public BudgetAppHomeViewModel(int budgetId)
        {
            //this.AssociatedCategoriesInfo = new DatabaseAccessor().SelectAllCategoryInformationAssociatedToBudget(budgetId);
            AddAccountsFromStoreNoRefresh();
            AddBudgetsFromStoreNoRefresh();
        }

        private AccountDataStore accountDataStore => AccountDataStore.DataStore;
        public ObservableCollection<Account> Accounts { get; set; }

        private BudgetDataStore budgetDataStore => BudgetDataStore.DataStore;
        public ObservableCollection<Budget> Budgets { get; set; }



        ObservableCollection<BudgetCategory> _associatedCategoriesInfo = new ObservableCollection<BudgetCategory>();
        public ObservableCollection<BudgetCategory> AssociatedCategoriesInfo
        {
            get { return _associatedCategoriesInfo; }
            set { SetProperty(ref _associatedCategoriesInfo, value); }
        }

        private int _currentSelectedBudgetId;

        public int CurrentSelectedBudgetId
        {
            get { return _currentSelectedBudgetId; }
            set { SetProperty(ref _currentSelectedBudgetId, value); }
        }
    }


}
