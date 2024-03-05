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
(N'������ ������'), (N'������ ������'), (N'����� ������'),
(N'����� ����� ����'), (N'��� �����'), (N'����� ������');
Insert into Books values
(N'�����������: ��������� ������� ������������� ����', 1),
(N'������ ���: ����������� �� ��������� ����������� ����', 2),
(N'�������� � ��������� ���������', 3),
(N'������ �����������', 4), (N'��� �� �����', 5),
(N'�, �����', 6), (N'������ ��������', 1),
(N'���� � ������ ����������������', 2);