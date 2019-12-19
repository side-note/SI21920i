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
        private EmailMapper mapper;
        public EmailRepository(IContext ctx)
        {
            context = ctx;
            mapper = new EmailMapper(ctx);
        }

        public bool Delete(IEmail value, Func<IEmail, bool> criteria)
        {
            if (criteria(value))
                return mapper.Delete(value);
            return false;
        }

        public IEnumerable<IEmail> Find(System.Func<IEmail, bool> criteria)
        {
            return FindAll().Where(criteria);
        }

        public IEnumerable<IEmail> FindAll()
        {
            return mapper.ReadAll();
        }

        public IEmail Insert(IEmail value)
        {
            return mapper.Create(value);
        }

        public bool Update(IEmail value, Func<IEmail, bool> criteria)
        {
            if (criteria(value))
                return mapper.Update(value);
            return false;
        }
    }
}

