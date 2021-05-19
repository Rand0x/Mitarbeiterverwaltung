

use dbMAV


go


insert into tblRight(szName, szInfo)
values (N'Admin', N'Administrator darf alles')
     , (N'HR', N'Human Ressource Mitarbeiter darf vieles')
     , (N'Employee', N'normaler Mitarbeiter darf weniger')




insert into tblEmployee(nEmployeeNumber, szFirstName, szLastName, dtBirthdate, szSex, szMail, szTelephone, nDepartementLink, szJobName, dtRecruitDate, nHoursPerWeek, rOvertime, rWage, nHolidyPerYear, nNoticePeriod, nAddressLink, nBankingLink, nTaxClass)
values (15654, N'HR', N'Mitarbeiter', GETDATE(), N'M', N'HR@MAV.de', N'01544645546', 3, N'HR-Mitarbeiter', GETDATE(), 40, 0, 1500, 30, 30, -1, -1, 1)
     , (87463, N'Max', N'Mustermann', GETDATE(), N'M', N'maxmustermann@MAV.de', N'015445456', 2, N'Mitarbeiter', GETDATE(), 40, 0, 1500, 30, 30, -1, -1, 1)
     , (87463, N'Erika', N'Mustermann', GETDATE(), N'W', N'erikamustermann@MAV.de', N'897445546', 1, N'Mitarbeiter', GETDATE(), 40, 0, 1500, 30, 30, -1, -1, 1)
     , (87463, N'Thomas', N'Mustermann', GETDATE(), N'M', N'thomasmustermann@MAV.de', N'087645546', 3, N'Mitarbeiter', GETDATE(), 40, 0, 1500, 30, 30, -1, -1, 1)
     , (87463, N'Franziska', N'Mustermann', GETDATE(), N'W', N'franziskamustermann@MAV.de', N'015135546', 2, N'Mitarbeiter', GETDATE(), 40, 0, 1500, 30, 30, -1, -1, 1)
     , (87463, N'Antonia', N'Mustermann', GETDATE(), N'W', N'fntoniamustermann@MAV.de', N'015488546', 1, N'Mitarbeiter', GETDATE(), 40, 0, 1500, 30, 30, -1, -1, 1)

     
--select * from tblRight
--select * from tblEmployee


insert into tblUser(szName, szPassword, nEmployeeLink, nRightLink)
values (N'Admin', N'Admin123', null, 1)
     , (N'HR_Mitarbeiter', N'HR123', 1, 2)
     , (N'Max_Mustermann', N'123', 2, 3)
     , (N'Erika_Mustermann', N'123', 3, 3)
     , (N'Thomas_Mustermann', N'123', 4, 3)
     , (N'Franziska_Mustermann', N'123', 5, 3)
     , (N'Antonia_Mustermann', N'123', 6, 3)

--select * from tblUser

insert into tblDepartement(szName, szIdentifier, szInfo, nManagerLink)
values (N'Produktion', N'PRO', N'Produziert Dinge', 6)
     , (N'Marketing', N'MAR', N'Entwickelt Marketingpläne', 5)
     , (N'Human Ressources', N'HR', N'Mitarbeiter einstellen', 1)

go


