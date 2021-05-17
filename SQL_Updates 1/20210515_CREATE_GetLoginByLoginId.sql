BEGIN TRANSACTION;
GO

CREATE PROCEDURE usp_GetLoginByLoginId
	@id_login BIGINT
AS
BEGIN

SELECT
	r.id AS IDROLE,
	r.name AS NAME
FROM login_role lr
	INNER JOIN role r
		ON lr.id_role = r.id
WHERE 
	lr.id_login = @id_login

END

GO
ROLLBACK;