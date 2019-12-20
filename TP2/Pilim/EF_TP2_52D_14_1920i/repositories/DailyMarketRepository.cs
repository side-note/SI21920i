using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesProject.dal;
using TypesProject.model;

namespace EF_TP2_52D_14_1920i.repositories
{
    public class DailyMarketRepository : IDailyMarketRepository
    {
        private DbSet<DailyMarket> db;
        private DbContext ctx;
        public DailyMarketRepository(DbContext ctx, DbSet<DailyMarket> dbset)
        {
            this.ctx = ctx;
            db = dbset;
        }

        public bool Delete(IDailyMarket value)
        {
            return db.Remove((DailyMarket) value).Equals(value);
        }

        public IDailyMarket Find(params object[] keys)
        {
            return db.Find(keys);
        }

        public IDailyMarket Insert(IDailyMarket value)
        {
            return db.Add((DailyMarket) value);
        }

        public bool Update(IDailyMarket value)
        {
            if (value == null) return false;

            ctx.Entry((DailyMarket)value).State = EntityState.Modified;
            IDailyMarket newDailyMarket = Find(value.code, value.date);
       
            newDailyMarket.market = value.market;
            newDailyMarket.idxmrkt = value.idxmrkt;
            newDailyMarket.dailyvar = value.dailyvar;
            newDailyMarket.idxopeningval = value.idxopeningval;
            newDailyMarket.market = value.market;
            
            return true;
        }
    }
}
