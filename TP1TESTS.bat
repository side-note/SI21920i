@ECHO OFF

sqlcmd -S 10.62.73.95 -U TL52D_14 -P CJN1920i -i "./TP1/DropTables.sql"
sqlcmd -S 10.62.73.95 -U TL52D_14 -P CJN1920i -i "./TP1/CreateTables.sql"
sqlcmd -S 10.62.73.95 -U TL52D_14 -P CJN1920i -i "./TP1/InsertTables.sql"

sqlcmd -S 10.62.73.95 -U TL52D_14 -P CJN1920i -i "./TP1/UpdateClient.sql"
sqlcmd -S 10.62.73.95 -U TL52D_14 -P CJN1920i -i "./TP1/RemoveClient.sql"
sqlcmd -S 10.62.73.95 -U TL52D_14 -P CJN1920i -i "./TP1/UpdateMarket.sql"
sqlcmd -S 10.62.73.95 -U TL52D_14 -P CJN1920i -i "./TP1/RemoveMarket.sql"
sqlcmd -S 10.62.73.95 -U TL52D_14 -P CJN1920i -i "./TP1/CurrentValue.sql"
sqlcmd -S 10.62.73.95 -U TL52D_14 -P CJN1920i -i "./TP1/DailyVarPerc.sql"
sqlcmd -S 10.62.73.95 -U TL52D_14 -P CJN1920i -i "./TP1/UpdateDailyValue.sql"
sqlcmd -S 10.62.73.95 -U TL52D_14 -P CJN1920i -i "./TP1/Average.sql"
sqlcmd -S 10.62.73.95 -U TL52D_14 -P CJN1920i -i "./TP1/UpdateDailyMarket.sql"
sqlcmd -S 10.62.73.95 -U TL52D_14 -P CJN1920i -i "./TP1/FundamentalData.sql"
sqlcmd -S 10.62.73.95 -U TL52D_14 -P CJN1920i -i "./TP1/CreatePortfolio.sql"
sqlcmd -S 10.62.73.95 -U TL52D_14 -P CJN1920i -i "./TP1/UpdateTotalValue.sql"
sqlcmd -S 10.62.73.95 -U TL52D_14 -P CJN1920i -i "./TP1/PortfolioList.sql"







