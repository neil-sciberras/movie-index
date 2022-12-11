## The 2 top most rated movies
```
query {
  topRatedMovies(amount: 2){
    name
    rate
  }
}
```

## Get all movies
```
query{
  moviesList{
    name
  }
}
```

## Filter by genre
```
query {
  moviesWithGenre(genre: SCI_FI){
    name
  }
}
```

## Add a movie
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

## Retrieve the latest added movie
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

## Update the latest added movie
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

## Delete the latest added movie
```
mutation{
  deleteMovie(movieId: 24){
    name
  }
}
```