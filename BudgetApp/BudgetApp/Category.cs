using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetApp
{
    class Category
    {
        public Category(int id, string name)
        {
            this.CategoryId = id;
            this.CategoryName = name;
        }

        public string CategoryName { get; set; }

        public int CategoryId { get; set; }

    }
}
