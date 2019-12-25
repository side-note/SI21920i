create table Market(
	code		int,
	description	varchar(300),
	name		varchar(50)	not null,
	constraint pkmarket primary key(code)
);

create table DailyMarket(
	idxmrkt			money	default 0,
	dailyvar		money	default 0,
	idxopeningval	money	default 0,
	code            int,
	date            date,
	constraint fkdailymrkt foreign key (code) references MARKET(code) on delete cascade,
	constraint pkdailymrkt primary key(date, code)
);

create table Client(
	nif		decimal(9),
	ncc		decimal(7)	not null unique,
	name	varchar(50)	not null,
	constraint pkclient primary key(nif)
);

create table Portfolio(
	name		varchar(50),
	totalval	money not null,
	constraint pkportfolio primary key(name)
);

create table Client_Portfolio(
	name varchar(50),
	nif decimal(9),
	constraint pkCLIENT_PORTFOLIO primary key(name, nif),
	constraint fkCLIENT foreign key(nif) references CLIENT(nif) on delete cascade,
	constraint fkPORTFOLIO foreign key(name) references PORTFOLIO(name) on delete cascade 
);

create table Instrument(
	isin			char(12),
	description		varchar(300),
	mrktcode        int not null,
	constraint pkinstrument primary key(isin),
	constraint fkinstrument foreign key (mrktcode) references MARKET(code) on delete cascade
);

create table Position(
	quantity		int	default 0,
	name			varchar(50),
	isin			char(12),
	constraint pkpositions primary key(isin, name),
	constraint fkportfolio_pos foreign key(name) references PORTFOLIO(name) on delete cascade,
	constraint fkinstrument_pos foreign key(isin) references INSTRUMENT(isin) on delete cascade
);

create table Email(
	code		int,
	description	varchar(300),
	addr		varchar(50)	not null unique,
	nif		decimal(9),
	constraint pkemail primary key(code),
	constraint fkclient_email foreign key(nif) references CLIENT(nif) on delete cascade
);

create table Phone(
	code		int,
	description	varchar(300),
	areacode	varchar(4),
	number		decimal(9) not null unique,
	nif		decimal(9),
	constraint pkphone primary key(code),
	constraint fkclient_phone foreign key(nif) references CLIENT(nif) on delete cascade
);
create table Exttriple(
	value		money 	default 0,
	datetime	datetime not null,
	id			char(12) not null,
    constraint pkexttriple primary key(id, datetime)
);

create table DailyReg(
	isin				char(12),
	minval				money	default 0,
	openingval			money	default 0,
	maxval				money	default 0,
	closingval			money	default 0,
	dailydate			date,
	constraint fkinstrument_reg foreign key(isin) references INSTRUMENT(isin) on delete cascade,
    constraint pkdailyreg primary key(isin, dailydate)
);