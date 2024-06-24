echo "commiting the container"
docker commit  bookstore-db
echo "tagging the container"
docker tag  bookstore-db-image alvanxp/bookstore-db-image:latest
echo "pushing the container"
docker push alvanxp/bookstore-db-image:latest
docker stop bookstore-db