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
   public class InstrumentRepository : IInstrumentRepository
        
    {
        private InstrumentMapper mapper;
        private readonly IContext context;

        public InstrumentRepository(IContext ctx)
        {
            context = ctx;
            mapper = new InstrumentMapper(ctx);
        }

        public bool Delete(IInstrument value)
        {
            return mapper.Delete(value);
        }

        public IInstrument Insert(IInstrument value)
        {
            return mapper.Create(value);
        }

        public bool Update(IInstrument value)
        {
            return mapper.Update(value);
        }

        public IInstrument Find(params object[] keys)
        {
            return mapper.Read((string)keys[0]);
        }
    }
}

