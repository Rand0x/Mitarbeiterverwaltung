use dbMAV

go


create table tblAbsence
(
  nKey                int identity(1,1) primary key,
  szReason            nvarchar(200)       not null,
  szInfo              nvarchar(max)       null,
  nEmployeeLink       int                 not null,
  dtStart             datetime            not null,
  dtEnd               datetime            null,
  nEffective          int                 null
)


go


