BEGIN TRANSACTION;

GO
CREATE PROCEDURE usp_GetAllOrders

AS
BEGIN

SELECT
	header.id AS ID,
	header.datetime AS DATETIME,
	c.name as CUSTOMER,
	c.CUIT as CUSTOMERCUIT,
	c.id_system as CUSTOMERINTERNALID,
	c.id_company as IDCOMPANY
FROM order_header header
	LEFT JOIN customer c
		ON c.id = header.id_customer
WHERE 
	header.transmited = 0
ORDER BY DATETIME desc

END
GO

ROLLBACK;