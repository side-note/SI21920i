create procedure update_market
    @description varchar(300),
    @name varchar(50),
    @code int
as
set transaction isolation level read committed
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

