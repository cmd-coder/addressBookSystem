using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace addressBook
{
    public class HandleDatabase
    {
        private static SqlConnection ConnectionSetup()
        {
            return new SqlConnection(@"Data Source=DESKTOP-DKOUJ1R\SQLEXPRESS;Initial Catalog=address_book_service;Integrated Security=True");
        }

        public static int StoreInDataBase(Dictionary<string, List<ContactClass>> addressDict)
        {
            int num = 0;
            List<string> tableNames = GetAllTheTables();
            foreach(KeyValuePair<string,List<ContactClass>> item in addressDict)
            {
                string name = item.Key;
                List<ContactClass> list = item.Value;
                if (tableNames.Contains(name))
                    continue;
                string query = "create table "+name+" (first varchar(25),last varchar(25),address varchar(25),city varchar(25),state varchar(25),zip int,phone varchar(25),email varchar(25), start_date date);";
                SqlConnection sqlConnection = ConnectionSetup();
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                try
                {
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                    //Console.WriteLine("Table Created Successfully");
                    num+=InsertIntoDataBase(list, name);
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Error Generated In StoreInDataBase. Details: " + e.ToString());
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
            return num;
        }

        public static int InsertIntoDataBase(List<ContactClass> list, string name)
        {
            int num = 0;
            foreach (var item in list)
            {
                string query = "INSERT INTO "+ name +" (first,last,address,city,state,zip,phone,email, start_date) VALUES('" + item.First +
                  "','" + item.Last + "','" + item.Address + "','" + item.City + "','" + item.State + "'," + item.Zip + ",'" + item.Phone + "','" + item.Email + "','"+item.DateAdded+"');";

                SqlConnection sqlConnection = ConnectionSetup();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                try
                {
                    sqlConnection.Open();
                    num+=cmd.ExecuteNonQuery();
                    //Console.WriteLine("Records Inserted Successfully");
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Error Generated In InsertIntoDataBase. Details: " + e.ToString());
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
            return num;
        }

        public static void RetrieveFromDataBase(Dictionary<string, List<ContactClass>> addressDict)
        {
            List<string> tablesList = GetAllTheTables();
            foreach (var item in tablesList)
            {
                SqlConnection sqlConnection = new SqlConnection();
                try
                {
                    List<ContactClass> list = new List<ContactClass>();
                    using (sqlConnection)
                    {
                        string query = "Select * from " + item+";";
                        SqlCommand cmd = new SqlCommand(query, sqlConnection);
                        sqlConnection.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            System.Console.WriteLine("First Name -- Last Name -- Address -- City -- State -- Zip -- Phone -- Email -- Date Added");
                            while (dr.Read())
                            {
                                ContactClass contactClass = new ContactClass();
                                contactClass.First = !dr.IsDBNull(0) ? dr.GetString(1) : "NA"; ;
                                contactClass.Last = !dr.IsDBNull(1) ? dr.GetString(1) : "NA";
                                contactClass.Address = !dr.IsDBNull(2) ? dr.GetString(2) : "NA"; ;
                                contactClass.City = !dr.IsDBNull(3) ? dr.GetString(3) : "NA";
                                contactClass.State = !dr.IsDBNull(4) ? dr.GetString(4) : "NA";
                                contactClass.Zip = !dr.IsDBNull(5) ? dr.GetInt32(5) : 0;
                                contactClass.Phone = !dr.IsDBNull(6) ? dr.GetString(6) : "NA";
                                contactClass.Email = !dr.IsDBNull(7) ? dr.GetString(7) : "NA";
                                contactClass.DateAdded = !dr.IsDBNull(8) ? dr.GetDateTime(8) : Convert.ToDateTime("01/01/0001");
                                list.Add(contactClass);
                            }
                            addressDict.Add(item, list);
                        }
                        else
                        {
                            System.Console.WriteLine("No data found");
                        }
                    }
                }
                catch (Exception e)
                {
                    System.Console.WriteLine(e.Message);
                }
            }
        }

        public static List<string> GetAllTheTables()
        {
            List<string> tablesList = new List<string>();
            SqlConnection connection = ConnectionSetup();
            DataTable tables = new DataTable("Tables");
            using (connection)
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "select table_name as Name from INFORMATION_SCHEMA.Tables where TABLE_TYPE = 'BASE TABLE'";
                connection.Open();
                //tables.Load(command.ExecuteReader(CommandBehavior.CloseConnection));
                SqlDataReader sqlDataReader = command.ExecuteReader();
                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                        tablesList.Add(sqlDataReader.GetString(0));
                }
            }
            //foreach(var i in tablesList)
            //  Console.WriteLine(i);
            return tablesList;
        }

        public static void DeleteAllTables()
        {
            List<string> list = GetAllTheTables();
            foreach(var item in list)
            {
                SqlConnection con = ConnectionSetup();
                String query = "drop table " + item + ";"; 

                SqlCommand cmd = new SqlCommand(query, con);
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //Console.WriteLine("Table Deleted Successfully");
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Error Generated. Details: " + e.ToString());
                }
                finally
                {
                    con.Close();
                    //Console.ReadKey();
                }
            }
        }

    }
}
