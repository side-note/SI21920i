using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using EF_TP2_52D_14_1920i;
using TypesProject.concrete;
using TypesProject.mapper;

namespace Benchmark
{
    class Benchmarks
    {
        static readonly IContext ADONET = 
            new Context(
                new SqlConnectionStringBuilder().ConnectionString = 
                "server=10.62.73.95;initial catalog=TL52D_14; User Id=TL52D_14; Password=CJN1920i;MultipleActiveResultSets=True"
                );
        static readonly IContext EF = new TL52D_14Entities();
        static void Main()
        {
            BenchmarkRunner.Run<Benchmarks>();
            while (Console.ReadKey() != null);
        }
        [Benchmark]
        static void ADONETFundamentalDataTable()
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                ADONET.FundamentalDataTable("111111111111", new DateTime(2019, 11, 1));
                ts.Complete();
            }
        }
        [Benchmark]
        static void EFFundamentalDataTable()
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                EF.FundamentalDataTable("111111111111", new DateTime(2019, 11, 1));
                ts.Complete();
            }
        }
        [Benchmark]
        static void ADONETPortfolioList()
        {
            ADONET.Portfolio_List("111111111_portfolio");
        }
        [Benchmark]
        static void EFPortfolioList()
        {
            EF.Portfolio_List("111111111_portfolio");
        }
        [Benchmark]
        static void ADONETCreatePortfolio()
        {
            ADONET.createPortfolio(555555555);
        }
        [Benchmark]
        static void EFCreatePortfolio()
        {
            EF.createPortfolio(555555555);
        }
        [Benchmark]
        static void ADONETCreateMarket()
        {
            Market newM = new Market();
            newM.code = 444;
            newM.description = "Description 4";
            newM.name = "Market 4";
            ADONET.CreateMarket(newM);
        }
        [Benchmark]
        static void EFCreateMarket()
        {
            Market newM = new Market();
            newM.code = 444;
            newM.description = "Description 4";
            newM.name = "Market 4";
            EF.CreateMarket(newM);
        }
        [Benchmark]
        static void ADONETDailyValueUpdate()
        {
            ADONET.p_actualizaValorDiario("11111111111", new DateTime(2019, 11, 1, 13, 13, 13));
        }
        [Benchmark]
        static void EFDailyDailyValueUpdate()
        {
            EF.p_actualizaValorDiario("11111111111", new DateTime(2019, 11, 1, 13, 13, 13));
        }
        [Benchmark]
        static void ADONETRemoveMarket()
        {
            ADONET.DeleteMarket(ADONET.Markets.Find(444));
        }
        [Benchmark]
        static void EFRemoveMarket()
        {
            EF.DeleteMarket(EF.Markets.Find(444));
        }
        [Benchmark]
        static void ADONETUpdateMarket()
        {
            Market m = (Market)ADONET.Markets.Find(444);
            m.description = "This is the new description";
            m.name = "IphoneMarket";
            ADONET.UpdateMarket(m);
        }
        [Benchmark]
        static void EFUpdateMarket()
        {
            Market m = (Market)EF.Markets.Find(444);
            m.description = "This is the new description";
            m.name = "IphoneMarket";
            EF.UpdateMarket(m);
        }
        [Benchmark]
        static void ADONETUpdateTotalVal()
        {
            ADONET.UpdateTotalVal("555555555_portfolio", 50000, "111111111111");
        }
        [Benchmark]
        static void EFUpdateTotalVal()
        {
            EF.UpdateTotalVal("555555555_portfolio", 50000, "111111111111");
        }
        [Benchmark]
        static void ADONETRemovePortfolio()
        {
            ADONET.DeletePortfolio(ADONET.Portfolios.Find("555555555_portfolio"));
        }
        [Benchmark]
        static void EFRemovePortfolio()
        {
            EF.DeletePortfolio(EF.Portfolios.Find("555555555_portfolio"));
        }
        [Benchmark]
        static void ADONETAverage()
        {
            ADONET.Average(180, "111111111111");
        }
        [Benchmark]
        static void EFAverage()
        {
            EF.Average(180, "111111111111");
        }
    }
}
