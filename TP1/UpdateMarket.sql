create procedure update_market
    @description varchar(300),
    @name varchar(50),
    @code int
as
set transaction isolation level read committed
begin tran
    if exists(select code from Market where code = @code)
        begin
            update dbo.Market
            set description = @description,
                name = @name
            where @code = dbo.Market.code
        end
    else
        begin
            insert into Market values
            (
            @code,
            @description,
            @name
            )
        end
commit 

