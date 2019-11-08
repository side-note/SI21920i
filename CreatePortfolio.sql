create procedure dbo.createPortfolio @nif decimal(9)
as
begin
    declare @name varchar(50)
    set @name = concat(@nif, '_portfolio')
    if not exists(select nif from dbo.CLIENT_PORTFOLIO where nif = @nif) and
       exists(select nif from dbo.CLIENT where nif = @nif)
        begin
            insert into dbo.PORTFOLIO(name)
            values (@name)
        end
end
GO