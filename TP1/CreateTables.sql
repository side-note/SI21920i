create table Market(
	code		int,
	description	varchar(300),
	name		varchar(50),
	constraint pkmarket primary key(code)
);

create table DailyMarket(
	idxmrkt			money,
	dailyvar		money,
	idxopeningval	money,
	code            int,
	date            date,
	constraint fkdailymrkt foreign key (code) references MARKET(code),
	constraint pkdailymrkt primary key(date, code)
);

create table Client(
	nif		decimal(9),
	ncc		decimal(7),
	name	varchar(50),
	constraint pkclient primary key(nif)
);

create table Portfolio(
	name		varchar(50),
	totalval	money,
	constraint pkportfolio primary key(name)
);

create table Client_Portfolio(
	name varchar(50),
	nif decimal(9),
	constraint pkCLIENT_PORTFOLIO primary key(name, nif),
	constraint fkCLIENT foreign key(nif) references CLIENT(nif),
	constraint fkPORTFOLIO foreign key(name) references PORTFOLIO(name) 
);

create table Instrument(
	isin			char(12),
	description		varchar(300),
	mrktcode        int,
	constraint pkinstrument primary key(isin),
	constraint fkinstrument foreign key (mrktcode) references MARKET(code)
);

create table Position(
	quantity		int,
	name			varchar(50),
	isin			char(12),
	constraint pkpositions primary key(isin, name),
	constraint fkportfolio_pos foreign key(name) references PORTFOLIO(name),
	constraint fkinstrument_pos foreign key(isin) references INSTRUMENT(isin)
);

create table Email(
	code		int,
	description	varchar(300),
	addr		varchar(50),
	nif		decimal(9),
	constraint pkemail primary key(code),
	constraint fkclient_email foreign key(nif) references CLIENT(nif)
);

create table Phone(
	code		int,
	description	varchar(300),
	areacode	varchar(4),
	number		decimal(9),
	nif		decimal(9),
	constraint pkphone primary key(code),
	constraint fkclient_phone foreign key(nif) references CLIENT(nif)
);
create table Exttriple(
	value		money not null,
	datetime	datetime not null,
	id			char(12) not null,
    constraint pkexttriple primary key(id, datetime)

);

create table DailyReg(
	isin				char(12),
	minval				money,
	openingval			money,
	maxval				money,
	closingval			money,
	dailydate			date,
	constraint fkinstrument_reg foreign key(isin) references INSTRUMENT(isin),
    constraint pkdailyreg primary key(isin, dailydate)

);