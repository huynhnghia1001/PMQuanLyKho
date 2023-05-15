Create database QuanLyKho
go

use QuanLyKho
go

create table Unit
(
	Id int identity(1,1) primary key,
	DisplayName nvarchar(max)
)
go

create table Suplier(
	Id int identity(1,1) primary key,
	DisplayName nvarchar(max),
	Address nvarchar(max),
	Phone nvarchar(20),
	Email nvarchar(200),
	MoreInfo nvarchar(max),
	ContractDate Datetime
)
go

create table Customer
(
	Id int identity(1,1) primary key,
	DisplayName nvarchar(max),
	Address nvarchar(max),
	Phone nvarchar(20),
	Email nvarchar(200),
	MoreInfo nvarchar(max),
	ContractDate Datetime
)
go

create table Object(
	Id nvarchar(128) primary key,
	DisplayName nvarchar(max),
	IdUnit int not null,
	IdSuplier int not null,
	QRCode nvarchar(max),
	BarCode nvarchar(max)

	foreign key (IdUnit) references Unit(Id),
	foreign key (IdSuplier) references Suplier(Id),
)
go

create table UserRole(
	Id int identity(1,1) primary key,
	DisplayName nvarchar(max),
)
go

insert into UserRole(DisplayName) values(N'Admin')
go
insert into UserRole(DisplayName) values(N'Nhân Viên')
go

create table Users(
	Id int identity(1,1) primary key,
	DisplayName nvarchar(max),
	UserName nvarchar(100),
	Password nvarchar(max),
	IdRole int not null

	foreign key (IdRole) references UserRole(Id) 
)
go

insert into Users(DisplayName,UserName,Password,IdRole) values(N'Nghia','admin',N'db69fc039dcbd2962cb4d28f5891aae1',1)
go
insert into Users(DisplayName,UserName,Password,IdRole) values(N'Tuong','user',N'978aae9bb6bee8fb75de3e4830a1be46',2)
go

create table Input(
	Id nvarchar(128) primary key,
	DateInput Datetime
)
go

create table InputInfo
(
	Id nvarchar(128) primary key,
	IdObject nvarchar(128) not null,
	IdInput nvarchar(128) not null,
	Count int,
	InputPrice float default 0,
	OutputPrice float default 0,
	Status nvarchar(max)

	foreign key (IdObject) references Object(Id),
	foreign key (IdInput) references Input(Id)
)
go
create table Output(
	Id nvarchar(128) primary key,
	DateOutput Datetime
)
go

create table OutputInfo
(
	Id nvarchar(128) primary key,
	IdObject nvarchar(128) not null,
	IdOutputInfo nvarchar(128) not null,
	IdCustomer int not null,
	Count int,
	Status nvarchar(max)

	foreign key (IdObject) references Object(Id),
	foreign key (Id) references Output(Id),
	foreign key (IdCustomer) references Customer(Id)
)
go