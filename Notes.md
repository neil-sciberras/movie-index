# Docker
- start docker desktop
- build the image from the Dockerfile (form the root directory)
`docker build . -t movies-server`
- run the image as a container
`docker run --publish 6600:6600 movies-server`


# Sample GraphQL query/mutations:

```
query {
  topRatedMovies(amount: 2){
    name
    rate
  }
}
```

```
query{
  moviesList{
    name
  }
}
```

```
query {
  moviesWithGenre(genre: SCI_FI){
    name
  }
}
```

```
mutation{
  addMovie(newMovie:{
    key:"test-movie"
    name: "Test Movie"
    description:"Test movie added while testing"
    rate: "1.2"
    length: "1hr 40mins"
    image: "n/a"
    genres: [MYSTERY, ACTION]
  }){
    id
  }
}
```

```
query {
  movie(id: 24){
    name
    description
    genres
    image
    rate
  }
}
```

```
mutation{
  updateMovie(movieUpdate:{
    id: 24
    description:"Updated description 3"  
    rate: "9.1"
    genres: [HISTORY]
  }){
    id
    genres
    description
    rate
  }
}
```

```
mutation{
  deleteMovie(movieId: 24){
    name
  }
}
```