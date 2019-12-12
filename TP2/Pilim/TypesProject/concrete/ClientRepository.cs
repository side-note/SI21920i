﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesProject.dal;
using TypesProject.mapper;
using TypesProject.model;

namespace TypesProject.concrete
{
    class ClientRepository : IClientRepository
    {
        private IContext context;
        public ClientRepository(IContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<Client> Find(Func<Client, bool> criteria)
        {
            //Implementação muito pouco eficiente.  
            return FindAll().Where(criteria);
        }

        public IEnumerable<Client> FindAll()
        {
            return new ClientMapper(context).ReadAll();
        }
    }
}

