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
        public BudgetAppHome()
        {
            InitializeComponent();
        }

        private void viewListItem(object sender, RoutedEventArgs e )
        {
            AccountManagement accountManagement = new AccountManagement();
            this.NavigationService.Navigate(accountManagement);
        }
    }
}
