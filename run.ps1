docker build . -t movies-server
docker run --publish 6600:6600 movies-server --name movies-server