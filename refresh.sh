removefront(){
  docker image rm pan-frontend
}

docker-compose down
docker container prune -f
docker image rm pan-backend
docker image rm postgres
docker-compose up -d
