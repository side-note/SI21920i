set transaction isolation level read committed

create procedure update_market
    @description varchar(300),
    @name varchar(50),
    @code int
as
begin tran
    if exists(select code from MARKET where code = @code)
        begin
            update dbo.MARKET
            set description = @description,
                name = @name
            where @code = dbo.MARKET.code
        end
    else
        begin
            insert into MARKET values
            (
            @code,
            @description,
            @name
            )
        end
commit 

create procedure remove_market
    @code int
as
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