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
    class InstrumentRepository : IInstrumentRepository
        
    {
        private InstrumentMapper mapper;
        private IContext context;
        public InstrumentRepository(IContext ctx)
        {
            context = ctx;
            mapper = new InstrumentMapper(ctx);
        }

        public bool Delete(IInstrument value, Func<IInstrument, bool> criteria)
        {
            if (criteria(value))
                return mapper.Delete(value);
            return false;
        }

        public IEnumerable<IInstrument> Find(Func<IInstrument, bool> criteria)
        {
            return FindAll().Where(criteria);
        }

        public IEnumerable<IInstrument> FindAll()
        {
            return mapper.ReadAll();
        }

        public IInstrument Insert(IInstrument value)
        {
            return mapper.Create(value);
        }

        public bool Update(IInstrument value, Func<IInstrument, bool> criteria)
        {
            if (criteria(value))
                return mapper.Update(value);
            return false;
        }
    }
}

