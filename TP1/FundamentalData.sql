create function dbo.FundamentalDataTable(@isin char(12), @date date)
    returns @ret table
                 (
                 dailyvar     money,
                 currval      money,
                 avg6m        money,
                 var6m        money,
                 dailyvarperc decimal(5, 2),
                 var6mperc    decimal(5, 2)
                 )
as
begin
    declare @dailyvar money
    set @dailyvar = (select maxval from dbo.DAILYREG where isin = @isin and dailydate = @date) -
                    (select minval from dbo.DAILYREG where isin = @isin and dailydate = @date)
    declare @var6m money
    set @var6m = (select top 1 maxval from dbo.DAILYREG where dailydate >= (getdate() - 180) order by maxval desc) -
                 (select top 1 minval from dbo.DAILYREG where dailydate >= (getdate() - 180) order by minval)
    insert into @ret
    values (@dailyvar,
            dbo.get_Currval(@isin),
            dbo.Average(180, @isin),
            @var6m,
            @dailyvar / (select minval from dbo.DAILYREG where isin = @isin and dailydate = @date),
            @var6m / (select top 1 minval from dbo.DAILYREG where dailydate >= (getdate() - 180) order by minval))
    return
end
GO