using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CineTec.Models;

namespace CineTec.DataAccess
{
    public interface IMovieProvider
    {

       
        List<Movie> GetAllMovies();
        Movie GetMovie(int id);
        void DeleteMovie(int id);
        void UpdateMovie(Movie movie);
        void InsertMovie(MovieComplete movie);

   
        
        
    }
}
