create table MARKET(
	code		int,
	description	varchar(300),
	name		varchar(50),
	constraint pkmarket primary key(code)
);

create table DAILYMARKET(
	idxmrkt			money,
	dailyvar		money,
	idxopeningval	money,
	constraint pkdailymrkt primary key(idxmrkt, dailyvar)
);

create table CLIENT(
	nif		decimal(9),
	ncc		decimal(7),
	name	varchar(50),
	constraint pkclient primary key(nif)
);

create table PORTFOLIO(
	name		varchar(50),
	totalval	money,
	nif			decimal(9),
	constraint pkportfolio primary key(name, nif),
	constraint fkclient foreign key(nif) references CLIENT(nif)
);

create table INSTRUMENT(
	isin			char(12),
	description		varchar(300),
	--currval			money,
	--avg6m			money,
	--varval6m		money,
	--dailyvar        money,
	--dailyvarperc	decimal(5,2),
	--var6mperc		decimal(5,2),
	constraint pkinstrument primary key(isin)	
);

create table POSITION(
	quantity		int,
	name			varchar(50),
	isin			char(12),
	nif				decimal(9),
	constraint pkpositions primary key(isin, name, nif),
	constraint fkportfolio foreign key(name, nif) references PORTFOLIO(name, nif),
	constraint fkinstrument_pos foreign key(isin) references INSTRUMENT(isin)
);

create table EMAIL(
	code		int,
	description	varchar(300),
	addr		varchar(50),
	constraint pkemail primary key(code)
);

create table PHONE(
	code		int,
	description	varchar(300),
	areacode	varchar(4),
	number		decimal(9),
	constraint pkphone primary key(code)
);
create table EXTTRIPLE(
	value		money not null,
	datetime		datetime not null,
	id			char(12) not null
);

create table DAILYREG(
	isin				char(12),
	minval				money,
	openingval			money,
	maxval				money,
	closingval			money,
	dailydate			date,
	--constraint pkdailyreg primary key(isin),
	constraint fkinstrument_reg foreign key(isin) references INSTRUMENT(isin)
	);