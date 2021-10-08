﻿using Microsoft.EntityFrameworkCore;
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
        public DbSet<Acts> Acts { get; set; }
        public DbSet<Actor> Actors { get; set; }


        // Overide del OnModelCreating para utilizar dos atributos como llave compuesta.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Seat>()
                .HasKey(s => new { s.room_id, s.number });

            modelBuilder.Entity<Acts>()
                .HasKey(a => new { a.movie_id, a.actor_id });


        }


        /*
         *              AUX
         *              AUX
         *              AUX
         *              
         */

        // Llena una sala con la cantidad de sillas que debe tener. Todas estan vacias.
        public void Add_seats_in_a_room(int id, int capacity)
        {
            for (int i = 1; i < capacity + 1; i++)
            {
                Seat s = new Seat();
                s.room_id = id;
                s.number = i;
                s.status = "EMPTY";
                Seats.Add(s);
               
            }
            SaveChanges();
        }


        /*
         *              GET
         *              GET
         *              GET
         *              
         */

        // Get de director por id.
        public Director GetDirector(int id)
        {
            return Directors.SingleOrDefault(x => x.id == id);
        }

        // Get de director por nombre.
        public Director GetDirector(string name)
        {
            return Directors.SingleOrDefault(x => x.name == name);
        }

        // Get de employee por cedula.
        public Employee GetEmployee(int cedula)
        {
            return Employees.Where(f => f.cedula == cedula).FirstOrDefault();
        }

        // Get de room por id.
        public Room GetRoom(int room_id)
        {
            return Rooms.Where(f => f.id == room_id).FirstOrDefault();

        }

        // Get de seat por room_id y numero de asiento.
        public Seat GetSeat(int room_id, int number)
        {
            return Seats.Where(f => f.number == number && f.room_id == room_id)
                        .FirstOrDefault();
        }
       

        // Get de projecciones por id.
        public Projection GetProjection(int id)
        {
            return Projections.Where(f => f.id == id).FirstOrDefault();
        }

        // 
        public IEnumerable<Projection> GetProjections_byRoomId(int room_id)
        {
            return Projections.Where(f => f.room_id == room_id);
        }

        //
        public IEnumerable<Projection> GetProjections_byMovieId(int movie_id)
        {
            return Projections.Where(f => f.movie_id == movie_id);
        }

        //
        public IList<string> GetProjections_dates_byBranch(string cinema_name)
        {
            var query = from b in Branches.Where(b => b.cinema_name == cinema_name)
                        join r in Rooms
                            on b.cinema_name equals r.branch_name
                        join p in Projections
                            on r.id equals p.room_id
                        select p.FormattedDate;

            IList<string> myList = query.Cast<string>().ToList();
            return myList;
        }





        //
        public Acts GetActs(int movie_id, int actor_id)
        {
            return Acts.Where(f => f.movie_id == movie_id && f.actor_id == actor_id).FirstOrDefault();
        }

        //
        public IEnumerable<Acts> GetActs_byMovieId(int movie_id)
        {
            return Acts
                    .Where(f => f.movie_id == movie_id);
        }

        //
        public IEnumerable<Acts> GetActs_byActorsId(int actor_id)
        {
            return Acts
                    .Where(f => f.actor_id == actor_id);
        }


        // Get de cliente por cedula.
        public Client GetClient(int cedula)
        {
            return Clients.SingleOrDefault(x => x.cedula == cedula);
        }

        // Get de bill por id.
        public Bill GetBill(int id)
        {
            return Bills.SingleOrDefault(x => x.id == id);
        }

        // Get de bills correspondientes a un cliente.
        public IEnumerable<Bill> GetBills_byClientId(int cedula)
        {
            return Bills.Where(x => x.client_id == cedula);
        }

        /// METODOS MAS ESPECIFICOS.

        // Funcion que retorna todas las salas de la sucursal que coincide con el cinema_name ingresado.
        public IList<Room> Get_all_rooms_of_a_branch(string cinema_name)
        {
            // Obtener todas las salas de la sucursal que coincide con el cinema_name ingresado.
            var query = from b in Branches.Where(b => b.cinema_name == cinema_name)
                        join room in Rooms
                            on b.cinema_name equals room.branch_name
                        select new { room };
            var queryRoom = from t in query
                            select t.room;
            IList<Room> myList = queryRoom.Cast<Room>().ToList();
            return myList;
        }

        // Funcion que retorna todas las sillas de una sala que coincida con el id ingresado y a su
        // vez que sea parte de la sucursal que coincide con el cinema_name ingresado.
        public IList<Seat> Get_all_seats_of_a_room(string cinema_name, int id)
        {
            // Obtener todas las sillas de una sala que coincida con el id ingresado
            // y a su vez que sea parte de la sucursal que coincide con el cinema_name ingresado.
            var query = from b in Branches.Where(b => b.cinema_name == cinema_name)
                        join room in Rooms.Where(r => r.id == id)
                            on b.cinema_name equals room.branch_name
                        join seat in Seats
                            on room.id equals seat.room_id
                        select new { seat };
            var querySeat = from t in query
                            select t.seat;
            IList<Seat> myList = querySeat.Cast<Seat>().ToList();
            return myList;
        }




        /*
         *              UPDATE
         *              UPDATE
         *              UPDATE
         *              
         */

        public void Update_Room(int id, Room r)
        {
            var room = Rooms.FirstOrDefault(x => x.id == id);
            if (room != null)
            {
                if (room.column_quantity != r.column_quantity || room.row_quantity != r.row_quantity)
                {
                    room.column_quantity = r.column_quantity;
                    room.row_quantity = r.row_quantity;
                    Rooms.Update(room);
                    SaveChanges();

                    // ELIMINAR SILLAS Y RECREARLAS.
                    Delete_seats_of_a_room(id);
                    Add_seats_in_a_room(id, room.capacity);
                }

            }
        }

        public void Update_Movie_ByName(string original_name, Movie m)
        {
            var movie = Movies.FirstOrDefault(x => x.original_name == original_name);
            if (movie != null)
            {
                movie.classification_id = m.classification_id;
                movie.original_name = m.original_name;
                movie.director_id = m.director_id;
                movie.name = m.name;
                movie.image = m.image;
                movie.length = m.length;
                Movies.Update(movie);
                SaveChanges();
            }
        }


        // Metodo que retorna las sillas modificadas de un cuarto con el estado dependiendo de
        // la probabilidad recibida.
        public void Update_room_seats_status_restriction(int room_id, int prob)
        {
            Room r = GetRoom(room_id);
        }





        /*
         *              DELETE
         *              DELETE
         *              DELETE
         *              
         */



        /* notas:
         * 
         *  1 : ELIMINACION EXITOSA.
         *  0 : NO SE PUEDE ELIMINAR POR RELACION.
         * -1 : NO EXISTE.
         */


        // Eliminacion especial de director tomando en cuenta si hay alguna referencia.
        public int Delete_actor(int id)
        {
            var actor = Actors.FirstOrDefault(x => x.id == id);
            if (actor != null)
            {
                // verificar si hay peliculas asociadas a este director.
                var acts = Acts.FirstOrDefault(m => m.actor_id == id);
                if (acts == null)
                {
                    // ELIMINAR ACTOR
                    Actors.Remove(actor);
                    SaveChanges();
                    return 1;
                }
                return 0;
            }
            return -1;
        }

        // Eliminacion especial de director tomando en cuenta si hay alguna referencia.
        public int Delete_acts(int movie_id, int actor_id)
        {
            var acts = GetActs(movie_id, actor_id);
            if (acts != null)
            {
                // verificar si hay peliculas asociadas a este acts.
                var movie = Movies.FirstOrDefault(m => m.id == movie_id);
                if (movie == null)
                {
                    // ELIMINAR ACTS
                    Acts.Remove(acts);
                    SaveChanges();
                    // NOTA: Los no se borran, solo quedan en la tabla sin relacion alguna. 
                    return 1;
                }
                return 0;
            }
            return -1;
        }

        // Eliminacion especial de cliente tomando en cuenta si hay alguna referencia.

        public int Delete_client(int cedula)
        {
            var client = GetClient(cedula);
            if (client != null)
            {
                // verificar si hay una factura asociada a este cliente.
                var bill = GetBill(client.cedula);
                if (bill == null)
                {
                    // ELIMINAR CLIENTE
                    Clients.Remove(client);
                    SaveChanges();
                    return 1;
                }
                return 0;
            }
            return -1;
        }

        // Eliminacion especial de clasificacion tomando en cuenta si hay alguna referencia.
        public int Delete_classification(string code)
        {
            var classif = Classifications.FirstOrDefault(x => x.code == code);
            if (classif != null)
            {
                // verificar si hay peliculas asociadas a esta clasificacion.
                var movie = Movies.FirstOrDefault(m => m.classification_id == code);
                if (movie == null)
                {
                    // ELIMINAR CLASSIF
                    Classifications.Remove(classif);
                    SaveChanges();
                    return 1;
                }
                return 0;
            }
            return -1;
        }


        // Eliminacion especial de director tomando en cuenta si hay alguna referencia.
        public int Delete_director(string name)
        {
            var director = GetDirector(name);
            if (director != null)
            {
                // verificar si hay peliculas asociadas a este director.
                var movie = Movies.FirstOrDefault(m => m.director_id == director.id);
                if (movie == null)
                {
                    // ELIMINAR DIRECTOR
                    Directors.Remove(director);
                    SaveChanges();
                    return 1;
                }
                return 0;
            }
            return -1;
        }

        // Eliminacion especial de projection tomando en cuenta si hay alguna referencia.
        public int Delete_projection(int id)
        {
            var projection = GetProjection(id);
            if (projection != null)
            {
                // verificar si hay una factura asociada a este cliente.
                var bill = GetBill(projection.id);
                if (bill == null)
                {
                    // ELIMINAR PROJECTION
                    Projections.Remove(projection);
                    SaveChanges();
                    return 1;
                }
                return 0;
            }
            return -1;
        }




        /// METODOS MAS ESPECIFICOS.

        // Elimina las salas de una sucursal y luego elimina la sucursal misma.
        public int Delete_cinema_and_rooms(string cinema_name)
        {
            var branch = Branches.FirstOrDefault(b => b.cinema_name == cinema_name);
            if (branch != null)
            {
                // query para obtener los empleados relacionados a la sucursal
                var emp = Employees.FirstOrDefault(e => e.branch_id == cinema_name);
                
                if (emp == null)
                {
                    // query para obtener las salas y sillas relacionadas a la sucursal
                    var queryRoomSeat = from b in Branches.Where(b => b.cinema_name == cinema_name)
                                        join room in Rooms
                                            on b.cinema_name equals room.branch_name
                                        join seat in Seats
                                            on room.id equals seat.room_id
                                        select new { room, seat };

                    var queryRoom = from t in queryRoomSeat select t.room;
                    var querySeat = from t in queryRoomSeat select t.seat;
                    Seat[] s = querySeat.ToArray();
                    Room[] r = queryRoom.ToArray();

                    // Procede a borrar las sillas, luego las salas y por ultimo las sucursales.
                    Seats.RemoveRange(s);
                    SaveChanges();

                    Rooms.RemoveRange(r);
                    SaveChanges();

                    Branches.Remove(branch);
                    SaveChanges();
                    return 1;
                }
                return 0; 
            }
            return -1; 

        }

        // Elimina las sillas de una sala y luego elimina la sala misma.
        public void Delete_room_and_seats(int id)
        {
            var room = Rooms.FirstOrDefault(x => x.id == id);
            if (room != null)
            {
                Delete_seats_of_a_room(id);
            }
            Rooms.Remove(room);
            SaveChanges();
        }

        // Elimina las sillas de una sala.
        public void Delete_seats_of_a_room(int id)
        {
            Seats.RemoveRange(Seats.Where(x => x.room_id == id));
            SaveChanges();
        }















        // COMENTADA POR SI SE NECESITA ALGUN JOIN PARECIDO.

        //// Funcion que verifica si existe espacio para crear una nueva sala dentro de una sucursal
        //// tomando en cuenta que no exceda la capacidad maxima de salas en la sucursal.
        //public bool Evaluate_if_its_space_for_new_room_in_a_branch(string cinema_name)
        //{
        //    var branch = Branches.FirstOrDefault(x => x.cinema_name == cinema_name);
        //    if (branch != null)
        //    {
        //        // Encontrar la cantidad de salas referentes a esta sucursal en el momento.
        //        var query = from b in Branches.Where(b => b.cinema_name == cinema_name)
        //                    join r in Rooms
        //                        on b.cinema_name equals r.branch_name
        //                    select new { b.cinema_name, r.id };

        //        // Evaluar si aun hay espacio para poder agregar salas en la sucursal.
        //        if (query != null)
        //        {
        //            int current_rooms_number = (from t in query select t.id).Count();
        //            return current_rooms_number < branch.room_quantity;
        //        }
        //    }
        //    return false;
        //}

    }

}
