using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.InProcess.Emit;
using EF_TP2_52D_14_1920i;
using TypesProject.concrete;
using TypesProject.mapper;

namespace Benchmark
{
    class BenchmarkMain
    {
        static void Main()
        {
            BenchmarkRunner.Run<Benchmarks>();
            while (Console.ReadKey() != null) ;
        }
    }
    public class BenchmarkConfig : ManualConfig
    {
        public BenchmarkConfig()
        {
            Add(Job.MediumRun
                   .WithLaunchCount(1)
                   .With(InProcessEmitToolchain.Instance)
                   .WithId("InProcess"));
        }
    }

    [RankColumn]
    [Config(typeof(BenchmarkConfig))]
    public class Benchmarks
    {
        public static readonly IContext ADONET =
            new Context(
                new SqlConnectionStringBuilder().ConnectionString =
                "server=10.62.73.95;initial catalog=TL52D_14; User Id=TL52D_14; Password=CJN1920i;MultipleActiveResultSets=True"
                );
        public static readonly IContext EF = new TL52D_14Entities();

        #region EF
        [Benchmark]
        public static void EFFundamentalDataTable()
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                EF.FundamentalDataTable("111111111111", new DateTime(2019, 11, 1));
                ts.Complete();
            }
        }
        [Benchmark]
        public static void EFPortfolioList()
        {
            EF.Portfolio_List("111111111_portfolio");
        }
        [Benchmark]
        public static void EFCreateUpdateAndRemovePortfolio()
        {
            EF.createPortfolio(555555555);

            EF.UpdateTotalVal("555555555_portfolio", 50000, "111111111111");

            EF.DeletePortfolio(EF.Portfolios.Find("555555555_portfolio"));
        }
        [Benchmark]
        public static void EFCreateUpdateAndRemoveMarket()
        {
            EF_TP2_52D_14_1920i.Market newM = new EF_TP2_52D_14_1920i.Market();
            newM.code = 444;
            newM.description = "Description 4";
            newM.name = "Market 4";
            EF.CreateMarket(newM);

            EF_TP2_52D_14_1920i.Market m = (EF_TP2_52D_14_1920i.Market)EF.Markets.Find(444);
            m.description = "This is the new description";
            m.name = "IphoneMarket";
            EF.UpdateMarket(m);

            EF.DeleteMarket(EF.Markets.Find(444));
        }
        [Benchmark]
        public static void EFDailyDailyValueUpdate()
        {
            EF.p_actualizaValorDiario("11111111111", new DateTime(2019, 11, 1, 13, 13, 13));
        }
        [Benchmark]
        public static void EFAverage()
        {
            EF.Average(180, "111111111111");
        }
        #endregion

        #region ADONET
        [Benchmark]
        public static void ADONETPortfolioList()
        {
            ADONET.Portfolio_List("111111111_portfolio");
        }
        [Benchmark]
        public static void ADONETFundamentalDataTable()
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                ADONET.FundamentalDataTable("111111111111", new DateTime(2019, 11, 1));
                ts.Complete();
            }
        }
        [Benchmark]
        public static void ADONETCreateUpdateAndRemovePortfolio()
        {
            ADONET.createPortfolio(555555555);

            ADONET.UpdateTotalVal("555555555_portfolio", 50000, "111111111111");

            ADONET.DeletePortfolio(ADONET.Portfolios.Find("555555555_portfolio"));
        }
        [Benchmark]
        public static void ADONETCreateUpdateAndRemoveMarket()
        {
            TypesProject.model.Market newM = new TypesProject.model.Market();
            newM.code = 444;
            newM.description = "Description 4";
            newM.name = "Market 4";
            ADONET.CreateMarket(newM);

            TypesProject.model.Market m = (TypesProject.model.Market)ADONET.Markets.Find(444);
            m.description = "This is the new description";
            m.name = "IphoneMarket";
            ADONET.UpdateMarket(m);

            ADONET.DeleteMarket(ADONET.Markets.Find(444));
        }
        [Benchmark]
        public static void ADONETDailyValueUpdate()
        {
            ADONET.p_actualizaValorDiario("11111111111", new DateTime(2019, 11, 1, 13, 13, 13));
        }
        [Benchmark]
        public static void ADONETAverage()
        {
            ADONET.Average(180, "111111111111");
        }
        #endregion
    }
}
