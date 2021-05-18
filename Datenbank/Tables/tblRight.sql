use dbMAV

go


create table tblRight
(
  nKey          int identity(1,1) primary key,
  szName        nvarchar(200)       not null,
  szInfo        nvarchar(max)       null
)


go


