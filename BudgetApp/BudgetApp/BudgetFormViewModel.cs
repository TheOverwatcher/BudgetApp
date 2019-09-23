using System;
using System.Collections.Generic;
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

        public BudgetFormViewModel(string name)
        {
            this.BudgetName = name;
        }

        private string _budgetName = "Example Name";
        public string BudgetName
        {
            get { return _budgetName; }
            set { SetProperty(ref _budgetName, value); }
        }
    }
}
