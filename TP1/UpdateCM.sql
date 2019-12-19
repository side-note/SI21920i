
set transaction isolation level read committed

create procedure update_client
    @ncc decimal(7),
    @nif decimal(9),
    @name varchar(50),
    @description varchar(300),
    @code int
as
begin transaction
    IF EXISTS(select nif from Client where nif = @nif)
        begin
            update dbo.Client
            set ncc  = @ncc,
            name = @name
            where @nif = dbo.Client.nif
        end
    ELSE
        begin
            insert into dbo.Client values
            (
             @nif,
             @ncc,
             @name
            )
        end
commit

EXEC update_client @ncc = 2345678,@nif = 333222111, @name = 'JoanaBanana'

select * from Client
drop procedure update_client
create procedure remove_client
    @nif decimal(9)
as
begin tran
    if exists(select nif from Client where @nif = nif)
        begin
            delete from Client where nif = @nif
            delete from Client_Portfolio where nif = @nif
        end
commit
exec remove_client @nif = 333222111
drop procedure remove_client

begin tran @update_Market
    update dbo.Market
    set description = @description,
        name        = @name
    where @code = dbo.Market.code
commit