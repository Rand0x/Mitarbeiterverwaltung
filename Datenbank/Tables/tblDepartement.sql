use dbMAV

go


create table tblDepartement
(
  nKey              int identity(1,1) primary key,
  szName            nvarchar(200)       not null,
  szIdentifier      nvarchar(10)        null,
  szInfo            nvarchar(max)       null,
  nManagerLink      int                 not null
)


go


