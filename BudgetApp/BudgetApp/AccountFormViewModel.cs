using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetApp
{
    class AccountFormViewModel : BaseViewModel
    {
        private string _accountType = "Checking";
        public string AccountType
        {
            get { return _accountType; }
            set { SetProperty(ref _accountType, value); }
        }

        private enum _accountTypes { Checking = 0, Savings = 1 };
        public Array AccountTypes
        {
            get { return Enum.GetValues(typeof(_accountTypes)); }
        }

        private string _accountName = "Example Name";
        public string AccountName
        {
            get { return _accountName; }
            set { SetProperty(ref _accountName, value); }
        }
        private string _accountCode = "EXPL";
        public string AccountCode
        {
            get { return _accountCode; }
            set { SetProperty(ref _accountCode, value); }
        }
        private string _balance = "0";
        public string Balance
        {
            get { return _balance; }
            set { SetProperty(ref _balance, value); }
        }
        private string _accountGroup = "0";
        public string AccountGroup
        {
            get { return _accountGroup; }
            set { SetProperty(ref _accountGroup, value); }
        }
    }
}
