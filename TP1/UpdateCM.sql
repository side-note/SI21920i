
set transaction isolation level read committed

create procedure update_client
    @ncc decimal(7),
    @nif decimal(9),
    @name varchar(50)
as
begin transaction
    IF EXISTS(select nif from CLIENT where nif = @nif)
        begin
            update dbo.CLIENT
            set ncc  = @ncc,
            name = @name
            where @nif = dbo.CLIENT.nif
        end
    ELSE
        begin
            insert into dbo.CLIENT values
            (
             @nif,
             @ncc,
             @name
            )
        end
commit

EXEC update_client @ncc = 2345678,@nif = 333222111, @name = 'JoanaBanana'

select * from CLIENT
drop procedure update_client
create procedure remove_client
    @nif decimal(9)
as
begin tran
    if exists(select nif from CLIENT where @nif = nif)
        begin
            delete from CLIENT where nif = @nif
            delete from CLIENT_PORTFOLIO where nif = @nif
        end
commit
exec remove_client @nif = 333222111
drop procedure remove_client

begin tran @update_Market
    update dbo.MARKET
    set description = @description,
        name        = @name
    where @code = dbo.MARKET.code
commit