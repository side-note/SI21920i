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
    public class MarketRepository : IMarketRepository
    {
        private DbContext ctx;
        private DbSet<Market> db;

        public MarketRepository(DbContext ctx, DbSet<Market> dbset)
        {
            this.ctx = ctx;
            db = dbset;
        }
        public bool Delete(IMarket value)
        {
            return db.Remove((Market)value).Equals(value);
        }

        public IMarket Find(params object[] keys)
        {
            return db.Find(keys);
        }

        public IMarket Insert(IMarket value)
        {
            return db.Add((Market)value);
        }

        public bool Update(IMarket value)
        {
            if (value == null) return false;

            ctx.Entry((Market)value).State = EntityState.Modified;
            IMarket newMarket = Find(value.code);
            newMarket.name = value.name;
            newMarket.description = value.description;
            newMarket.dailyMarkets = value.dailyMarkets;
            newMarket.marketInstruments = value.marketInstruments;

            return true;
        }
    }
}
