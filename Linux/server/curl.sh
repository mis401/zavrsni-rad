curl -X POST -d "10" localhost:8080
curl -X POST -d "4" localhost:8080
curl -X POST -d "15" localhost:8080
curl -X POST -d "20" localhost:8080

sleep 1

curl localhost:8080 --http0.9 --output -