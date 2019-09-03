using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

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
                connection.Open();
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

        public void SelectAllAccounts()
        {
            string selectAllQuery = "SELECT * FROM ACCOUNTS";

            using(SqlCommand command = new SqlCommand(selectAllQuery, this.connection))
            {
                try
                {
                    // TODO determine which execute is needed to select all data, update return value
                    command.ExecuteReader();
                }
                catch(Exception e) // General Exception handling
                {
                    Console.WriteLine("Error processing SQL while selecting. Message:" + e.Message + " SQL: " + selectAllQuery);
                }
            }

        }

        public void CloseConnection()
        {
            this.connection.Close();
        }
    }
}
