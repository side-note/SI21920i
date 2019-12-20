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
    public class ExttripleRepository : IExttripleRepository
    {
        private DbSet<Exttriple> db;
        private DbContext ctx;
        public ExttripleRepository(DbContext ctx, DbSet<Exttriple> dbset)
        {
            this.ctx = ctx;
            db = dbset;
        }

        public bool Delete(IExttriple value)
        {
            return db.Remove((Exttriple)value).Equals(value);
        }

        public IExttriple Find(params object[] keys)
        {
            return db.Find(keys);
        }

        public IExttriple Insert(IExttriple value)
        {
            return db.Add((Exttriple)value);
        }

        public bool Update(IExttriple value)
        {
            if (value == null) return false;

            ctx.Entry((Exttriple)value).State = EntityState.Modified;
            IExttriple newExttriple = Find(value.id, value.datetime);

            newExttriple.value = value.value;

            return true;
        }
    }
}
