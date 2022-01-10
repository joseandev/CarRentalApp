create database CarRental
--Creamos la DB

use CarRental
--Referenciamos que vamos a usar nuestra DB

--Creamos nuestra tabla de tipos de carros
	create table TypesOfCars(
	id int not null primary key identity,
	Make nvarchar(50),
	Model nvarchar(50),
	VIN nvarchar(50),
	LicensePlateNumber nvarchar(50),
	Year int,
)

--Creamos nuestra tabla de registros de vehiculos rentados
create table CarRentalRecord (
id int not null primary key identity,--Será primary key y se incrementará automaticamente
customerName nvarchar(100),
dateRented datetime,
dateReturned datetime,
cost decimal(18,0),
typeOfCar int foreign key references TypesOfCars(id)
--La columna TypeOfCar sera int y se tomara de una llave primaria en la tabla
--TypesOfCars o sea el id
)

--Tabla de usuarios (Login)
create table Users (
id int not null primary key identity,
username nvarchar(50) not null,
password nvarchar(100) not null,
isActive bit null
)

--Tabla de Roles
create table Roles (
id int not null primary key identity,
name varchar(50),
shortname nvarchar(50)
)

--Tabla para administrar el rol de cada usuario
create table UserRoles(
id int not null primary key identity,
userid int foreign key references Users (id),
roleid int foreign key references Roles (id)
)

--DROP TABLES WITH REFERENCES
drop table UserRoles
drop table Roles
drop table Users

--Algunas inserciones de marcas para pruebas
insert into TypesOfCars values ('Toyota', 'Corola')
insert into TypesOfCars values ('Honda', 'Civic')
insert into TypesOfCars values ('Mercedes Benz', '839')
insert into TypesOfCars values ('Ferrari', 'Lotzon')
insert into TypesOfCars values ('Bugatti', 'Beyron')
insert into TypesOfCars values ('McClaren', 'P1')


--User
insert into Users values ('admin', /*password*/ '5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8', 1)
insert into Users values ('user', '5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8', 1)

select *from Users
-- PASSWORD: 5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8

--Insercion de los tipos de roles
insert into Roles values ('Administrador', 'admin')
insert into Roles values ('Data Entry', 'clerk')
insert into Roles values ('View Only', 'view')

--UserRoles
insert into UserRoles values (1, 1)
insert into UserRoles values (2, 2)

--select *from TypesOfCars
--select *from CarRentalRecord
--select *from Users
--select *from Roles
--select *from UserRoles

--drop table TypesOfCars
--drop table CarRentalRecord
--drop table Users
--drop table Roles
--drop table UserRoles

--truncate table TypeOfCars
--truncate table CarRentalRecord
--truncate table Users
--truncate table Roles
--truncate table UserRoles