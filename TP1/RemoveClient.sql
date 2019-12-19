create procedure remove_client
    @nif decimal(9)
as
set transaction isolation level read committed
begin tran
    if exists(select nif from Client where @nif = nif)
        begin
            delete from Client where nif = @nif
            delete from Client_PORTFOLIO where nif = @nif
        end
commit