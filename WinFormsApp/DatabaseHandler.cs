using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp
{
    public class DatabaseHandler
    {
        private static readonly string ConnectionString = "Driver={Microsoft Access Driver (*.mdb, *.accdb)};"
                                                            + "Dbq=c:\\Data\\Database1.mdb;";

        public static void StoreData(int thread, string data)
        {
            string queryString = "INSERT INTO Table1 (ThreadID, [Time], Data) VALUES (?, ?, ?)";

            using (OdbcConnection connection = new OdbcConnection(ConnectionString))
            {
                OdbcCommand command = new OdbcCommand(queryString, connection);
                command.Parameters.AddWithValue("?", thread);
                command.Parameters.AddWithValue("?", DateTime.Now);
                command.Parameters.AddWithValue("?", data);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }
        }

        public static List<Tuple<int, string>> FetchLatestData()
        {
            string queryString = "SELECT * FROM (SELECT TOP 20 ID, ThreadID, Data FROM Table1 ORDER BY ID DESC) ORDER BY ID";

            using OdbcConnection connection = new OdbcConnection(ConnectionString);
            OdbcCommand command = new OdbcCommand(queryString, connection);
            var data = new List<Tuple<int, string>>();
            try
            {
                connection.Open();
                OdbcDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    data.Add(new Tuple<int, string>((int)reader[1], (string)reader[2]));
                }
                reader.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            return data;
        }
    }
}
