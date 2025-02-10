# Otanimes API

Dockerfile.database
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YOUR_PASSWORD>" -p 1433:1433 -d --name sqlserverfts sqlserver-fts

dotnet user-secrets set "ConnectionStrings:SqlServer" "Server=127.0.0.1;User Id=sa;Password=<YOUR_PASSWORD>;Database=Otanimes;TrustServerCertificate=True"

dotnet user-secrets set "Hash:Salt" "<YOUR_SALT>"

dotnet user-secrets set "Jwt:Secret" "<YOUR_SALT>"

dotnet ef migrations add InitialMigration --project src/Otanimes.Infrastructure/Otanimes.Infrastructure.csproj

dotnet ef database update --connection "Server=127.0.0.1;User Id=sa;Password=<YOUR_PASSWORD>;Database=Otanimes;TrustServerCertificate=True" --context OtanimesContext --project src/Otanimes.Infrastructure/Otanimes.Infrastructure.csproj