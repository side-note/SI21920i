create procedure dbo.p_actualizaValorDiario @id char(12)
as
begin
    declare @minval money, @maxval money, @openingval money, @closingval money
    set @minval = (select top 1 value from dbo.EXTTRIPLE where @id = id order by value)
    set @maxval = (select top 1 value from dbo.EXTTRIPLE where @id = id order by value desc)
    set @openingval = (select top 1 value from dbo.EXTTRIPLE where @id = id order by datetime)
    set @closingval = (select top 1 value from dbo.EXTTRIPLE where @id = id order by datetime desc)
    update dbo.DAILYREG
    set minval     = @minval,
        maxval     = @maxval,
        openingval = @openingval,
        closingval = @closingval
    where exists(select isin from dbo.DAILYREG join dbo.EXTTRIPLE on id=isin where CONVERT(date, dbo.EXTTRIPLE.datetime) = dailydate and isin = @id);

    if not exists(select dbo.INSTRUMENT.isin
                  from dbo.INSTRUMENT
                           join dbo.DAILYREG on dbo.INSTRUMENT.isin = dbo.DAILYREG.isin and dbo.INSTRUMENT.isin = @id)
        begin
            insert into dbo.DAILYREG
            values (@id,
                    @minval,
                    @openingval,
                    @maxval,
                    @closingval,
                    CONVERT(date, (select
                                   top 1
                                   datetime
                                   from dbo.EXTTRIPLE
                                   where dbo.EXTTRIPLE.id = @id
                                   order by datetime desc)))
        end
end

go
