using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypesProject.mapper
{
    public interface IMapper<T, Tid, TCol>
    {
        T Create(T entity);
        T Read(Tid id);
        TCol ReadAll();
        bool Update(T entity);
        bool Delete(T entity);
        T Map(IDataRecord record);
        TCol MapAll(IDataReader reader);
    }
}
