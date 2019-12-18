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
    class PositionRepository : IPositionRepository
    {
        private IContext context;
        public PositionRepository(IContext ctx)
        {
            context = ctx;
        }
        public IEnumerable<IPosition> Find(Func<IPosition, bool> criteria)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IPosition> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
