using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Transactions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypesProject.concrete;
using TypesProject.mapper;
using TypesProject.model;

namespace Test
{
    [TestClass]
    public class TestAdo
    {
        public Context ctx()
        {
            SqlConnectionStringBuilder con = new SqlConnectionStringBuilder();
            con.ConnectionString = "server=10.62.73.95;initial catalog=TL52D_14; User Id=TL52D_14; Password=CJN1920i;MultipleActiveResultSets=True";
            Context ctx = new Context(con.ConnectionString);
            return ctx;
        }
        [TestMethod]
        public void createPortfolioTest()
        {
            ctx().createPortfolio(555555555);
            IPortfolio P = ctx().Portfolios.Find("555555555_portfolio");
            Assert.AreEqual(P.name, "555555555_portfolio");
           
        }

        [TestMethod]
        public void p_actualizavalordiarioTest()
        {
            DateTime d = new DateTime(2019, 11, 1, 13, 13, 13);
            ctx().p_actualizaValorDiario("11111111111", d);
            IDailyReg dr = ctx().DailyRegs.Find("111111111111", d.Date);
            Assert.AreEqual(dr.minval, 11);
            Assert.AreEqual(dr.maxval, 1111);
            Assert.AreEqual(dr.openingval, 11111);
            Assert.AreEqual(dr.closingval, 111);
        }

        [TestMethod]
        public void AverageTest()
        {
            decimal d = ctx().Average(180, "111111111111");
            Assert.AreEqual(d, 222);
        }

        [TestMethod]

        public void FundamentalDataTableTest()
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                DateTime d = new DateTime(2019, 11, 1);
                InstrumentProxy i = (InstrumentProxy)ctx().FundamentalDataTable("111111111111", d);
                ts.Complete();
                Assert.AreEqual(i.avg6m, 222);
                Assert.AreEqual(i.currval, 111);
                Assert.AreEqual(i.dailyvar, 1100);
                Assert.AreEqual(i.dailyvarperc, 100);
                Assert.AreEqual(i.var6m, 4433);
                Assert.AreEqual(i.var6mperc, 403);
                
            }

        }

        [TestMethod]

        public void portfolio_listTest()
        {
            IEnumerable<IPosition> portfolioList = ctx().Portfolio_List("111111111_portfolio");
            IEnumerator<IPosition> plEnumerator = portfolioList.GetEnumerator();
            plEnumerator.MoveNext();
            PositionProxy p = (PositionProxy)plEnumerator.Current;
            Assert.AreEqual(p.isin, "111111111111");
            Assert.AreEqual(p.CurrVal, 3333);
            Assert.AreEqual(p.Dailyvarperc, 90);
            Assert.AreEqual(p.quantity, 1);
            plEnumerator.MoveNext();
            p = (PositionProxy)plEnumerator.Current;
            Assert.AreEqual(p.isin, "333333333333");
            Assert.AreEqual(p.CurrVal, 666);
            Assert.AreEqual(p.Dailyvarperc, (decimal)16.66);
            Assert.AreEqual(p.quantity, 3);
        }
        [TestMethod]
        public void updateTotalValTest()
        {
            ctx().UpdateTotalVal("555555555_portfolio", 50000, "111111111111");
            Portfolio p = (Portfolio)ctx().Portfolios.Find("555555555_portfolio");
            Assert.AreEqual(p.totalval, 5550000);
        }

        [TestMethod]

        public void CreateMarketTest()
        {
            Market newM = new Market();
            newM.code = 444;
            newM.description = "Description 4";
            newM.name = "Market 4";
            ctx().CreateMarket(newM);
            Market m = (Market)ctx().Markets.Find(444);
            Assert.AreNotEqual(m, null);
            Assert.AreEqual(m.code, 444);
            Assert.AreEqual(m.description, "Description 4");
            Assert.AreEqual(m.name, "Market 4");
        }

        [TestMethod]

        public void updateMarketTest()
        {
            Market M = (Market)ctx().Markets.Find(444);
            M.description = "This is the new description";
            M.name = "IphoneMarket";
            ctx().UpdateMarket(M);
            Market m1 = (Market)ctx().Markets.Find(444);
            Assert.AreEqual(m1.description, "This is the new description");
            Assert.AreEqual(m1.name, "IphoneMarket");
        }

        [TestMethod]
        public void removeMarketTest()
        {
            Market m = (Market)ctx().Markets.Find(222);
            ctx().DeleteMarket(m);
            m = (Market)ctx().Markets.Find(222);
            Assert.AreEqual(m, null);
        }
        [TestMethod]
        public void removePortfolioTest()
        {
            IPortfolio p = ctx().Portfolios.Find("555555555_portfolio");
            ctx().DeletePortfolio(p);
            p = ctx().Portfolios.Find("555555555_portfolio");
            Assert.AreEqual(null, p);
        }
    }
}
