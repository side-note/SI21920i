create function dbo.Portfolio_List(@name varchar(50))
    returns table
    as
    return
        (
        select isin,
               quantity,
               isnull((select dbo.get_Currval(isin)), 0)      as CurrVal,
               isnull((select dbo.get_dailypercvar(isin)), 0) as Dailyvarperc
        from Position
        where name = @name
        )
go

select * from Instrument
select * from DailyReg where isin=333333333333