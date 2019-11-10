--d)
exec update_client @ncc = 1111112, @nif = 111111111, @name = 'Samsung'
exec update_client @ncc = 3333333, @nif = 333333333, @name =  'Client3'
exec remove_client @nif = 222222222

--e)
exec update_market @description = 'This is the new description', @name = 'IphoneMarket', @code = '111'
exec update_market @description = 'Description3', @name = 'Market3', @code = '333'
exec remove_market @code = 222

--f)
exec p_actualizaValorDiario @id = 111111111111, @date ='2019-11-01 13:13:13'
exec p_actualizaValorDiario @id = 222222222222, @date = '2019-10-02 13:13:13'
exec p_actualizaValorDiario @id = 444444444444, @date= '2019-12-14 12:55:08'
--g)
select dbo.Average(180, 111111111111)

--h)
select * from dbo.FundamentalDataTable(111111111111, '2019-11-01')

--i)
exec createPortfolio @nif = 555555555

--j)
exec UpdateTotalVal @name = '555555555_portfolio', @quantity = 50000, @isin = 111111111111

--k)
select * from Portfolio_List('111111111_portfolio')

--l)
select * from PORTFOLIO_SUMMARY

