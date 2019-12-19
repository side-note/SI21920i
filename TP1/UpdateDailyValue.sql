create procedure dbo.p_actualizaValorDiario @id char(12), @date datetime
as
begin
    declare @minval money, @maxval money, @openingval money, @closingval money
    set @minval = (select top 1 value from dbo.Exttriple where @id= id and convert(date, datetime) = convert(date, @date) order by value)
    set @maxval = (select top 1 value from dbo.Exttriple where @id = id and convert(date, datetime) = convert(date, @date) order by value desc)
    set @openingval = (select top 1 value from dbo.Exttriple where @id = id and convert(date, datetime) = convert(date, @date) order by datetime)
    set @closingval = (select top 1 value from dbo.Exttriple where @id = id and convert(date, datetime) = convert(date, @date) order by datetime desc)
    if exists(select distinct isin, dailydate from dbo.DailyReg join dbo.Exttriple on isin = id where convert(date,@date) = dailydate and isin = @id)
        begin
            update dbo.DailyReg
            set minval     = @minval,
                maxval     = @maxval,
                openingval = @openingval,
                closingval = @closingval
            where isin = @id and dailydate = CONVERT(date,@date)
        end;
    if exists(select isin from Instrument where isin = @id)
        begin
    if not exists((select isin from DailyReg where dailydate = CONVERT(date, @date)))
        begin
            insert into dbo.DailyReg
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

