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
    public class InstrumentRepository : IInstrumentRepository
    {
        private DbContext ctx;
        private DbSet<Instrument> db;

        public InstrumentRepository(DbContext ctx, DbSet<Instrument> dbset)
        {
            this.ctx = ctx;
            db = dbset;
        }
        public bool Delete(IInstrument value)
        {
            return db.Remove((Instrument)value).Equals(value);
        }

        public IInstrument Find(params object[] keys)
        {
            return db.Find(keys);
        }

        public IInstrument Insert(IInstrument value)
        {
            return db.Add((Instrument)value);
        }

        public bool Update(IInstrument value)
        {
            if (value == null) return false;

            ctx.Entry((Instrument)value).State = EntityState.Modified;
            IInstrument newInstrument = Find(value.isin);
            newInstrument.mrktcode = value.mrktcode;
            newInstrument.dailyRegs = value.dailyRegs;
            newInstrument.description = value.description;
            newInstrument.instrumentMarket = value.instrumentMarket;
            newInstrument.instrumentposition = value.instrumentposition;

            return true;
        }
    }
}
