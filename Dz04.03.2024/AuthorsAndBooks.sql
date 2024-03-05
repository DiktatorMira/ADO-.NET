Use master go
Create database AuthorsAndBooks go
Use AuthorsAndBooks go

Create table Authors (
    [id] int not null primary key identity(1,1),
    [fullname] nvarchar(max) not null
);
Create table Books (
    [id] int not null primary key identity(1,1),
    [title] nvarchar(max) not null,
    [author_id] int not null foreign key references Authors(id)
);

Insert into Authors values
(N'Мартин Фаулер'), (N'Роберт Мартин'), (N'Агата Кристи'),
(N'Артур Конан Дойл'), (N'Дэн Браун'), (N'Айзек Азимов');
Insert into Books values
(N'Рефакторинг: Улучшение дизайна существующего кода', 1),
(N'Чистый код: Руководство по написанию приемлемого кода', 2),
(N'Убийство в Восточном экспрессе', 3),
(N'Собака Баскервилей', 4), (N'Код да Винчи', 5),
(N'Я, робот', 6), (N'Дизайн паттерны', 1),
(N'Мифы о языках программирования', 2);