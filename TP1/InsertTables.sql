insert into MARKET values (012, 'Descri��o', 'Caf� Cardeal');
insert into MARKET values (123, 'Restaura��o', 'Restaurante do Chico Manel');
insert into MARKET values (234, 'Acess�rios', 'Guida Colorida');

insert into	DAILYMARKET values (300, 400, 300,012,'2019-11-04');
insert into	DAILYMARKET values (500, 400, 500,123,'2019-11-03');
insert into	DAILYMARKET values (300, 200, 300,234,'2019-11-02');

insert into CLIENT values (247664294, 1482405, 'Nuno Cardeal');
insert into CLIENT values (111222333, 1234567, 'Carolina Couto');
insert into CLIENT values (333222111, 7654321, 'Joana Campos');
insert into CLIENT values (123456789, 1230456, 'Some One');

insert into PORTFOLIO values ('Nuno Cardeal', 50);
insert into PORTFOLIO values ('Carolina Couto', 20000);
insert into PORTFOLIO values ('Joana Campos', 2000000);

insert into CLIENT_PORTFOLIO values('Nuno Cardeal', 247664294);
insert into CLIENT_PORTFOLIO values('Carolina Couto', 111222333);
insert into CLIENT_PORTFOLIO values('Joana Campos', 333222111);

insert into INSTRUMENT values (112233445566, 'ALGO',012);
insert into INSTRUMENT values (111222333444, 'ALGO2',123);
insert into INSTRUMENT values (111122223333, 'ALGO3',234);

insert into POSITION values (1, 'Nuno Cardeal', 112233445566);
insert into POSITION values (1, 'Carolina Couto', 111222333444);
insert into POSITION values (1, 'Joana Campos', 111122223333);

insert into EMAIL values (1,'Nuno mail','ncardeal@pilim.com');
insert into EMAIL values (2,'Carol mail','carolct@pilim.com');
insert into EMAIL values (3,'Juju mail','jujucmps@pilim.com');

insert into PHONE values (1,'Nuno phone', '+351', 961222333);
insert into PHONE values (2,'Carol phone', '+351', 911222333);
insert into PHONE values (3,'Juju phone', '+351', 921222333);

insert into EXTTRIPLE values (140, '2019-11-4 08:43:12', 112233445566);
insert into EXTTRIPLE values (123, '2019-11-4 12:54:13', 112233445566);
insert into EXTTRIPLE values (135, '2019-11-4 12:55:08', 112233445566);
insert into EXTTRIPLE values (178, '2018-12-14 12:55:08', 111222333444);
insert into EXTTRIPLE values (155, '2018-12-14 07:50:08', 111222333444);
insert into EXTTRIPLE values (165, '2018-12-14 22:10:08', 111222333444);
insert into EXTTRIPLE values (199, '2018-12-14 12:55:08', 111222333888);

insert into DAILYREG values (112233445566, 100, 120, 500, 300, '2019-11-04');
insert into DAILYREG values (112233445566, 100, 120, 500, 300, '2019-08-03');
insert into DAILYREG values (112233445566, 100, 120, 500, 300, '2019-11-05');
insert into DAILYREG values (112233445566, 100, 120, 500, 300, '2019-02-02');
insert into DAILYREG values (111222333444, 100, 120, 500, 234, '2019-02-03');
insert into DAILYREG values (111222333444, 100, 120, 500, 345, '2019-02-07');
insert into DAILYREG values (111222333444, 100, 120, 500, 275, '2019-02-01');
insert into DAILYREG values (111122223333, 100, 120, 500, 400, '2019-05-01');
insert into DAILYREG values (111122223333, 100, 120, 500, 700, '2019-05-03');
insert into DAILYREG values (111122223333, 100, 120, 500, 100, '2019-05-09');

select * from phone
delete from DAILYREG

delete from EXTTRIPLE
delete from DAILYREG
select * from Sys.dm_tran_database_transactions
select * from DAILYREG

