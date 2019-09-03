using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
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
        private const string AccountOverviewLanding = "Account Overview";
        private const string BudgetOverviewLanding = "Budget Overview";

        private string pageName = Constants.HOME;

        public BudgetAppHome()
        {
            InitializeComponent();

            PopulateData();
        }

        public BudgetAppHome(int selectedIndex)
        {
            InitializeComponent();

            MainTabControl.SelectedIndex = selectedIndex;

            PopulateData();
        }

        private void PopulateData() {
            // Populate the data presented on each tab for the home page
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
            AccountForm accountForm = new AccountForm(this.pageName, Constants.HOME_ACCOUNT);
            this.NavigationService.Navigate(accountForm);
        }

        private void AddNewAccountGroup(object sender, RoutedEventArgs e)
        {
            // Make account group form to navigate
        }

        private void RemoveAccount(object sender, RoutedEventArgs e)
        {
            // Remove selected account that has been selected
        }

        private void RemoveAccountGroup(object sender, RoutedEventArgs e)
        {
            // Remove selected account group that has been selected, update the accounts
            // If deleting this entry causes the FKs of Accounts to fail, don't allow the delete.
        }

        public string GetPageName()
        {
            return this.pageName;
        }
    }
}
