using MySql.Data.MySqlClient;
using System;
using System.Runtime.CompilerServices;

namespace SpecFlowTemplate.Utils
{
    public static class DBConnection
    {
        private static MySqlConnection _cnn;
        private const string CONNETION_STRING = "Server=.\\SQLEXPRESS;Port=44385;Database=CuriousReadersContext;Uid=sfd;Pwd=P@ssw0rd01;";

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Init()
        {
            if (_cnn is null)
            {
                _cnn = new MySqlConnection(CONNETION_STRING);
                try
                {
                    Console.WriteLine("Connecting to DB");
                    _cnn.Open();
                    Console.WriteLine("Connected to DB");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Problem connecting to DB");
                    Console.WriteLine(ex.Message);
                }
            }
        }

        internal static void Close()
        {
            _cnn.Dispose();
        }

        public static void InsertRecord(String sqlQuery)
        {
            MySqlCommand command; 
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            command = new MySqlCommand(sqlQuery, _cnn);
            adapter.InsertCommand = command;

            Console.WriteLine($"Inserting in to DB with SQL: {sqlQuery}");
            if (adapter.InsertCommand.ExecuteNonQuery() < 1)
            {
                Console.WriteLine("Nothing Has been inserted...");
            }

            command.Dispose();
        }
    }
}