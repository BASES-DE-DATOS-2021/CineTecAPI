using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CineTec.Models;

namespace CineTec.DataAccess
{
    public interface IProjectionProvider
    {

       
        List<Projection> GetAllProjections();
        Projection GetProjection(int id);
        void DeleteProjection(int id);
        void UpdateProjection(Projection projection);
        void InsertProjection(Projection projection);

        public List<Projection> GetProjectionByBranchAndMovie(int room_id, int movie_id);






    }
}
