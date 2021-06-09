use dbMAV

go


create table tblEmployee
(
  nKey                  int identity(1,1) primary key,
  nEmployeeNumber       int                 not null,
  szFirstName           nvarchar(200)       not null,
  szLastName            nvarchar(200)       not null,
  dtBirthdate           datetime            not null,
  szSex                 nvarchar(1)         null,
  szMail                nvarchar(200)       not null,
  szTelephone           nvarchar(50)        not null,
  nDepartementLink      int                 not null,
  szJobName             nvarchar(200)       not null,
  dtRecruitDate         datetime            not null,
  nHoursPerWeek         int                 not null,
  rOvertime             decimal(5,1)        null,
  rWage                 decimal(10,2)       not null,
  nHolidyPerYear        int                 not null,
  nNoticePeriod         int                 not null, --in Tagen
  nAddressLink          int                 not null,
  nBankingLink          int                 not null,
  nTaxClass             int                 not null,
  szComment             nvarchar(max)       null
)


go


