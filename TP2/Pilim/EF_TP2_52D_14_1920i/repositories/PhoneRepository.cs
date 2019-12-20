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
    public class PhoneRepository : IPhoneRepository
    {
        private DbContext ctx;
        private DbSet<Phone> db;

        public PhoneRepository(DbContext ctx, DbSet<Phone> dbset) {
            this.ctx = ctx;
            db = dbset;
        }
        public bool Delete(IPhone value)
        {
            return db.Remove((Phone)value).Equals(value);
        }

        public IPhone Find(params object[] keys)
        {
            return db.Find(keys);
        }

        public IPhone Insert(IPhone value)
        {
            return db.Add((Phone)value);
        }

        public bool Update(IPhone value)
        {
            if (value == null) return false;

            ctx.Entry((Phone)value).State = EntityState.Modified;
            IPhone newPhone = Find(value.code);
            newPhone.nif = value.nif;
            newPhone.number = value.number;
            newPhone.areacode = value.areacode;
            newPhone.description = value.description;
            newPhone.clientPhone = value.clientPhone;

            return true;
        }
    }
}
