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
    public class DailyRegRepository : IDailyRegRepository
    {
        private DbSet<DailyReg> db;
        private DbContext ctx;
        public DailyRegRepository(DbContext ctx, DbSet<DailyReg> dbset)
        {
            this.ctx = ctx;
            db = dbset;
        }
        public bool Delete(IDailyReg value)
        {
            return db.Remove((DailyReg) value).Equals(value);
        }

        public IDailyReg Find(params object[] keys)
        {
            return db.Find(keys);
        }

        public IDailyReg Insert(IDailyReg value)
        {
            return db.Add((DailyReg) value);
        }

        public bool Update(IDailyReg value)
        {
            if (value == null) return false;

            ctx.Entry((DailyReg)value).State = EntityState.Modified;
            IDailyReg newDailyReg = Find(value.dailydate, value.isin);
            newDailyReg.minval = value.minval;
            newDailyReg.maxval = value.maxval;
            newDailyReg.openingval = value.openingval;
            newDailyReg.instrument = value.instrument;
            newDailyReg.closingval = value.closingval;

            return true;
        }
    }
}
