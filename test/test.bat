del .\output\dat.txt

docker-compose up -d

curl -v -X POST http://localhost:8084/processing

timeout 1

type .\output\dat.txt

docker-compose down