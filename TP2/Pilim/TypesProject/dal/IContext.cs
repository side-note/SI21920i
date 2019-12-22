using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TypesProject.dal;
using TypesProject.model;

namespace TypesProject.mapper
{
    public interface IContext : IDisposable
    {
        void Open();
        IDbCommand createCommand();        
        void EnlistTransaction();

        IClientRepository Clients { get; }
        IDailyMarketRepository DailyMarkets { get; }
        IDailyRegRepository DailyRegs { get;  }
        IEmailRepository Emails { get; }
        IExttripleRepository Exttriples { get; }
        IInstrumentRepository Instruments { get; }
        IMarketRepository Markets { get;}
        IPhoneRepository Phones { get; }
        IPortfolioRepository Portfolios{ get; }
        IPositionRepository Positions { get;}

        int SaveChanges();
        void p_actualizaValorDiario(string id, DateTime date);
        decimal Average(int days, string isin);
        IInstrument FundamentalDataTable(string isin, DateTime date);
        void createPortfolio(decimal nif);
        void UpdateTotalVal(string name, int quantity, string isin);
        IEnumerable<IPosition> Portfolio_List(string name);
        bool DeletePortfolio(IPortfolio value);
        bool DeleteMarket(IMarket value);
        bool UpdateMarket(IMarket value);
        IMarket CreateMarket(IMarket value);


    }
}
