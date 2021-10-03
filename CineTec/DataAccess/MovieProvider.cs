using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CineTec.Models;

namespace CineTec.DataAccess
{
    public class MovieProvider : IMovieProvider
    {
        private readonly PostgreSqlContext _context;

        public MovieProvider(PostgreSqlContext context)
        {
            _context = context;
        }


        public List<Movie> GetAllMovies()
        {

            return _context.movie.ToList();
   
        }

        public Movie GetMovie(int id)
        {
            return _context.movie.FirstOrDefault(c => c.id == id);
        }

        public void DeleteMovie(int id)
        {
            var movie = _context.movie.FirstOrDefault(c => c.id == id);
            _context.movie.Remove(movie);
            _context.SaveChanges();
        }

        public void UpdateMovie(Movie movie)
        {
            _context.movie.Update(movie);
            _context.SaveChanges();
        }

        public void InsertMovie(MovieComplete movie)
        {

            var newMovie = new Movie();
            newMovie.classification_id = movie.classification_id;
            newMovie.original_name = movie.original_name;
            newMovie.image = movie.image;
            newMovie.name = movie.name;
            newMovie.length = movie.length;

           _context.movie.Add(newMovie);

            var currentMovie = _context.movie.OrderByDescending(x => x.id).Take(1);


      





            




            _context.SaveChanges();
        }

    }
        
}
