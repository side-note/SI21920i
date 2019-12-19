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
        private PositionMapper mapper;
        public PositionRepository(IContext ctx)
        {
            context = ctx;
            mapper = new PositionMapper(ctx);
        }

        public bool Delete(IPosition value, Func<IPosition, bool> criteria)
        {
            if (criteria(value))
                return mapper.Delete(value);
            return false;
        }

        public IEnumerable<IPosition> Find(Func<IPosition, bool> criteria)
        {
            return FindAll().Where(criteria);
        }

        public IEnumerable<IPosition> FindAll()
        {
            return mapper.ReadAll();
        }

        public IPosition Insert(IPosition value)
        {
            return mapper.Create(value);
        }

        public bool Update(IPosition value, Func<IPosition, bool> criteria)
        {
            if (criteria(value))
                return mapper.Update(value);
            return false;
        }
    }
}
