using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using TypesProject.dal;
using TypesProject.mapper;
using TypesProject.model;

namespace TypesProject.concrete
{
    public class Context : IContext
    {
        private string connectionString;
        private SqlConnection con = null;

        private ClientRepository _clientRepository;
        private DailyMarketRepository _dailyMarketRepository;
        private DailyRegRepository _dailyRegRepository;
        private EmailRepository _emailRepository;
        private ExttripleRepository _exttripleRepository;
        private InstrumentRepository _instrumentRepository;
        private MarketRepository _marketRepository;
        private PhoneRepository _phoneRepository;
        private PortfolioRepository _portfolioRepository;
        private PositionRepository _positionRepository;


        public Context(string cs)
        {
            connectionString = cs;
            _clientRepository = new ClientRepository(this);
            _dailyMarketRepository = new DailyMarketRepository(this);
            _dailyRegRepository = new DailyRegRepository(this);
            _emailRepository = new EmailRepository(this);
            _exttripleRepository = new ExttripleRepository(this);
            _instrumentRepository = new InstrumentRepository(this);
            _marketRepository = new MarketRepository(this);
            _phoneRepository = new PhoneRepository(this);
            _portfolioRepository = new PortfolioRepository(this);
            _positionRepository = new PositionRepository(this);
        }

        public void Open()
        {
            if (con == null)
            {
                con = new SqlConnection(connectionString);

            }
            if (con.State != ConnectionState.Open)
                con.Open();
        }

        public IDbCommand createCommand()
        {
            Open();
            IDbCommand cmd = con.CreateCommand();
            return cmd;
        }
        public void Dispose()
        {
            if (con != null)
            {
                con.Dispose();
                con = null;
            }

        }

        public void EnlistTransaction()
        {
            if (con != null)
            {
                con.EnlistTransaction(Transaction.Current);
            }
        }

        public IClientRepository Clients => _clientRepository;

        public IDailyMarketRepository DailyMarkets => _dailyMarketRepository;

        public IDailyRegRepository DailyRegs => _dailyRegRepository;

        public IEmailRepository Emails =>_emailRepository;
      
        public IExttripleRepository Exttriples =>_exttripleRepository;

        public IInstrumentRepository Instruments=> _instrumentRepository;

        public IMarketRepository Markets =>_marketRepository;
      
        public IPhoneRepository Phones => _phoneRepository;
     

        public IPortfolioRepository Portfolios =>_portfolioRepository;
      
        public IPositionRepository Positions =>_positionRepository;


        //TODO: Try later
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

        public void  ProcedureExecutor(string procName, Action<IDbCommand> param)
        {
            using IDbCommand cmd = createCommand();
            cmd.CommandText = procName;
            cmd.CommandType = CommandType.StoredProcedure;
            param(cmd);
            cmd.ExecuteNonQuery();
        }

        public void p_actualizaValorDiario(string str, DateTime date)
        {
            ProcedureExecutor("p_actualizaValorDiario", cmd =>
            {
                cmd.Parameters.Add(str);
                cmd.Parameters.Add(date);
            });
        }

        public void UpdateTotalVal(string name, int quantity, string isin)
        {
            ProcedureExecutor("UpdateTotalVal", cmd =>
            {
                cmd.Parameters.Add(name);
                cmd.Parameters.Add(quantity);
                cmd.Parameters.Add(isin);
            });
        }

        public void createPortfolio(decimal nif)
        {
            ProcedureExecutor("createPortfolio", cmd =>
            {
                cmd.Parameters.Add(nif);
            });
        }

        public int SaveChanges()
        {
            return 0;
        }

        public IInstrument FundamentalDataTable(string isin, DateTime date)
        {
            IInstrument instrument = Instruments.Find(isin);
            if (instrument == null) return null;
            using (TransactionScope ts = new TransactionScope())
            {
                InstrumentProxy ins = new InstrumentProxy(instrument, this);
                using (IDbCommand cmd = createCommand())
                {
                    cmd.CommandText = "select dailyvar, currval, avg6m, dailyvarperc, var6m, var6mperc from dbo.FundamentalDataTable(@isin, @date)";
                    cmd.CommandType = CommandType.Text;

                    SqlParameter i = new SqlParameter("@isin", isin);
                    SqlParameter d = new SqlParameter("@date", date);

                    cmd.Parameters.Add(i);
                    cmd.Parameters.Add(d);
                    cmd.ExecuteNonQuery();

                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        IDataRecord record = reader;
                        ins.avg6m = record.GetDecimal(0);
                        ins.currval = record.GetDecimal(1);
                        ins.dailyvar = record.GetDecimal(2);
                        ins.dailyvarperc = record.GetDecimal(3);
                        ins.var6m = record.GetDecimal(4);
                        ins.var6mperc = record.GetDecimal(5);
                    }
                }
                ts.Complete();
                return ins;
            }
        }

     

        IEnumerable<IPosition> IContext.Portfolio_List(string name)
        {
            IEnumerable<IPosition> position = ((PortfolioProxy)Portfolios.Find(name)).positionInstruments;
            if (position == null) return null;
            
            
            using (IDbCommand cmd = createCommand())
            {
                cmd.CommandText = "select dailyvar, currval, isin,  quantity from dbo.Portfolio_List(@name)";
                cmd.CommandType = CommandType.Text;

                SqlParameter n = new SqlParameter("@name", name); 
                cmd.Parameters.Add(n);
                cmd.ExecuteNonQuery();

                using (IDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read()) return position;
                    List<IPosition> positions = new List<IPosition>();
                    IEnumerator<IPosition> posEnumerator = position.GetEnumerator();
                    posEnumerator.MoveNext();
                    while (posEnumerator.MoveNext() && reader.Read())
                    {
                        IDataRecord record = reader;
                        PositionProxy pos = new PositionProxy(posEnumerator.Current, this);
                        pos.isin = record.GetString(0);
                        pos.quantity = record.GetInt32(1);
                        pos.CurrVal = record.GetDecimal(2);
                        pos.Dailyvarperc = record.GetDecimal(3);
                        positions.Add(pos);

                    }              
                    return positions;
                }
            }
            
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

