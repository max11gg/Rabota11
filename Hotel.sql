create database Hotel
go

use Hotel
go

CREATE TABLE Tokens (
    token_id INT IDENTITY(1,1) PRIMARY KEY,
    token VARCHAR(200) NOT NULL,
    token_datetime DATETIME2 NOT NULL DEFAULT(SYSDATETIME())
);

create table [dbo].[Role]
(
	[ID_Role] [int] not null identity(1,1),
	[Name_Role] [varchar] (50) not null,
	
	constraint [PK_Role] primary key clustered
	([ID_Role] ASC) on [PRIMARY],
	constraint [UQ_Name_Role] unique ([Name_Role])
)
go

insert into [dbo].[Role] ([Name_Role])
values ('Администратор БД'),
('Гость'),
 ('Администратор'),
('Управляющий')
go

create table [dbo].[User]
(
	[ID_User] [int] not null identity(1,1),
	[First_Name_User] [varchar] (30) not null,
	[Last_Name_User] [varchar] (30) not null,
	[Middle_name_User] [varchar] (30) not null,
	[Email_User] [varchar] (60) null,
	[Number_Phone] [varchar] (30) null,
	[Pasport_series] [varchar] (4) null,
	[Pasport_number] [varchar] (6) null,
	[Login_User] [varchar] (50) not null,
	[Password_User] [varchar] (100) not null,
	[Salt] [varchar] (256) null,
	[Role_ID] [int]  not null,
	
	constraint [PK_User] primary key clustered
	([ID_User] ASC) on [PRIMARY],
	constraint [FK_Role_User] foreign key ([Role_ID])
	references [dbo].[Role] ([ID_Role])
)
go

insert into [dbo].[User] ([First_Name_User], [Last_Name_User], [Middle_name_User], [Login_User], [Password_User], [Role_ID])
values('First', 'Second', 'Middle', 'admin', 'password', 1)

select * from [dbo].[User]

create table Status(
	[ID_Status] [int] not null identity(1,1),
	[Availability] [varchar] (30) not null,
	
	constraint [PK_Status] primary key clustered
	([ID_Status] ASC) on [PRIMARY],
	constraint [UQ_Name_Status] unique ([Availability])
)

insert into Status([Availability])
values ('Свободен'),
('Занят')

create table Hotels(
	[ID_Hotels] [int] not null identity(1,1),
	[Adress_hotel] [varchar] (max) not null,
	[Rate_hotel] [int] not null,
	[Number_Phone_hotel] [varchar] (20) not null,
	[Email_hotel] [varchar] (60) not null,
	
	constraint [PK_Hotels] primary key clustered
	([ID_Hotels] ASC) on [PRIMARY]
)

insert into Hotels([Adress_hotel], [Rate_hotel], [Number_Phone_hotel], [Email_hotel])
values ('адрес', 5, '84953688752', 'hotelemail@gmail.com')
go


create table Room(
	[ID_Room] [int] not null identity(1,1),
	[Count_room] [int] not null,
	[Price] [int] not null,
	[Number_room] [int] not null,
	[Type_room] [varchar] (60) not null,
	Status_ID int not null,
	Hotels_ID int not null,
	
	constraint [FK_Status_Room] foreign key (Status_ID)
	references [dbo].[Status] ([ID_Status]),
	constraint [FK_Hotels_Room] foreign key (Hotels_ID)
	references [dbo].[Hotels] ([ID_Hotels]),
	constraint [PK_Room] primary key clustered
	([ID_Room] ASC) on [PRIMARY]
)

insert into Room([Count_room], [Price], [Number_room], [Type_room], Status_ID, Hotels_ID)
values (100, 2500, 4, 'Эконом', 1, 1)
go

create table Service(
	[ID_Service] [int] not null identity(1,1),
	[Name_Services] [varchar] (50) not null,
	[Description_Services] [varchar] (max) not null,
	[Price_Services] [int] not null,
	
	constraint [PK_Service] primary key clustered
	([ID_Service] ASC) on [PRIMARY]
)

insert into Service([Name_Services], [Description_Services], [Price_Services])
values('Услуга', 'Описание', 1000)
go

create table Booking(
	[ID_Booking] [int] not null identity(1,1),
	Arrival_date date not null,
	Departure_date date not null,
	User_ID int not null,
	Service_ID int not null,
	Room_ID int not null,
	[Is_Booking] BIT NOT NULL DEFAULT 0,
	constraint [FK_User_Booking] foreign key (User_ID)
	references [dbo].[User] ([ID_User]),
	constraint [FK_Service_Booking] foreign key (Service_ID)
	references [dbo].[Service] ([ID_Service]),
	constraint [FK_Room_Booking] foreign key (Room_ID)
	references [dbo].[Room] ([ID_Room]),
	constraint [PK_Booking] primary key clustered
	([ID_Booking] ASC) on [PRIMARY]
)

insert into Booking(Arrival_date, Departure_date, User_ID, Service_ID, Room_ID)
values('01.09.2023', '10.09.2023', 1, 1, 1)
go

insert into Booking(Arrival_date, Departure_date, User_ID, Service_ID, Room_ID)
values('01.09.2023', '10.09.2023', 2, 1, 1)

select * from Booking


create table Check_in(
	[ID_Check_in] [int] not null identity(1,1),
	Status_Check_in varchar (20) not null,
	User_ID int not null,
	Booking_ID int not null,
	
	constraint [FK_User_Check_in] foreign key (User_ID)
	references [dbo].[User] ([ID_User]),
	constraint [FK_Booking_Check_in] foreign key (Booking_ID)
	references [dbo].[Booking] ([ID_Booking]),
	constraint [PK_Check_in] primary key clustered
	([ID_Check_in] ASC) on [PRIMARY]
)

insert into Check_in(Status_Check_in, User_ID, Booking_ID)
values('Заселено', 1, 1)
go


create table Check_out(
	[ID_Check_out] [int] not null identity(1,1),
	Payment_date date not null,
	Total_cost int not null,
	Check_in_ID int not null,
	User_ID int not null,
	constraint [FK_User_Check_out] foreign key (User_ID)
	references [dbo].[User] ([ID_User]),
	constraint [FK_Check_in_Check_out] foreign key (Check_in_ID)
	references [dbo].[Check_in] ([ID_Check_in]),
	constraint [PK_Check_out] primary key clustered
	([ID_Check_out] ASC) on [PRIMARY]
)

insert into Check_out(Payment_date, Total_cost, Check_in_ID, User_ID)
values ('01.09.2023', 3500, 1, 2)
go

select * from Check_out