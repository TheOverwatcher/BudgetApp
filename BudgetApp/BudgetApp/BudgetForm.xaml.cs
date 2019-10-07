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
    /// Interaction logic for BudgetForm.xaml
    /// </summary>
    public partial class BudgetForm : Page
    {
        public BudgetForm()
        {
            InitializeComponent();

            DataContext = new BudgetFormViewModel();
        }

        public BudgetForm(string navigatedFrom, int tabIndex)
        {
            InitializeComponent();

            this.NavigateTo = navigatedFrom;
            this.PageName = Constants.BUDGET_FORM;
            this.TabIndex = tabIndex;

            DataContext = new BudgetFormViewModel();
        }

        public BudgetForm(string navigatedFrom, int tabIndex, Budget selectedBudget)
        {
            InitializeComponent();

            this.NavigateTo = navigatedFrom;
            this.PageName = Constants.BUDGET_FORM;
            this.TabIndex = tabIndex;
            this.Budget = selectedBudget;
            this.IsUpdateForm = true;

            DataContext = new BudgetFormViewModel(selectedBudget.BudgetName, selectedBudget.BudgetId);
        }

        public void SaveAction(object sender, RoutedEventArgs e)
        {
            string message = "";
            MessageBoxButtons mbb = MessageBoxButtons.OK;
            string caption = "";

            string budgetName = _budgetName.Text;

            //TODO Budget validation
            //TODO Budget Category association
            //message = ValidateParameters(accountName, accountType, accountCode, groupId, currentBalance);
            if (message.Length > 0)
            {
                caption = "Invalid Budget Information";

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
                    if (this.Budget.BudgetName.Equals(budgetName))
                    {
                        caption = "No changes were made. Returning to Budget Overview.";

                        // No changes were made. Inform the user and then navigate away.
                        DialogResult result = System.Windows.Forms.MessageBox.Show(message, caption, mbb);
                        if (result == DialogResult.OK)
                        {
                            Redirect(this.NavigateTo);
                        }
                    }
                    else
                    {
                        dbaccessor.UpdateBudget(this.Budget.BudgetId, budgetName);
                        caption = "Budget updated sucessfully. Returning to Budget Overview";

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
                    dbaccessor.InsertBudget(budgetName);

                    // Redirect back to page
                    Redirect(this.NavigateTo);
                }
                dbaccessor.CloseConnection();
            }
        }

        public void CancelAction(object sender, RoutedEventArgs e)
        {
            Redirect(this.NavigateTo);
        }

        public void AssociateCategoryToBudget(object sender, RoutedEventArgs e)
        {

        }

        private void DisassociateCategoryToBudget(object sender, RoutedEventArgs e)
        {

        }

        private void Redirect(string page)
        {
            switch (page)
            {
                case Constants.BUDGET_MANAGEMENT:
                    this.NavigationService.Navigate(new BudgetManagement());
                    break;
                case Constants.HOME:
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

        public Budget Budget { get; set; }
    }
}
