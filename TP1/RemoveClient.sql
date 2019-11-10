create procedure remove_client
    @nif decimal(9)
as
set transaction isolation level read committed
begin tran
    if exists(select nif from CLIENT where @nif = nif)
        begin
            delete from CLIENT where nif = @nif
            delete from CLIENT_PORTFOLIO where nif = @nif
        end
commit