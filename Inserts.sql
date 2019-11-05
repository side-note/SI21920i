insert into MARKET values (012, 'Descrição', 'Café Cardeal');
insert into MARKET values (123, 'Restauração', 'Restaurante do Chico Manel');
insert into MARKET values (234, 'Acessórios', 'Guida Colorida');

insert into	DAILYMARKET values (300, 400, 300);
insert into	DAILYMARKET values (500, 400, 500);
insert into	DAILYMARKET values (300, 200, 300);

insert into CLIENT values (247664294, 1482405, 'Nuno Cardeal');
insert into CLIENT values (111222333, 1234567, 'Carolina Couto');
insert into CLIENT values (333222111, 7654321, 'Joana Campos');


insert into PORTFOLIO values ('Nuno Cardeal', 50, 247664294);
insert into PORTFOLIO values ('Carolina Couto', 20000, 111222333);
insert into PORTFOLIO values ('Joana Campos', 2000000, 333222111);

insert into INSTRUMENT values (112233445566, 'ALGO');
insert into INSTRUMENT values (111222333444, 'ALGO2');
insert into INSTRUMENT values (111122223333, 'ALGO3');

insert into POSITION values (1, 'Nuno Cardeal', 112233445566, 247664294);
insert into POSITION values (1, 'Carolina Couto', 111222333444, 111222333);
insert into POSITION values (1, 'Joana Campos', 111122223333, 333222111);

insert into EMAIL values (1,'Nuno mail','ncardeal@pilim.com');
insert into EMAIL values (2,'Carol mail','carolct@pilim.com');
insert into EMAIL values (3,'Juju mail','jujucmps@pilim.com');

insert into PHONE values (1,'Nuno phone', '+351', 961222333);
insert into PHONE values (2,'Carol phone', '+351', 911222333);
insert into PHONE values (3,'Juju phone', '+351', 921222333);

insert into EXTTRIPLE values (234, '2019-11-4 08:43:12', 112233445566);
insert into EXTTRIPLE values (234, '2019-11-3 12:54:13', 112233445567);
insert into EXTTRIPLE values (234, '2019-11-2 12:55:08', 112233445568);

insert into DAILYREG values (112233445566, 100, 120, 500, 300, '2019-08-03');
insert into DAILYREG values (112233445566, 100, 120, 500, 300, '2019-11-04');
insert into DAILYREG values (112233445566, 100, 120, 500, 300, '2019-11-05');
insert into DAILYREG values (112233445566, 100, 120, 500, 300, '2019-02-02');

delete from EXTTRIPLE where value = 234

select * from Sys.dm_tran_database_transactions

