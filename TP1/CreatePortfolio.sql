create procedure dbo.createPortfolio @nif decimal(9)
as
begin
    declare @name varchar(50)
    set @name = concat(@nif, '_portfolio')
    if not exists(select nif from dbo.Client_Portfolio where nif = @nif) and
       exists(select nif from dbo.Client where nif = @nif)
        begin
            insert into dbo.Portfolio(name)
            values (@name)
        end
end
GO