# Installion Instructions

### 1. Downloading SQL Server Management Studio (Recommended) and SQL Server
If you do not have SQL Server Management Studio or SQL Server downloaded, they can be downloaded via the following links:
- SQL Server Management Studio: https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16&redirectedfrom=MSDN#download-ssms
- Microsoft SQL Server: https://www.microsoft.com/en-us/sql-server/sql-server-downloads

### 2. Connecting the API to SQL Server
SQL Server's default name is set to SQLEXPRESS if using the express version, if your server name is different then you will need to change it to that name in app.settings
- Open the Command Line Interface and copy and paste the following into the CLI: \
`dotnet ef database update`
- To add a new migration you can use the following in the Command Line Interface: \
`dotnet ef migrations add <name>`