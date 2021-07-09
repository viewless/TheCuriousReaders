using SpecFlowTemplate.Utils;
using System;
using TechTalk.SpecFlow;

namespace SpecFlowTemplate.Hooks
{
    [Binding]
    internal class DBHooks
    {
        private static string isInit;

        [BeforeTestRun]
        public static void initDBConn()
        {
            Console.WriteLine("Initializing the DB Connection");
            DBConnection.Init();
        }

        [AfterTestRun]
        public static void closeDBConn()
        {
            Console.WriteLine("Closing the DB Connection");
            DBConnection.Close();
        }
    }
}