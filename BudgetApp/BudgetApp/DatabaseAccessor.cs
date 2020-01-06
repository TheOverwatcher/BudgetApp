﻿using System;
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
            AppDomain.CurrentDomain.SetData("DataDirectory", "E:\\Projects\\Visual Studio\\repos\\BudgetApp\\BudgetApp\\BudgetApp\\BudgetApp");

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
            string deleteQuery = "DELETE FROM ACCOUNTS WHERE ACCOUNT_ID = @Id";

            using(SqlCommand command = new SqlCommand(deleteQuery, this.connection))
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
            string insertQuery = "INSERT INTO ACCOUNTS (ACCOUNT_NAME,ACCOUNT_CODE, ACCOUNT_TYPE, ACCOUNT_GROUP_ID, CURRENT_BALANCE, CONDITION)"
                 + " VALUES (@Name,@Code,@Type,@GroupId,@Balance,'Open')";

            using (SqlCommand command = new SqlCommand(insertQuery, this.connection))
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

        public ObservableCollection<Account> SelectAllAccounts()
        {
            string selectAllAccountsQuery = "SELECT * FROM ACCOUNTS";

            using(SqlCommand command = new SqlCommand(selectAllAccountsQuery, this.connection))
            {
                ObservableCollection<Account> accountInfo = new ObservableCollection<Account>();
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
            string updateAccountQuery = "UPDATE ACCOUNTS SET ACCOUNT_NAME = @Name, ACCOUNT_CODE = @Code, ACCOUNT_TYPE = @Type, ACCOUNT_GROUP_ID = @GroupId, CURRENT_BALANCE = @Balance WHERE ACCOUNT_ID = @Id";

            using (SqlCommand command = new SqlCommand(updateAccountQuery, this.connection))
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
            string insertBudgetQuery = "INSERT INTO BUDGET (BUDGET_NAME) VALUES (@Name)";

            using (SqlCommand command = new SqlCommand(insertBudgetQuery, this.connection))
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
            string deleteBudgetQuery = "DELETE FROM BUDGET WHERE BUDGET_ID = @Id";

            using (SqlCommand command = new SqlCommand(deleteBudgetQuery, this.connection))
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
            string updateBudgetQuery = "UPDATE BUDGET SET BUDGET_NAME = @Name WHERE BUDGET_ID = @Id";

            using (SqlCommand command = new SqlCommand(updateBudgetQuery, this.connection))
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

        public ObservableCollection<Budget> SelectAllBudgets()
        {
            string selectAllBudgetsQuery = "SELECT * FROM BUDGET";

            using (SqlCommand command = new SqlCommand(selectAllBudgetsQuery, this.connection))
            {
                ObservableCollection<Budget> budgetInfo = new ObservableCollection<Budget>();
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
            string insertCategoryQuery = "INSERT INTO CATEGORY (CATEGORY_NAME) VALUES (@Name)";

            using (SqlCommand command = new SqlCommand(insertCategoryQuery, this.connection))
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
            string deleteCategoryQuery = "DELETE FROM CATEGORY WHERE CATEGORY_ID = @Id";

            using (SqlCommand command = new SqlCommand(deleteCategoryQuery, this.connection))
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
            string updateCategoryQuery = "UPDATE CATEGROY SET CATEGORY_NAME = @Name WHERE CATEGORY_ID = @Id";

            using (SqlCommand command = new SqlCommand(updateCategoryQuery, this.connection))
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

        public ObservableCollection<Category> SelectAllCategories()
        {
            string selectAllCategoriesQuery = "SELECT * FROM CATEGORY";

            using (SqlCommand command = new SqlCommand(selectAllCategoriesQuery, this.connection))
            {
                ObservableCollection<Category> categoryInfo = new ObservableCollection<Category>();
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

        public ObservableCollection<Category> SelectAllCategoriesAssociatedToBudget(int budgetId, int recordCount)
        {
            string selectAllCategoriesQuery = "SELECT * FROM CATEGORY C LEFT OUTER JOIN BUDGET_CATEGORY_REL BCR ON BCR.BUDGET_ID = @BudgetId";
                
            if(recordCount > 0)
                selectAllCategoriesQuery.Concat(" WHERE BCR.CATEGORY_ID = C.CATEGORY_ID");

            using (SqlCommand command = new SqlCommand(selectAllCategoriesQuery, this.connection))
            {
                ObservableCollection<Category> categoryInfo = new ObservableCollection<Category>();
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

        public ObservableCollection<Category> SelectAllCategoriesDisassociatedToBudget(int budgetId, int recordCount)
        {
            string selectAllCategoriesQuery = "SELECT * FROM CATEGORY C LEFT OUTER JOIN BUDGET_CATEGORY_REL BCR ON BCR.BUDGET_ID = @BudgetId";

            if (recordCount > 0)
                selectAllCategoriesQuery.Concat(" WHERE BCR.CATEGORY_ID <> C.CATEGORY_ID");

            using (SqlCommand command = new SqlCommand(selectAllCategoriesQuery, this.connection))
            {
                ObservableCollection<Category> categoryInfo = new ObservableCollection<Category>();
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
            string insertBudgetCategoryRelQuery = "INSERT INTO BUDGET_CATEGORY_REL (BUDGET_ID, CATEGORY_ID) VALUES (@BudgetId, @CategoryId)";

            using (SqlCommand command = new SqlCommand(insertBudgetCategoryRelQuery, this.connection))
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
            string deleteBudgetCategoryRelQuery = "DELETE FROM BUDGET_CATEGORY_REL WHERE BUDGET_ID = @BudgetId AND CATEGORY_ID = @CategoryId";

            using (SqlCommand command = new SqlCommand(deleteBudgetCategoryRelQuery, this.connection))
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

        public int CountBudgetCategoryRel()
        {
            string countBudgetCategoryRelQuery = "SELECT COUNT(*) FROM BUDGET_CATEGORY_REL";

            using (SqlCommand command = new SqlCommand(countBudgetCategoryRelQuery, this.connection))
            {
                int count = 0;
                try
                {
                    this.connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    count = reader.GetInt32(0);

                }
                catch (Exception e)
                {
                    Console.WriteLine("Error processing SQL while counting records in budget_category_rel. Message:" + e.Message + " SQL: " + countBudgetCategoryRelQuery);
                }
                return count;
            }
        }

        public void CloseConnection()
        {
            this.connection.Close();
        }
    }
}
