create function dbo.get_Currval(@isin char(12))
    returns money
as
begin
    return (select top 1 closingval from dbo.DAILYREG where isin = @isin order by dailydate desc)
end
go