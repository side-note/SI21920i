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
    public class EmailRepository : IEmailRepository
    {
        private DbContext ctx;
        private DbSet<Email> db;
        public EmailRepository(DbContext ctx, DbSet<Email> dbset)
        {
            this.ctx = ctx;
            db = dbset;
        }
        public bool Delete(IEmail value)
        {
            return db.Remove((Email)value).Equals(value);
        }

        public IEmail Find(params object[] keys)
        {
            return db.Find(keys);
        }

        public IEmail Insert(IEmail value)
        {
            return db.Add((Email)value);
        }

        public bool Update(IEmail value)
        {
            if (value == null) return false;

            ctx.Entry((Email)value).State = EntityState.Modified;
            IEmail newEmail = Find(value.code);
            newEmail.nif = value.nif;
            newEmail.addr = value.addr;
            newEmail.description = value.description;
            newEmail.ClientEmail = value.ClientEmail;

            return true;
        }
    }
}
