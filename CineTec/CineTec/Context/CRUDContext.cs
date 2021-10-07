using Microsoft.EntityFrameworkCore;
using CineTec.Models;
using System.Linq;
using System;
using System.Collections.Generic;

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

        // 
        public bool Evaluate_if_its_space_for_new_room_in_branch(string cinema_name)
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


        // Elimina las salas de una sucursal y luego elimina la sucursal misma.
        public void Delete_cinema_and_rooms(string cinema_name)
        {
            var branch = Branches.FirstOrDefault(b => b.cinema_name == cinema_name);
            if (branch != null)
            {
                var query = from b in Branches
                        .Where(b => b.cinema_name == cinema_name)
                            join room in Rooms
                                on b.cinema_name equals room.branch_name
                            join seat in Seats
                                on room.id equals seat.room_id
                            select new { room, seat };

                var queryRoom = from t in query
                                select t.room;

                var querySeat = from t in query
                                select t.seat;

                Seat[] s = querySeat.ToArray();
                Room[] r = queryRoom.ToArray();

                Seats.RemoveRange(s);
                SaveChanges();
                Rooms.RemoveRange(r);
                SaveChanges();
                Branches.Remove(branch);
                SaveChanges();
            }
        }

        // Elimina las sillas de una sala y luego elimina la sala misma.
        public void Delete_room_and_seats(int id)
        {
            var room = Rooms.FirstOrDefault(x => x.id == id);
            if (room != null)
            {
                Seats.RemoveRange(Seats.Where(x => x.room_id == id));
            }
            Rooms.Remove(room);
            SaveChanges();
        }



    }

}
