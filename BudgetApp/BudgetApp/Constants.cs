using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetApp
{
    class Constants
    {
        // Page name constants
        public const string HOME = "Home";
        public const string ACCOUNT_MANAGEMENT = "Account Management";
        public const string ACCOUNT_FORM = "Account Form";
        public const string BUDGET_MANAGEMENT = "Budget Management";

        // Tab name constants
        public const string ACCOUNT_OVERVIEW = "Account Overview";
        public const string BUDGET_OVERVIEW = "Budget Overview";


        //TODO modularize path
        //public const string CONNECTION_STRING = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\grayson\\Documents\\GitHub\\BudgetApp\\BudgetApp\\BudgetApp\\BudgetAppDatabase.mdf;Integrated Security=True";
        public const string CONNECTION_STRING = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"E:\\Projects\\Visual Studio\\repos\\BudgetApp\\BudgetApp\\BudgetApp\\BudgetApp\\BudgetAppDatabase.mdf\";Integrated Security = True";

        // Home tab indexes
        public const int HOME_BUDGET = 1;
        public const int HOME_ACCOUNT = 2;

        // Miscellaneous 
        public const string TYPE_CHECKING = "Checking";
        public const string TYPE_SAVINGS = "Savings";
        public const char CHECKING = 'C';
        public const char SAVINGS = 'S';
        public const int CODE_LENGTH = 4;
    }
}
