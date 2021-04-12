

use dbXXXX

go

if not exists(select * from sys.objects where name = N'tblXXXX')
begin
  create table tblXXXX(nID int identity(1,1) primary key)
end

go

/********************************************************************************
    Name:

    Erstellt am:

    Inhalt:
********************************************************************************/

alter table tblXXXX
add column_name       datatype        --Description


go


