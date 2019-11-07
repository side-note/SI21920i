create function dbo.UpdateClient(@nif decimal(9), @ncc decimal(7), @name varchar(50))
returns void
as
    begin
        update dbo.CLIENT
        set ncc = @ncc,
            name = @name
        where @nif = dbo.CLIENT.nif
    end
go

create function dbo.UpdateMarket(@code int, @description varchar(300), @name varchar(50))
returns void
as
    begin
        update dbo.MARKET
        set description = @description,
			name = @name
        where @code = dbo.MARKET.code
    end
go

create procedure p_actualizaValorDiario
   @id char(12)
as
    begin
		declare @minval money, @maxval money, @openingval money, @closingval money
		set @minval = (select top 1 value from dbo.EXTTRIPLE where @id = id order by value)
        set @maxval = (select top 1 value from dbo.EXTTRIPLE where @id = id order by value desc)
        set @openingval = (select top 1 value from dbo.EXTTRIPLE where @id = id order by datetime)
        set @closingval = (select top 1 value from dbo.EXTTRIPLE where @id = id order by datetime desc)
        update dbo.DAILYREG
        set minval = @minval,
            maxval = @maxval,
            openingval = @openingval,
            closingval = @closingval
        where exists(select isin from dbo.EXTTRIPLE where CONVERT(date, dbo.EXTTRIPLE.datetime) =dailydate and isin=@id);

		if not exists(select dbo.INSTRUMENT.isin from dbo.INSTRUMENT join dbo.DAILYREG on dbo.INSTRUMENT.isin = dbo.DAILYREG.isin and dbo.INSTRUMENT.isin = @id)
			begin
				insert into dbo.DAILYREG values
				(@id,
				@minval,
				@openingval,
				@maxval,
				@closingval,
				CONVERT(date,(select top 1 datetime from dbo.EXTTRIPLE where dbo.EXTTRIPLE.id = @id order by datetime desc)))
			end	
    end

go

exec p_actualizaValorDiario @id = 111222333888
select * from dbo.DAILYREG
delete from DAILYREG
drop procedure p_actualizaValorDiario
GO

create function dbo.Average(@days int,@isin char(12))
returns money
as 
begin
declare @initdate date
set @initdate = getdate() - @days
declare @average money
set @average = (select AVG(closingval) from dbo.DAILYREG where DAILYREG.isin = @isin and dailydate >= @initdate)
return @average 
end
Go


create function dbo.FundamentalDataTable(@isin char(12), @date date)
returns @ret table (dailyvar money, currval money, avg6m money, var6m money, dailyvarperc decimal(5,2), var6mperc decimal(5,2))
as
begin
	declare @dailyvar money 
	set @dailyvar = (select maxval from dbo.DAILYREG where isin = @isin and dailydate = @date) - (select minval from dbo.DAILYREG where isin = @isin and dailydate = @date)
    declare @var6m money
	set @var6m = (select top 1 maxval from dbo.DAILYREG where dailydate >= (getdate()-180) order by maxval desc) - (select top 1 minval from dbo.DAILYREG where dailydate >= (getdate()-180) order by minval)
	insert into @ret
	values(
		@dailyvar,
        dbo.get_Currval(@isin),
        dbo.Average(180, @isin),
		@var6m,
		@dailyvar / (select minval from dbo.DAILYREG where isin = @isin and dailydate = @date),
		@var6m / (select top 1 minval from dbo.DAILYREG where dailydate >= (getdate()-180) order by minval)
	)
	return
end
GO

select * from dbo.FundamentalDataTable(112233445566, '2019-11-04')
drop function dbo.FundamentalDataTable

GO
create procedure createPortfolio 
@nif decimal(9)
as
begin
	declare @name varchar(50)
	set @name = concat(@nif, '_portfolio')
	if not exists(select nif from dbo.CLIENT_PORTFOLIO where nif = @nif) and exists(select nif from dbo.CLIENT where nif = @nif)
	begin
		insert into dbo.PORTFOLIO(name) 
		values(@name)
	end
end
GO

exec dbo.createPortfolio @nif = 123456789
select * from dbo.PORTFOLIO
drop procedure dbo.createPortfolio
delete from PORTFOLIO where nif = 123456789
go

create procedure dbo.UpdateTotalVal
	@name varchar(50),
	@quantity int,
	@isin char(12)
as
begin
	insert into dbo.POSITION values(
		@quantity,
		@name,
		@isin
	)
	update dbo.PORTFOLIO
		set totalval = (select sum(quantity * closingval)
		from dbo.POSITION join dbo.DAILYREG on POSITION.isin = dbo.DAILYREG.isin
		where name = @name and dailydate = (select top 1 dailydate from dbo.DAILYREG where isin = dbo.POSITION.isin order by dailydate desc))
	where name = @name	
end
go

exec dbo.UpdateTotalVal @name = 'Carolina Couto', @quantity = 2, @isin = 111122223333
select * from dbo.PORTFOLIO
drop procedure dbo.UpdateTotalVal

go
create function dbo.get_Currval(@isin char(12))
returns money
as
begin
return (select top 1 closingval from dbo.DAILYREG where isin = @isin order by dailydate desc)
end
go
create function Portfolio_List(@name varchar(50))
returns table
as
return (select isin, quantity, (select dbo.get_Currval(isin)) as CurrVal,(select dbo.get_dailypercvar(isin)) as Dailyvarperc from POSITION where name=@name)

go
create function dbo.get_dailypercvar(@isin char(12))
returns decimal(5,2)
as 
begin
declare @current_closingval money
set @current_closingval=dbo.get_Currval(@isin)
declare @last_closingval money
set @last_closingval = (select closingval from (select top 2 closingval, ROW_NUMBER() over (order by dailydate desc) as rn from DAILYREG where isin=@isin) as cte where rn = 2)
return  @current_closingval/@last_closingval*100
end
go
select * from dbo.Portfolio_List('Carolina Couto')
drop function Portfolio_List