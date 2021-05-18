use dbMAV

go


create table tblUser
(
  nKey              int identity(1,1) primary key,
  szName            nvarchar(200)       not null,
  szPassword        nvarchar(max)       not null,
  nEmployeeLink     int                 null,
  nRightLink        int                 not null
)


go


