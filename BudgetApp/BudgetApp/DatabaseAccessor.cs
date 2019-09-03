using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace BudgetApp
{
    class DatabaseAccessor
    {
        SqlConnection connection;

        public DatabaseAccessor()
        {
            this.connection = new SqlConnection(Constants.CONNECTION_STRING);
        }

        public void InsertAccount(string code, string type, int groupId, Double currentBalance)
        {
            string insertQuery = "INSERT INTO ACCOUNTS (ACCOUNT_CODE, ACCOUNT_TYPE, ACCOUNT_GROUP_ID, CURRENT_BALANCE, CONDITION)"
                 + " VALUES (@Code,@Type,@GroupId,@Balance,'Open');";

            using (SqlCommand command = new SqlCommand(insertQuery, this.connection))
            {
                this.connection.Open();
                try
                {
                    //command.Parameters.Add("@Name", SqlDbType.VarChar);
                    //command.Parameters["@Name"].Value = name;
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

        public ArrayList SelectAllAccounts()
        {
            string selectAllQuery = "SELECT * FROM ACCOUNTS";

            using(SqlCommand command = new SqlCommand(selectAllQuery, this.connection))
            {
                ArrayList accountInfo = new ArrayList();
                this.connection.Open();
                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            //TODO debug the reader columns
                            Console.WriteLine("Types: " + reader.GetFieldType(0) + "," + reader.GetFieldType(1) + "," + reader.GetFieldType(2) + "," + reader.GetFieldType(3) + "," + reader.GetFieldType(4) + "," + reader.GetFieldType(5) + ",");// + reader.GetFieldType(6));
                            Account acc = new Account(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4), reader.GetFloat(5), reader.GetString(6));
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

        public void CloseConnection()
        {
            this.connection.Close();
        }
    }
}
