--Tables
drop table if exists PHONE;
drop table if exists EMAIL;
drop table if exists POSITION;
drop table if exists INSTRUMENT;
drop table if exists CLIENT_PORTFOLIO;
drop table if exists PORTFOLIO;
drop table if exists CLIENT;
drop table if exists DAILYMARKET;
drop table if exists MARKET;
drop table if exists EXTTRIPLE;
drop table if exists DAILYREG;
--Functions
drop function if exists dbo.UpdateMarket
drop function if exists dbo.UpdateClient
drop function if exists dbo.Average
drop function if exists dbo.FundamentalDataTable
drop function if exists  dbo.get_Currval
drop function if exists dbo.Portfolio_List
drop function if exists dbo.get_dailypercvar
--Procedures
drop procedure if exists dbo.Dailymarketupdate
drop procedure if exists dbo.p_actualizaValorDiario
drop procedure if exists dbo.createPortfolio
drop procedure if exists dbo.UpdateTotalVal
--Triggers
drop trigger if exists dbo.EXTTRIPLE_trigger