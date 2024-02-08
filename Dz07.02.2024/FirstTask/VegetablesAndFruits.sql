Create database VegetablesAndFruits;
Use VegetablesAndFruits;

Create table Products(
	id int not null primary key identity(1,1),
	kind nvarchar(50) not null,
	color nvarchar(50) not null,
	calories decimal(10,2) not null
);

Insert into Products values
('Vegetable', 'orange', '100'),
('Fruit', 'green', '120'),
('Vegetable', 'red', '49'),
('Fruit', 'yellow', '145');