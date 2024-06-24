echo "Creating and pushing the database image"
echo "stoping and removing the container"
docker stop  bookstore-db
docker rm  bookstore-db
docker rmi  bookstore-db-image
docker rmi  bookstore-db-image:latest

echo "building and running the container"
docker build -t  bookstore-db-image .
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=abcDEF123#" -p 1433:1433 --name bookstore-db  bookstore-db-image