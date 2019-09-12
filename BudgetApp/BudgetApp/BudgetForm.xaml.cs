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
    /// Interaction logic for BudgetForm.xaml
    /// </summary>
    public partial class BudgetForm : Page
    {
        public BudgetForm()
        {
            InitializeComponent();

        }

        public BudgetForm(string navigatedFrom, int tabIndex)
        {
            InitializeComponent();

            this.NavigateTo = navigatedFrom;
            this.PageName = Constants.ACCOUNT_FORM;
            this.TabIndex = tabIndex;

            DataContext = new AccountFormViewModel();
        }

        public void SaveAction(object sender, RoutedEventArgs e)
        {

        }

        public void CancelAction(object sender, RoutedEventArgs e)
        {
            Redirect(this.NavigateTo);
        }

        private void Redirect(string page)
        {
            switch (page)
            {
                case Constants.BUDGET_MANAGEMENT:
                    this.NavigationService.Navigate(new BudgetManagement());
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

        private bool _isUpdateForm = false;
        public bool IsUpdateForm
        {
            get { return _isUpdateForm; }
            set { _isUpdateForm = value; }
        }
    }
}
