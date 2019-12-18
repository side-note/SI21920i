using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using TypesProject.mapper;

namespace TypesProject.concrete
{
    class Context : IContext
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

        public SqlCommand createCommand()
        {
            Open();
            SqlCommand cmd = con.CreateCommand();
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

        public ClientRepository Clients
        {
            get
            {
                return _clientRepository;
            }
        }

        public DailyMarketRepository DailyMarkets
        {
            get
            {
                return _dailyMarketRepository;
            }
        }

        public DailyRegRepository DailyRegs
        {
            get
            {
                return _dailyRegRepository;
            }
        }

        public EmailRepository Emails
        {
            get
            {
                return _emailRepository;
            }
        }

        public ExttripleRepository Exttriples
        {
            get
            {
                return _exttripleRepository;
            }
        }

        public InstrumentRepository Instruments
        {
            get
            {
                return _instrumentRepository;
            }
        }

        public MarketRepository Markets
        {
            get
            {
                return _marketRepository;
            }
        }

        public PhoneRepository Phones
        {
            get
            {
                return _phoneRepository;
            }
        }

        public PortfolioRepository Portfolios
        {
            get
            {
                return _portfolioRepository;
            }
        }

        public PositionRepository Positions
        {
            get
            {
                return _positionRepository;
            }
        }
    }
}

