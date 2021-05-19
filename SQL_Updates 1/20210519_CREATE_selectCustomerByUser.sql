BEGIN TRANSACTION;
GO;

CREATE OR ALTER PROCEDURE usp_selectCustomerByUser
	@id_user BIGINT
AS
BEGIN

SELECT
	id AS ID,
	id_system AS ID_SYSTEM,
	id_company AS ID_COMPANY,
	name AS NAME,
	address AS ADDRESS,
	CUIT as CUIT,
	phone AS PHONE,
	balance AS BALANCE,
	(SELECT zipcode.zipcode from zipcode where zipcode.id=c.zipcode) as ZIPCODE,
	(SELECT city.name from city where city.id=c.city) as CITY,
	(SELECT iva.name from iva where iva.id=c.iva) as IVA,
	(SELECT seller.name from seller where seller.id=c.seller) as SELLER,
	(SELECT zone.zone from zone where zone.id=c.zone) as ZONE,
	(SELECT route.name from route where route.id=c.route) as ROUTE,
	(SELECT custommer_type.name from custommer_type where custommer_type.id=c.custommer_type) as TYPE,
	(SELECT activity.name from activity where activity.id=c.activity) as ACTIVITY,
	(SELECT branch.name from branch where branch.id=c.branch) as BRANCH

FROM customer c
WHERE 
	c.seller = (SELECT seller FROM login WHERE id = @id_user)
ORDER BY c.name ASC

END

GO;
ROLLBACK;