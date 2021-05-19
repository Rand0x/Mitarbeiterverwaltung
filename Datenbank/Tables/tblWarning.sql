use dbMAV

go


create table tblWarning
(
  nKey                  int identity(1,1) primary key,
  nEmployeeLink         int                 not null,
  szReason              nvarchar(200)       not null,
  dtIssueDate           datetime            not null,
  szComment             nvarchar(max)       null
)


go


