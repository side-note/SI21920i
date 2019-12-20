using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using TypesProject.mapper;
using TypesProject.model;

namespace TypesProject.concrete
{
    public class PhoneMapper : IPhoneMapper
    {
        MapperHelper<IPhone, int, List<IPhone>> mapperHelper;
        public PhoneMapper(IContext ctx) 
        {
            mapperHelper = new MapperHelper<IPhone, int, List<IPhone>>(ctx, this);
        }
        internal IClient LoadClient(Phone p)
        {
            ClientMapper cm = new ClientMapper(mapperHelper.context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@id", p.code));

            using (IDataReader rd = mapperHelper.ExecuteReader("select client from phone where phoneId=@id", parameters))
            {
                if (rd.Read())
                {
                    int key = rd.GetInt32(0);
                    return cm.Read(key);
                }
            }
            return null;

        }
 
        protected void DeleteParameters(IDbCommand cmd, IPhone p)
        {

            SqlParameter id= new SqlParameter("@id", p.code);
            cmd.Parameters.Add(id);
        }

        protected void InsertParameters(IDbCommand cmd, IPhone p)
        {
            SqlParameter ac = new SqlParameter("@area", p.areacode);
            SqlParameter id = new SqlParameter("@id", p.code);
            SqlParameter d = new SqlParameter("@desc", p.description);
            SqlParameter nb = new SqlParameter("@numb", p.number);

            id.Direction = ParameterDirection.InputOutput;

            cmd.Parameters.Add(ac);
            cmd.Parameters.Add(id);
            cmd.Parameters.Add(d);
            cmd.Parameters.Add(nb);

        }


        protected void SelectParameters(IDbCommand cmd, int k)
        {
            SqlParameter id = new SqlParameter("@id", k);
            cmd.Parameters.Add(id);
        }

        protected void UpdateParameters(IDbCommand cmd, IPhone p)
        {
            InsertParameters(cmd, p);
        }

        public IPhone Map(IDataRecord record)
        {
            Phone p = new Phone();
            p.code = record.GetInt32(0);
            p.description = record.GetString(1);
            p.areacode = record.GetString(2);
            p.number = record.GetInt32(3);
            return new PhoneProxy(p, mapperHelper.context);
        }
        public IPhone Create(IPhone phone)
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                mapperHelper.Create(phone,
                    (cmd, phone) => InsertParameters(cmd, phone),
                    "INSERT INTO Phone (code, areacode, number, description) VALUES(@id, @area, @numb, @desc); select @id=code"
                    );
                ts.Complete();
                return phone;
            }

        }


        public bool Update(IPhone phone)
        {
            return mapperHelper.Update(phone,
                (cmd, phone) => UpdateParameters(cmd, phone),
               "update Phone set number=@numb, areacode=@area, description=@desc where phoneId=@id"
                );
        }

        public IPhone Read(int id)
        {
            return mapperHelper.Read(id,
              (cmd, i) => SelectParameters(cmd, i),
             "select code, areacode, number, description from Phone  where phoneId = @id"
              );
           
        }

        public List<IPhone> ReadAll()
        {
            return mapperHelper.ReadAll(
            cmd => { },
            "select code, areacode, number, description from Phone"
            );
        }

        public bool Delete(IPhone entity)
        {
            return mapperHelper.Delete(entity,
                (cmd, phone) => DeleteParameters(cmd, phone),
                 "delete from Phone where phoneId=@id"
                );
        }

        public List<IPhone> MapAll(IDataReader reader)
        {
            return mapperHelper.MapAll(reader);
        }
    }
}

