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
                Redirect(Constants.HOME, Constants.HOME_ACCOUNT);
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

        private void AddNewBudget(object sender, RoutedEventArgs e)
        {
            BudgetForm budgetForm = new BudgetForm(this.PageName, Constants.HOME_BUDGET);
            this.NavigationService.Navigate(budgetForm);
        }

        private void RemoveBudget(object sender, RoutedEventArgs e)
        {
            if (BudgetList.SelectedItems.Count > 0)
            {

                Budget selectedBudget = (Budget)BudgetList.SelectedItems[0];

                //Verify the account should be deleted
                string message = "Are you sure you want to delete account " + selectedBudget.BudgetName;
                string caption = "Verify account removal.";
                MessageBoxButtons mbb = MessageBoxButtons.YesNo;

                DialogResult result = System.Windows.Forms.MessageBox.Show(message, caption, mbb);
                if (result == DialogResult.Yes)
                {
                    // Remove selected account that has been selected
                    DatabaseAccessor DbAccessor = new DatabaseAccessor();
                    DbAccessor.DeleteBudget(selectedBudget.BudgetId);
                    DbAccessor.CloseConnection();
                }
                Redirect(Constants.HOME, Constants.HOME_BUDGET);
            }
            else
            {
                string message = "Please select a budget to delete.";
                string caption = "No budget selected.";
                MessageBoxButtons mbb = MessageBoxButtons.OK;

                //DialogResult result; 
                System.Windows.Forms.MessageBox.Show(message, caption, mbb);
            }
        }

        private void SelectionChanged (object sender, SelectionChangedEventArgs e)
        {
            // Update the other lists to have correct information to the budget
            //Console.WriteLine("Selection was changed");
        }

        private void ManageBudget(object sender, RoutedEventArgs e)
        {
            if(BudgetList.SelectedItems.Count > 0)
            {
                Budget selectedBudget = (Budget)BudgetList.SelectedItems[0];

                BudgetForm budgetForm = new BudgetForm(this.PageName, Constants.HOME_BUDGET, selectedBudget);
                this.NavigationService.Navigate(budgetForm);
            }
            else
            {
                string message = "Please select a budget to update.";
                string caption = "No budget selected.";
                MessageBoxButtons mbb = MessageBoxButtons.OK;

                //DialogResult result; 
                System.Windows.Forms.MessageBox.Show(message, caption, mbb);
            }
            
        }

        private void ManageCategories(object sender, RoutedEventArgs e)
        {

        }

        private void Redirect(string page, int tabIndex)
        {
            switch (page)
            {
                default:
                    this.NavigationService.Navigate(new BudgetAppHome(tabIndex));
                    break;
            }
        }

        public string PageName { get; set; }

    }
}
