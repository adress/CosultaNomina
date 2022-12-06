USE TestDB;
-- Create a new stored procedure called 'USP_CONSULTA_NOMINA_POR_DOCUMENTO' in schema 'dbo'
-- Drop the stored procedure if it already exists
IF EXISTS (
SELECT *
FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'dbo'
    AND SPECIFIC_NAME = N'USP_CONSULTA_NOMINA_POR_DOCUMENTO'
    AND ROUTINE_TYPE = N'PROCEDURE'
)
DROP PROCEDURE dbo.USP_CONSULTA_NOMINA_POR_DOCUMENTO
GO
-- Create the stored procedure in the specified schema
CREATE PROCEDURE dbo.USP_CONSULTA_NOMINA_POR_DOCUMENTO
    @DOC_IDENTIDAD VARCHAR(50),
    @EVENTO NUMERIC (5),
    @USUARIO VARCHAR(50) = ADMIN,
    @REGISTRO_NOM_NOMINA_DEFINITIVA NUMERIC(18) = NULL,
    @NEW_VAL_NOMINA INT = NULL
AS
BEGIN
    -- body of the stored procedure
    
    --Consultar
    IF @EVENTO = 1
    BEGIN
        SELECT
            NOMINAS.REGISTRO AS REGISTRO ,
            CONCEPTOS.COD_CONCEPTO AS COD_CONCEPTO,
            CONCEPTOS.DESC_CONCEPTO AS DESC_CONCEPTO,
            EMPLEADOS.ID_EMPLEADO AS ID_EMPLEADO,
            SOLICITUDES.DOCUMENTO AS DOCUMENTO,
            SOLICITUDES.APELLIDOS AS APELLIDOS,
            SOLICITUDES.NOMBRES AS NOMBRES,
            NOMINAS.VAL_NOMINA AS VAL_NOMINA,
            NOMINAS.CANTIDAD AS CANTIDAD
        FROM NOM_NOMINA_DEFINITIVA AS NOMINAS
            INNER JOIN NOM_CONCEPTOS_NOMINA AS CONCEPTOS ON (NOMINAS.ID_CONCEPTO = CONCEPTOS.ID_CONCEPTO)
            INNER JOIN NOM_EMPLEADOS AS EMPLEADOS ON (NOMINAS.ID_EMPLEADO = EMPLEADOS.ID_EMPLEADO)
            INNER JOIN REG_SOLICITUDES_INGRESOS AS SOLICITUDES ON (EMPLEADOS.ID_SOLICITUD = SOLICITUDES.ID_SOLICITUD)
        WHERE
            SOLICITUDES.DOCUMENTO = @DOC_IDENTIDAD
            AND FCH_CRE > '01/01/2014';

        INSERT INTO log_consulta_nom_nomina_definitiva
        VALUES
            (@USUARIO, CAST( GETDATE() AS Date ), 'READ', 'SE CONSULTA INFORMACION DE NOMINA DEL EMPLEADO '+ @DOC_IDENTIDAD);
    END;

    --Modificar el registro @REGISTRO_NOM_NOMINA_DEFINITIVA
    IF @EVENTO = 2
    BEGIN
        UPDATE NOM_NOMINA_DEFINITIVA SET VAL_NOMINA = @NEW_VAL_NOMINA WHERE REGISTRO = @REGISTRO_NOM_NOMINA_DEFINITIVA
        INSERT INTO log_consulta_nom_nomina_definitiva
        VALUES
            (@USUARIO, CAST( GETDATE() AS Date ), 'UPDATE', 'SE ACTUALIZA INFORMACION DE NOMINA DEL EMPLEADO '+ @DOC_IDENTIDAD)
    END;

    --Elimianar el registro @REGISTRO_NOM_NOMINA_DEFINITIVA
    IF @EVENTO = 3
    BEGIN
        DELETE FROM NOM_NOMINA_DEFINITIVA WHERE REGISTRO = @REGISTRO_NOM_NOMINA_DEFINITIVA
        INSERT INTO log_consulta_nom_nomina_definitiva
        VALUES
            (@USUARIO, CAST( GETDATE() AS Date ), 'DELETE', 'SE ELIMINA EL REGISTRO DE NOMINA DEL EMPLEADO '+ @DOC_IDENTIDAD)
    END;

END
GO
-- example to execute the stored procedure we just created
-- EXECUTE dbo.USP_CONSULTA_NOMINA_POR_DOCUMENTO '12345', 1, 'USUARIO', 1, 444
GO
