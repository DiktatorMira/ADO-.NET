Create database StationeryCompany;
Use StationeryCompany;

Create table Products(
	id int not null primary key identity(1,1),
	title nvarchar(50) not null,
	type nvarchar(50) not null,
	amount int not null,
	price decimal(10,2) not null
);
Create table Managers(
	id int not null primary key identity(1,1),
	name nvarchar(50) not null,
	surname nvarchar(50) not null,
	email nvarchar(50) not null
);
Create table Companies(
	id int not null primary key identity(1,1),
	title nvarchar(100) not null,
	email nvarchar(50) not null
);
Create table Sales(
	id int not null primary key identity(1,1),
	product_id int not null foreign key references Products(id),
	manager_id int not null foreign key references Managers(id),
	company_id int not null foreign key references Companies(id),
	sales_number int not null,
	price decimal(10,2) not null,
	sale_date date not null
);

Insert into Products values('���������', '�����������', 100, 1.5),
('������', '�������', 200, 10.0), ('�����', '�������', 150, 2.0);

Insert into Managers values ('����', '������', 'ivan.ivanov@example.com'),
('�����', '�������', 'maria.petrova@example.com'),
('�������', '�������', 'alexey.sidorov@example.com');

Insert into Companies values ('��� "����������"', 'info@officeservice.com'),
('�� "����������"', 'info@kanc-tovary.ua'),
('��� "������"', 'info@zao-bumaga.com');

Insert into Sales values (1, 1, 1, 50, 75.0, '2024-02-14'),
(2, 2, 2, 20, 200.0, '2024-02-15'),(3, 3, 3, 30, 60.0, '2024-02-16');