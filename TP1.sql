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

create procedure p_actualizaValorDiario
    @id char(12)
as
    begin
        update dbo.DAILYREG
        set minval = (select top 1 value from dbo.EXTTRIPLE order by value),
            maxval = (select top 1 value from dbo.EXTTRIPLE order by value desc),
            openingval = (select top 1 value from dbo.EXTTRIPLE order by date),
            closingval = (select top 1 value from dbo.EXTTRIPLE order by date desc)
        where exists(select isin from dbo.DAILYREG join dbo.EXTTRIPLE on isin = id where isin = @id)
    end



create function dbo.Average(@days int,@isin char(12))
returns money
as 
begin
declare @initdate date
set @initdate=getdate() - @days
declare @average money
set @average = (select AVG(closingval) from dbo.DAILYREG where DAILYREG.isin = @isin and date >= @initdate)
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
        currval      = (select top 1 dbo.DAILYREG.closingval from dbo.DAILYREG order by date desc),
        avg6m        = Average(180, @isin)
    where dbo.DAILYREG.isin = @isin
end


















