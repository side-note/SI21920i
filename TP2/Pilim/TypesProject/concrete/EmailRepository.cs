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
    class EmailRepository : IEmailRepository
    {
        private IContext context;
        public EmailRepository(IContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<IEmail> Find(System.Func<IEmail, bool> criteria)
        {
            //Implementação muito pouco eficiente.  
            return FindAll().Where(criteria);
        }

        public IEnumerable<IEmail> FindAll()
        {
            return new EmailMapper(context).ReadAll();
        }
    }
}

