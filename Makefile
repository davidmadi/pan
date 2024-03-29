## help:
all: help

## help: print this help message
.PHONY: help
help:
	@echo 'Usage:'
	@sed -n 's/^##//p' ${MAKEFILE_LIST} | column -t -s ':' |  sed -e 's/^/ /'

.PHONY: migration/add
migration/add:
	@echo 'run this -> dotnet ef migrations add InitMyName --project ./backend/BackApi.csproj'

## migrate: run migration
.PHONY: migrate
migrate:
	export POSTGRES_PASSWORD=postgres
	export POSTGRES_USER=postgres
	export POSTGRES_DB=postgres
	export POSTGRES_HOST=localhost
	dotnet ef database update  -p ./backend/BackApi.csproj
	awslocal s3api create-bucket --bucket my-bucket --region us-east-1

## refresh: refresh docker containers #make refresh
.PHONY: refresh
refresh:
	docker-compose down
	docker image remove -f pan-backend
	docker-compose up -d
	make migrate

## start/docker: migrate and bucket
.PHONY: start/docker
start/docker:
	docker compose up -d

## start/front: migrate and bucket
.PHONY: start/front
start/front:
	cd ./ang
	npm start

## prune: prune docker containers 
.PHONY: prune
prune:
	docker-compose down
	docker image remove -f pan-backend
	docker container prune -f
	docker-compose up -d
