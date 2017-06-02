using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace cgi_omgevingswet.Classes
{
    public static class Database_Init
    {
        /// <summary>
        /// Executes a function that returns a datatable containing the data asked for in the query...
        /// This is for select statements only
        /// </summary>
        /// <param name="query">The query you want to get executed, this query will be a prepared statement</param>
        /// <param name="parameters">The parameters you need to send with the query stored into an object array for the prepared statements, default is null</param>
        /// <returns></returns>
        public static DataTable SQLQueryReader(string query, object[] parameters = null)
        {
            string connectionstring = //@"Server=localhost;Database=test123;Trusted_Connection=True;";
                @"server=84.83.30.240; user id=DatabaseFactory5; password = D$j.UiK4fF;";

            SqlConnection conn = new SqlConnection(connectionstring);
            SqlCommand cmd = conn.CreateCommand();
            SqlDataReader dr = null;
            DataTable dt = new DataTable();

            if (parameters != null)
            {
                if (parameters.Length > 0)
                {
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        if (parameters[i] == null) break;
                        cmd.Parameters.Add(i.ToString(), parameters[i]);
                    }
                }
            }

            try
            {
                //prepare the sql command
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = query;
                cmd.Connection = conn;

                //open connection
                conn.Open();

                dr = cmd.ExecuteReader();

                //execute query and load the results from the query into a variable
                dt.Load(dr);
                //dispose and close the connection
                conn.Dispose();
                conn.Close();
            }
            catch(Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }

            return dt;
        }

        public static String SQLQueryScaler(string query, object[] parameters = null)
        {
            string connectionstring =// @"Server=localhost;Database=test123;Trusted_Connection=True;";
                @"server=84.83.30.240; user id=DatabaseFactory5; password = D$j.UiK4fF;";
            /* @"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=84.83.30.240)(PORT=1521))(CONNECT_DATA=(SID=orcl)));User Id=Omgevingswet;Password=D$j.UiK4fF;";*/

            SqlConnection conn = new SqlConnection(connectionstring);
            SqlCommand cmd = conn.CreateCommand();
            string dr = string.Empty;

            if (parameters != null)
            {
                if (parameters.Length > 0)
                {
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        if (parameters[i] == null) break;
                        cmd.Parameters.Add(i.ToString(), parameters[i]);
                    }
                }
            }

            try
            {
                //prepare the sql command
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = query;
                cmd.Connection = conn;

                //open connection
                conn.Open();

                dr = cmd.ExecuteScalar().ToString();

                //dispose and close the connection
                conn.Dispose();
                conn.Close();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }

            return dr;
        }

        public static void SQLExecProcedure(string procedureName, object[] parameters = null, string[] parameternaam = null)
        {
            string connectionstring =// @"Server=localhost;Database=test123;Trusted_Connection=True;";
                @"server=84.83.30.240; user id=DatabaseFactory5; password = D$j.UiK4fF;";
            /* @"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=84.83.30.240)(PORT=1521))(CONNECT_DATA=(SID=orcl)));User Id=Omgevingswet;Password=D$j.UiK4fF;";*/

            SqlConnection conn = new SqlConnection(connectionstring);

            // 1.  create a command object identifying the stored procedure
            SqlCommand cmd = new SqlCommand(procedureName, conn);

            // 2. set the command object so it knows to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // 3. add parameter to command, which will be passed to the stored procedure
            if (parameters != null)
            {
                if (parameters.Length > 0)
                {
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        if (parameters[i] == null) break;
                        cmd.Parameters.Add(parameternaam[i], parameters[i]);
                    }
                }
            }

            try
            {
                conn.Open();
                //System.NullReferenceException occurs when their is no data/result
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (SqlException ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
        }
    }
}
