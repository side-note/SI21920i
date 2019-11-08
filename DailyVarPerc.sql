create function dbo.get_dailypercvar(@isin char(12))
    returns decimal(5, 2)
as
begin
    declare @current_closingval money
    set @current_closingval = dbo.get_Currval(@isin)
    declare @last_closingval money
    set @last_closingval = (select closingval
                            from (select
                                  top 2
                                  closingval
                                  ,
                                  ROW_NUMBER() over (order by dailydate desc) as rn
                                  from DAILYREG
                                  where isin = @isin) as cte
                            where rn = 2)
    return @current_closingval / @last_closingval * 100
end
go
