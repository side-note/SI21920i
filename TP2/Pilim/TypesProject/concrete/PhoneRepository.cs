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
        public PhoneRepository(IContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<Phone> Find(Func<Phone, bool> criteria)
        {
            //Implementação muito pouco eficiente.  
            return FindAll().Where(criteria);
        }

        public IEnumerable<Phone> FindAll()
        {
            return new PhoneMapper(context).ReadAll();
        }
    }
}

