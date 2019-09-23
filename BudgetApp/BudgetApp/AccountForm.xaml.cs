using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using static BudgetApp.AccountFormViewModel;

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
            this.IsUpdateForm = true;
            this.Account = accountToUpdate;

            DataContext = new AccountFormViewModel(accountToUpdate.AccountName,accountToUpdate.AccountType, accountToUpdate.AccountCode, accountToUpdate.Balance.ToString(), accountToUpdate.AccountGroupId.ToString());

        }

        private void SaveAction(object sender, RoutedEventArgs e)
        {
            string message = "";
            MessageBoxButtons mbb = MessageBoxButtons.OK;
            string caption = "";

            string accountName = _accountName.Text;
            string accountCode = _accountCode.Text.ToUpper(); ;
            string accountType = _accountType.Text;
            int groupId = -1;
            int.TryParse(_accountGroup.Text, out groupId);
            
            //TODO currency format
            float currentBalance;
            float.TryParse(_balance.Text, out currentBalance);



            message = ValidateParameters(accountName, accountType, accountCode, groupId, currentBalance);
            if (message.Length > 0)
            {
                caption = "Invalid Account Information";

                System.Windows.Forms.MessageBox.Show(message, caption, mbb);
            }
            else
            {
                // Access the database and update the table
                // Error handling done in class, connection handled in own method
                DatabaseAccessor dbaccessor = new DatabaseAccessor();
                // If we are updating existing info, don't create a new instance
                if (this.IsUpdateForm)
                {
                    //Check if values changed. If not don't update
                    if (this.Account.AccountName.Equals(accountName)
                        && this.Account.AccountCode.Equals(accountCode)
                        && this.Account.AccountType.Equals(new AccountFormViewModel().convertStringToAccountType(accountType))
                        && this.Account.AccountGroupId.Equals(groupId)
                        && this.Account.Balance.Equals(currentBalance))
                    {
                        caption = "No changes were made. Returning to Account Overview.";

                        // No changes were made. Inform the user and then navigate away.
                        DialogResult result = System.Windows.Forms.MessageBox.Show(message, caption, mbb);
                        if (result == DialogResult.OK)
                        {
                            Redirect(this.NavigateTo);
                        }
                    }
                    else
                    {
                        dbaccessor.UpdateAccount(this.Account.AccountId, accountName, accountCode, accountType, groupId, currentBalance);
                        caption = "Account updated sucessfully. Returning to Account Overview";

                        // No changes were made. Inform the user and then navigate away.
                        DialogResult result = System.Windows.Forms.MessageBox.Show(message, caption, mbb);
                        if (result == DialogResult.OK)
                        {
                            Redirect(this.NavigateTo);
                        }
                    }
                    
                }
                else
                {
                    dbaccessor.InsertAccount(accountName, accountCode, accountType, groupId, currentBalance);

                    // Redirect back to page
                    Redirect(this.NavigateTo);
                }
                dbaccessor.CloseConnection();
            }

        }

        private string ValidateParameters(string name, string type, string code, int groupId, float balance)
        {
            if (name == null || name.Length <= 0) return "Name required for account.";

            if (!type.Equals(Constants.TYPE_CHECKING) && !type.Equals(Constants.TYPE_SAVINGS)) return "Type of account must be Checking or Savings";

            if (code.Length != Constants.CODE_LENGTH) return "Account Code must be 4 characters"; //TODO Check that code doesn't already exist

            if (groupId < 0) return "Group Id must be greater than 0";

            if (balance < 0) return "Balance must be greater than 0";


            return "";
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
                case Constants.HOME:
                default:
                    this.NavigationService.Navigate(new BudgetAppHome(this.TabIndex));
                    break;
            }
        }

        //TODO redefine properties
        public string NavigateTo { get; set; }

        public string PageName { get; set; }

        public int TabIndex { get; set; }

        private bool _isUpdateForm = false;
        public bool IsUpdateForm
        {
            get { return _isUpdateForm; }
            set { _isUpdateForm = value;  }
        }

        public Account Account { get; set;}
    }
}
