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
        public DbSet<Acts> ActsIn { get; set; }
        public DbSet<Actor> Actors { get; set; }


        // Overide del OnModelCreating para utilizar dos atributos como llave compuesta.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Seat>()
                .HasKey(s => new { s.room_id, s.number });

            modelBuilder.Entity<Acts>()
                .HasKey(a => new { a.movie_id, a.actor_id });

            modelBuilder.Entity<Branch>()
                .Property(b => b.rooms_quantity)
                .HasDefaultValue(0);
        }


        // Llena una sala con la cantidad de sillas que debe tener. Todas estan vacias.
        public void Update_branch_rooms_quantity(string branch_name)
        {
            var branch = Branches.FirstOrDefault(b => b.cinema_name == branch_name);
            if (branch != null)
            {
                var count = (from r in Rooms.Where(r => r.branch_name == branch_name)
                            select r).CountAsync();
                branch.rooms_quantity = count.Result;
                SaveChanges();
            }
        }


        // Llena una sala con la cantidad de sillas que debe tener. Todas estan vacias.
        public void Add_room_seats(int id, int capacity)
        {
            for (int i = 1; i < capacity + 1; i++)
            {
                Seats.Add(new Seat(id, i, "EMPTY"));
            }
            SaveChanges();

        }


        public IList<Seat> Get_all_seats_of_a_room(string cinema_name, int id)
        {
            // Obtener todas las sillas de una sala que coincida con el id ingresado
            // y a su vez que sea parte de la sucursal que coincide con el cinema_name ingresado.
            var query = from b in Branches.Where(b => b.cinema_name == cinema_name)
                        join room in Rooms.Where(r => r.id == id)
                            on b.cinema_name equals room.branch_name
                        join seat in Seats
                            on room.id equals seat.room_id
                        select new {seat};
            var querySeat = from t in query
                            select t.seat;
            if (!querySeat.Any())
            {
                return null;
            }
            //Seat[] s = querySeat.ToArray();
            IList<Seat> myList = querySeat.Cast<Seat>().ToList();
            return myList;
        }



        // Elimina las salas de una sucursal y luego elimina la sucursal misma.
        public void Delete_cinema_and_rooms(string cinema_name)
        {
            var branch = Branches.FirstOrDefault(b => b.cinema_name == cinema_name);
            if (branch != null)
            {
                // query para obtener las salas y sillas relacionadas a la sucursal
                var queryRoomSeat = from b in Branches
                        .Where(b => b.cinema_name == cinema_name)
                            join room in Rooms
                                on b.cinema_name equals room.branch_name
                            join seat in Seats
                                on room.id equals seat.room_id
                            select new { room, seat };

                var queryRoom = from t in queryRoomSeat
                                select t.room;

                var querySeat = from t in queryRoomSeat
                                select t.seat;

                // query para obtener los empleados relacionados a la sucursal
                var queryEmployees = from b in Branches
                        .Where(b => b.cinema_name == cinema_name)
                            join emp in Employees
                                on b.cinema_name equals emp.branch_id
                            select new { emp };

                var queryEmp = from t in queryEmployees
                               select t.emp;

                Seat[] s = querySeat.ToArray();
                Room[] r = queryRoom.ToArray();
                Employee[] e = queryEmp.ToArray();

                Seats.RemoveRange(s);
                SaveChanges();

                Rooms.RemoveRange(r);
                SaveChanges();

                Employees.RemoveRange(e);
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
            SaveChanges();
            Rooms.Remove(room);
            SaveChanges();
        }
        public void Update_Room(int id, Room r)
        {
            var room = Rooms.FirstOrDefault(x => x.id == id);
            if (room != null)
            {
                room.column_quantity = r.column_quantity;
                room.row_quantity = r.row_quantity;
            }
            Rooms.Update(room);
            //Update_branch_rooms_quantity(room.branch_name);
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
