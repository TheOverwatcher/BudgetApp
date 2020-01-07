using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Collections.ObjectModel;
using System.Configuration;

namespace BudgetApp
{
    class DatabaseAccessor
    {
        SqlConnection connection;

        public DatabaseAccessor()
        {
            //TODO make data directory configurable
            AppDomain.CurrentDomain.SetData("DataDirectory", "C:\\Users\\grayson\\Documents\\GitHub\\BudgetApp\\BudgetApp\\BudgetApp");//"E:\\Projects\\Visual Studio\\repos\\BudgetApp\\BudgetApp\\BudgetApp\\BudgetApp");

            string connectionString = null;
            ConnectionStringSettingsCollection settings = ConfigurationManager.ConnectionStrings;
            if (settings != null)
            {
                try
                {
                    connectionString = settings[1].ConnectionString; // settings[0] is def
                    this.connection = new SqlConnection(connectionString);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Could not connect to database with connection string: " + settings[1].ConnectionString);
                }
                
            }
            else
            {
                Console.WriteLine("Error finding connection string. Please check that configuration was setup.");
            }
        }

        /* Account Related SQL */

        public void DeleteAccount( int id)
        {
            StringBuilder deleteQuery = new StringBuilder("DELETE FROM ACCOUNTS WHERE ACCOUNT_ID = @Id");

            using(SqlCommand command = new SqlCommand(deleteQuery.ToString(), this.connection))
            {
                
                try
                {
                    this.connection.Open();
                    command.Parameters.Add("@Id", SqlDbType.Int);
                    command.Parameters["@Id"].Value = id;
                    command.ExecuteNonQuery();
                }
                catch (Exception e) // General exception handling
                {
                    Console.WriteLine("Error processing SQL while deleting from Accounts table. Message:" + e.Message + " SQL: " + deleteQuery);
                }
            }
        }

        public void InsertAccount(string name, string code, string type, int groupId, Double currentBalance)
        {
            StringBuilder insertQuery = new StringBuilder("INSERT INTO ACCOUNTS (ACCOUNT_NAME,ACCOUNT_CODE, ACCOUNT_TYPE, ACCOUNT_GROUP_ID, CURRENT_BALANCE, CONDITION)")
                .Append(" VALUES (@Name,@Code,@Type,@GroupId,@Balance,'Open')");

            using (SqlCommand command = new SqlCommand(insertQuery.ToString(), this.connection))
            {
                try
                {
                    this.connection.Open();
                    command.Parameters.Add("@Name", SqlDbType.VarChar);
                    command.Parameters["@Name"].Value = name;
                    command.Parameters.Add("@Code", SqlDbType.VarChar);
                    command.Parameters["@Code"].Value = code;
                    command.Parameters.Add("@Type", SqlDbType.VarChar);
                    command.Parameters["@Type"].Value = type;
                    command.Parameters.Add("@GroupId", SqlDbType.Int);
                    command.Parameters["@GroupId"].Value = groupId;
                    command.Parameters.Add("@Balance", SqlDbType.Float);
                    command.Parameters["@Balance"].Value = currentBalance;
                    command.ExecuteNonQuery();
                }
                catch(Exception e) // General exception handling
                {
                    Console.WriteLine("Error processing SQL while inserting. Message:" + e.Message + " SQL: " + insertQuery);
                }
            }
        }

        public List<Account> SelectAllAccounts()
        {
            StringBuilder selectAllAccountsQuery = new StringBuilder("SELECT * FROM ACCOUNTS");

            using(SqlCommand command = new SqlCommand(selectAllAccountsQuery.ToString(), this.connection))
            {
                List<Account> accountInfo = new List<Account>();
                try
                {
                    this.connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            // account_id,account_code,account_type,account_group_id,current_balance,condition
                            // System.Int32,System.String,System.String,System.String,System.Int32,System.Double,System.String
                            Account acc = new Account(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4), reader.GetDouble(5), reader.GetString(6));
                            accountInfo.Add(acc);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No entries found for Accounts");
                    }
                    reader.Close();

                }
                catch (Exception e) // General Exception handling
                {
                    Console.WriteLine("Error processing SQL while selecting accounts. Message:" + e.Message + " SQL: " + selectAllAccountsQuery);
                }

