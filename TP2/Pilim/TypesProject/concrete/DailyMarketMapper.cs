using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using TypesProject.mapper;
using TypesProject.model;

namespace TypesProject.concrete
{
    public class DailyMarketMapper:  IDailyMarketMapper
    {
        MapperHelper<IDailyMarket, KeyValuePair<int, DateTime>, List<IDailyMarket>> mapperHelper;
        public DailyMarketMapper(IContext ctx) 
        {
            mapperHelper = new MapperHelper<IDailyMarket, KeyValuePair<int, DateTime>, List<IDailyMarket>>(ctx, this);
        }

        internal IMarket LoadMarket(DailyMarket dm)
        {
            MarketMapper mm = new MarketMapper(mapperHelper.context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@id", dm.code));
            parameters.Add(new SqlParameter("@datetime", dm.date));

            using (IDataReader rd = mapperHelper.ExecuteReader("select market from dailymarket where dailymarketId=@id and dailymarketdt=@datetime ", parameters))
            {
                if (rd.Read())
                {
                    int key = rd.GetInt32(0);
                    return mm.Read(key);
                }
            }
            return null;

        }


        protected void DeleteParameters(IDbCommand cmd, IDailyMarket dm)
        {

            SqlParameter id = new SqlParameter("@id", dm.code);
            SqlParameter datetime = new SqlParameter("@datetime", dm.date);

            cmd.Parameters.Add(id);
            cmd.Parameters.Add(datetime);
        }

        protected  void InsertParameters(IDbCommand cmd, IDailyMarket dm)
        {
            SqlParameter idm = new SqlParameter("@IdxMrkt", dm.idxmrkt);
            SqlParameter id = new SqlParameter("@id", dm.code);
            SqlParameter dt = new SqlParameter("@datetime", dm.date);
            SqlParameter dv = new SqlParameter("@dailyVar", dm.dailyvar);
            SqlParameter iov = new SqlParameter("@IdxOpeningVal", dm.idxopeningval);
            id.Direction = ParameterDirection.InputOutput;
            dt.Direction = ParameterDirection.InputOutput;

            cmd.Parameters.Add(idm);
            cmd.Parameters.Add(id);
            cmd.Parameters.Add(dt);
            cmd.Parameters.Add(dv);
            cmd.Parameters.Add(iov);
        }


        protected void SelectParameters(IDbCommand cmd, KeyValuePair<int, DateTime> p )
        {
            SqlParameter id = new SqlParameter("@id", p.Key);
            SqlParameter dt = new SqlParameter("@datetime", p.Value);
            cmd.Parameters.Add(id);
            cmd.Parameters.Add(dt);
        }

       
        protected void UpdateParameters(IDbCommand cmd, IDailyMarket dm)
        {
            InsertParameters(cmd, dm);
        }



        public IDailyMarket Create(IDailyMarket idm)
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                mapperHelper.Create(idm,
                    (cmd, idailymarket) => InsertParameters(cmd, idailymarket),
                    "INSERT INTO DailyMarket (idxMrkt,dailyvar,idxopeningval, code, date) VALUES(@idxmrkt, @dailyvar, @idxopv, @id, @datetime); select @id=code, @datetime=date"
                    );
                ts.Complete();
                return idm;
            }
        }

        public IDailyMarket Read(KeyValuePair<int, DateTime> id)
        {
        return mapperHelper.Read(id,
           (cmd, i) => SelectParameters(cmd, i),
           "select idxMrkt,dailyvar,idxopeningval, code, date from DailyMarket where dailyMarketId=@id and dailymarketdt=@datetime"
           );
    }

        public List<IDailyMarket> ReadAll()
        {
            return mapperHelper.ReadAll(
            cmd => { },
            "select idxMrkt,dailyvar,idxopeningval, code, date from DailyMarket"
            );
        }

        public bool Update(IDailyMarket idm)
        {
            return mapperHelper.Update(idm,
                    (cmd, idailymarket) => UpdateParameters(cmd, idailymarket),
                    "update DailyMarket set idxMrkt=@idxmrkt, dailyvar=@dailyvar, idxopeningval=@idxopv where dailyMarketId=@id and dailymarketdt=@datetime"
                    );
        }

        public bool Delete(IDailyMarket idm)
        {
            return mapperHelper.Delete(idm,
               (cmd, idailymarket) => DeleteParameters(cmd,idailymarket),
               "delete from DailyMarket where dailyMarketId=@id and dailymarketdt = @datetime"
               );
        }

        public IDailyMarket Map(IDataRecord record)
        {
            DailyMarket dm = new DailyMarket();
            dm.dailyvar = record.GetDecimal(0);
            dm.idxmrkt = record.GetDecimal(1);
            dm.idxopeningval = record.GetDecimal(2);
            dm.date = record.GetDateTime(3);
            dm.code = record.GetInt32(4);
            return dm;
        }

        public List<IDailyMarket> MapAll(IDataReader reader)
        {
            return mapperHelper.MapAll(reader);
        }
    }
}

