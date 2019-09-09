using System;
using System.Collections;
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
    /// Interaction logic for BudgetAppHome.xaml
    /// </summary>
    public partial class BudgetAppHome : Page
    {
        public BudgetAppHome()
        {
            InitializeComponent();

            DataContext = new BudgetAppHomeViewModel();

            this.PageName = Constants.HOME;
        }

        public BudgetAppHome(int selectedIndex)
        {
            InitializeComponent();

            DataContext = new BudgetAppHomeViewModel();

            MainTabControl.SelectedIndex = selectedIndex;
            this.PageName = Constants.HOME;

        }

        // Event handlers for buttons
        private void GoToBudgetPage(object sender, RoutedEventArgs e)
        {
            BudgetManagement budgetManagement = new BudgetManagement();
            this.NavigationService.Navigate(budgetManagement);
        }

        private void GoToAccountPage(object sender, RoutedEventArgs e)
        {
            AccountManagement accountManagement = new AccountManagement();
            this.NavigationService.Navigate(accountManagement);
        }

        private void AddNewAccount(object sender, RoutedEventArgs e)
        {
            AccountForm accountForm = new AccountForm(this.PageName, Constants.HOME_ACCOUNT);
            this.NavigationService.Navigate(accountForm);
        }

        private void AddNewAccountGroup(object sender, RoutedEventArgs e)
        {
            // Make account group form to navigate
        }

        private void RemoveAccount(object sender, RoutedEventArgs e)
        {
            if (AccountsList.SelectedItems.Count > 0)
            {

                Account selectedAccount = (Account)AccountsList.SelectedItems[0];

                //Verify the account should be deleted
                string message = "Are you sure you want to delete account " + selectedAccount.AccountCode;
                string caption = "Verify account removal.";
                MessageBoxButtons mbb = MessageBoxButtons.YesNo;

                DialogResult result = System.Windows.Forms.MessageBox.Show(message, caption, mbb);
                if (result == DialogResult.Yes)
                {
                    // Remove selected account that has been selected
                    DatabaseAccessor DbAccessor = new DatabaseAccessor();
                    DbAccessor.DeleteAccount(selectedAccount.AccountId);
                    DbAccessor.CloseConnection();
                }
            }
            else
            {
                string message = "Please select an account to delete.";
                string caption = "No account selected.";
                MessageBoxButtons mbb = MessageBoxButtons.OK;

                //DialogResult result; 
                System.Windows.Forms.MessageBox.Show(message, caption, mbb);
            }
            
        }

        private void UpdateSelectedAccount(object sender, RoutedEventArgs e)
        {
            if (AccountsList.SelectedItems.Count > 0)
            {
                // Get account selected
                Account selectedAccount = (Account)AccountsList.SelectedItems[0];

                AccountForm accountForm = new AccountForm(this.PageName, Constants.HOME_ACCOUNT, selectedAccount);
                this.NavigationService.Navigate(accountForm);
            }
            else
            {
                string message = "Please select an account to update.";
                string caption = "No account selected.";
                MessageBoxButtons mbb = MessageBoxButtons.OK;

                //DialogResult result; 
                System.Windows.Forms.MessageBox.Show(message, caption, mbb);
            }

        }

        private void RemoveAccountGroup(object sender, RoutedEventArgs e)
        {
            // Remove selected account group that has been selected, update the accounts
            // If deleting this entry causes the FKs of Accounts to fail, don't allow the delete.
        }

        public string PageName { get; set; }

    }
}
