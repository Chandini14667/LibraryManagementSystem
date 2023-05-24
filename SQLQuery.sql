create database LibraryManagement
use LibraryManagement

create table LoginUser
(
UserName  varchar(50),
Password varchar(50)
)
insert into LoginUser values('Chandini','pass@123');
select*from LoginUser

create table Students
(
StudentID int primary key identity,
StudentName varchar(50),
Department varchar(50)
)
--Drop table Students
select*from Students

create table Books
(
BookID int identity primary key,
Title varchar(100),
Author varchar(100),
Quantity int
)
--Drop Table Books
select*from Books

create table IssueBook
(
StudentID int primary key references Students(StudentID),
BookID int references Books(BookID),
IssuedDate date
)
select * from IssueBook
--select Students.StudentID,Students.StudentName,Books.Title,Books.Author from IssueBook join Students on IssueBook.StudentID = Students.StudentID join Books on IssueBook.BookID = Books.BookID

select*from Students 
select*from Books
select * from IssueBook