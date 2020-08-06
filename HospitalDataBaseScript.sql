IF DB_ID('Hospital') IS NULL
CREATE DATABASE Hospital
GO
USE Hospital;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblRequest')
drop table tblRequest;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblEmployee')
drop table tblEmployee;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblDoctor')
drop table tblDoctor;


create table tblDoctor(
DoctorID int identity(1,1) primary key,
FullName varchar(30) not null,
JMBG varchar(30) check(len(JMBG) = 13)  not null unique,
BankAccount varchar(30) check (LEN(BankAccount) = 10) not null unique,
UserName varchar(30) check(len(UserName) > 5) not null unique,
Pass varchar(30) check(len(Pass) > 5) not null,
constraint checkDoctorJMBG check(JMBG not like '%[^0-9]%'),
constraint checkBankAccount check(BankAccount not like '%[^0-9]%')
)

create table tblEmployee(
EmployeeID int identity(1,1) primary key,
FullName varchar(30) not null,
JMBG varchar(30) check(len(JMBG) = 13)  not null unique,
InsuranceCardNumber int not null unique,
UserName varchar(30) check(len(UserName) > 5) not null unique,
Pass varchar(30) check(len(Pass) > 5) not null,
constraint checkEmployeeJMBG check(JMBG not like '%[^0-9]%'),
DoctorID int foreign key (DoctorID) references tblDoctor(DoctorID) not null,)

create table tblRequest(
RequestID int identity(1,1) primary key,
EmployeeID int foreign key (EmployeeID) references tblEmployee(EmployeeID) not null,
RequestDate date not null,
Reason varchar(40),
CompanyName varchar(40),
Emergent bit,
Approved bit,
)