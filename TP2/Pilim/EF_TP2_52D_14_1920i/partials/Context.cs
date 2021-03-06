﻿using System;
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
    public partial class TL52D_14Entities : IContext
    {
        readonly ClientRepository clientRepository ;
        readonly DailyMarketRepository dailyMarketRepository;
        readonly DailyRegRepository dailyRegRepository;
        readonly EmailRepository emailRepository;
        readonly ExttripleRepository exttripleRepository;
        readonly InstrumentRepository instrumentRepository;
        readonly MarketRepository marketRepository;
        readonly PhoneRepository phoneRepository;
        readonly PortfolioRepository portfolioRepository;
        readonly PositionRepository positionRepository;

       
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

        decimal IContext.Average(int days, string isin)
        {
            using (IDbCommand cmd = createCommand())
            {
                cmd.CommandText = "select dbo.Average(@days, @isin)";
                cmd.CommandType = CommandType.Text;

                SqlParameter p1 = new SqlParameter("@days", days);
                SqlParameter p2 = new SqlParameter("@isin", isin);
                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                cmd.ExecuteNonQuery();

                using (IDataReader r = cmd.ExecuteReader())
                {
                    if(!r.Read()) return default;
                    IDataRecord rcrd = r;


                    return rcrd.IsDBNull(0) ? default : rcrd.GetDecimal(0);                    
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
            InstrumentProxy iproxy = new InstrumentProxy(instrument, this)
            {
                avg6m = result.ElementAt(0).avg6m == null ? default : (decimal)result.ElementAt(0).avg6m,
                currval = result.ElementAt(0).currval == null ? default : (decimal)result.ElementAt(0).currval,
                dailyvar = result.ElementAt(0).dailyvar == null ? default : (decimal)result.ElementAt(0).dailyvar,
                dailyvarperc = result.ElementAt(0).dailyvarperc == null ? default : (decimal)result.ElementAt(0).dailyvarperc,
                var6m = result.ElementAt(0).var6m == null ? default : (decimal)result.ElementAt(0).var6m,
                var6mperc = result.ElementAt(0).var6mperc == null ? default : (decimal)result.ElementAt(0).var6mperc
            };
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

        IEnumerable<IPosition> IContext.Portfolio_List(string name)
        {
            IPortfolio por = Portfolios.Find(name);

            ICollection<Portfolio_List_Result> result = Portfolio_List(name).ToList();
            
            IEnumerable<IPosition> positions = por.Position;
            List<IPosition> pos = new List<IPosition>();
            for (int i = 0; i < result.Count; ++i) {
                PositionProxy posproxy = new PositionProxy(positions.ElementAt(i), this)
                {
                    CurrVal = (decimal)result.ElementAt(i).CurrVal,
                    Dailyvarperc = (decimal)result.ElementAt(i).Dailyvarperc,
                    quantity = (int)result.ElementAt(i).quantity,
                    isin = result.ElementAt(i).isin
                };
                pos.Add(posproxy);
            }
            return pos;
        }

        public bool DeletePortfolio(IPortfolio value)
        {
            bool b = Portfolios.Delete(value);
            SaveChanges();
            return b;
        }

        public bool DeleteMarket(IMarket value)
        {
            bool b = Markets.Delete(value);
            SaveChanges();
            return b;
        }

        public bool UpdateMarket(IMarket value)
        {
            bool b = Markets.Update(value);
            SaveChanges();
            return b;
        }

        public IMarket CreateMarket(IMarket value)
        {
            IMarket m = Markets.Insert(value);
            SaveChanges();
            return m;
        }
    }
}
