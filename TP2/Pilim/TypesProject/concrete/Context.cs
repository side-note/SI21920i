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
                cmd.CommandText = "select dbo.Average(@days, @isin)";
                cmd.CommandType = CommandType.Text;

                SqlParameter d = new SqlParameter("@days", days);
                SqlParameter i = new SqlParameter("@isin", isin);
                cmd.Parameters.Add(d);
                cmd.Parameters.Add(i);
                cmd.ExecuteNonQuery();

                using (IDataReader r = cmd.ExecuteReader())
                {
                    if (!r.Read()) return default;
                    IDataRecord rcrd = r;
                    decimal a = rcrd.IsDBNull(0) ? default : rcrd.GetDecimal(0);
                    return a;
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
                SqlParameter s = new SqlParameter("@id", str);
                SqlParameter d = new SqlParameter("@date", date);
                cmd.Parameters.Add(s);
                cmd.Parameters.Add(d);
            });
        }

        public void UpdateTotalVal(string name, int quantity, string isin)
        {
            ProcedureExecutor("UpdateTotalVal", cmd =>
            {
                SqlParameter n = new SqlParameter("@name", name);
                SqlParameter q = new SqlParameter("@quantity", quantity);
                SqlParameter i = new SqlParameter("@isin", isin);
                cmd.Parameters.Add(n);
                cmd.Parameters.Add(q);
                cmd.Parameters.Add(i);
            });
        }

        public void createPortfolio(decimal nif)
        {
            ProcedureExecutor("createPortfolio", cmd =>
            {
                SqlParameter n = new SqlParameter("@nif", nif);
                cmd.Parameters.Add(n);
            });
            IPortfolio p = Portfolios.Find(nif + "_portfolio");
            p.client = Clients.Find(nif);
            Portfolios.Update(p);
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
                        if (!reader.Read()) return instrument;
                        IDataRecord record = reader;
                        ins.avg6m = record.IsDBNull(2) ? default : record.GetDecimal(2);
                        ins.currval = record.IsDBNull(1) ? default : record.GetDecimal(1);
                        ins.dailyvar = record.IsDBNull(0) ? default : record.GetDecimal(0);
                        ins.dailyvarperc = record.IsDBNull(3) ? default : record.GetDecimal(3);
                        ins.var6m = record.IsDBNull(4) ? default : record.GetDecimal(4);
                        ins.var6mperc = record.IsDBNull(5) ? default : record.GetDecimal(5);
                    }
                }
                ts.Complete();
                return ins;
            }
        }

     

        public IEnumerable<IPosition> Portfolio_List(string name)
        {
            IEnumerable<IPosition> position = ((PortfolioProxy) Portfolios.Find(name)).Position;
            if (position == null) return null;
            
            using (IDbCommand cmd = createCommand())
            {
                cmd.CommandText = "select dailyvarperc, currval, isin,  quantity from dbo.Portfolio_List(@name)";
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
                    do
                    {
                        IDataRecord record = reader;
                        PositionProxy pos = new PositionProxy(posEnumerator.Current, this);
                        pos.isin = record.IsDBNull(2) ? default : record.GetString(2);
                        pos.quantity = record.IsDBNull(3) ? default : record.GetInt32(3);
                        pos.CurrVal = record.IsDBNull(1) ? default : record.GetDecimal(1);
                        pos.Dailyvarperc = record.IsDBNull(0) ? default : record.GetDecimal(0);
                        positions.Add(pos);
                    } while (posEnumerator.MoveNext() && reader.Read());
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
            bool b = Markets.Delete(value);
            SaveChanges();
            return b;
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

