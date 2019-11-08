create view PORTFOLIO_SUMMARY as
select PORTFOLIO.name, T1.NoInstruments, PORTFOLIO.totalval from (select name, count(isin) as NoInstruments from POSITION group by name) T1
JOIN
PORTFOLIO
on T1.name = PORTFOLIO.name
go
select * from PORTFOLIO_SUMMARY
drop view PORTFOLIO_SUMMARY