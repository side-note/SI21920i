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
                    "INSERT INTO DailyMarket (idxmrkt,dailyvar,idxopeningval, code, date) VALUES(@idxmrkt, @dailyvar, @idxopv, @id, @datetime); select @id=code, @datetime=date"
                    );
                ts.Complete();
                return idm;
            }
        }

        public IDailyMarket Read(KeyValuePair<int, DateTime> id)
        {
        return mapperHelper.Read(id,
           (cmd, i) => SelectParameters(cmd, i),
           "select idxmrkt,dailyvar,idxopeningval, code, date from DailyMarket where code=@id and date=@datetime"
           );
    }

        public List<IDailyMarket> ReadAll()
        {
            return mapperHelper.ReadAll(
            cmd => { },
            "select idxmrkt,dailyvar,idxopeningval, code, date from DailyMarket"
            );
        }

        public bool Update(IDailyMarket idm)
        {
            return mapperHelper.Update(idm,
                    (cmd, idailymarket) => UpdateParameters(cmd, idailymarket),
                    "update DailyMarket set idxmrkt=@idxmrkt, dailyvar=@dailyvar, idxopeningval=@idxopv where code=@id and date=@datetime"
                    );
        }

        public bool Delete(IDailyMarket idm)
        {
            return mapperHelper.Delete(idm,
               (cmd, idailymarket) => DeleteParameters(cmd,idailymarket),
               "delete from DailyMarket where code=@id and date = @datetime"
               );
        }

        public IDailyMarket Map(IDataRecord record)
        {
            DailyMarket dm = new DailyMarket();
            dm.idxmrkt = record.GetDecimal(0);
            dm.dailyvar = record.GetDecimal(1);
            dm.idxopeningval = record.GetDecimal(2);
            dm.code = record.GetInt32(3);
            dm.date = record.GetDateTime(4);
            return dm;
        }

        public List<IDailyMarket> MapAll(IDataReader reader)
        {
            return mapperHelper.MapAll(reader);
        }
    }
}

