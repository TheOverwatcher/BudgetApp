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
            string selectAllQuery = "SELECT * FROM ACCOUNTS";

            using(SqlCommand command = new SqlCommand(selectAllQuery, this.connection))
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
                    Console.WriteLine("Error processing SQL while selecting. Message:" + e.Message + " SQL: " + selectAllQuery);
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

        public void CloseConnection()
        {
            this.connection.Close();
        }
    }
}
