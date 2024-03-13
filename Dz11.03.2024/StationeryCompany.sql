Create database StationeryCompany;
Go Use StationeryCompany;

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

Go
Insert into Products values('Карандаши', 'Рисовальные', 100, 1.5),
('Бумага', 'Офисная', 200, 10.0), ('Ручки', 'Гелевые', 150, 2.0);

Insert into Managers values ('Иван', 'Иванов', 'ivan.ivanov@example.com'),
('Мария', 'Петрова', 'maria.petrova@example.com'),
('Алексей', 'Сидоров', 'alexey.sidorov@example.com');

Insert into Companies values ('ООО "ОфисСервис"', 'info@officeservice.com'),
('ИП "КанцТовары"', 'info@kanc-tovary.ua'),
('ЗАО "Бумага"', 'info@zao-bumaga.com');

Insert into Sales values (1, 1, 1, 50, 75.0, '2024-02-14'),
(2, 2, 2, 20, 200.0, '2024-02-15'),(3, 3, 3, 30, 60.0, '2024-02-16');

Go
create procedure GetProductsInfo
As begin
    SELECT * FROM Products
End;

CREATE FUNCTION GetProductTypes()
RETURNS TABLE AS RETURN (
    SELECT DISTINCT type
    FROM Products
);
CREATE FUNCTION GetSalesManagers()
RETURNS TABLE AS RETURN (
    SELECT DISTINCT
        m.id AS ManagerId,
        m.name AS ManagerName,
        m.surname AS ManagerSurname,
        m.email AS ManagerEmail
    FROM
        Sales s
        INNER JOIN Managers m ON s.manager_id = m.id
);
CREATE FUNCTION GetProductsMaxQuantity()
RETURNS TABLE AS RETURN (
    SELECT p.id AS ProductId,
           p.title AS ProductTitle,
           p.type AS ProductType,
           p.amount AS ProductAmount,
           p.price AS ProductPrice
    FROM Products p
    WHERE p.amount = (SELECT MAX(amount) FROM Products)
);
CREATE FUNCTION GetProductsMinQuantity()
RETURNS TABLE AS RETURN (
    SELECT p.id AS ProductId,
           p.title AS ProductTitle,
           p.type AS ProductType,
           p.amount AS ProductAmount,
           p.price AS ProductPrice
    FROM Products p
    WHERE p.amount = (SELECT MIN(amount) FROM Products)
);
CREATE FUNCTION GetProductsMinCost()
RETURNS TABLE AS RETURN (
    SELECT p.id AS ProductId,
           p.title AS ProductTitle,
           p.type AS ProductType,
           p.amount AS ProductAmount,
           p.price AS ProductPrice
    FROM Products p
    WHERE p.price = (SELECT MIN(price) FROM Products)
);
CREATE FUNCTION GetProductsMaxCost()
RETURNS TABLE AS RETURN (
    SELECT p.id AS ProductId,
           p.title AS ProductTitle,
           p.type AS ProductType,
           p.amount AS ProductAmount,
           p.price AS ProductPrice
    FROM Products p
    WHERE p.price = (SELECT MAX(price) FROM Products)
);
CREATE FUNCTION GetProductsByType(@productType NVARCHAR(50))
RETURNS TABLE AS RETURN (
    SELECT p.type AS ProductType
    FROM Products p
    WHERE p.type = @productType
);
CREATE FUNCTION GetProductsSoldByManager(@managerId INT)
RETURNS TABLE AS RETURN (
    SELECT p.id AS ProductId,
           p.title AS ProductTitle,
           p.type AS ProductType,
           p.amount AS ProductAmount,
           p.price AS ProductPrice
    FROM Products p
    JOIN Sales s ON p.id = s.product_id
    WHERE s.manager_id = @managerId
);
CREATE FUNCTION GetProductsPurchasedByCompany(@companyId INT)
RETURNS TABLE AS RETURN (
    SELECT p.id AS ProductId,
           p.title AS ProductTitle,
           p.type AS ProductType,
           p.amount AS ProductAmount,
           p.price AS ProductPrice
    FROM Products p
    JOIN Sales s ON p.id = s.product_id
    WHERE s.company_id = @companyId
);
CREATE FUNCTION GetLatestSalesInfo()
RETURNS TABLE AS RETURN (
    SELECT 
        p.id AS ProductId,
        p.title AS ProductTitle,
        p.type AS ProductType,
        p.amount AS ProductAmount,
        p.price AS ProductPrice,
        s.id AS SaleId,
        s.sales_number AS SaleNumber,
        s.price AS SalePrice,
        s.sale_date AS SaleDate,
        ROW_NUMBER() OVER (PARTITION BY p.id ORDER BY s.sale_date DESC) AS RowNum
    FROM 
        Products p
        JOIN Sales s ON p.id = s.product_id
);
CREATE FUNCTION GetAverageProductAmountByType()
RETURNS TABLE AS RETURN (
    SELECT 
        p.type AS ProductType,
        AVG(p.amount) AS AverageProductAmount
    FROM 
        Products p
    GROUP BY 
        p.type
);
CREATE PROCEDURE AddProduct
    @Title NVARCHAR(50),
    @Type NVARCHAR(50),
    @Amount INT,
    @Price DECIMAL(10,2)
AS
BEGIN
    INSERT INTO Products (title, type, amount, price)
    VALUES (@Title, @Type, @Amount, @Price);
END

CREATE PROCEDURE InsertTypeProduct
    @Type NVARCHAR(50)
AS BEGIN
    INSERT INTO Products (type)
    VALUES (@Type);
END

CREATE PROCEDURE AddManager
    @Name NVARCHAR(50),
    @Surname NVARCHAR(50),
    @Email NVARCHAR(50)
AS BEGIN
    INSERT INTO Managers VALUES (@Name, @Surname, @Email);
END

CREATE PROCEDURE AddCompany
    @Title NVARCHAR(50), @Email NVARCHAR(50)
AS BEGIN
    INSERT INTO Companies VALUES (@Title, @Email);
END

CREATE PROCEDURE UpdateProduct
	@Title nvarchar(50), @Type nvarchar(50),
	@Amount int, @Price decimal(10,2)
AS BEGIN
     UPDATE Products SET title = @Title, type = @Type,
     amount = @Amount, @Price = price WHERE id = id;
END

CREATE PROCEDURE UpdateCompanies
	@Title nvarchar(50), @Email nvarchar(50)
AS BEGIN
     UPDATE Companies SET title = @Title, email = @Email
     WHERE id = id;
END

CREATE PROCEDURE UpdateManagers
	@Name nvarchar(50), @Surname nvarchar(50), @Email nvarchar(50)
AS BEGIN
     UPDATE Managers SET name = @Name, surname = @Surname, 
	 email = @Email WHERE id = id;
END

CREATE PROCEDURE UpdateTypeProduct
	@Type nvarchar(50)
AS BEGIN
     UPDATE Products SET type = @Type
     WHERE id = id;
END

CREATE PROCEDURE DeleteProduct
    @ProductId int
AS BEGIN
    DELETE FROM Products
    WHERE id = @ProductId;
END

CREATE PROCEDURE DeleteManager
    @ManagerId int
AS BEGIN
    DELETE FROM Managers
    WHERE id = @ManagerId;
END

CREATE PROCEDURE DeleteTypeProduct
    @Type nvarchar(50)
AS BEGIN
    DELETE FROM Products
    WHERE type = @Type;
END

CREATE PROCEDURE DeleteCompany
    @CompanyId int
AS BEGIN
    DELETE FROM Companies
    WHERE id = @CompanyId;
END