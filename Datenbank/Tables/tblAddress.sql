use dbMAV

go


create table tblAddress
(
  nKey                  int identity(1,1) primary key,
  szStreet              nvarchar(200)       not null,
  szHouseNumber         nvarchar(10)        not null,
  szCity                nvarchar(200)       not null,
  szPLZ                 nvarchar(5)         not null,
  szPrivateMail         nvarchar(200)       null,
  szPrivateTelephone    nvarchar(50)        null,
  szPrivateMobileNmb    nvarchar(50)        null
)


go


