using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BudgetApp
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class AccountForm : Page
    {
        public AccountForm()
        {
            InitializeComponent();

            //default navigation
            this.NavigateTo = Constants.HOME;
            this.PageName = Constants.ACCOUNT_FORM;
            this.TabIndex = 0;

            DataContext = new AccountFormViewModel();
        }

        // setup form with reference from where we navigated
        public AccountForm(string navigatedFrom, int tabIndex)
        {
            InitializeComponent();

            this.NavigateTo = navigatedFrom;
            this.PageName = Constants.ACCOUNT_FORM;
            this.TabIndex = tabIndex;

            DataContext = new AccountFormViewModel();
        }

        public AccountForm(string navigatedFrom, int tabIndex, Account accountToUpdate)
        {
            InitializeComponent();

            this.NavigateTo = navigatedFrom;
            this.PageName = Constants.ACCOUNT_FORM;
            this.TabIndex = tabIndex;

            DataContext = new AccountFormViewModel();

            if (accountToUpdate != null)
            {
                //TODO update DataContext with current information
            }
        }

        private void SaveAction(object sender, RoutedEventArgs e)
        {
            /*
             * Validate parameters
             * name = string
             * code = 4 characters
             * type = Checking or Savings (C or S)
             * group can be null
             */
            //string name = _accountName.Text;
            string codeString = _accountCode.Text;
            string type = _accountType.Text;
            int groupId = -1;
            if(int.TryParse(_accountGroup.Text, out groupId))
            {

            }

            int currentBalance;
            if(int.TryParse(_balance.Text, out currentBalance))
            {

            }


            if(codeString.Length == Constants.CODE_LENGTH && (type.Equals(Constants.TYPE_CHECKING) || type.Equals(Constants.TYPE_SAVINGS)) && currentBalance >= 0)
            {
                string code = codeString.ToUpper();

                // Access the database and update the table
                // Error handling done in class, connection handled in own method
                DatabaseAccessor dbaccessor = new DatabaseAccessor();
                dbaccessor.InsertAccount(code, type, groupId, currentBalance);
                dbaccessor.CloseConnection();


                // Redirect back to page
                Redirect(this.NavigateTo);
            }
            else
            {
                // Say the code should be 4 characters
                // Or type should be checking/savings

                //TODO better error messages

                string message = "Please provide valid input";
                string caption = "Invalid code or type";
                MessageBoxButtons mbb = MessageBoxButtons.OK; // 0 is for OK

                //DialogResult result; 
                System.Windows.Forms.MessageBox.Show(message, caption, mbb);
            }

            
        }

        private void CancelAction(object sender, RoutedEventArgs e)
        {
            // don't save the data, redirect home
            Redirect(this.NavigateTo);
        }

        private void Redirect(string page)
        {
            switch (page)
            {
                case Constants.ACCOUNT_MANAGEMENT:
                    this.NavigationService.Navigate(new AccountManagement());
                    break;
                case Constants.HOME: //TODO take into account tab on home page
                default:
                    this.NavigationService.Navigate(new BudgetAppHome(this.TabIndex));
                    break;
            }
        }

        public string NavigateTo { get; set; }

        public string PageName { get; set; }

        public int TabIndex { get; set; }

    }
}
