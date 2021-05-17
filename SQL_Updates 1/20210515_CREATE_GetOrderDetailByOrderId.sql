BEGIN TRANSACTION;
GO;

CREATE PROCEDURE usp_GetOrderDetailByOrderId
	@id_order BIGINT
AS
BEGIN

SELECT 
	detail.id AS ORDERID,
	c.id AS IDCOMMODITY,
	c.internal_code AS INTERNALCODE,
	c.name AS NAME,
	c.average_weight AS AVERAGEWEIGHT,
	detail.amount AS AMOUNT,
	detail.price AS PRICE,
	detail.no_unit AS NOUNIT,
	(SELECT name from unit_of_measurement where unit_of_measurement.id = c.id_unit_of_measurement) AS UNIT
FROM order_detail detail
	INNER JOIN commodity c
		ON detail.id_commodity = c.id
WHERE
	detail.id = 64

END

GO
ROLLBACK;