use dbMAV

go


create table tblBanking
(
  nKey              int identity(1,1) primary key,
  szBankName        nvarchar(200)         not null,
  szIBAN            nvarchar(22)          not null,
  szBIC             nvarchar(11)          not null
)


go


