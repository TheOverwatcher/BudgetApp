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

        ObservableCollection<Account> _accounts = new DatabaseAccessor().SelectAllAccounts();
        public ObservableCollection<Account> Accounts
        {
            get { return _accounts; }
            set { SetProperty(ref _accounts, value); }
        }
    }
}
