using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypesProject.dal
{
   public  interface IRepository<T>
    {

        bool Update(T value);
        bool Delete(T value);
        T Insert(T value);
        T Find(params object[] keys);
    }
}
