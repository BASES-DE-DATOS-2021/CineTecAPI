using Microsoft.EntityFrameworkCore;
using CineTec.Models;

namespace CineTec.Context
{
    public class CRUDContext : DbContext
    {

        public CRUDContext(DbContextOptions<CRUDContext> options) : base(options)
        {

        }

        // Entities
        public DbSet<Student> Students { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Projection> Projections { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Classification> Classifications { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Acts> ActsIn { get; set; }
        public DbSet<Actor> Actors { get; set; }


        // we override the OnModelCreating method here.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Seat>()
                .HasKey(s => new { s.room_id, s.number });

            modelBuilder.Entity<Acts>()
                .HasKey(a => new { a.movie_id, a.actor_id });
        }

        private bool EvalRooms(string cinema_name)
        {
            var branch = Branches.SingleOrDefaultAsync(x => x.cinema_name == cinema_name);
            if (branch != null)
            {
                //var query = from b in Set<BlogWithMultiplePosts>()
                //            where b.PostCount > 3
                //            select new { b.Url, b.PostCount };
                // Encontrar la cantidad de salas referentes a esta sucursal en el momento.
                //var rooms = Rooms.Where(r => r.branch_name == cinema_name);

                //// Evaluar si aun hay espacio para poder agregar salas en la sucursal.
                //if (rooms != null)
                //{
                //    return rooms.Count() < branch.room_quantity;
                //}
            }
            return false;
        }

    }

}
