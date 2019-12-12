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
        private IContext context;
        public InstrumentRepository(IContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<Instrument> Find(Func<Instrument, bool> criteria)
        {
            //Implementação muito pouco eficiente.  
            return FindAll().Where(criteria);
        }

        public IEnumerable<Instrument> FindAll()
        {
            return new InstrumentMapper(context).ReadAll();
        }
    }
}

