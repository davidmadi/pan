removefront(){
  docker image rm pan-frontend
}
removeDB(){
  docker image rm postgres
}
toPrune(){
  docker container prune -f
}

docker-compose down
docker image rm pan-backend
docker-compose up -d
