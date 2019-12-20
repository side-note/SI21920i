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
    public class EmailRepository : IEmailRepository
    {
        private readonly IContext context;
        private EmailMapper mapper;
        public EmailRepository(IContext ctx)
        {
            context = ctx;
            mapper = new EmailMapper(ctx);
        }

        public bool Delete(IEmail value)
        {
            return mapper.Delete(value);
        }

        public IEmail Find(params object[] keys)
        {
            return mapper.Read((int)keys[0]);
        }

        public IEmail Insert(IEmail value)
        {
            return mapper.Create(value);
        }

        public bool Update(IEmail value)
        {          
            return mapper.Update(value);
        }
    }
}

