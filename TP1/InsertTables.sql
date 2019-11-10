insert into MARKET values (111, 'Description1', 'Market1');
insert into MARKET values (222, 'Description2', 'Market2');

insert into	DAILYMARKET values (111, 1111, 11111, 111,'2019-11-01');
insert into	DAILYMARKET values (222, 2222, 22222, 222,'2019-11-03');

insert into CLIENT values (111111111, 1111111, 'Client1');
insert into CLIENT values (222222222, 2222222, 'Client2');
insert into CLIENT values (444444444, 4444444, 'Client4');
insert into CLIENT values (555555555, 5555555, 'Client5');

insert into PORTFOLIO values ('111111111_portfolio', 10000);
insert into PORTFOLIO values ('444444444_portfolio', 40000);

insert into CLIENT_PORTFOLIO values('111111111_portfolio', 111111111);
insert into CLIENT_PORTFOLIO values('444444444_portfolio', 444444444);

insert into INSTRUMENT values (111111111111, 'Description1', 111);
insert into INSTRUMENT values (222222222222, 'Description2', 222);
insert into INSTRUMENT values (333333333333, 'Description3', 333);
insert into INSTRUMENT values (555555555555, 'Description5', 111)

insert into POSITION values (1, '111111111_portfolio', 111111111111);
insert into POSITION values (2, '444444444_portfolio', 222222222222);
insert into POSITION values (3, '111111111_portfolio', 333333333333);

insert into EMAIL values (1,'Nuno mail','ncardeal@pilim.com');
insert into EMAIL values (2,'Carol mail','carolct@pilim.com');
insert into EMAIL values (3,'Juju mail','jujucmps@pilim.com');

insert into PHONE values (1,'Nuno phone', '+351', 961222333);
insert into PHONE values (2,'Carol phone', '+351', 911222333);
insert into PHONE values (3,'Juju phone', '+351', 921222333);

insert into EXTTRIPLE values (1111, '2019-11-01 11:11:11', 111111111111);
insert into EXTTRIPLE values (2222, '2019-11-01 12:12:12', 111111111111);
insert into EXTTRIPLE values (3333, '2019-11-01 13:13:13', 111111111111);
insert into EXTTRIPLE values (4444, '2019-10-02 11:11:11', 222222222222);
insert into EXTTRIPLE values (5555, '2019-10-02 12:12:12', 222222222222);
insert into EXTTRIPLE values (6666, '2019-10-02 13:13:13', 222222222222);
insert into EXTTRIPLE values (7777, '2019-12-14 12:55:08', 444444444444);
insert into EXTTRIPLE values (8888, '2019-09-10 12:55:08', 555555555555);
insert into EXTTRIPLE values (9999, '2019-09-10 13:55:08', 555555555555);
insert into EXTTRIPLE values (1000, '2019-10-10 14:55:08', 555555555555);

insert into DAILYREG values (111111111111, 11, 11111, 1111, 111, '2019-11-01');
insert into DAILYREG values (111111111111, 22, 22222, 2222, 222, '2019-08-02');
insert into DAILYREG values (111111111111, 33, 33333, 3333, 333, '2019-08-03');
insert into DAILYREG values (222222222222, 44, 44444, 4444, 444, '2019-09-04');


