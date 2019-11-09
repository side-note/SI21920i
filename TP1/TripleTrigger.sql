create trigger dbo.EXTTRIPLE_trigger
    on dbo.EXTTRIPLE
    for insert
    as
begin
    declare @id char(12),
        @date datetime
    select @id = id, @date = datetime from inserted
    declare  @code int
    set @code = (select mrktcode from INSTRUMENT where isin = @id)
    exec dbo.p_actualizaValorDiario @id
    select * from dbo.FundamentalDataTable(@id, convert(date, @date))
    exec dbo.Dailymarketupdate @code,@date
end
go

