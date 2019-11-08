create function dbo.Average(@days int, @isin char(12))
    returns money
as
begin
    declare @initdate date
    set @initdate = getdate() - @days
    declare @average money
    set @average = (select AVG(closingval) from dbo.DAILYREG where DAILYREG.isin = @isin and dailydate >= @initdate)
    return @average
end
Go
