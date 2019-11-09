
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
