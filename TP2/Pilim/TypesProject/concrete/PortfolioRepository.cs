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
    class PortfolioRepository : IPortfolioRepository
    {
        private IContext context;
        private PortfolioMapper mapper;
        public PortfolioRepository(IContext ctx)
        {
            context = ctx;
            mapper = new PortfolioMapper(ctx);
        }

        public bool Delete(IPortfolio value, Func<IPortfolio, bool> criteria)
        {
            if (criteria(value))
                return mapper.Delete(value);
            return false;
        }

        public IEnumerable<IPortfolio> Find(Func<IPortfolio, bool> criteria)
        {
            return FindAll().Where(criteria);
        }

        public IEnumerable<IPortfolio> FindAll()
        {
            return mapper.ReadAll();
        }

        public IPortfolio Insert(IPortfolio value)
        {
            return mapper.Create(value);
        }

        public bool Update(IPortfolio value, Func<IPortfolio, bool> criteria)
        {
            if (criteria(value))
                return mapper.Update(value);
            return false;
        }
    }
}

