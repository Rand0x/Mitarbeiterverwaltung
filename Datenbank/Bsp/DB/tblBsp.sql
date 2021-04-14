use dbBsp

create table tblPerson
(
  nID int identity(1,1) primary key,
  szFirstName nvarchar(200),
  szLastName nvarchar(200)
)

insert into tblPerson (szFirstName, szLastName)
values (N'Max', N'Mustermann')
      ,(N'Erika', N'Mustermann')
      ,(N'Jon', N'Doe')


select * from tblPerson