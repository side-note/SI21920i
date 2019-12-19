using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesProject.dal;
using TypesProject.mapper;
using TypesProject.model;

namespace TypesProject.concrete
{
    class ClientRepository : IClientRepository
    {
        private IContext context;
        private ClientMapper mapper;
        public ClientRepository(IContext ctx)
        {
            context = ctx;
            mapper = new ClientMapper(ctx);
        }

        public bool Delete(IClient value,Func<IClient, bool> criteria)
        {
            if(criteria(value))
                return mapper.Delete(value);
            return false;
        }

        public IEnumerable<IClient> Find(Func<IClient, bool> criteria)
        {
           
            return FindAll().Where(criteria);
        }

        public IEnumerable<IClient> FindAll()
        {
            return mapper.ReadAll();
        }

        public IClient Insert(IClient value)
        {
            return mapper.Create(value);
        }

        public bool Update(IClient value, Func<IClient, bool> criteria)
        {
            if (criteria(value))
                return mapper.Update(value);
            return false;
        }
    }
}

