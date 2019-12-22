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
    public class PositionRepository : IPositionRepository
    {
        private DbContext ctx;
        private DbSet<Position> db;

        public PositionRepository(DbContext ctx, DbSet<Position> dbset) {
            this.ctx = ctx;
            db = dbset;
        }

        public bool Delete(IPosition value)
        {
            return db.Remove((Position)value).Equals(value);
        }

        public IPosition Find(params object[] keys)
        {
            return db.Find(keys);
        }

        public IPosition Insert(IPosition value)
        {
            return db.Add((Position)value);
        }

        public bool Update(IPosition value)
        {
            if (value == null) return false;

            ctx.Entry((Position)value).State = EntityState.Modified;
            IPosition newPosition = Find(value.isin, value.name);
            newPosition.quantity = value.quantity;
            newPosition.Portfolio = value.Portfolio;
            newPosition.Instrument= value.Instrument;

            return true;
        }
    }
}
