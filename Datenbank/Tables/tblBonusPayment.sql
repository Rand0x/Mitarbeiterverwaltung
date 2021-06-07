use dbMAV

go


create table tblBonusPayment
(
  nKey                  int identity(1,1) primary key,
  nEmployeeLink         int                 not null,
  szReason              nvarchar(200)       not null,
  rAmount               decimal(10,2)       not null,
  dtDateOfPayment       datetime            not null,
  szComment             nvarchar(max)       null
)


go


