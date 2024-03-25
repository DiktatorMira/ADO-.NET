Create database Promotions;
Use Promotions;

Create table Countries(
	Id int not null primary key identity(1,1),
	Title nvarchar(50)
);
Create table Cities(
	Id int not null primary key identity(1,1),
	Title nvarchar(50),
);
Create table Chapters(
	Id int not null primary key identity(1,1),
	Title nvarchar(50),
);
Create table Buyers (
    Id int not null primary key identity(1,1),
	CountryId int not null foreign key references Countries(Id),
	CityId int not null foreign key references Cities(Id),
    FullName nvarchar(max),
    Birth date,
    Male nvarchar(50),
    Email nvarchar(50)
);
Create table Interests(
	BuyerId int not null foreign key references Buyers(Id),
    ChapterId INT not null foreign key references Chapters(Id),
    primary key (BuyerId, ChapterId)
);
Create table Promotionals(
	Id int not null primary key identity(1,1),
	ChapterId int not null foreign key references Chapters(Id),
	CountryId int not null foreign key references Countries(Id),
	StartDate date,
	EndDate date
);
Create table Products(
	Id int not null primary key identity(1,1),
	PromotionalId int not null foreign key references Promotionals(Id),
	Title nvarchar(50),
	Description nvarchar(max),
	Price decimal(10,2)
);

Insert into Countries values 
('Украина'), ('США'), ('Китай'), ('Германия'), ('Франция');
Insert into Cities values 
('Киев'), ('Нью-Йорк'), ('Пекин'), ('Берлин'), ('Париж');
Insert into Chapters (Title) values 
('Мобильные телефоны'), ('Ноутбуки'), ('Кухонная техника'), ('Телевизоры'), 
('Аксессуары');
Insert into Buyers values 
(1, 1, 'Ковальчук Григорий Тарасович', '1990-05-15', 'Мужской', 'coval@example.com'),
(2, 2, 'John Smith', '1985-10-20', 'Мужской', 'john.smith@example.com'),
(3, 3, '王小明', '1988-03-08', 'Мужской', 'wangxiaoming@example.com'),
(4, 4, 'Maria Müller', '1995-12-25', 'Женский', 'maria.mueller@example.com'),
(5, 5, 'Jean Dupont', '1992-07-30', 'Мужской', 'jean.dupont@example.com');
Insert into Interests values 
(1, 1), (2, 2), (3, 3), (4, 4), (5, 5);
Insert into Promotionals values 
(1, 1, '2024-03-25', '2024-04-25'), (2, 2, '2024-03-25', '2024-04-25'),
(3, 3, '2024-03-25', '2024-04-25'), (4, 4, '2024-03-25', '2024-04-25'),
(5, 5, '2024-03-25', '2024-04-25');
Insert into Products values 
(1, 'iPhone 15', 'Смартфон от Apple', 1599.99),
(2, 'MacBook Pro', 'Ноутбук от Apple', 1499.99),
(3, 'Пароварка Philips', 'Кухонный прибор', 79.99),
(4, 'Samsung QLED TV', 'Телевизор с технологией QLED', 1299.99),
(5, 'AirPods Pro', 'Беспроводные наушники от Apple', 199.99);