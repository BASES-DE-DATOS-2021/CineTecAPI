using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CineTec.Models;

namespace CineTec.DataAccess
{
    public class ProjectionProvider : IProjectionProvider
    {
        private readonly PostgreSqlContext _context;

        public ProjectionProvider(PostgreSqlContext context)
        {
            _context = context;
        }


        public List<Projection> GetAllProjections()
        {

            return _context.projection.ToList();
   
        }

        public Projection GetProjection(int id)
        {
            return _context.projection.FirstOrDefault(c => c.id == id);
        }

        public void DeleteProjection(int id)
        {
            var projection = _context.projection.FirstOrDefault(c => c.id == id);
            _context.projection.Remove(projection);
            _context.SaveChanges();
        }

        public void UpdateProjection(Projection projection)
        {
            _context.projection.Update(projection);
            _context.SaveChanges();
        }

        public void InsertProjection(Projection projection)
        {
            _context.projection.Add(projection);
            _context.SaveChanges();
        }

        public List<Projection> GetProjectionByBranchAndMovie(int room_id, int movie_id)
        {

            var projections = _context.projection.ToList();


          

            return projections.Where(projection => (projection.room_id == room_id && projection.movie_id == movie_id)).ToList();


        }

    }
        
}
