create function dbo.get_dailypercvar(@isin char(12))
    returns decimal(5,2)
as
begin
    declare @current_closingval money
    set @current_closingval = dbo.get_Currval(@isin)
    declare @min money
    declare @max money
    declare @last_closingval money
    set @last_closingval = (select closingval from
                               (select top 2 closingval,
                                ROW_NUMBER() over (order by dailydate desc) as rn from DailyReg
                                where isin = @isin) as cte
                            where rn = 2)
    if @current_closingval < @last_closingval
        begin
            set @min = @current_closingval
            set @max = @last_closingval
        end
    else
        begin
            set @min = @last_closingval
            set @max = @current_closingval
        end
    return CONVERT(decimal(5,2), ((@max - @min)/@max) * 100)
end
go



