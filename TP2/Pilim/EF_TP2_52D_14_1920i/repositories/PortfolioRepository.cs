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
   public class PortfolioRepository : IPortfolioRepository
    {
        private DbContext ctx;
        private DbSet<Portfolio> db;

        public PortfolioRepository(DbContext ctx, DbSet<Portfolio> dbset) {
            this.ctx = ctx;
            db = dbset;
        }
        public bool Delete(IPortfolio value)
        {
            return db.Remove((Portfolio)value).Equals(value);
        }

        public IPortfolio Find(params object[] keys)
        {
            return db.Find(keys);
        }

        public IPortfolio Insert(IPortfolio value)
        {
            return db.Add((Portfolio)value);
        }

        public bool Update(IPortfolio value)
        {
            if (value == null) return false;

            ctx.Entry((Portfolio)value).State = EntityState.Modified;
            IPortfolio newPortfolio = Find(value.name);
            newPortfolio.client = value.client;
            newPortfolio.totalval = value.totalval;
            newPortfolio.Positions = value.Positions;
            
            return true;
        }
    }
}
