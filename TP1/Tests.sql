--d)
exec update_client @ncc = 1111112, @nif = 111111111, @name = 'Samsung'
exec update_client @ncc = 3333333, @nif = 333333333, @name =  'Client3'
exec remove_client @nif = 222222222

--e)
exec update_market @description = 'This is the new description', @name = 'IphoneMarket', @code = '111'
exec update_market @description = 'Description4', @name = 'Market4', @code = '444'
exec remove_market @code = 222
select * from Market
select * from DailyMarket
--f)
exec p_actualizaValorDiario @id = 111111111111, @date ='2019-11-01 13:13:13'
exec p_actualizaValorDiario @id = 222222222222, @date = '2019-10-02 13:13:13'
exec p_actualizaValorDiario @id = 444444444444, @date= '2019-12-14 12:55:08'

select * from DailyReg

--g)
select dbo.Average(180, 111111111111)
select * from DAILYREG
select * from EXTTRIPLE
--h)
select * from dbo.FundamentalDataTable(111111111111, '2019-11-01')

--i)
exec createPortfolio @nif = 555555555

--j)
exec UpdateTotalVal @name = '555555555_portfolio', @quantity = 50000, @isin = 111111111111
select * from Portfolio

delete from Portfolio where name ='555555555_portfolio'
delete from position where name ='555555555_portfolio'

--k)
select * from Portfolio_List('111111111_portfolio')

--l)
select * from Portfolio_Summary



