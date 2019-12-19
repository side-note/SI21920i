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
    class PhoneRepository : IPhoneRepository
    {
        private IContext context;
        private PhoneMapper mapper;
        public PhoneRepository(IContext ctx)
        {
            context = ctx;
            mapper = new PhoneMapper(ctx);
        }

        public bool Delete(IPhone value, Func<IPhone, bool> criteria)
        {
            if (criteria(value))
                return mapper.Delete(value);
            return false;
        }

        public IEnumerable<IPhone> Find(Func<IPhone, bool> criteria)
        {
            return FindAll().Where(criteria);
        }

        public IEnumerable<IPhone> FindAll()
        {
            return mapper.ReadAll();
        }

        public IPhone Insert(IPhone value)
        {
            return mapper.Create(value);
        }

        public bool Update(IPhone value, Func<IPhone, bool> criteria)
        {
            if (criteria(value))
                return mapper.Update(value);
            return false;
        }
    }
}

