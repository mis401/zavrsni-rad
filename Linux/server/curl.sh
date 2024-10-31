curl -X POST -d "10" localhost:8080
curl -X POST -d "4" localhost:8080
curl -X POST -d "15" localhost:8080
curl -X POST -d "20" localhost:8080
curl -X POST -d "3" localhost:8080
curl -X POST -d "16" localhost:8080
curl -X POST -d "19" localhost:8080
curl -X POST -d "28" localhost:8080
curl -X POST -d "6" localhost:8080
curl -X POST -d "35" localhost:8080
curl -X POST -d "12" localhost:8080


sleep 1

curl localhost:8080 --http0.9 --output -