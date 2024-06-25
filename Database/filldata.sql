--create db if not exists
-- Create the 'BookStore' database if it does not exist
use master;
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'BookStore')
    CREATE DATABASE BookStore;
GO

-- Grant the necessary permissions to the 'sa' user
USE BookStore;
GO
create table Book
(
    Id int identity(1,1) primary key ,
    Title nvarchar(100) not null,
    Description nvarchar(1000) null,
    Author nvarchar(100) not null,
    ISBN nvarchar(20) not null,
    PublishDate datetime not null,
    Price decimal(18,2) not null,
    Genre nvarchar(50) null
);

insert into Book(Title, Description, Author, ISBN, PublishDate, Price, Genre) values ('The Great Gatsby', 'The Great Gatsby is a 1925 novel by American writer F. Scott Fitzgerald.', 'F. Scott Fitzgerald', '9780743273565', '1925-04-10', 7.99, 'Fiction');
insert into Book(Title, Description, Author, ISBN, PublishDate, Price, Genre) values ('To Kill a Mockingbird', 'To Kill a Mockingbird is a novel by Harper Lee published in 1960.', 'Harper Lee', '9780061120084', '1960-07-11', 6.99, 'Fiction');
insert into Book(Title, Description, Author, ISBN, PublishDate, Price, Genre) values ('1984', '1984 is a dystopian social science fiction novel by English novelist George Orwell.', 'George Orwell', '9780451524935', '1949-06-08', 8.99, 'Fiction');
insert into Book(Title, Description, Author, ISBN, PublishDate, Price, Genre) values ('The Catcher in the Rye', 'The Catcher in the Rye is a novel by J. D. Salinger, partially published in serial form in 1945–1946 and as a novel in 1951.', 'J. D. Salinger', '9780316769488', '1951-07-16', 5.99, 'Fiction');
insert into Book(Title, Description, Author, ISBN, PublishDate, Price, Genre) values ('The Hobbit', 'The Hobbit, or There and Back Again is a childrens fantasy novel by English author J. R. R. Tolkien.', 'J. R. R. Tolkien', '9780345534835', '1937-09-21', 9.99, 'Fantasy');
insert into Book(Title, Description, Author, ISBN, PublishDate, Price, Genre) values ('The Lord of the Rings', 'The Lord of the Rings is an epic high fantasy novel by the English author and scholar J. R. R. Tolkien.', 'J. R. R. Tolkien', '9780618640157', '1954-07-29', 12.99, 'Fantasy');
insert into Book(Title, Description, Author, ISBN, PublishDate, Price, Genre) values ('The Da Vinci Code', 'The Da Vinci Code is a 2003 mystery thriller novel by Dan Brown.', 'Dan Brown', '9780307474278', '2003-03-18', 10.99, 'Mystery');
insert into Book(Title, Description, Author, ISBN, PublishDate, Price, Genre) values ('Angels & Demons', 'Angels & Demons is a 2000 bestselling mystery-thriller novel written by American author Dan Brown.', 'Dan Brown', '9780671027360', '2000-05-16', 11.99, 'Mystery');
insert into Book(Title, Description, Author, ISBN, PublishDate, Price, Genre) values ('The Lost Symbol', 'The Lost Symbol is a 2009 novel written by American writer Dan Brown.', 'Dan Brown', '9780385504225', '2009-09-15', 13.99, 'Mystery');
go
create table [User]
(
    Id int identity(1,1) primary key,
    UserName nvarchar(50) not null,
    FirstName nvarchar(50) not null,
    LastName nvarchar(50) not null,
    HashedPassword nvarchar(100) not null,
    Salt nvarchar(100) not null
);

--add a default sa user

