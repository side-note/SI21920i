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
    public class PhoneRepository : IPhoneRepository
    {
        private readonly IContext context;
        private PhoneMapper mapper;
        public PhoneRepository(IContext ctx)
        {
            context = ctx;
            mapper = new PhoneMapper(ctx);
        }

        public bool Delete(IPhone value)
        {
            return mapper.Delete(value);
        }

        public IPhone Find(params object[] keys)
        {
            return mapper.Read((int)keys[0]);
        }

        public IPhone Insert(IPhone value)
        {
            return mapper.Create(value);
        }

        public bool Update(IPhone value)
        {
            return mapper.Update(value);
        }
    }
}

