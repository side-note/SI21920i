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
    public class DailyRegRepository : IDailyRegRepository
    {
        private readonly IContext context;
        private DailyRegMapper mapper;
        public DailyRegRepository(IContext ctx)
        {
            context = ctx;
            mapper = new DailyRegMapper(context);
        }

        public bool Delete(IDailyReg value)
        {
            return mapper.Delete(value);
        }

        public IDailyReg Find(params object[] keys)
        {
            KeyValuePair<string, DateTime> key = new KeyValuePair<string, DateTime>((string)keys[0], (DateTime)keys[1]);
            return mapper.Read(key);
        }

        public IDailyReg Insert(IDailyReg value)
        {
            return mapper.Create(value);
        }

        public bool Update(IDailyReg value)
        {
            return mapper.Update(value);
        }

    }
}

