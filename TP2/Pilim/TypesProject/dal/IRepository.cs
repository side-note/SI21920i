using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypesProject.dal
{
    interface IRepository<T>
    {

        bool Update(T value, Func<T, bool> criteria);
        bool Delete(T value, Func<T, bool> criteria);
        T Insert(T value);
        IEnumerable<T> FindAll();
        IEnumerable<T> Find(Func<T, bool> criteria);
    }
}
