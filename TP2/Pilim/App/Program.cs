using EF_TP2_52D_14_1920i;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesProject.concrete;
using TypesProject.mapper;
using TypesProject.model;

namespace App
{
    class App
    {
        public static string ctx = null;
        IContext context = null;

        private enum Option
        {
            Unknown = -1,
            Exit,
            UpdateDailyValue,
            CalculateAverage,
            FundamentalDataTable,
            CreatePortfolio,
            UpdateTotalValue,
            PortfolioList,
            InsertMarket,
            UpdateMarket,
            DeleteMarket,
            DeletePortfolio
        }
        private static App __instance;
        private App()
        {
            __dbMethods = new Dictionary<Option, DBMethod>();
            __dbMethods.Add(Option.UpdateDailyValue, p_actualizaValorDiario);
            __dbMethods.Add(Option.CalculateAverage, Average);
            __dbMethods.Add(Option.FundamentalDataTable, FundamentalDataTable);
            __dbMethods.Add(Option.CreatePortfolio, CreatePortfolio);
            __dbMethods.Add(Option.UpdateTotalValue, UpdateTotalVal);
            __dbMethods.Add(Option.PortfolioList, Portfolio_List);
            __dbMethods.Add(Option.InsertMarket, InsertMarket);
            __dbMethods.Add(Option.UpdateMarket, UpdateMarket);
            __dbMethods.Add(Option.DeleteMarket, DeleteMarket);
            __dbMethods.Add(Option.DeletePortfolio, DeletePortfolio);

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
        private Option DisplayMenu()
        {
            Option option = Option.Unknown;
            try
            {
                Console.WriteLine("Select your option:");
                Console.WriteLine();
                Console.WriteLine("1. Update Daily Value");
                Console.WriteLine("2. Calculate Average");
                Console.WriteLine("3. Fundamental Data Table");
                Console.WriteLine("4. Create Portfolio");
                Console.WriteLine("5. Update Total Value");
                Console.WriteLine("6. Portfolio List");
                Console.WriteLine("7. Insert Market");
                Console.WriteLine("8. Update Market");
                Console.WriteLine("9. Delete Market");
                Console.WriteLine("10. Delete Portfolio");
                Console.WriteLine("0. Exit");
                var result = Console.ReadLine();
                option = (Option)Enum.Parse(typeof(Option), result);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Insert valid value." + ex);
                return DisplayMenu();
            }
            return option;
        }

        private delegate void DBMethod();
        private System.Collections.Generic.Dictionary<Option, DBMethod> __dbMethods;
        public string ConnectionString
        {
            get;
            set;
        }

        public void Run()
        {

            if (ctx.Equals("1")) context = new TL52D_14Entities();
            else
            {
                SqlConnectionStringBuilder con = new SqlConnectionStringBuilder();
                con.ConnectionString = "server=10.62.73.95;initial catalog=TL52D_14; User Id=TL52D_14; Password=CJN1920i;MultipleActiveResultSets=True";
                context = new Context(con.ConnectionString);
            }
            using (context)
            {
                Option userInput = Option.Unknown;
                do
                {
                    Console.Clear();
                    userInput = DisplayMenu();
                    Console.Clear();
                    try
                    {
                        __dbMethods[userInput]();
                        Console.ReadKey();
                    }
                    catch (KeyNotFoundException ex)
                    {
                        //Nothing to do. The option was not a valid one. Read another.
                    }

                } while (userInput != Option.Exit);
            }

        }

        private void p_actualizaValorDiario()
        {
            Console.WriteLine("p_actualizaValorDiario()");
            Console.WriteLine();
            Console.WriteLine("Insert an ISIN and a date(yyyy/mm/dd):");
            string str = Console.ReadLine();
            string[] param = str.Split(' ');
            DateTime date = new DateTime();
            try
            {
                date = DateTime.Parse(param[1]);
            }
            catch
            {
                Console.WriteLine("Invalid date");
                return;
            }
            context.p_actualizaValorDiario(param[0], date);
        }
        private void Average()
        {
            Console.WriteLine("Average()");
            Console.WriteLine();
            Console.WriteLine("Insert a number of days and a ISIN:");
            string str = Console.ReadLine();
            string[] param = str.Split(' ');
            decimal avg = context.Average(Int32.Parse(param[0]), param[1]);
            Console.WriteLine(avg);
        }
        private void FundamentalDataTable()
        {
            Console.WriteLine("FundamentalDataTable()");
            Console.WriteLine();
            Console.WriteLine("Insert an ISIN and a date(yyyy/mm/dd):");
            string str = Console.ReadLine();
            string[] param = str.Split(' ');
            DateTime date = new DateTime();
            try
            {
                date = DateTime.Parse(param[1]);
            }
            catch
            {
                Console.WriteLine("Invalid date");
                return;
            }
            InstrumentProxy ip = (InstrumentProxy)context.FundamentalDataTable(param[0], date);

            Console.WriteLine("dailyvar: " + ip.dailyvar);
            Console.WriteLine("currval: " + ip.currval);           
            Console.WriteLine("avg6m: " + ip.avg6m);           
            Console.WriteLine("var6m: " + ip.var6m);           
            Console.WriteLine("dailyvarperc: " + ip.dailyvarperc);           
            Console.WriteLine("var6mperc: " + ip.var6mperc);           

        }
        private void CreatePortfolio()
        {
            Console.WriteLine("CreatePortfolio()");
            Console.WriteLine();
            Console.WriteLine("Insert a nif:");
            context.createPortfolio(Int32.Parse(Console.ReadLine()));
            Console.WriteLine("Potfolio created");
        }
        private void UpdateTotalVal()
        {
            Console.WriteLine("UpdateTotalVal()");
            Console.WriteLine();
            Console.WriteLine("Insert a name, quantity and an ISIN:");
            string str = Console.ReadLine();
            string[] param = str.Split(' ');
            context.UpdateTotalVal(param[0], Int32.Parse(param[1]), param[2]);           

        }
        private void Portfolio_List()
        {
            Console.WriteLine("Portfolio_List()");
            Console.WriteLine();
            Console.WriteLine("Insert a name:");
           

            IEnumerator<IPosition> pf =  context.Portfolio_List(Console.ReadLine()).GetEnumerator();

            while (pf.MoveNext())
            {
                PositionProxy pos = (PositionProxy)pf.Current;
                Console.WriteLine("ISIN " + pos.isin);
                Console.WriteLine("Quantity " + pos.quantity);
                Console.WriteLine("Currval " + pos.CurrVal);
                Console.WriteLine("DailyVarPerc " + pos.Dailyvarperc);                    
            }

        }

        private void InsertMarket()
        {
            Console.WriteLine("InsertMarket()");
            Console.WriteLine();
            Console.WriteLine("Insert a code, description and name:");
            string str = Console.ReadLine();
            string[] param = str.Split(' ');
            IMarket market;
            if (ctx.Equals("1"))
            {
                market = new EF_TP2_52D_14_1920i.Market();
            }
            else
            {
                market = new TypesProject.model.Market();
            }
            market.code = Int32.Parse(param[0]);
            market.description = param[1];
            market.name = param[2];
            context.CreateMarket(market);
            Console.WriteLine("Market created successfully");
        }

        private void UpdateMarket()
        {
            Console.WriteLine("UpdateMarket()");
            Console.WriteLine();
            Console.WriteLine("Insert a code, description and name:");
            string str = Console.ReadLine();
            string[] param = str.Split(' ');
            IMarket market = context.Markets.Find(Int32.Parse(param[0]));
            market.description = param[1];
            market.name = param[2];
            context.UpdateMarket(market);
            Console.WriteLine("Market updated sucessfuly");
        }

        private void DeleteMarket()
        {
            Console.WriteLine("DeleteMarket()");
            Console.WriteLine();
            Console.WriteLine("Insert a code:");
            IMarket market = context.Markets.Find(Int32.Parse(Console.ReadLine()));
            context.DeleteMarket(market);
            Console.WriteLine("Market deleted sucessfully");
        }

        private void DeletePortfolio()
        {
            Console.WriteLine("DeletePortfolio()");
            Console.WriteLine();
            Console.WriteLine("Please insert a name:");
            IPortfolio portfolio = context.Portfolios.Find(Console.ReadLine());
            context.DeletePortfolio(portfolio);
            Console.WriteLine("Portfolio deleted successfully");
        }

    }

    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Which one do you want to test:");
            Console.WriteLine();
            Console.WriteLine("1. Entity Framework");
            Console.WriteLine("2. ADO.NET");
            string result = Console.ReadLine();
            App.ctx = result;
            App.Instance.Run();
        }

    }

}
