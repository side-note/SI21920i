create function dbo.UpdateClient(@nif decimal(9), @ncc decimal(7), @name varchar(50))
returns table
with schemabinding
as
    begin
        update dbo.CLIENT
        set ncc = @ncc,
            name = @name
        where @nif = dbo.CLIENT.nif
    end
go
create function dbo.UpdateMarket(@code int, @description varchar(300), @name varchar(50))
returns table
with schemabinding
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
        update dbo.DAILYREG
        set minval = (select top 1 value from dbo.EXTTRIPLE order by value),
            maxval = (select top 1 value from dbo.EXTTRIPLE order by value desc),
            openingval = (select top 1 value from dbo.EXTTRIPLE order by datetime),
            closingval = (select top 1 value from dbo.EXTTRIPLE order by datetime desc)
        where exists(select isin from dbo.EXTTRIPLE where CONVERT(date, dbo.EXTTRIPLE.datetime) =dailydate and isin=@id);
              --exist(isin = @id and dailydate = (CONVERT(date, dbo.EXTTRIPLE.datetime)))
    end

go

exec p_actualizaValorDiario @id = 112233445566

drop procedure p_actualizaValorDiario
select * from dbo.DAILYREG where exist(isin =112233445566 and dailydate = CONVERT(date, dbo.EXTTRIPLE.datetime))
delete DAILYREG
select * from DAILYREG

create function dbo.Average(@days int,@isin char(12))
returns money
as 
begin
declare @initdate date
set @initdate=getdate() - @days
declare @average money
set @average = (select AVG(closingval) from dbo.DAILYREG where DAILYREG.isin = @isin and dailydate >= @initdate)
return @average 
end
Go

create function dbo.FundamentalDataTable(@isin char(12))
returns @ret table (dailyvar money, currval money, avg6m money, var6m money, dailyvarperc decimal(5,2), var6mperc decimal(5,2))
with schemabinding
as
begin
    update @ret
    set dailyvar     = dbo.DAILYREG.maxval - dbo.DAILYREG.minval,
        currval      = (select top 1 dbo.DAILYREG.closingval from dbo.DAILYREG order by dailydate desc),
        avg6m        = Average(180, @isin)
    where dbo.DAILYREG.isin = @isin
end


















