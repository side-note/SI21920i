create procedure dbo.p_actualizaValorDiario @id char(12), @date datetime
as
begin
    declare @minval money, @maxval money, @openingval money, @closingval money
    set @minval = (select top 1 value from dbo.EXTTRIPLE where @id= id and convert(date, datetime) = convert(date, @date) order by value)
    set @maxval = (select top 1 value from dbo.EXTTRIPLE where @id = id and convert(date, datetime) = convert(date, @date) order by value desc)
    set @openingval = (select top 1 value from dbo.EXTTRIPLE where @id = id and convert(date, datetime) = convert(date, @date) order by datetime)
    set @closingval = (select top 1 value from dbo.EXTTRIPLE where @id = id and convert(date, datetime) = convert(date, @date) order by datetime desc)
    if exists(select distinct isin, dailydate from dbo.DAILYREG join dbo.EXTTRIPLE on isin = id where convert(date,@date) = dailydate and isin = @id)
        begin
            update dbo.DAILYREG
            set minval     = @minval,
                maxval     = @maxval,
                openingval = @openingval,
                closingval = @closingval
            where isin = @id and dailydate = CONVERT(date,@date)
        end;
    if exists(select isin from INSTRUMENT where isin = @id)
        begin
    if not exists((select isin from DAILYREG where dailydate = CONVERT(date, @date)))
        begin
            insert into dbo.DAILYREG
            values (@id,
                    @minval,
                    @openingval,
                    @maxval,
                    @closingval,
                    CONVERT(date, @date))
        end
    end
end

go

