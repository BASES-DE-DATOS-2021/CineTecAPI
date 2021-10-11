﻿using Microsoft.EntityFrameworkCore;
using CineTec.Models;
using System.Linq;
using System;
using System.Collections.Generic;
using EntityFramework.Exceptions.Common;
using Npgsql;
using CineTec.JSON_Models;

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

        public DbSet<ProjectionJSON> ProjectionJSONs { get; set; }


        // Overide del OnModelCreating para utilizar dos atributos como llave compuesta.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Seat>()
                .HasKey(s => new { s.room_id, s.number });

            modelBuilder.Entity<Acts>()
                .HasKey(a => new { a.movie_id, a.actor_id });

            modelBuilder.Entity<ProjectionJSON>()
                .HasKey(a => new { a.movie, a.date});

            // ACTOR SOLO PUEDE TENER UN NOMBRE UNICO EN TODA LA TABLA.
            modelBuilder.Entity<Actor>()
                .HasIndex(a => a.name)
                .IsUnique(true);

            // DIRECTOR SOLO PUEDE TENER UN NOMBRE UNICO EN TODA LA TABLA.
            modelBuilder.Entity<Director>()
                .HasIndex(d => d.name)
                .IsUnique(true);

            // CLASSIF SOLO PUEDE TENER UN RANGO UNICO DE EDAD EN TODA LA TABLA.
            modelBuilder.Entity<Classification>()
                .HasIndex(c => c.age_rating)
                .IsUnique(true);


        }



        /* notas:
         * 
         *  1 : ELIMINACION EXITOSA.
         *  0 : NO SE PUEDE ELIMINAR POR RELACION.
         * -1 : NO EXISTE.
         */

        /*
         *  LOGINS
         */

        public Employee Login_admin(string username, string password)
        {
            Employee admin = Employees.Where(x => x.username == username && x.password == password).FirstOrDefault();
            if (admin == null) return null;
            return admin;
        }

        public Client Login_client(string username, string password)
        {
            Client user = Clients.Where(x => x.username == username && x.password == password).FirstOrDefault();
            if (user == null) return null;
            return user;
        }


        /*
         *      ACTOR
         */

        // GET ACTOR BY NAME
        public Actor GetActor(string name) => Actors.SingleOrDefault(x => x.name == name);

        // GET ACTOR BY ID
        public Actor GetActor(int id) => Actors.SingleOrDefault(x => x.id == id);

        // GET ACTORS NAMES BY MOVIE_ID
        public List<string> GetActors_names(int movie_id)
        {
            var query = from a in Acts.Where(x => x.movie_id == movie_id)
                        join p in Actors
                            on a.actor_id equals p.id
                        select p.name;
            var actors = query.ToList();
            return actors;
        }

        // GET ACTORS IDS BY MOVIE_ID
        public List<int> GetActors_ids(int movie_id)
        {
            var query = from a in Acts.Where(x => x.movie_id == movie_id)
                        join p in Actors
                            on a.actor_id equals p.id
                        select p.id;
            var actors = query.ToList();
            return actors;
        }


        // POST
        public int Post_actor(Actor actor)
        {
            // Verificar la existencia.
            Actor existing = GetActor(actor.name);
            if (existing != null)
                return 0; //Ya existe

            Actors.Add(actor);
            SaveChanges();
            return 1; // Se logra agregar.
        }

        // PUT
        public string Put_actor(Actor actor, string current_name)
        {
            // Verificar la existencia.
            Actor existing = GetActor(current_name);
            if (existing == null)
                return "No se ha encontrado un actor con este nombre."; // No existe
            // Verificar existencia de un actor con el nombre.
            Actor put_name = GetActor(actor.name);
            if (put_name != null)
                return "El nombre al que desea actualizar ya se encuentra en uso. Por favor ingrese otro.";

            existing.name = actor.name;
            Actors.Update(existing);
            SaveChanges();
            return "";
        }

        // DELETE especial de director tomando en cuenta si hay alguna referencia.
        public string Delete_actor(string name)
        {
            var actor = GetActor(name);
            if (actor == null)
                return "No se ha encontrado un actor con ese nombre."; // No existe.

            // verificar si hay peliculas asociadas a este director.
            var acts = Acts.FirstOrDefault(m => m.actor_id == actor.id);
            if (acts == null)
            {
                // ELIMINAR ACTOR
                Actors.Remove(actor);
                SaveChanges();
                return ""; // Se borra correctamente.
            }
            return "No se puede eliminar un actor que se encuentra asignado a una pelicula."; // Presenta relacion con peliculas.
        }


        /*
         *      ACTS
         */

        // GET ACTS BY KEY (MOVIE_ID, ACTOR_ID)
        public Acts GetActs(int movie_id, int actor_id) => Acts.Where(f => f.movie_id == movie_id && f.actor_id == actor_id).FirstOrDefault();

        // GET ACTS BY MOVIE_ID
        public IEnumerable<Acts> GetActs_byMovieId(int movie_id) => Acts.Where(f => f.movie_id == movie_id);

        // GET ACTS BY ACTOR_ID
        public IEnumerable<Acts> GetActs_byActorsId(int actor_id) => Acts.Where(f => f.actor_id == actor_id);


        // POST AN ACTS
        public int Post_acts(int movie_id, int actor_id)
        {
            // Verificar la existencia.
            Acts existing = GetActs(movie_id, actor_id);
            if (existing != null)
                return 0; // ya existe.

            Acts a = new Acts
            {
                actor_id = actor_id,
                movie_id = movie_id
            };
            Acts.Add(a);
            SaveChanges();
            return 1; // Se logra agregar.
        }

        // PUT -> no hay.

        // DELETE especial de acts tomando en cuenta si hay alguna referencia.
        private void Delete_acts_background(int movie_id, int actor_id)
        {
            var acts = GetActs(movie_id, actor_id);
            if (acts != null)
            {
                // ELIMINAR ACTS
                Acts.Remove(acts);
                SaveChanges();
                // NOTA: Los actores no se borran, solo quedan en la tabla sin relacion alguna. 
            }
        }



        /*
         *      BILL
         */

        // GET BILL BY ID
        public Bill GetBill(int cedula) => Bills.SingleOrDefault(x => x.client_id == cedula);

        // GET ALL CLIENT BILLS
        public IEnumerable<Bill> GetBills_byClientId(int cedula) => Bills.Where(x => x.client_id == cedula);

        // POST BILL
        public void Post_bill(Bill bill)
        {
            Bills.Add(bill);
            SaveChanges();
        }

        // DELETE
        public int Delete_bill(int cedula)
        {
            var bill = GetBill(cedula);
            if (bill == null)
                return -1;  // No existe.
            Bills.Remove(bill);
            SaveChanges();
            return 1; // Se logra eliminar.
        }


        /*
         *      BRANCH
         */

        // GET BRANCHES BY NAME
        public Object GetBranches()
        {
            var b = from r in Branches
                    select new
                    {
                        cinema_name = r.cinema_name,
                        district = r.district,
                        province = r.province,
                        rooms_quantity = Rooms.Count(t => t.branch_name == r.cinema_name)
                    };
            return b;
        }

        // GET BRANCH BY NAME
        public Branch GetBranch(string cinema_name) => Branches.FirstOrDefault(x => x.cinema_name == cinema_name);


        // GET BRANCH BY NAME SELECT
        public Object GetBranch_select(string cinema_name)
        {
            var b = from r in Branches
                    where r.cinema_name == cinema_name
                    select new
                    {
                        cinema_name = r.cinema_name,
                        district = r.district,
                        province = r.province,
                        rooms_quantity = Rooms.Count(t => t.branch_name == cinema_name)
                    };
            return b;
        }
        // GET ALL ROOMS OF A BRANCH
        // Funcion que retorna todas las salas de la sucursal que coincide con el cinema_name ingresado.
        public Object Get_all_rooms_of_a_branch(string cinema_name)
        {
            // Obtener todas las salas de la sucursal que coincide con el cinema_name ingresado.
            var q = (from r in Rooms.Where(r => r.branch_name == cinema_name)
                     select new
                     {
                         branch_name = r.branch_name,
                         column_quantity = r.column_quantity,
                         row_quantity = r.row_quantity,
                         capacity = r.column_quantity * r.row_quantity,
                         free_spaces = (from seat in Seats.Where(s => s.room_id == r.id)                                       //join s in Seats on room.branch_name equals s.room_id
                                        where seat.status == "EMPTY"
                                        select seat).Count()
                     });
            return q;
        }

        // POST BRANCH
        public int Post_branch(Branch branch)
        {
            // Verificar la existencia.
            Branch existing = GetBranch(branch.cinema_name);
            if (existing != null)
                return 0; // Ya existe.

            Branches.Add(branch);
            SaveChanges();
            return 1; // Se logra agregar.
        }

        // PUT BRANCH
        public int Put_branch(Branch branch)
        {
            // Verificar la existencia.
            Branch existing = GetBranch(branch.cinema_name);
            if (existing == null)
                return -1; // No existe.

            Branches.Update(branch);
            SaveChanges();
            return 1; // Se logra agregar.
        }

        public int Delete_branch(string cinema_name)
        {
            if (!Exist_branch(cinema_name)) { return -1; } // no existe
            else if (Branch_has_relation_with_employee(cinema_name)) { return 2; } // relacion con empleados
            else if (Branch_has_relation_with_room(cinema_name)) { return 3; } // relacion con rooms.
            else
            {
                Branches.Remove(GetBranch(cinema_name));
                SaveChanges();
                return 1;
            }
        }

        // AUXILIARES
        public bool Exist_branch(string cinema_name) => (GetBranch(cinema_name) != null);
        public bool Branch_has_relation_with_room(string cinema_name)
        {
            var query = from x in Rooms
                        where x.branch_name == cinema_name
                        select x;
            Room[] y = query.ToArray();
            bool b = y.Length > 0;
            return b;
        }

        public bool Branch_has_relation_with_employee(string cinema_name)
        {
            var query = from x in Employees
                        where x.branch_id == cinema_name
                        select x;
            Employee[] y = query.ToArray();
            bool b = y.Length > 0;
            return b;
        }


        /*
         *      CLASSIFICATION
         */

        // GET CLASSIFICATION BY CODE
        public Classification GetClassification(string code) => Classifications.SingleOrDefault(x => x.code == code);

        // GET CLASSIFICATION BY CODE
        public Classification GetClassification(int age) => Classifications.SingleOrDefault(x => x.age_rating == age);

        // POST A CLASSIFICATION
        public int Post_classification(Classification classif)
        {
            // Verificar la existencia.
            Classification existing = GetClassification(classif.code);
            if (existing != null)
                return 0; // Ya existe.

            Classifications.Add(classif);
            SaveChanges();
            return 1; // Se logra agregar.
        }

        // PUT
        public int Put_classification(Classification classif)
        {
            // Verificar la existencia.
            Classification existing = GetClassification(classif.code);
            if (existing == null)
                return -1; // No existe.

            // Verificar la existencia de otra clasificacion con el mismo rango de edad.
            Classification existing_age = GetClassification(classif.age_rating);
            if (existing_age != null)
                return 0; // Ya existe esa clasificacion de edad.
            existing.details = classif.details;
            existing.age_rating = classif.age_rating;

            Classifications.Update(existing);
            SaveChanges();
            return 1; // Se logra agregar.
        }

        // DELETE especial de clasificacion tomando en cuenta si hay alguna referencia.
        public int Delete_classification(string code)
        {
            var classif = Classifications.FirstOrDefault(x => x.code == code);
            if (classif == null)
                return -1; // No existe.
            // verificar si hay peliculas asociadas a esta clasificacion.
            var movie = Movies.FirstOrDefault(m => m.classification_id == code);
            if (movie == null)
            {
                // ELIMINAR CLASSIF
                Classifications.Remove(classif);
                SaveChanges();
                return 1; // Se elimina correctamente.
            }
            return 0; // Tiene relacion con alguna pelicula.
        }


        /*
         *      CLIENT
         */

        // GET CLIENTS WITH DERIVATED AGE
        public object GetClients_select()
        {
            var e = from r in Clients
                    select new
                    {
                        cedula = r.cedula,
                        first_name = r.first_name,
                        middle_name = r.middle_name,
                        first_surname = r.first_surname,
                        second_surname = r.second_surname,
                        birth_date = r.FormattedBirth_date,
                        age = DateTime.Now.Year - r.birth_date.Year - 1,
                        username = r.username,
                        password = r.password,
                    };
            return e;
        }

        // GET CLIENT BY CEDULA.
        public Client GetClient(int cedula) => Clients.SingleOrDefault(x => x.cedula == cedula);

        // GET CLIENT WITH DERIVATED AGE
        public object GetClient_select(int cedula)
        {
            var e = from r in Clients.Where(f => f.cedula == cedula)
                    select new
                    {
                        cedula = r.cedula,
                        first_name = r.first_name,
                        middle_name = r.middle_name,
                        first_surname = r.first_surname,
                        second_surname = r.second_surname,
                        birth_date = r.FormattedBirth_date,
                        age = DateTime.Now.Year - r.birth_date.Year - 1,
                        username = r.username,
                        password = r.password,
                    };
            return e;
        }

        // IS_USERNAME_FREE?
        public bool Is_client_username_free(string username) => (Employees.Where(f => f.username == username).FirstOrDefault() == null);


        // POST A CLIENT
        public int Post_client(Client client)
        {
            // Verificar la existencia.
            Client existing = GetClient(client.cedula);
            if (existing != null)
                return 0; // Ya existe.

            if (!Is_client_username_free(client.username))
                return 2; // Ya existe este username

            Clients.Add(client);
            SaveChanges();
            return 1; // Se logra agregar.
        }

        // PUT A CLIENT
        public int Put_client(Client client)
        {
            // Verificar la existencia.
            Client existing = GetClient(client.cedula);
            if (existing == null)
                return -1; // No existe.

            existing.first_name = client.first_name;
            existing.middle_name = client.middle_name;
            existing.first_surname = client.first_surname;
            existing.second_surname = client.second_surname;
            existing.birth_date = client.birth_date;
            existing.phone_number = client.phone_number;
            existing.password = client.password;

            Clients.Update(existing);
            SaveChanges();
            return 1; // Se logra agregar.
        }

        // DELETE especial de cliente tomando en cuenta si hay alguna referencia.
        public int Delete_client(int cedula)
        {
            var client = GetClient(cedula);
            if (client == null)
                return -1; // No existe.

            // verificar si hay una factura asociada a este cliente.
            var bill = GetBill(client.cedula);
            if (bill != null)
                return 0; // Tiene relacion con facturas.

            // ELIMINAR CLIENTE
            Clients.Remove(client);
            SaveChanges();
            return 1;
        }




        /*
         *      DIRECTOR
         */

        // GET ACTOR BY NAME
        public Director GetDirector(string name) => Directors.FirstOrDefault(x => x.name == name);

        // GET ACTOR BY ID
        public Director GetDirector(int id) => Directors.FirstOrDefault(x => x.id == id);

        // POST A DIRECTOR
        public int Post_director(Director director)
        {
            // Verificar la existencia.
            Director existing = GetDirector(director.name);
            if (existing != null)
                return 0; // Ya existe

            Directors.Add(director);
            SaveChanges();
            return 1; // Se logra agregar.
        }

        // PUT
        public int Put_director(Director director, string current_name)
        {
            // Verificar la existencia.
            Director existing = GetDirector(current_name);
            if (existing == null)
                return -1; // No existe
            existing.name = director.name;
            Directors.Update(existing);
            SaveChanges();
            return 1; // Se logra agregar.
        }

        // DELETE especial de director tomando en cuenta si hay alguna referencia.
        public int Delete_director(string name)
        {
            var director = GetDirector(name);
            if (director == null)
                return -1; // No existe

            // verificar si hay peliculas asociadas a este director.
            var movie = Movies.FirstOrDefault(m => m.director_id == director.id);
            if (movie != null)
                return 0; // Existe una refeencia a pelicula.

            // ELIMINAR DIRECTOR
            Directors.Remove(director);
            SaveChanges();
            return 1;
        }




        /*
         *      EMPLOYEE
         */
        // GET EMPLOYEES WITH DERIVATED AGE
        public object GetEmployees_select()
        {
            var e = from r in Employees
                    select new
                    {
                        cedula = r.cedula,
                        first_name = r.first_name,
                        middle_name = r.middle_name,
                        first_surname = r.first_surname,
                        second_surname = r.second_surname,
                        birth_date = r.FormattedBirth_date,
                        age = DateTime.Now.Year - r.birth_date.Year - 1,
                        username = r.username,
                        password = r.password,
                        branch_id = r.branch_id
                    };
            return e;
        }

        // GET EMPLOYEE BY CEDULA
        public Employee GetEmployee(int cedula) => Employees.Where(f => f.cedula == cedula).FirstOrDefault();

        // GET EMPLOYEE WITH DERIVATED AGE
        public object GetEmployee_select(int cedula)
        {
            var e = from r in Employees.Where(f => f.cedula == cedula)
                    select new
                    {
                        cedula = r.cedula,
                        first_name = r.first_name,
                        middle_name = r.middle_name,
                        first_surname = r.first_surname,
                        second_surname = r.second_surname,
                        birth_date = r.FormattedBirth_date,
                        age = DateTime.Now.Year - r.birth_date.Year - 1,
                        username = r.username,
                        password = r.password,
                        branch_id = r.branch_id
                    };
            return e;
        }

        // IS_USERNAME_FREE?
        public bool Is_employee_username_free(string username) => (Employees.Where(f => f.username == username).FirstOrDefault() == null);

        // POST A EMPLOYEE
        public int Post_employee(Employee employee)
        {
            // Verificar la existencia.
            var existing = GetClient(employee.cedula);
            if (existing != null)
                return 0; // Ya existe.

            if (!Is_employee_username_free(employee.username))
                return 2; // Ya existe este username

            Employees.Add(employee);
            SaveChanges();
            return 1; // Se logra agregar.
        }

        // PUT A EMPLOYEE
        public int Put_employee(Employee employee, int cedula)
        {
            // Verificar la existencia.
            var existing = GetEmployee(cedula);
            if (existing == null)
                return -1; // No existe.

            existing.branch_id = employee.branch_id;
            existing.first_name = employee.first_name;
            existing.middle_name = employee.middle_name;
            existing.first_surname = employee.first_surname;
            existing.second_surname = employee.second_surname;
            existing.birth_date = employee.birth_date;
            existing.phone_number = employee.phone_number;
            existing.password = employee.password;

            Employees.Update(existing);
            SaveChanges();
            return 1; // Se logra agregar.
        }

        // DELETE
        public int Delete_employee(int cedula)
        {
            var emp = GetEmployee(cedula);
            if (emp == null)
                return -1;  // No existe.
            Employees.Remove(emp);
            SaveChanges();
            return 1; // Se logra eliminar.
        }




        /*
         *      MOVIE
         */

        // GET MOVIE BY NAME
        public Movie GetMovie(string name) => Movies.SingleOrDefault(x => x.original_name == name || x.name == name);

        // GET MOVIE MODIFIED
        public object GetMovie_select(string name)
        {
            Movie mo = Movies.SingleOrDefault(x => x.original_name == name || x.name == name);
            if (mo == null) return Enumerable.Empty<string>();

            var query = (from m in Movies.Where(x => x.original_name == name || x.name == name)
                         join d in Directors on m.director_id equals d.id
                         join c in Classifications on m.classification_id equals c.code
                         select new
                         {
                             original_name = m.original_name,
                             name = m.name,
                             length = m.length,
                             image = m.image,
                             code = c.code,
                             age_rating = c.age_rating,
                             details = c.details,
                             director = d.name,
                             actors = (from a in Acts.Where(x => x.movie_id == m.id)
                                       join p in Actors on a.actor_id equals p.id
                                       select p.name).ToList()
                         }).ToList();
            return query;
        }

        // GET MOVIE NAME BY ID
        public string GetMovieName(int id) => Movies.SingleOrDefault(x => x.id == id).original_name;


        // GET MOVIE BY ID
        public Movie GetMovie(int id) => Movies.SingleOrDefault(x => x.id == id);



        // POST A MOVIE
        public string Post_movie(MovieCreation movie_stats)
        {
            // VERIFICAR CLASIFICACION.
            if (!Check_movie_classification(movie_stats.classification_id)) return "No existe esta clasificacion";

            // OBTENER ID DIRECTOR.
            int director_id = Get_movie_director_id(movie_stats.director);

            // OBTENER ID ACTORES.
            int[] actors_ids = Get_movie_actors_id(movie_stats.actors);

            // CREAR PELICULA.
            Movie movie = new Movie
            {
                original_name = movie_stats.original_name,
                name = movie_stats.name,
                classification_id = movie_stats.classification_id,
                length = movie_stats.length,
                image = movie_stats.image,
                director_id = director_id
            };
            Movies.Add(movie);
            SaveChanges();
            // GetMOVIE ID
            int movie_id = GetMovie(movie_stats.original_name).id;

            // AGREGAR RELACION CON ACTORES en ACTS.
            foreach (int id in actors_ids)
            {
                Post_acts(movie_id, id);
            }
            return "";
        }

        private bool Check_movie_classification(string code) => (GetClassification(code) != null);

        private int Get_movie_director_id(string director_name)
        {
            var dir = GetDirector(director_name);
            // Si existe el director entonces perfecto.
            if (dir != null) return dir.id;
            // Si no existe crear un director con ese nombre.
            Director d = new Director { name = director_name };
            Directors.Add(d);
            SaveChanges();
            return d.id;
        }

        private int[] Get_movie_actors_id(string[] actors_names)
        {
            int[] ids = new int[actors_names.Length];
            for (int i = 0; i < ids.Length; i++)
            {
                var actor = GetActor(actors_names[i]);

                // Si existe el actor entonces perfecto.
                if (actor != null)
                {
                    ids[i] = actor.id;
                }
                // Si no existe crear un director con ese nombre.
                else
                {
                    Actor a = new Actor { name = actors_names[i] };
                    Actors.Add(a);
                    SaveChanges();

                    ids[i] = GetActor(a.name).id;
                }
            }
            return ids;
        }

        // PUT
        public string Put_movie(int id, MovieCreation movie_stats)
        {
            // VERIFICAR CLASIFICACION.
            if (!Check_movie_classification(movie_stats.classification_id)) return "No existe esta clasificacion";

            // OBTENER ID DIRECTOR.
            int director_id = Get_movie_director_id(movie_stats.director);

            // OBTENER ID ACTORES.
            int[] actors_ids = Get_movie_actors_id(movie_stats.actors);

            // GET MOVIE
            Movie movie = GetMovie(id);


            // ELIMINAR RELACION ANTERIOR CON ACTORES.
            List<int> current_actors_ids = GetActors_ids(id);
            foreach (int actor_id in current_actors_ids)
            {
                Delete_acts_background(id, actor_id);
            }
            SaveChanges();

            movie.classification_id = movie_stats.classification_id;
            movie.original_name = movie_stats.name;
            movie.director_id = director_id;
            movie.name = movie_stats.name;
            movie.image = movie_stats.image;
            movie.length = movie_stats.length;
            Movies.Update(movie);
            SaveChanges();

            // AGREGAR RELACION CON ACTORES en ACTS.
            foreach (int actor_id in actors_ids)
            {
                Post_acts(id, actor_id);
            }
            return "";
        }


        // DELETE especial de projection tomando en cuenta si hay alguna referencia.
        public string Delete_movie(string name)
        {
            var movie = GetMovie(name);
            if (movie == null)
                return "No existe esta pelicula.";

            // No se puede eliminar si existen proyecciones.        
            // verificar si hay una proyeccion asociada a este cliente.
            var pr = GetProjections_byMovieId(movie.id);
            if (pr.Count() == 0)
            {
                // ELIMINAR RELACION ANTERIOR CON ACTORES.
                List<int> current_actors_ids = GetActors_ids(movie.id);
                foreach (int actor_id in current_actors_ids)
                {
                    Delete_acts_background(movie.id, actor_id);
                }
                SaveChanges();

                // ELIMINAR PELICULA
                Movies.Remove(movie);
                SaveChanges();
                return ""; // Se logra agregar.
            }
            return "No se puede eliminar esta pelicula, tiene proyecciones asociadas.";
        }




        /*
         *      PROJECTION
         */

        // GET PROJECTION BY ID
        public Projection GetProjection(int id) => Projections.Where(f => f.id == id).FirstOrDefault();


        // GET ALL PROJECTIONS BY ID SPECIAL OUTPUT
        public Object GetProjections_in_branch_select(string cinema_name, DateTime date)
        {

            var sinRep = (from b in Branches.Where(b => b.cinema_name == cinema_name)
                          join r in Rooms on b.cinema_name equals r.branch_name
                          join pro in Projections on r.id equals pro.room_id
                          where pro.date == date

                          select new  {
                                movie = (from m in Movies
                                            where m.id == pro.movie_id
                                            select m.original_name).FirstOrDefault(),
                                date = pro.date
                            }).Distinct().ToList();

            var query = (from b in Branches.Where(b => b.cinema_name == cinema_name)
                         join r in Rooms on b.cinema_name equals r.branch_name
                         join p in Projections on r.id equals p.room_id
                         where p.date == date

                         select new ProjectionJSON
                         {
                             //id = p.id,
                             movie = (from m in Movies
                                      where m.id == p.movie_id
                                      select m.original_name).FirstOrDefault(),
                             //room = p.room_id,
                             date = p.FormattedDate,
                             schedule = (from t in Projections.Where(f => f.date == p.date)
                                         select t.schedule).ToList()
                         }).ToList();

            List<ProjectionJSON> lista = new List<ProjectionJSON>();
            for(int i = 0; i < sinRep.Count(); i++) {
                var x= sinRep.ElementAt(i);
                lista.Add(query.Where(f => f.movie == x.movie && f.date == string.Format("{0:MM/dd/yy}", x.date)).FirstOrDefault());
            }
            return lista;
        }


        // GET PROJECTION BY ID SPECIAL OUTPUT
        public Object GetProjection_select(int movie_id)
        {
            var query = from p in Projections.Where(f => f.movie_id == movie_id)
                        select new
                        {
                            id = p.id,
                            //movie = GetMovieName(movie_id),
                            room = p.room_id,
                            date = p.FormattedDate,
                            schedule = (from t in Projections.Where(f => f.date == p.date)
                                        select t.schedule).ToList()
                        };
            return query;
        }



        // GET PROJECTION BY ROOM_ID, MOVIE_ID, DATE
        public Projection GetProjection_byRoom_Movie_Date(int room_id, DateTime date, string schedule)
            => Projections.Where(f => f.room_id == room_id && f.date == date && f.schedule == schedule).FirstOrDefault();

        // GET PROJECTION BY ROOM_ID
        public IEnumerable<Projection> GetProjections_byRoomId(int room_id) => Projections.Where(f => f.room_id == room_id);

        // GET PROJECTION BY MOVIE_ID
        public IEnumerable<Projection> GetProjections_byMovieId(int movie_id) => Projections.Where(f => f.movie_id == movie_id);

        // GET PROJECTIONS DATES BY BRANCH_NAME
        public IList<string> GetProjections_dates_byBranch(string cinema_name)
        {
            List<string> query = (from b in Branches.Where(b => b.cinema_name == cinema_name)
                        join r in Rooms
                            on b.cinema_name equals r.branch_name
                        join p in Projections
                            on r.id equals p.room_id
                        select p.FormattedDate).ToList();

            List<string> ouput = new List<string>();
            // Remove duplicates
            foreach(string s in query)
            {
                if (ouput.Contains(s))
                    continue;
                else
                    ouput.Add(s);
            }
            return ouput;
        }

        // POST A PROJECTION
        public string Post_projection(Projection p)
        {
            // Verificar la existencia de otra proyeccion igual.
            Projection myList = GetProjection_byRoom_Movie_Date(p.room_id, p.date, p.schedule);

            if (myList != null)
                return "Esa sala ya se encuentra asignada a otra proyeccion durante el horario ingresado.";

            
            Projections.Add(p);
            SaveChanges();
            return ""; // Se logra agregar.
        }

        // PUT PROJECTION
        public string Put_projection(Projection p)
        {
            // Verificar la existencia.
            Projection existing = GetProjection(p.id);
            if (existing == null)
                return "No se ha encontrado ninguna proyeccion que coincida con el ID ingresado.";

            // Verificar la existencia de otra proyeccion igual.
            Projection myList = GetProjection_byRoom_Movie_Date(p.room_id, p.date, p.schedule);

            if (myList != null)
                return "Esa sala ya se encuentra asignada a otra proyeccion durante el horario ingresado.";
            existing.movie_id = p.movie_id;
            existing.room_id = p.room_id;
            existing.schedule = p.schedule;
            Projections.Update(existing);
            SaveChanges();
            return ""; // Se logra actualizar.
        }


        // DELETE especial de projection
        // Tomando en cuenta si hay alguna referencia.
        public string Delete_projection(int id)
        {
            var projection = GetProjection(id);
            if (projection == null)
                return "No existe ninguna proyeccion que coincida con el ID ingresado."; // No exite.
            
            // ELIMINAR PROJECTION
            Projections.Remove(projection);
            SaveChanges();
            return ""; // Se elimina correctamente.
        }



        /*
         *      ROOM
         */

        // GET ROOM BY ID
        public Room GetRoom(int id) => Rooms.Where(f => f.id == id).FirstOrDefault();

        // GET especifico
        /// Retorna todas las sillas de una sala que coincida con el id ingresado y a su vez que sea parte de la
        /// sucursal que coincide con el cinema_name ingresado.
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

        // POST 
        public void Post_room(Room room)
        {
            Rooms.Add(room);
            SaveChanges();
            Add_seats_in_a_room(room.id, room.column_quantity*room.row_quantity);
            
        }


        // Llena una sala con la cantidad de sillas que debe tener. Todas estan vacias.
        public void Add_seats_in_a_room(int id, int capacity)
        {
            for (int i = 1; i < capacity + 1; i++)
            {
                Seat s = new Seat
                {
                    room_id = id,
                    number = i,
                    status = "EMPTY"
                };
                Seats.Add(s);

            }
            SaveChanges();
        }

        // PUT A ROOM
        public int Put_room(int id, Room r)
        {
            var room = GetRoom(id);
            if (room == null)
                return -1; // No existe.

            if (room.column_quantity != r.column_quantity || room.row_quantity != r.row_quantity)
            {
                room.column_quantity = r.column_quantity;
                room.row_quantity = r.row_quantity;
                Rooms.Update(room);
                SaveChanges();

                // ELIMINAR SILLAS Y RECREARLAS.
                Delete_seats_of_a_room(id);
                int size = room.row_quantity * room.column_quantity;
                Add_seats_in_a_room(id, size);
            }
            return 1; // Se edita y se crean las sillas si es necesario.
        }

        // PUT especifico
        // Metodo que retorna las sillas modificadas de un cuarto con el estado dependiendo de la probabilidad recibida.
        public void Update_room_seats_status_restriction(int room_id, int prob)
        {
            //Room r = GetRoom(room_id);
            // implementar
        }

        // DELETE especifico de room.
        // elimina las sillas de una sala y luego elimina la sala misma.
        public int Delete_room_and_seats(int id)
        {
            if (!Exist_room(id)) { return -1; } // no existe
            else if (Room_has_relation_with_proyection(id)) { return 2; } // relacion con projection.
            else
            {
                Delete_seats_of_a_room(id);
                Rooms.Remove(GetRoom(id));
                SaveChanges();
                return 1;
            }
        }

        public bool Exist_room(int id) => (GetRoom(id) != null);

        public bool Room_has_relation_with_proyection(int id)
        {
            var query = from x in Projections
                        where x.room_id == id
                        select x;
            Projection[] y = query.ToArray();
            bool b = y.Length > 0;
            return b;
        }


        /*
         *      SEAT
         */

        // GET SEAT BY ROOM_ID, NUMBER
        public Seat GetSeat(int room_id, int number)
        {
            return Seats.Where(f => f.number == number && f.room_id == room_id)
                        .FirstOrDefault();
        }

        public bool Exist_seat(int id, int num) => GetSeat(id, num) != null;

        // DELETE para eliminar una silla.
        public int Delete_seat(int id, int num)
        {
            if (!Exist_seat(id, num))
                return -1; // No existe.

            Seats.Remove(GetSeat(id, num));
            SaveChanges();
            return 1;
        }


        // DELETE para eliminar las sillas de una sala.
        public void Delete_seats_of_a_room(int id)
        {
            Seats.RemoveRange(Seats.Where(x => x.room_id == id));
            SaveChanges();
        }
    }

    internal class PInform
    {
    }
}
