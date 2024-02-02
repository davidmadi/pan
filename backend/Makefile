
all: help

## help: print this help message
.PHONY: help
help:
	@echo 'Usage:'
	@sed -n 's/^##//p' ${MAKEFILE_LIST} | column -t -s ':' |  sed -e 's/^/ /'

## migrate: run migration
.PHONY: migrate
migrate:
	export POSTGRES_PASSWORD=postgres
	export POSTGRES_USER=postgres
	export POSTGRES_DB=postgres
	export POSTGRES_HOST=localhost
	dotnet ef database update  -p ./BackApi.csproj

## start: run dotnet api
.PHONY: start
start:
	dotnet ef database update
	dotnet /App/bin/Release/net8.0/BackApi.dll
