Create database Storage;
Use Storage;

Create table ProductTypes(
    id int not null primary key identity(1,1),
    types nvarchar(50) not null
);
Create table Products(
    id int not null primary key identity(1,1),
    title nvarchar(100) not null,
    ptype_id int not null foreign key references ProductTypes(id),
    cost_price decimal(10, 2) not null
);
Create table Providers(
    id int not null primary key identity(1,1),
    title nvarchar(50) not null
);
Create table Delivery(
    id int not null primary key identity(1,1),
    product_id int not null foreign key references Products(id),
    provider_id int not null foreign key references Providers(id),
    quantity int not null,
    delivery_date date not null
);

Insert into ProductTypes values ('Meal'), ('Chemistry'), ('Food'), 
('Metal'), ('Electrician');

Insert into Products values ('Steak', 1, 10.50), ('Deodorant', 2, 15.75),
('Butter', 3, 8.99), ('Gold', 4, 22.30), ('Battery', 5, 12.50);

Insert into Providers values ('Metalind'), ('Ultracraft'), ('Moneymake');

Insert into Delivery values (1, 1, 50, '2024-02-08'),(2, 2, 30, '2024-02-09'),
(3, 3, 25, '2024-02-10'),(4, 4, 40, '2024-02-11'),(5, 5, 20, '2024-02-12');