                return accountInfo;
            }

        }

        public void UpdateAccount(int accountId, string name, string code, string type, int groupId, Double currentBalance)
        {
            StringBuilder updateAccountQuery = new StringBuilder("UPDATE ACCOUNTS SET ACCOUNT_NAME = @Name, ACCOUNT_CODE = @Code, ACCOUNT_TYPE = @Type, ACCOUNT_GROUP_ID = @GroupId, CURRENT_BALANCE = @Balance WHERE ACCOUNT_ID = @Id");

            using (SqlCommand command = new SqlCommand(updateAccountQuery.ToString(), this.connection))
            {
                try
                {
                    this.connection.Open();
                    command.Parameters.Add("@Name", SqlDbType.VarChar);
                    command.Parameters["@Name"].Value = name;
                    command.Parameters.Add("@Code", SqlDbType.VarChar);
                    command.Parameters["@Code"].Value = code;
                    command.Parameters.Add("@Type", SqlDbType.VarChar);
                    command.Parameters["@Type"].Value = type;
                    command.Parameters.Add("@GroupId", SqlDbType.Int);
                    command.Parameters["@GroupId"].Value = groupId;
                    command.Parameters.Add("@Balance", SqlDbType.Float);
                    command.Parameters["@Balance"].Value = currentBalance;
                    command.Parameters.Add("@Id", SqlDbType.Int);
                    command.Parameters["@Id"].Value = accountId;
                    command.ExecuteNonQuery();
                }
                catch (Exception e) // General Exception handling
                {
                    Console.WriteLine("Error processing SQL while updating account " + accountId + ". Message:" + e.Message + " SQL: " + updateAccountQuery);
                }
            }

        }

        /* Budget Related SQL */

        public void InsertBudget(string name)
        {
            StringBuilder insertBudgetQuery = new StringBuilder("INSERT INTO BUDGET (BUDGET_NAME) VALUES (@Name)");

            using (SqlCommand command = new SqlCommand(insertBudgetQuery.ToString(), this.connection))
            {
                try
                {
                    this.connection.Open();
                    command.Parameters.Add("@Name", SqlDbType.VarChar);
                    command.Parameters["@Name"].Value = name;
                    command.ExecuteNonQuery();
                }
                catch(Exception e)
                {
                    Console.WriteLine("Error processing SQL while inserting budget " + name + ". Message:" + e.Message + " SQL: " + insertBudgetQuery);
                }
            }
        }

        public void DeleteBudget(int budgetId)
        {
            StringBuilder deleteBudgetQuery = new StringBuilder("DELETE FROM BUDGET WHERE BUDGET_ID = @Id");

            using (SqlCommand command = new SqlCommand(deleteBudgetQuery.ToString(), this.connection))
            {
                try
                {
                    this.connection.Open();
                    command.Parameters.Add("@Id", SqlDbType.VarChar);
                    command.Parameters["@Id"].Value = budgetId;
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error processing SQL while deleting budget " + budgetId + ". Message:" + e.Message + " SQL: " + deleteBudgetQuery);
                }
            }
        }

        public void UpdateBudget(int budgetId, string name)
        {
            StringBuilder updateBudgetQuery = new StringBuilder("UPDATE BUDGET SET BUDGET_NAME = @Name WHERE BUDGET_ID = @Id");

            using (SqlCommand command = new SqlCommand(updateBudgetQuery.ToString(), this.connection))
            {
                try
                {
                    this.connection.Open();
                    command.Parameters.Add("@Name", SqlDbType.VarChar);
                    command.Parameters["@Name"].Value = name;
                    command.Parameters.Add("@Id", SqlDbType.VarChar);
                    command.Parameters["@Id"].Value = budgetId;
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error processing SQL while updating budget " + budgetId + ". Message:" + e.Message + " SQL: " + updateBudgetQuery);
                }
            }
        }

        public List<Budget> SelectAllBudgets()
        {
            StringBuilder selectAllBudgetsQuery = new StringBuilder("SELECT * FROM BUDGET");

            using (SqlCommand command = new SqlCommand(selectAllBudgetsQuery.ToString(), this.connection))
            {
                List<Budget> budgetInfo = new List<Budget>();
                try
                {
                    this.connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Budget budget = new Budget(reader.GetInt32(0), reader.GetString(1));
                            budgetInfo.Add(budget);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No entries found for Budgets");
                    }
                    reader.Close();

                }
                catch (Exception e) // General Exception handling
                {
                    Console.WriteLine("Error processing SQL while selecting budgets. Message:" + e.Message + " SQL: " + selectAllBudgetsQuery);
                }

                return budgetInfo;
            }
        }

        public void InsertCategory(string name)
        {
            StringBuilder insertCategoryQuery = new StringBuilder("INSERT INTO CATEGORY (CATEGORY_NAME) VALUES (@Name)");

            using (SqlCommand command = new SqlCommand(insertCategoryQuery.ToString(), this.connection))
            {
                try
                {
                    this.connection.Open();
                    command.Parameters.Add("@Name", SqlDbType.VarChar);
                    command.Parameters["@Name"].Value = name;
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error processing SQL while inserting budget " + name + ". Message:" + e.Message + " SQL: " + insertCategoryQuery);
                }
            }
        }

        public void DeleteCategory(int categoryId)
        {
            StringBuilder deleteCategoryQuery = new StringBuilder("DELETE FROM CATEGORY WHERE CATEGORY_ID = @Id");

            using (SqlCommand command = new SqlCommand(deleteCategoryQuery.ToString(), this.connection))
            {
                try
                {
                    this.connection.Open();
                    command.Parameters.Add("@Id", SqlDbType.VarChar);
                    command.Parameters["@Id"].Value = categoryId;
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error processing SQL while deleting category " + categoryId + ". Message:" + e.Message + " SQL: " + deleteCategoryQuery);
                }
            }
        }

        public void UpdateCategory(int categoryId, string name)
        {
            StringBuilder updateCategoryQuery = new StringBuilder("UPDATE CATEGROY SET CATEGORY_NAME = @Name WHERE CATEGORY_ID = @Id");

            using (SqlCommand command = new SqlCommand(updateCategoryQuery.ToString(), this.connection))
            {
                try
                {
                    this.connection.Open();
                    command.Parameters.Add("@Name", SqlDbType.VarChar);
                    command.Parameters["@Name"].Value = name;
                    command.Parameters.Add("@Id", SqlDbType.VarChar);
                    command.Parameters["@Id"].Value = categoryId;
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error processing SQL while updating category " + categoryId + ". Message:" + e.Message + " SQL: " + updateCategoryQuery);
                }
            }
        }

        public List<Category> SelectAllCategories()
        {
            StringBuilder selectAllCategoriesQuery = new StringBuilder("SELECT * FROM CATEGORY");

            using (SqlCommand command = new SqlCommand(selectAllCategoriesQuery.ToString(), this.connection))
            {
                List<Category> categoryInfo = new List<Category>();
                try
                {
                    this.connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Category category = new Category(reader.GetInt32(0), reader.GetString(1));
                            categoryInfo.Add(category);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No entries found for Categories");
                    }
                    reader.Close();

                }
                catch (Exception e) // General Exception handling
                {
                    Console.WriteLine("Error processing SQL while selecting categories. Message:" + e.Message + " SQL: " + selectAllCategoriesQuery);
                }

                return categoryInfo;
            }
        }

        public List<Category> SelectAllCategoriesAssociatedToBudget(int budgetId)
        {
            StringBuilder selectAllCategoriesQuery = new StringBuilder("SELECT * FROM CATEGORY C LEFT OUTER JOIN BUDGET_CATEGORY_REL BCR ON BCR.BUDGET_ID = @BudgetId WHERE BCR.CATEGORY_ID = C.CATEGORY_ID");
                
            using (SqlCommand command = new SqlCommand(selectAllCategoriesQuery.ToString(), this.connection))
            {
                List<Category> categoryInfo = new List<Category>();
                try
                {
                    this.connection.Open();
                    command.Parameters.Add("@BudgetId", SqlDbType.VarChar);
                    command.Parameters["@BudgetId"].Value = budgetId;
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Category category = new Category(reader.GetInt32(0), reader.GetString(1));
                            categoryInfo.Add(category);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No entries found for categories associated to budget " + budgetId);
                    }
                    reader.Close();

                }
                catch (Exception e) // General Exception handling
                {
                    Console.WriteLine("Error processing SQL while selecting categories for budget " + budgetId + ". Message:" + e.Message + " SQL: " + selectAllCategoriesQuery);
                }

                return categoryInfo;
            }
        }

        public List<Category> SelectAllCategoriesDisassociatedToBudget(int budgetId)
        {
            StringBuilder selectAllCategoriesQuery = new StringBuilder("SELECT * FROM CATEGORY C LEFT OUTER JOIN BUDGET_CATEGORY_REL BCR ON BCR.BUDGET_ID = 3 AND BCR.category_id = C.category_id WHERE BCR.category_id IS NULL");

            using (SqlCommand command = new SqlCommand(selectAllCategoriesQuery.ToString(), this.connection))
            {
                List<Category> categoryInfo = new List<Category>();
                try
                {
                    this.connection.Open();
                    command.Parameters.Add("@BudgetId", SqlDbType.VarChar);
                    command.Parameters["@BudgetId"].Value = budgetId;
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Category category = new Category(reader.GetInt32(0), reader.GetString(1));
                            categoryInfo.Add(category);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No entries found for categories associated to budget " + budgetId);
                    }
                    reader.Close();

                }
                catch (Exception e) // General Exception handling
                {
                    Console.WriteLine("Error processing SQL while selecting categories for budget " + budgetId + ". Message:" + e.Message + " SQL: " + selectAllCategoriesQuery);
                }

                return categoryInfo;
            }
        }

        public void AssociateCategoryToBudget(int budgetId, int categoryId)
        {
            StringBuilder insertBudgetCategoryRelQuery = new StringBuilder("INSERT INTO BUDGET_CATEGORY_REL (BUDGET_ID, CATEGORY_ID) VALUES (@BudgetId, @CategoryId)");

            using (SqlCommand command = new SqlCommand(insertBudgetCategoryRelQuery.ToString(), this.connection))
            {
                try
                {
                    this.connection.Open();
                    command.Parameters.Add("@BudgetId", SqlDbType.Int);
                    command.Parameters["@BudgetId"].Value = budgetId;
                    command.Parameters.Add("@CategoryId", SqlDbType.Int);
                    command.Parameters["@CategoryId"].Value = categoryId;
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error processing SQL while associating category " + categoryId + "to budget " + budgetId + ". Message:" + e.Message + " SQL: " + insertBudgetCategoryRelQuery);
                }
            }
        }

        public void DisassociateCategoryToBudget(int budgetId, int categoryId)
        {
            StringBuilder deleteBudgetCategoryRelQuery = new StringBuilder("DELETE FROM BUDGET_CATEGORY_REL WHERE BUDGET_ID = @BudgetId AND CATEGORY_ID = @CategoryId");

            using (SqlCommand command = new SqlCommand(deleteBudgetCategoryRelQuery.ToString(), this.connection))
            {
                try
                {
                    this.connection.Open();
                    command.Parameters.Add("@BudgetId", SqlDbType.Int);
                    command.Parameters["@BudgetId"].Value = budgetId;
                    command.Parameters.Add("@CategoryId", SqlDbType.Int);
                    command.Parameters["@CategoryId"].Value = categoryId;
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error processing SQL while deleting category " + categoryId + ". Message:" + e.Message + " SQL: " + deleteBudgetCategoryRelQuery);
                }
            }
        }

        public int CountBudgetCategoryRel(int budgetId)
        {
            StringBuilder countBudgetCategoryRelQuery = new StringBuilder("SELECT COUNT(*) FROM BUDGET_CATEGORY_REL WHERE BUDGET_ID = @BudgetId");

            using (SqlCommand command = new SqlCommand(countBudgetCategoryRelQuery.ToString(), this.connection))
            {
                int count = 0;
                try
                {
                    this.connection.Open();
                    command.Parameters.Add("@BudgetId", SqlDbType.Int);
                    command.Parameters["@BudgetId"].Value = budgetId;
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            count = reader.GetInt32(0);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Budget-Category relations exist for budget " + budgetId);
                    }
                    reader.Close();

                }
                catch (Exception e)
                {
                    Console.WriteLine("Error processing SQL while counting records in budget_category_rel. Message:" + e.Message + " SQL: " + countBudgetCategoryRelQuery);
                }
                return count;
            }
        }

        public List<BudgetCategory> SelectAllCategoryInformationAssociatedToBudget(int budgetId)
        {
            StringBuilder selectAllCategoriesQuery = new StringBuilder("SELECT BCR.budget_id, B.budget_name, BCR.category_id, C.category_name, BCR.amount_limit, BCR.current_amount FROM CATEGORY C ")
                .Append("LEFT OUTER JOIN BUDGET_CATEGORY_REL BCR ON BCR.BUDGET_ID = @BudgetId LEFT OUTER JOIN BUDGET B on B.budget_id = @BudgetId WHERE BCR.CATEGORY_ID = C.CATEGORY_ID");

            using (SqlCommand command = new SqlCommand(selectAllCategoriesQuery.ToString(), this.connection))
            {
                List<BudgetCategory> budgetCategoryInfo = new List<BudgetCategory>();
                try
                {
                    this.connection.Open();
                    command.Parameters.Add("@BudgetId", SqlDbType.VarChar);
                    command.Parameters["@BudgetId"].Value = budgetId;
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            BudgetCategory category = new BudgetCategory(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3), reader.GetInt64(4), reader.GetInt64(5));
                            budgetCategoryInfo.Add(category);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No entries found for categories associated to budget " + budgetId);
                    }
                    reader.Close();

                }
                catch (Exception e) // General Exception handling
                {
                    Console.WriteLine("Error processing SQL while selecting categories for budget " + budgetId + ". Message:" + e.Message + " SQL: " + selectAllCategoriesQuery);
                }

                return budgetCategoryInfo;
            }
        }

        public void CloseConnection()
        {
            this.connection.Close();
        }
    }
}
