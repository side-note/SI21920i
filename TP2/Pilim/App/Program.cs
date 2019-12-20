using EF_TP2_52D_14_1920i;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesProject.mapper;

namespace App
{
    class App
    {
        public string ctx = null;
        private enum Option1
        {
            Unknown = -1,
            Exit,
            EntityFramework,
            ADO
        }

        private enum Option2
        {
            Unknown = -1,
            Exit,
            UpdateDailyValue,
            CalculateAverage,
            FundamentalDataTable,
            CreatePortfolio,
            UpdateTotalValue,
            PortfolioList
        }
        private static App __instance;
        private App()
        {
            __dbOption = new Dictionary<Option1, DBMethod>();
            __dbOption.Add(Option1.EntityFramework, EFMenu);
            __dbOption.Add(Option1.ADO, ADOMenu);

        }      

        public static App Instance
        {
            get
            {
                if (__instance == null)
                    __instance = new App();
                return __instance;
            }
            private set { }
        }

        private void Menu()
        {
            __dbMethods = new Dictionary<Option2, DBMethod>();
            __dbMethods.Add(Option2.UpdateDailyValue, p_actualizaValorDiario);
            __dbMethods.Add(Option2.CalculateAverage, Average);
            __dbMethods.Add(Option2.FundamentalDataTable, FundamentalDataTable);
            __dbMethods.Add(Option2.CreatePortfolio, CreatePortfolio);
            __dbMethods.Add(Option2.UpdateTotalValue, UpdateTotalVal);
            __dbMethods.Add(Option2.PortfolioList, Portfolio_List);
        }
            private Option1 DisplayMenu()
            {
                Option1 option = Option1.Unknown;
                try
                {
                    Console.WriteLine("Which one do you want to test:");               
                    Console.WriteLine();
                    Console.WriteLine("1. Entity Framework");
                    Console.WriteLine("2. ADO.NET");
                    Console.WriteLine("0. Exit");
                    var result = Console.ReadLine();
                    option = (Option1)Enum.Parse(typeof(Option1), result);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("Insert valid value." + ex);
                    return DisplayMenu();
                }
                return option;
            }

            private void DisplayOptions()
            {
                Console.WriteLine("Select your option:");
                Console.WriteLine();
                Console.WriteLine("1. Update Daily Value");
                Console.WriteLine("2. Calculate Average");
                Console.WriteLine("3. Fundamental Data Table");
                Console.WriteLine("4. Create Portfolio");
                Console.WriteLine("5. Update Total Value");
                Console.WriteLine("6. Portfolio List");
                Console.WriteLine("0. Exit");
            }

            private Option2 DisplayMenuEF()
            {
                Option2 option = Option2.Unknown;
                try
                {
                    DisplayOptions();
                    var result = Console.ReadLine();
                    option = (Option2)Enum.Parse(typeof(Option2), result);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("Insert valid value." + ex);
                    return DisplayMenuEF();
                }
                return option;
            }

            private Option2 DisplayMenuADO()
            {
                Option2 option = Option2.Unknown;
                try
                {
                    DisplayOptions();
                    var result = Console.ReadLine();
                    option = (Option2)Enum.Parse(typeof(Option2), result);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("Insert valid value." + ex);
                    return DisplayMenuEF();
                }
                return option;
            }



            private void Login()
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                }

            }
            private delegate void DBMethod();
            private System.Collections.Generic.Dictionary<Option1, DBMethod> __dbOption;
            private System.Collections.Generic.Dictionary<Option2, DBMethod> __dbMethods;
            public string ConnectionString
            {
                get;
                set;
            }

            public void Run()
            {
                IContext context = null;

            if (ctx.Equals("1")) context = new TL52D_14Entities9();
            else
            {
                SqlConnectionStringBuilder con = new SqlConnectionStringBuilder();
                con.ConnectionString = "server=10.62.73.95;initial catalog=TL52D_14; User Id=TL52D_14; Password=CJN1920i;MultipleActiveResultSets=True";
            }
                Option1 userInput = Option1.Unknown;
                do
                {
                    Console.Clear();
                    userInput = DisplayMenu();
                    Console.Clear();
                    try
                    {
                        __dbOption[userInput]();
                        Console.ReadKey();
                        Option2 option = DisplayMenuADO();
                    do
                    {
                        Console.Clear();

                        try
                        {
                            __dbMethods[option]();
                            //Console.Clear();
                        }
                        catch (KeyNotFoundException ex)
                        {

                        }
                    } while (option != Option2.Exit);
                    }
                    catch (KeyNotFoundException ex)
                    {
                        //Nothing to do. The option was not a valid one. Read another.
                    }

                } while (userInput != Option1.Exit);
            }
            #endregion
            private void p_actualizaValorDiario(IContext ctx)
            {
                //TODO: Implement
                Console.WriteLine("p_actualizaValorDiario()");
                
            }
            private void AverageADO()
            {
                //TODO: Implement
                Console.WriteLine(" Average()");
            }
            private void FundamentalDataTable(IContext ctx)
            {
                //TODO: Implement
                Console.WriteLine("FundamentalDataTable()");
            }
            private void CreatePortfolio(IContext ctx)
            {
                //TODO: Implement
                Console.WriteLine("CreatePortfolio()");
            }
            private void UpdateTotalVal(IContext ctx)
            {
                //TODO: Implement
                Console.WriteLine("UpdateTotalVal()");
            }
            private void Portfolio_List(IContext ctx)
            {
                //TODO: Implement
                Console.WriteLine("Portfolio_List()");
            }            
    }
        class MainClass
        {
            #region DO_NOT_CHANGE_NOTHING_IN_THIS_REGION__				
            public static Credentials getCredentials()
            {
                Console.Write("Enter your username: ");
                string username = Console.ReadLine();
                string password = "";
                Console.Write("Enter your password: ");

                ConsoleKeyInfo key;

                do
                {
                    key = Console.ReadKey(true);

                    // Backspace Should Not Work
                    if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                    {
                        password += key.KeyChar;
                        Console.Write("*");
                    }
                    else
                    {
                        if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                        {
                            password = password.Substring(0, (password.Length - 1));
                            Console.Write("\b \b");
                        }
                    }
                }
                while (key.Key != ConsoleKey.Enter);

                return new Credentials(username, password);
            }
            #endregion
            public static void Main(string[] args)
            {
                Credentials cr = getCredentials();
                //TODO: Pass credentials to App
                App.Instance.Run();
            }

        }
    }

}
