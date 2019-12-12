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
    class ExttripleRepository : IExttripleRepository
    {
        private IContext context;
        public ExttripleRepository(IContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<Exttriple> Find(System.Func<Exttriple, bool> criteria)
        {
            //Implementação muito pouco eficiente.  
            return FindAll().Where(criteria);
        }

        public IEnumerable<Exttriple> FindAll()
        {
            return new ExttripleMapper(context).ReadAll();
        }
    }
}

