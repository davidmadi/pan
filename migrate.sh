export POSTGRES_PASSWORD=postgres
export POSTGRES_USER=postgres
export POSTGRES_DB=postgres
export POSTGRES_HOST=localhost
dotnet ef database update  -p ./backend/BackApi.csproj