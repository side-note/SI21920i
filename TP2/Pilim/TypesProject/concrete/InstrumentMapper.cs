using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesProject.mapper;
using TypesProject.model;

namespace TypesProject.concrete
{
    class InstrumentMapper: AbstractMapper<Instrument, int?, List<Instrument>>, IInstrumentMapper
    {
        public InstrumentMapper(IContext ctx) : base(ctx)
        {
        }
        internal ICollection<DailyReg> LoadDailyRegs(Instrument i)
        {
            List<DailyReg> lst = new List<DailyReg>();

            DailyRegMapper drm = new DailyRegMapper(context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@id", i.isin));
            using (IDataReader rd = ExecuteReader("select dailyregid, dailyregdt from instrumentdailyreg where instrumentId=@id", parameters))
            {
                while (rd.Read())
                {
                    int key = rd.GetInt32(0);
                    lst.Add(drm.Read(key));
                }
            }
            return lst;
        }
        internal ICollection<Portfolio> LoadPortfolios(Instrument i)
        {
            List<Portfolio> lst = new List<Portfolio>();

            PortfolioMapper pm = new PortfolioMapper(context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@id", i.isin));
            using (IDataReader rd = ExecuteReader("select portfolioid from portfolioinstrument where instrumentId=@id", parameters))
            {
                while (rd.Read())
                {
                    int key = rd.GetInt32(0);
                    lst.Add(pm.Read(key));
                }
            }
            return lst;
        }
        internal Market LoadMarket(Instrument i)
        {
            MarketMapper mm = new MarketMapper(context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@id", i.isin));
            using (IDataReader rd = ExecuteReader("select market from instrument where instrumentId=@id", parameters))
            {
                if (rd.Read())
                {
                    int key = rd.GetInt32(0);
                    return mm.Read(key);
                }
            }
            return null;

        }

        protected override string DeleteCommandText
        {
            get
            {
                return "delete from Instrument where instrumentId=@id";
            }
        }

        protected override string InsertCommandText
        {
            get
            {
                return "INSERT INTO Instrument (isin, mrktcode, description) VALUES(@id, @code, @desc); select @id=isin";
            }
        }

        protected override string SelectAllCommandText
        {
            get
            {
                return "select isin, mrktcode, description from Instrument";
            }
        }

        protected override string SelectCommandText
        {
            get
            {
                return String.Format("{0} where instrumentId=@id", SelectAllCommandText); ;
            }
        }

        protected override string UpdateCommandText
        {
            get
            {
                return "update Instrument set mrktcode=@code, description=@desc where instrumentId=@id";
            }
        }

        protected override void DeleteParameters(IDbCommand cmd, Instrument i)
        {

            SqlParameter id = new SqlParameter("@id", i.isin);
            cmd.Parameters.Add(id);
        }

        protected override void InsertParameters(IDbCommand cmd, Instrument i)
        {
            SqlParameter desc = new SqlParameter("@desc", i.description);
            SqlParameter id = new SqlParameter("@id", i.isin);
            SqlParameter mc = new SqlParameter("@code", i.mrktcode);
            id.Direction = ParameterDirection.InputOutput;


            cmd.Parameters.Add(desc);
            cmd.Parameters.Add(id);
            cmd.Parameters.Add(mc);
        }


        protected override void SelectParameters(IDbCommand cmd, int? k)
        {
            SqlParameter p1 = new SqlParameter("@id", k);
            cmd.Parameters.Add(p1);
        }

        protected override Instrument UpdateEntityID(IDbCommand cmd, Instrument i)
        {
            var param = cmd.Parameters["@id"] as SqlParameter;
            i.isin = int.Parse(param.Value.ToString());
            return i;
        }

        protected override void UpdateParameters(IDbCommand cmd, Instrument i)
        {
            InsertParameters(cmd, i);
        }

        protected override Instrument Map(IDataRecord record)
        {
            Instrument i = new Instrument();
            i.isin = record.GetInt32(0);
            i.description = record.GetString(1);
            return new InstrumentProxy(i, context, record.GetInt32(2));
        }
        public override Instrument Create(Instrument instrument)
        {

            return new InstrumentProxy(instrument, context);

        }


        public override Instrument Update(Instrument instrument)
        {
            return new InstrumentProxy(base.Update(instrument), context);
        }
    }
}

