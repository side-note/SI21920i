create procedure dbo.DailyMarketUpdate @code int, @date date
    as
    begin
     declare   @idxmrkt money, @dailyvar money, @idxopeningval money
     set @idxmrkt = (select sum(openingval) from DailyReg join Instrument on DailyReg.isin = Instrument.isin where mrktcode = @code and dailydate = @date)
     set @idxopeningval = (select top 1 idxmrkt from DailyMarket where date < @date and code = @code order by date desc)
     set @dailyvar = (select maxval from dbo.DailyReg join Instrument on DailyReg.isin = Instrument.isin where mrktcode = @code and dailydate = @date) -
                    (select minval from dbo.DailyReg join Instrument on DAILYREG.isin = Instrument.isin where mrktcode = @code and dailydate = @date)
     if exists(select date from DailyMarket where code = @code and date = @date)
         begin
            update dbo.DailyMarket
            set
                idxmrkt = @idxmrkt,
                idxopeningval = @idxopeningval,
                dailyvar = @dailyvar
            where code = @code and date = @date
    end

    if not exists(select date from DailyMarket where code = @code and date = @date)
        begin
            insert into dbo.DailyMarket
            values (
                    @idxmrkt,
                    @dailyvar,
                    @idxopeningval,
                    @code,
                    @date)
        end
    end

