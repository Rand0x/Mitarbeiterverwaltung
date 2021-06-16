

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


insert into tblUser(szName, szPassword, szSalt, nEmployeeLink, nRightLink)
values (N'Admin', N'005f3e75dea50b01e3d3bed0a0d86d4967e55d8b', N'f637c635415bc01a',null, 1) --Pwd: Admin123
     , (N'HR_Mitarbeiter', N'202c8ca5a518c5ac84d26cb07be5c0a15109a290', N'f507690acd40ccf2', 1, 2) --Pwd: HR123
     , (N'Max_Mustermann', N'10745c5f090193feb7d9dabecbbebcbf2bd076f5', N'c91fe344c7e87aa0', 2, 3) --Pwd: MM123

--select * from tblUser



insert into tblDepartement(szName, szIdentifier, szInfo, nManagerLink)
values (N'Produktion', N'PRO', N'Produziert Dinge', 6)
     , (N'Marketing', N'MAR', N'Entwickelt Marketingpläne', 5)
     , (N'Human Ressources', N'HR', N'Mitarbeiter einstellen', 1)

go


