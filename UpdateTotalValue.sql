create procedure dbo.UpdateTotalVal @name varchar(50),
                                    @quantity int,
                                    @isin char(12)
as
begin
    insert into dbo.POSITION
    values (@quantity,
            @name,
            @isin)
    update dbo.PORTFOLIO
    set totalval = (select sum(quantity * closingval)
                    from dbo.POSITION
                             join dbo.DAILYREG on POSITION.isin = dbo.DAILYREG.isin
                    where name = @name
                      and dailydate = (select
                                       top 1
                                       dailydate
                                       from dbo.DAILYREG
                                       where isin = dbo.POSITION.isin
                                       order by dailydate desc))
    where name = @name
end
go