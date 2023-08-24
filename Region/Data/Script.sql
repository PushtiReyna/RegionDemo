CREATE DATABASE RegionDB

CREATE TABLE Country(
	CountryId int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	CountryName [Varchar](100) NOT NULL,
	IsActive [Bit] Not Null,
	IsDelete [Bit] Not Null,
	CreateOn [Datetime] Not Null,
	UpdateOn [Datetime] Not Null
)	

CREATE TABLE State(
	StateId int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	CountryId int Not Null,
	StateName [Varchar](100) NOT NULL,
	IsActive [Bit] Not Null,
	IsDelete [Bit] Not Null,
	CreateOn [Datetime] Not Null,
	UpdateOn [Datetime] Not Null
)

CREATE TABLE City(
	CityId int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	CountryId int Not Null,
	StateId Int Not Null,
	CityName [Varchar](100) NOT NULL,
	IsActive [Bit] Not Null,
	IsDelete [Bit] Not Null,
	CreateOn [Datetime] Not Null,
	UpdateOn [Datetime] Not Null
)

 ALTER TABLE Country
 ALTER COLUMN UpdateOn Datetime  NULL;