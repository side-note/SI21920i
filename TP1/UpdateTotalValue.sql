create procedure dbo.UpdateTotalVal @name varchar(50),
                                    @quantity int,
                                    @isin char(12)
as
begin
    insert into dbo.Position
    values (@quantity,
            @name,
            @isin)
    update dbo.Portfolio
    set totalval = (select sum(quantity * closingval)
                    from dbo.Position
                             join dbo.DailyReg on Position.isin = dbo.DailyReg.isin
                    where name = @name
                      and dailydate = (select
                                       top 1
                                       dailydate
                                       from dbo.DailyReg
                                       where isin = dbo.Position.isin
                                       order by dailydate desc))
    where name = @name
end
go