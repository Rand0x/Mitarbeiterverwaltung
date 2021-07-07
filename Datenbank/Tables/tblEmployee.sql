use dbMAV

go


create table tblEmployee
(
  nKey                  int identity(1,1) primary key,
  nEmployeeNumber       int                 not null,
  szFirstName           nvarchar(200)       not null,
  szLastName            nvarchar(200)       not null,
  dtBirthdate           datetime            null,
  szSex                 nvarchar(1)         null,
  szMail                nvarchar(200)       null,
  szTelephone           nvarchar(50)        null,
  szMobileNumber        nvarchar(50)        null,
  nDepartementLink      int                 null,
  szJobName             nvarchar(200)       null,
  dtRecruitDate         datetime            null,
  nHoursPerWeek         int                 null,
  rOvertime             decimal(5,1)        null,
  rWage                 decimal(10,2)       null,
  nHolidyPerYear        int                 null,
  nNoticePeriod         int                 null, --in Tagen
  nAddressLink          int                 null,
  nBankingLink          int                 null,
  nTaxClass             int                 null,
  szComment             nvarchar(max)       null,
  szMaritalStatus       nvarchar(50)        null
)


go


