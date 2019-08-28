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

        public BudgetAppHome()
        {
            InitializeComponent();
        }

        private void viewListItem(object sender, RoutedEventArgs e )
        {
            if (this.homeSelectBox.SelectedItem.Equals(AccountOverviewLanding))
            {
                AccountManagement accountManagement = new AccountManagement();
                this.NavigationService.Navigate(accountManagement);
            }
            else if (this.homeSelectBox.SelectedItem.ToString() == BudgetOverviewLanding)
            {
                BudgetManagement budgetManagement = new BudgetManagement();
                this.NavigationService.Navigate(budgetManagement);
            }
            else;
            {
                //Provide a nice message saying why the page didn't change
                MessageBox.Show("Cannot navigate to " + this.homeSelectBox.SelectedItem.ToString());
            }
        }
    }
}
