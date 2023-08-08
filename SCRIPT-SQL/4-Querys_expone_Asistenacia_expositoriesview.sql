create table expone(
	Id serial primary key,
    evento char(36) CHARACTER SET ascii NOT NULL,
    customer char(36) CHARACTER SET ascii NOT NULL
);


create view expositoriesview as select e.Id as idEvento, e.evento, c.CustomerName, c.ContactPerson, c.Email, c.MobileNo, c.PhoneNo, c.Website, 
c.Description, c.Address, c.CountryName, c.CityName  from expone e 
inner join customers c on c.Id = e.customer
inner join eventos x on x.Id = e.evento where c.IsDeleted = 0


CREATE TABLE `asistencia` (
  `Id` serial primary KEY,
  `usersId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `eventosId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL
);

