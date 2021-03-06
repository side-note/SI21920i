﻿using System;
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
    public class PositionMapper : IPositionMapper
    {
        MapperHelper<IPosition, KeyValuePair<string, string>,List<IPosition>> mapperHelper;
        public PositionMapper(IContext ctx)
        {
            mapperHelper = new MapperHelper<IPosition, KeyValuePair<string, string>, List<IPosition>>(ctx, this);
        }

        internal IPortfolio LoadPortfolios(IPosition p)
        {
            PortfolioMapper pm = new PortfolioMapper(mapperHelper.context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@id", p.name));
            using (IDataReader rd = mapperHelper.ExecuteReader("select name, totalval from Portfolio where name=@id", parameters))
            {
                if (rd.Read())
                    return pm.Read(rd.IsDBNull(0) ? default : rd.GetString(0));
            }
            return null;
        }

        internal IInstrument LoadInstruments(Position p)
        { 
            InstrumentMapper im = new InstrumentMapper(mapperHelper.context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@id", p.isin));
            using (IDataReader rd = mapperHelper.ExecuteReader("select isin from Instrument where isin=@id", parameters))
            {
               if (rd.Read())
                    return im.Read(rd.IsDBNull(0) ? default : rd.GetString(0));
            }
            return null;
        }

        public IPosition Create(IPosition entity)
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                mapperHelper.Create(entity,
                    (cmd, position) => InsertParameters(cmd, position),
                     "INSERT INTO Position (name, isin, quantity) VALUES(@id2, @id1, @quantity); select @id1=isin and @id2=name"
                    );
                ts.Complete();
                return entity;
            }
        }

        private void InsertParameters(IDbCommand cmd, IPosition p)
        {
            SqlParameter id2 = new SqlParameter("@id2", p.name);
            SqlParameter id1 = new SqlParameter("@id", p.isin);
            SqlParameter q = new SqlParameter("@quantity", p.quantity);
            id1.Direction = ParameterDirection.InputOutput;
            id2.Direction = ParameterDirection.InputOutput;

            cmd.Parameters.Add(id1);
            cmd.Parameters.Add(id2);
            cmd.Parameters.Add(q);
        }

        public bool Delete(IPosition entity)
        {
            return mapperHelper.Delete(entity,
               (cmd, position) => DeleteParameters(cmd, position),
               "delete from Position where isin=@id1 and name=@id2"
               );
        }

        private void DeleteParameters(IDbCommand cmd, IPosition p)
        {
            SqlParameter id1 = new SqlParameter("@id1", p.isin);
            SqlParameter id2 = new SqlParameter("@id2", p.name);
            cmd.Parameters.Add(id1);
            cmd.Parameters.Add(id2);
        }

        public IPosition Map(IDataRecord record)
        {
            Position p = new Position();
            p.quantity = record.IsDBNull(0) ? default : record.GetInt32(0);
            p.isin = record.IsDBNull(1) ? default : record.GetString(1);
            p.name = record.IsDBNull(2) ? default : record.GetString(2);
            return p;
        }

        public List<IPosition> MapAll(IDataReader reader)
        {
            return mapperHelper.MapAll(reader);
        }

        public IPosition Read(KeyValuePair<string, string> id)
        {
            return mapperHelper.Read(id,
                (cmd, i) => SelectParameters(cmd, i),
                "select isin, name, quantity from Position where isin=@id1 and name=@id2"
                );
        }

        private void SelectParameters(IDbCommand cmd, KeyValuePair<string, string> i)
        {
            SqlParameter id1 = new SqlParameter("@id1", i.Key);
            SqlParameter id2 = new SqlParameter("@id2", i.Value);
            cmd.Parameters.Add(id1);
            cmd.Parameters.Add(id2);
        }

        public List<IPosition> ReadAll()
        {
            return mapperHelper.ReadAll(
                cmd => { },
                "select isin, name, quantity from Position"
                );
        }

        public bool Update(IPosition entity)
        {
            return mapperHelper.Update(entity,
                (cmd, position) => UpdateParameters(cmd, position),
                "update Position set quantity = @quantity where isin = @id1 and name = @id2"
                );
        }

        private void UpdateParameters(IDbCommand cmd, IPosition p)
        {
            InsertParameters(cmd, p);
        }
    }
}
