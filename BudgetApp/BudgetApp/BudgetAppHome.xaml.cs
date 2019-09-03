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
        }

        public BudgetAppHome(int selectedIndex)
        {
            InitializeComponent();

            MainTabControl.SelectedIndex = selectedIndex;
        }

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

        public string GetPageName()
        {
            return this.pageName;
        }
    }
}
