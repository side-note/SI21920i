create procedure remove_market
    @code int
as
set transaction isolation level read committed
begin tran
    if exists(select code from Market where @code = code)
        begin
            delete from DailyReg where isin = (select isin from INSTRUMENT where mrktcode = @code)
            delete from Position where isin = (select isin from INSTRUMENT where mrktcode = @code)
            delete from Instrument where mrktcode = @code
            delete from DailyMarket where code = @code
            delete from MARKET where code = @code
        end
commit