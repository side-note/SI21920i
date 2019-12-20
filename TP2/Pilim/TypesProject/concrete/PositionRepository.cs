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
    public class PositionRepository : IPositionRepository
    {
        private readonly IContext context;
        private PositionMapper mapper;
        public PositionRepository(IContext ctx)
        {
            context = ctx;
            mapper = new PositionMapper(ctx);
        }

        public bool Delete(IPosition value)
        {
            return mapper.Delete(value);
        }

        public IPosition Find(params object[] keys)
        {
            KeyValuePair<string, string> key = new KeyValuePair<string, string>((string)keys[0], (string)keys[1]);
            return mapper.Read(key);
        }

        public IPosition Insert(IPosition value)
        {
            return mapper.Create(value);
        }

        public bool Update(IPosition value)
        {
            return mapper.Update(value);
        }
    }
}
