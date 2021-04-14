

use dbBsp

go


create proc sp_LoadPersons
as begin

select nID, szFirstName, szLastName from tblPerson

end


go



