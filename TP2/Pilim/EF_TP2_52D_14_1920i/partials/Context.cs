using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF_TP2_52D_14_1920i.repositories;
using TypesProject.dal;
using TypesProject.mapper;
using TypesProject.model;

namespace EF_TP2_52D_14_1920i
{
    public partial class TL52D_14Entities9 : IContext
    {
       
        ClientRepository clientRepository ;
        DailyMarketRepository dailyMarketRepository;
        DailyRegRepository dailyRegRepository;
        EmailRepository emailRepository;
        ExttripleRepository exttripleRepository;
        InstrumentRepository instrumentRepository;
        MarketRepository marketRepository;
        PhoneRepository phoneRepository;
        PortfolioRepository portfolioRepository;
        PositionRepository positionRepository;

       
        private IDbConnection con = null;

        public IClientRepository Clients => clientRepository ?? new ClientRepository(this, Client);

        public IDailyMarketRepository DailyMarkets => dailyMarketRepository ?? new DailyMarketRepository(this,DailyMarket);

        public IDailyRegRepository DailyRegs => dailyRegRepository ?? new DailyRegRepository(this, DailyReg);

        public IEmailRepository Emails => emailRepository ?? new EmailRepository(this, Email);

        public IExttripleRepository Exttriples => exttripleRepository ?? new ExttripleRepository(this, Exttriple);

        public IInstrumentRepository Instruments => instrumentRepository ?? new InstrumentRepository(this, Instrument);

        public IMarketRepository Markets => marketRepository ?? new MarketRepository(this,Market);

        public IPhoneRepository Phones => phoneRepository ?? new PhoneRepository(this, Phone);

        public IPortfolioRepository Portfolios => portfolioRepository ?? new PortfolioRepository(this, Portfolio);

        public IPositionRepository Positions => positionRepository ?? new PositionRepository(this, Position);

        public decimal Average(int days, string isin)
        {
            using (IDbCommand cmd = createCommand())
            {
                cmd.CommandText = "select from dbo.Average(@days, @isin)";
                cmd.CommandType = CommandType.Text;

                SqlParameter p1 = new SqlParameter("@days", days);
                SqlParameter p2 = new SqlParameter("@isin", isin);
                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                cmd.ExecuteNonQuery();

                using (IDataReader r = cmd.ExecuteReader())
                {
                    IDataRecord rcrd = r;
                    return rcrd.GetDecimal(0);
                }
            }
        }

        public IDbCommand createCommand()
        {
            Open();
            IDbCommand cmd = con.CreateCommand();
            return cmd;

        }

        void IContext.createPortfolio(decimal nif)
        {
            createPortfolio(nif);
        }

        public void EnlistTransaction()
        {
        }

        IInstrument IContext.FundamentalDataTable(string isin, DateTime date)
        {
            IList<FundamentalDataTable_Result> result = FundamentalDataTable(isin, date).ToList();
            IInstrument instrument = Instruments.Find(isin);
            InstrumentProxy iproxy = new InstrumentProxy(instrument, this);
            iproxy.avg6m = (decimal)result.ElementAt(0).avg6m;
            iproxy.currval = (decimal)result.ElementAt(0).currval;
            iproxy.dailyvar = (decimal)result.ElementAt(0).dailyvar;
            iproxy.dailyvarperc = (decimal)result.ElementAt(0).dailyvarperc;
            iproxy.var6m = (decimal)result.ElementAt(0).var6m;
            iproxy.var6mperc = (decimal)result.ElementAt(0).var6mperc;
            return iproxy;            

        }

        public void Open()
        {       
            if (con == null)
            {
                con = this.Database.Connection;
            }

            if (con.State != ConnectionState.Open) con.Open();
            
        }

        public void Dispose()
        {
            if (con == null) return;
            con.Dispose();
            con = null;
        }

        void IContext.p_actualizaValorDiario(string id, DateTime date)
        {
            p_actualizaValorDiario(id, date);
        }

        void IContext.UpdateTotalVal(string name, int quantity, string isin)
        {
            UpdateTotalVal(name, quantity, isin);
        }

        IPosition IContext.Portfolio_List(string name)
        {
            IList<Portfolio_List_Result> result = Portfolio_List(name).ToList();
            IPosition pos = Positions.Find(name);
            PositionProxy posproxy = new PositionProxy(pos, this);
            posproxy.CurrVal = (decimal)result.ElementAt(0).CurrVal;
            posproxy.Dailyvarperc = (decimal)result.ElementAt(0).Dailyvarperc;
            posproxy.quantity = (int)result.ElementAt(0).quantity;
            posproxy.isin = (string)result.ElementAt(0).isin;
            return posproxy;
        }

        public bool DeletePortfolio(IPortfolio value)
        {
            return Portfolios.Delete(value);
        }

        public bool DeleteMarket(IMarket value)
        {
            return Markets.Delete(value);
        }

        public bool UpdateMarket(IMarket value)
        {
            return Markets.Update(value);
        }

        public IMarket CreateMarket(IMarket value)
        {
            return Markets.Insert(value);
        }
    }
}
