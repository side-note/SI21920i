create procedure dbo.Dailymarketupdate @code int, @date date
    as
    begin
     declare   @idxmrkt money, @dailyvar money,@idxopeningval money
     set @idxmrkt = (select sum(openingval) from DAILYREG join INSTRUMENT on DAILYREG.isin = INSTRUMENT.isin where mrktcode = 234 and dailydate = @date)
     set @idxopeningval = (select top 1 idxmrkt from DAILYMARKET where date <> @date order by date desc)
     set @dailyvar = (select  maxval from dbo.DAILYREG join INSTRUMENT on DAILYREG.isin = INSTRUMENT.isin where mrktcode = @code and dailydate = @date) -
                    (select  minval from dbo.DAILYREG join INSTRUMENT on DAILYREG.isin = INSTRUMENT.isin where mrktcode = @code and dailydate = @date)
    update dbo.DAILYMARKET
    set
        idxmrkt = @idxmrkt,
        idxopeningval = @idxopeningval,
        dailyvar = @dailyvar
    where exists(select date from DAILYMARKET where code = @code and date=@date);
  if not exists( select date from DAILYMARKET where code =@code and date=@date)
        begin
            insert into dbo.DAILYMARKET
            values (
                    @idxmrkt,
                    @dailyvar,
                    @idxopeningval,
                    @code,
                    @date)
        end
    end
go
