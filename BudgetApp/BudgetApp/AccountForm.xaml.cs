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
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class AccountForm : Page
    {
        private string navigateTo;
        private string pageName = Constants.ACCOUNT_FORM;
        public AccountForm()
        {
            InitializeComponent();

            //default navigation
            this.navigateTo = Constants.HOME;
        }

        // setup form with reference from where we navigated
        public AccountForm(string navigatedFrom)
        {
            InitializeComponent();

            this.navigateTo = navigatedFrom;
        }

        private void SaveAction(object sender, RoutedEventArgs e)
        {
            // Update data set

            // Redirect back to page
            Redirect(this.navigateTo);
        }

        private void CancelAction(object sender, RoutedEventArgs e)
        {
            // don't save the data, redirect home
            Redirect(this.navigateTo);
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
                    this.NavigationService.Navigate(new BudgetAppHome());
                    break;
            }
        }

        public string GetNavigateTo()
        {
            return navigateTo;
        }

        public void SetNavigateTo(string navigateTo)
        {
            this.navigateTo = navigateTo;
        }

        public string GetPageName()
        {
            return this.pageName;
        }
    }
}
