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
        public PortfolioRepository(IContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<IPortfolio> Find(Func<IPortfolio, bool> criteria)
        {
            //Implementação muito pouco eficiente.  
            return FindAll().Where(criteria);
        }

        public IEnumerable<IPortfolio> FindAll()
        {
            return new PortfolioMapper(context).ReadAll();
        }
    }
}

