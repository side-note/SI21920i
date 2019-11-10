create procedure update_client
    @ncc decimal(7),
    @nif decimal(9),
    @name varchar(50)
as
set transaction isolation level read committed
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

