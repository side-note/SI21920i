using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesProject.mapper;
using TypesProject.model;

namespace TypesProject.concrete
{
    class PhoneMapper : AbstractMapper<Phone, int?, List<Phone>>,IPhoneMapper
    {
        public PhoneMapper(IContext ctx) : base(ctx)
        {
        }
        internal Client LoadClient(Phone p)
        {
            ClientMapper cm = new ClientMapper(context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@id", p.code));

            using (IDataReader rd = ExecuteReader("select client from phone where phoneId=@id", parameters))
            {
                if (rd.Read())
                {
                    int key = rd.GetInt32(0);
                    return cm.Read(key);
                }
            }
            return null;

        }
        protected override string DeleteCommandText
        {
            get
            {
                return "delete from Phone where phoneId=@id";
            }
        }

        protected override string InsertCommandText
        {
            get
            {
                return "INSERT INTO Phone (code, areacode, number, description) VALUES(@id, @area, @numb, @desc); select @id=code";
            }
        }

        protected override string SelectAllCommandText
        {
            get
            {
                return "select code, areacode, number, description from Phone";
            }
        }

        protected override string SelectCommandText
        {
            get
            {
                return String.Format("{0} where phoneId=@id", SelectAllCommandText); ;
            }
        }

        protected override string UpdateCommandText
        {
            get
            {
                return "update Phone set number=@numb, areacode=@area, description=@desc where phoneId=@id";
            }
        }

        protected override void DeleteParameters(IDbCommand cmd, Phone p)
        {

            SqlParameter id= new SqlParameter("@id", p.code);
            cmd.Parameters.Add(id);
        }

        protected override void InsertParameters(IDbCommand cmd, Phone p)
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


        protected override void SelectParameters(IDbCommand cmd, int? k)
        {
            SqlParameter id = new SqlParameter("@id", k);
            cmd.Parameters.Add(id);
        }

        protected override Phone UpdateEntityID(IDbCommand cmd, Phone p)
        {
            var param = cmd.Parameters["@id"] as SqlParameter;
            p.code = int.Parse(param.Value.ToString());
            return p;
        }

        protected override void UpdateParameters(IDbCommand cmd, Phone p)
        {
            InsertParameters(cmd, p);
        }

        protected override Phone Map(IDataRecord record)
        {
            Phone p = new Phone();
            p.code = record.GetInt32(0);
            p.description = record.GetString(1);
            p.areacode = record.GetString(2);
            p.number = record.GetInt32(3);
            return new PhoneProxy(p, context);
        }
        public override Phone Create(Phone phone)
        {

            return new PhoneProxy(phone, context);

        }


        public override Phone Update(Phone phone)
        {
            return new PhoneProxy(base.Update(phone), context);
        }
    }
}

