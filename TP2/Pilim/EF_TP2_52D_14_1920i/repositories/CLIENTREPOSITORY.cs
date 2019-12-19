using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesProject.dal;
using TypesProject.model;

namespace EF_TP2_52D_14_1920i
{
    public class ClientRepository : IClientRepository
    {
        private DbSet<Client> db;
        private DbContext ctx;
        public ClientRepository(DbContext ctx, DbSet<Client> dbset)
        {
            this.ctx = ctx;
            db = dbset;
        }
        public bool Delete(IClient value)
        {
            return db.Remove((Client)value).Equals(value);
        }

        public IClient Find(params object[] keys)
        {
            return db.Find(keys);
        }

        public IClient Insert(IClient value)
        {
            return db.Add((Client)value);
        }

        public bool Update(IClient value)
        {
           if(value == null) return false;

            ctx.Entry<Client>((Client)value).State = EntityState.Modified;
            IClient newClient = Find(value.nif);
            newClient.ncc = value.ncc;
            newClient.name = value.name;
            newClient.phone = value.phone;
            newClient.email = value.email;
            newClient.portfolio = value.portfolio;
            return true;

        }
    }
}
