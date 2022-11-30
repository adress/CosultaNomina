-- Create a new stored procedure called 'SPR_CONSULTA_EMPLEADOS' in schema 'dbo'
-- Drop the stored procedure if it already exists
IF EXISTS (
SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'dbo'
    AND SPECIFIC_NAME = N'SPR_CONSULTA_EMPLEADOS'
    AND ROUTINE_TYPE = N'PROCEDURE'
)
DROP PROCEDURE dbo.SPR_CONSULTA_EMPLEADOS
GO
-- Create the stored procedure in the specified schema
CREATE PROCEDURE dbo.SPR_CONSULTA_EMPLEADOS
AS
BEGIN
    -- body of the stored procedure
    WITH
        resumen
        AS
        (
            SELECT 
                sol.ID_SOLICITUD,
                sol.DOCUMENTO,
                sol.APELLIDOS,
                sol.NOMBRES,
                ROW_NUMBER() OVER(PARTITION BY sol.DOCUMENTO 
                                    ORDER BY sol.ID_SOLICITUD DESC) AS rank
            FROM REG_SOLICITUDES_INGRESOS sol
        )
    SELECT ID_SOLICITUD, DOCUMENTO, APELLIDOS, NOMBRES
    FROM resumen
    WHERE rank = 1;
END
GO
-- example to execute the stored procedure we just created
-- EXECUTE dbo.SPR_CONSULTA_EMPLEADOS
GO