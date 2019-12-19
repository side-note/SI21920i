create view Portfolio_Summary as
select Portfolio.name, T1.NoInstruments, Portfolio.totalval from (select name, count(isin) as NoInstruments from Position group by name) T1
JOIN
Portfolio
on T1.name = Portfolio.name
go