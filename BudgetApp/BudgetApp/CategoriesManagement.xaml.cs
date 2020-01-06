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
    /// Interaction logic for CategoriesManagement.xaml
    /// </summary>
    public partial class CategoriesManagement : Page
    {
        public CategoriesManagement()
        {
            InitializeComponent();

            DataContext = new CategoryManagementViewModel();
        }

        public void AddCategory(object sender, RoutedEventArgs e)
        {
            if (_newCategoryName != null && _newCategoryName.Text.Length > 0)
            {
                // Access the database and update the table
                // Error handling done in class, connection handled in own method
                DatabaseAccessor dbaccessor = new DatabaseAccessor();
                // If we are updating existing info, don't create a new instance
                dbaccessor.InsertCategory(_newCategoryName.Text);

                dbaccessor.CloseConnection();
                Redirect(Constants.CATEGORIES_OVERVIEW);
            }
        }

        public void RemoveCategory(object sender, RoutedEventArgs e)
        {
            if (CategoriesList.SelectedItems.Count > 0)
            {

                Category selectedCategory = (Category)CategoriesList.SelectedItems[0];

                //Verify the account should be deleted
                string message = "Are you sure you want to delete category " + selectedCategory.CategoryName;
                string caption = "Verify category removal.";
                MessageBoxButtons mbb = MessageBoxButtons.YesNo;

                DialogResult result = System.Windows.Forms.MessageBox.Show(message, caption, mbb);
                if (result == DialogResult.Yes)
                {
                    // Remove selected account that has been selected
                    DatabaseAccessor DbAccessor = new DatabaseAccessor();
                    DbAccessor.DeleteCategory(selectedCategory.CategoryId);
                    DbAccessor.CloseConnection();
                }
                Redirect(Constants.CATEGORIES_OVERVIEW);
            }
            else
            {
                string message = "Please select a category to delete.";
                string caption = "No category selected.";
                MessageBoxButtons mbb = MessageBoxButtons.OK;

                //DialogResult result; 
                System.Windows.Forms.MessageBox.Show(message, caption, mbb);
            }
        }

        public void RedirectHome(object sender, RoutedEventArgs e)
        {
            Redirect(Constants.HOME);
        }

        private void Redirect(string page)
        {
            switch (page)
            {
                case Constants.CATEGORIES_OVERVIEW:
                    this.NavigationService.Navigate(new CategoriesManagement());
                    break;
                case Constants.HOME:
                default:
                    this.NavigationService.Navigate(new BudgetAppHome(Constants.HOME_BUDGET));
                    break;
            }
        }
    }
}
