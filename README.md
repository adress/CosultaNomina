## Consulta Nomina .NET6

### Ejecución del proyecto
- Correr los scripts de la base de datos que se encuentran en la carpeta `/Recursos`
    - tablas.sql
    - procecure1.sql
    - procedure2.sql

- Configurar la cadena de conexión `ConexionSqlServer` que se encuentra en el archivo `appsettings.json` con las credenciales de su base de datos SQLServer.

- Ejecutar la aplicacion. Si usa dotnet CLI puede ejecutar
    ```bash
    dotnet run
    ```

### Ejecución Docker con compose

- Configurar la cadena de conexión `ConexionSqlServer` que se encuentra en el archivo `appsettings.json` con las credenciales de su base de datos SQLServer (la clave se define en el archivo `docker-compose` en la variable de entorno `MSSQL_SA_PASSWORD`)

   ```bash
    docker compose up
    ```
- Crear la base de datos
    ```bash
    docker compose exec -it  admin-consultanomina-db bash
    /opt/mssql-tools/bin/sqlcmd -S localhost -U SA
    ```
    ```sql
    CREATE DATABASE TestDB;
    GO
    exit
    ```
- Ejecutar los archivos necesarios para el funcionamiento
    ```bash
    /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -i tablas.sql
    /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -i procedure1.sql
    /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -i procedure2.sql
    ```