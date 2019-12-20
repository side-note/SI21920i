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
    public class ClientRepository : IClientRepository
    {
        private readonly IContext context;
        private ClientMapper mapper;
        public ClientRepository(IContext ctx)
        {
            context = ctx;
            mapper = new ClientMapper(ctx);
        }

        public bool Delete(IClient value)
        {
            return mapper.Delete(value);
        }

        public IClient Find(params object[] keys)
        {
            return mapper.Read((decimal)keys[0]);
        }

        public IClient Insert(IClient value)
        {
            return mapper.Create(value);
        }

        public bool Update(IClient value)
        {
            return mapper.Update(value);
        }
    }
}

