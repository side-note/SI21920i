create procedure update_client
    @ncc decimal(7),
    @nif decimal(9),
    @name varchar(50)
as
set transaction isolation level read committed
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

