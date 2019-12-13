using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesProject.model;

namespace TypesProject.mapper
{
    interface IEmailMapper : IMapper<Email, int?, List<Email>>
    {
    }
}
