using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BudgetApp
{
    public abstract class BaseForm : Page
    {
        public abstract string NavigateTo { get; set; }

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
