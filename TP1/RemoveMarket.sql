create procedure remove_market
    @code int
as
set transaction isolation level read committed
begin tran
    if exists(select code from MARKET where @code = code)
        begin
            delete from DAILYREG where isin = (select isin from INSTRUMENT where mrktcode = @code)
            delete from POSITION where isin = (select isin from INSTRUMENT where mrktcode = @code)
            delete from INSTRUMENT where mrktcode = @code
            delete from DAILYMARKET where code = @code
            delete from MARKET where code = @code
        end
commit