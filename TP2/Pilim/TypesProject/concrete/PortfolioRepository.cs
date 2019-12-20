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
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly IContext context;
        private PortfolioMapper mapper;
        public PortfolioRepository(IContext ctx)
        {
            context = ctx;
            mapper = new PortfolioMapper(ctx);
        }

        public bool Delete(IPortfolio value)
        {
            return mapper.Delete(value);
        }

        public IPortfolio Find(params object[] keys)
        {
            return mapper.Read((string)keys[0]);
        }

        public IPortfolio Insert(IPortfolio value)
        {
            return mapper.Create(value);
        }

        public bool Update(IPortfolio value)
        {
            return mapper.Update(value);
        }
    }
}

