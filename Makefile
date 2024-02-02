## help:
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
	dotnet ef database update  -p ./backend/BackApi.csproj

## refresh: refresh docker containers #make refresh
.PHONY: refresh
refresh:
	docker-compose down
	docker image remove -f pan-backend
	docker-compose up -d

## prune: prune docker containers 
.PHONY: prune
prune:
	docker-compose down
	docker image remove -f pan-backend
	docker container prune -f
	docker-compose up -d
