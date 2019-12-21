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
     public  class PortfolioMapper: IPortfolioMapper
    {
        MapperHelper<IPortfolio, String, List<IPortfolio>> mapperHelper;
        public PortfolioMapper(IContext ctx)
        {
            mapperHelper = new MapperHelper<IPortfolio, string, List<IPortfolio>>(ctx, this);
        }

        internal IClient LoadClient(Portfolio p)
        {
            ClientMapper cm = new ClientMapper(mapperHelper.context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@id", p.name));

            using (IDataReader rd = mapperHelper.ExecuteReader("select client from email where emailId=@id", parameters))
            {
                if (rd.Read())
                {
                    int key = rd.GetInt32(0);
                    return cm.Read(key);
                }
            }
            return null;

        }

        internal ICollection<IPosition> LoadPositions(Portfolio p)
        {
            List<IPosition> lst = new List<IPosition>();

            InstrumentMapper im = new InstrumentMapper(mapperHelper.context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@id", p.name));
            using (IDataReader rd = mapperHelper.ExecuteReader("select isin, name, quantity from Position where name = @id", parameters))
            {
                while (rd.Read())
                {
                    Position pos = new Position();
                    pos.quantity = rd.GetInt32(0);
                    pos.name = rd.GetString(1);
                    pos.isin = rd.GetString(2);
                    KeyValuePair<string, string> key = new KeyValuePair<string, string>(rd.GetString(1),rd.GetString(2));
                    pos.Instrument = im.Read(rd.GetString(2));
                    pos.Portfolio = p;

                    lst.Add(pos);
                }
            }
            return lst;
        }

        protected void DeleteParameters(IDbCommand cmd, IPortfolio p)
        {

            SqlParameter id = new SqlParameter("@id",p.name);
            cmd.Parameters.Add(id);
        }

        protected void InsertParameters(IDbCommand cmd, IPortfolio p)
        {
            SqlParameter id = new SqlParameter("@id", p.name);
            SqlParameter tv = new SqlParameter("@total", p.totalval);
            id.Direction = ParameterDirection.InputOutput;

            cmd.Parameters.Add(id);
            cmd.Parameters.Add(tv);
        }


        protected  void SelectParameters(IDbCommand cmd, String k)
        {
            SqlParameter id = new SqlParameter("@id", k);
            cmd.Parameters.Add(id);
        }

        protected void UpdateParameters(IDbCommand cmd, IPortfolio p)
        {
            InsertParameters(cmd, p);
        }

        public  IPortfolio Map(IDataRecord record)
        {
            Portfolio p = new Portfolio();
            p.totalval = record.GetDecimal(0);
            p.name = record.GetString(1);
            return new PortfolioProxy( p, mapperHelper.context);

        }

       
        public IPortfolio Create(IPortfolio entity)
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                mapperHelper.Create(entity,
                    (cmd, portfolio) => InsertParameters(cmd, portfolio),
                     "INSERT INTO portfolio (name, totalval) VALUES(@id, @total); select @id=name"
                     );
                ts.Complete();
                return entity;
            }
        }

        public IPortfolio Read(string id)
        {
            return mapperHelper.Read(id,
                (cmd, i) => SelectParameters(cmd, i),
                "select name,totalval from Portfolio  where portfolioId=@id"
                );
        }

        public List<IPortfolio> ReadAll()
        {
            return mapperHelper.ReadAll(
                cmd => { },
                "select name,totalval from Portfolio"
                );
        }

        public bool Update(IPortfolio entity)
        {
            return mapperHelper.Update(entity,
                (cmd, portfolio) => UpdateParameters(cmd, portfolio),
                 "update Portfolio set totalval=@total where portfolioId=@id"
                );
        }

        public bool Delete(IPortfolio entity)
        {
            return mapperHelper.Delete(entity,
                (cmd, portfolio) => DeleteParameters(cmd, portfolio),
                "delete from Portfolio where potfolioId=@id"
                );
        }

        public List<IPortfolio> MapAll(IDataReader reader)
        {
            return mapperHelper.MapAll(reader);
        }
    }
}

