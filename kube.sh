kubectl get pod
kubectl apply -f mongo-config.yaml
kubectl apply -f mongo-secret.yaml
kubectl apply -f mongo.yaml
kubectl apply -f webapp.yaml

kubectl get all
kubectl get svc
kubectl get configmap
kubectl describe mongo-service-url
kubectl get node -o wide
kubectl get svc -o wide
kubectl get pod
kubectl logs webapp-deployment-5db75479c6-t2v7k
kubectl describe service webapp-service-url

minikube start
minikube ip
minikube service webapp-service-url
minikube dashboard

192.168.49.2:30100