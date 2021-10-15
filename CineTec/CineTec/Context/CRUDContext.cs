using CineTec.JSON_Models;
using CineTec.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CineTec.Context
{
    public class CRUDContext : DbContext
    {

        public CRUDContext(DbContextOptions<CRUDContext> options) : base(options)
        {

        }

        // Entities
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Seat> Seats { get; set; }
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

            // SEAT COMPOSITE KEY
            modelBuilder.Entity<Seat>()
                .HasKey(s => new { s.projection_id, s.number });

            // ACTS COMPOSITE KEY
            modelBuilder.Entity<Acts>()
                .HasKey(a => new { a.movie_id, a.actor_id });

            // PROJECTIONJSON COMPOSITE KEY
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

        // Metodo toma un usuario y contraseña y retorna la información del empleado
        // de la cuenta a la que corresponde, null si no coincide con ninguna cuenta.
        public Employee Login_admin(string username, string password)
        {
            Employee admin = Employees.Where(x => x.username == username && x.password == password).FirstOrDefault();
            if (admin == null) return null;
            return admin;
        }

        // Metodo toma un usuario y contraseña y retorna la información del cliente
        // de la cuenta a la que corresponde, null si no coincide con ninguna cuenta.
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
        // Retornar el actor que coincida con el nombre que se recibe de parámetro, en caso
        // de no encontrar ningun actor, retorna null.
        public Actor GetActor(string name) => Actors.SingleOrDefault(x => x.name == name);

        // GET ACTOR BY ID
        // Retornar el actor que coincida con el id que se recibe de parámetro, en caso
        // de no encontrar ningun actor, retorna null.
        public Actor GetActor(int id) => Actors.SingleOrDefault(x => x.id == id);

        // GET ACTORS IDS BY MOVIE_ID
        // Retornar el id de los actores que protagonizan en la pelicula del id recibido por parametro,
        // en caso de no encontrar ningun actor, retorna null.
        public List<int> GetActors_ids(int movie_id)
        {
            var query = from a in Acts.Where(x => x.movie_id == movie_id)
                        join p in Actors
                            on a.actor_id equals p.id
                        select p.id;
            var actors = query.ToList();
            return actors;
        }

        // PUT
        // Actualiza un el actor recibido como parametro en la tabla de actores
        // en la base de datos, si no se logra envia texto explicando la razon.

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

        // DELETE
        // Eliminacion especial de actor tomando en cuenta si hay alguna referencia.
        // Si no se logra eliminar envia la razon por la cual no se logra.
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
        // Retornar el acts que coincida con los ids que se reciben por parámetro, en caso
        // de no encontrar ninguna coincidencia, retorna null.
        public Acts GetActs(int movie_id, int actor_id) => Acts.Where(f => f.movie_id == movie_id && f.actor_id == actor_id).FirstOrDefault();

        // GET ACTS BY MOVIE_ID
        // Retornar una lista de atcs en los que el id de pelicula coincida con el parametro,
        // en caso de no encontrar ninguna coincidencia, retorna null.
        public IEnumerable<Acts> GetActs_byMovieId(int movie_id) => Acts.Where(f => f.movie_id == movie_id);

        // GET ACTS BY ACTOR_ID
        // Retornar una lista de atcs en los que el id de actor coincida con el parametro,
        // en caso de no encontrar ninguna coincidencia, retorna null.
        public IEnumerable<Acts> GetActs_byActorsId(int actor_id) => Acts.Where(f => f.actor_id == actor_id);


        // POST AN ACTS
        // Inserta el acts recibido como parametro a la tabla en la base de datos.

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

        // DELETE
        // Eliminacion especial de acts tomando en cuenta si hay alguna referencia.
        // Si no se logra eliminar envia la razon por la cual no se logra.
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
         *      BRANCH
         */

        // GET BRANCHES BY NAME
        // Metodo que retorna las 
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
                         id = r.id,
                         column_quantity = r.column_quantity,
                         row_quantity = r.row_quantity,
                         capacity = r.column_quantity * r.row_quantity,
                     }).Distinct().ToList();
            return q;
        }



        // Projection x Movie x Branch x dia.
        // Listado de todas las projection que hay para un dia en especifico para una sucursal en especifico.
        // Tiene toda la informacion de las peliculas que salen ese dia.
        public Object GetBranches_Movie_Projection_no_date_select(string cinema_name)
        {

            var query = (from b in Branches.Where(b => b.cinema_name == cinema_name)
                         join r in Rooms on b.cinema_name equals r.branch_name
                         join p in Projections on r.id equals p.room_id
                         join m in Movies on p.movie_id equals m.id
                         join d in Directors on m.director_id equals d.id
                         join c in Classifications on m.classification_id equals c.code

                         select new
                         {
                             id = p.id,
                             name = m.original_name,
                             classification = c.code,
                             length = m.length,
                             date = p.date,
                             director = d.name,
                             actors = (from a in Acts.Where(x => x.movie_id == m.id)
                                       join p in Actors on a.actor_id equals p.id
                                       select p.name).ToList(),
                             price = 3600,
                             schedule = p.schedule,
                             room = p.room_id,
                             free_spaces = (from seat in Seats.Where(s => s.projection_id == p.id)
                                            where seat.status == "EMPTY"
                                            select seat).Count()
                         }).ToList();
            return query;
        }

        // Projection x Movie x Branch x dia.
        // Listado de todas las projection que hay para un dia en especifico para una sucursal en especifico.
        // Tiene toda la informacion de las peliculas que salen ese dia.
        public Object GetBranches_Movie_Projection_select(string cinema_name, string date)
        {
            DateTime convertedDate = DateTime.Parse(date);
            DateTime t = convertedDate.Date;

            var query = (from b in Branches.Where(b => b.cinema_name == cinema_name)
                         join r in Rooms on b.cinema_name equals r.branch_name
                         join p in Projections on r.id equals p.room_id
                         where p.date == t
                         join m in Movies on p.movie_id equals m.id
                         join d in Directors on m.director_id equals d.id
                         join c in Classifications on m.classification_id equals c.code

                         select new
                         {
                             id = p.id,
                             name = m.original_name,
                             classification = c.code,
                             length = m.length,
                             director = d.name,
                             actors = (from a in Acts.Where(x => x.movie_id == m.id)
                                       join p in Actors on a.actor_id equals p.id
                                       select p.name).ToList(),
                             price = 3600,
                             schedule = p.schedule,
                             room = p.room_id,
                             free_spaces = (from seat in Seats.Where(s => s.projection_id == p.id)
                                           where seat.status == "EMPTY"
                                           select seat).Count()
                         }).ToList();
            return query;
        }

        // POST BRANCH
        // Agrega una sucursal a la tabla Branches de la base de datos.
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
        // Modifica una sucursal a la tabla Branches de la base de datos,
        // en caso de no lograrlo, devuelve la razon.
        public int Put_branch(Branch branch)
        {
            // Verificar la existencia.
            Branch existing = GetBranch(branch.cinema_name);
            if (existing == null)
                return -1; // No existe.

            existing.district = branch.district;
            existing.province = branch.province;

            Branches.Update(existing);
            SaveChanges();
            return 1; // Se logra agregar.
        }


        // Eliminacion especial de branch tomando en cuenta si hay alguna referencia.
        // Si no se logra eliminar envia la razon por la cual no se logra.
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
        // Verifica la existencia de una sucursal.
        public bool Exist_branch(string cinema_name) => (GetBranch(cinema_name) != null);

        // Verifica la existencia de una relacon entre sucursal y sala.
        public bool Branch_has_relation_with_room(string cinema_name)
        {
            var query = from x in Rooms
                        where x.branch_name == cinema_name
                        select x;
            Room[] y = query.ToArray();
            bool b = y.Length > 0;
            return b;
        }

        // Verifica la existencia de una relacion entre sucursal y empleado.

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

        // GET CLASSIFICATION BY Age
        public Classification GetClassification(int age) => Classifications.SingleOrDefault(x => x.age_rating == age);

        // POST A CLASSIFICATION
        // Inserta la clasificacion recibida como parametro a la tabla de clasificaciones en la base de datos.

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
        // Actualiza la clasificacion recibida como parametro en la tabla de Classification
        // en la base de datos, si no se logra envia texto explicando la razon.
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

        // DELETE
        // Eliminacion especial de clasificacion tomando en cuenta si hay alguna referencia.
        // Si no se logra eliminar envia la razon por la cual no se logra.
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
                        phone_number = r.phone_number,
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
                        phone_number = r.phone_number,
                        age = DateTime.Now.Year - r.birth_date.Year - 1,
                        username = r.username,
                        password = r.password,
                    };
            return e;
        }

        // IS_USERNAME_FREE?
        public bool Is_client_username_free(string username) => (Employees.Where(f => f.username == username).FirstOrDefault() == null);


        // POST A CLIENT
        // Inserta el cliente recibido como parametro a la tabla de clientes en la base de datos.

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
        // Actualiza un cliente en la tabla Clients de la base de datos.
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

        // DELETE
        // Eliminacion especial de client tomando en cuenta si hay alguna referencia.
        // Si no se logra eliminar envia la razon por la cual no se logra.
        public int Delete_client(int cedula)
        {
            var client = GetClient(cedula);
            if (client == null)
                return -1; // No existe.

            // ELIMINAR CLIENTE
            Clients.Remove(client);
            SaveChanges();
            return 1;
        }




        /*
         *      DIRECTOR
         */

        // GET DIRECTOR BY NAME
        public Director GetDirector(string name) => Directors.FirstOrDefault(x => x.name == name);

        // GET DIRECTOR BY ID
        public Director GetDirector(int id) => Directors.FirstOrDefault(x => x.id == id);

        // POST A DIRECTOR
        // Inserta el director recibido como parametro a la tabla de directores en la base de datos.

        // PUT
        // Actualiza un director en la tabla Directors de la base de datos.
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

        // DELETE
        // Eliminacion especial de director tomando en cuenta si hay alguna referencia.
        // Si no se logra eliminar envia la razon por la cual no se logra.
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
                        start_date = r.FormattedStart_date,
                        phone_number = r.phone_number,
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
                        start_date = r.FormattedStart_date,
                        phone_number = r.phone_number,
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
        // Inserta el employee recibido como parametro a la tabla de employees en la base de datos.

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
        // Actualiza un emplpeado en la tabla Employees de la base de datos.

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
            existing.start_date = employee.start_date;
            existing.phone_number = employee.phone_number;
            existing.password = employee.password;

            Employees.Update(existing);
            SaveChanges();
            return 1; // Se logra agregar.
        }

        // DELETE
        // Eliminacion especial de employee tomando en cuenta si hay alguna referencia.
        // Si no se logra eliminar envia la razon por la cual no se logra.
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

        // GET MOVIE BY ID
        public Movie GetMovie_by_id(int id) => Movies.SingleOrDefault(x => x.id == id);

        // GET MOVIE BY NAME
        public Movie GetMovie_by_name(string name) => Movies.SingleOrDefault(x => x.original_name == name || x.name == name);

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
                             id = m.id,
                             original_name = m.original_name,
                             name = m.name,
                             length = m.length,
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


        // POST A MOVIE
        // Crea e inserta todas las entidades necesarias para la creacion de una pelicula utilizando
        // la infoamcion recibida como parametro en cada tabla respectiva de en la base de datos.

        public string Post_movie(MovieCreation movie_stats)
        {
            // VERIFICAR CLASIFICACION.
            if (!Check_movie_classification(movie_stats.classification_id)) return "No existe esta clasificacion";

            // OBTENER ID DIRECTOR.
            int director_id = Get_movie_director_id(movie_stats.director);

            // OBTENER ID ACTORES.
            int[] actors_ids = Get_movie_actors_id(movie_stats.actors);

            if (GetMovie_by_name(movie_stats.name) != null || GetMovie_by_name(movie_stats.original_name) != null)
            {
                return "No se puede crear la pelicula debido a que ya existe una pelicula con este nombre";
            }

            // CREAR PELICULA.
            Movie movie = new Movie
            {
                original_name = movie_stats.original_name,
                name = movie_stats.name,
                classification_id = movie_stats.classification_id,
                length = movie_stats.length,
                director_id = director_id
            };
            Movies.Add(movie);
            SaveChanges();
            // GetMOVIE ID
            int movie_id = GetMovie_by_name(movie_stats.original_name).id;

            // AGREGAR RELACION CON ACTORES en ACTS.
            foreach (int id in actors_ids)
            {
                Post_acts(movie_id, id);
            }
            return "";
        }

        // Verifica la existencia de una clasificacion con el codigo correspondiente al parametro.
        private bool Check_movie_classification(string code) => (GetClassification(code) != null);

        // Retorna el id del director que corresponde al del nombre recibido por parametro.
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


        public IEnumerable<object> GetMovie_select_All()
        {
            var query = (from m in Movies
                         join d in Directors on m.director_id equals d.id
                         join c in Classifications on m.classification_id equals c.code
                         select new
                         {
                             id = m.id,
                             original_name = m.original_name,
                             name = m.name,
                             length = m.length,
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

        // PUT
        // Actualiza un la pelicula recibida como parametro en la tabla de peliculas
        // en la base de datos, si no se logra envia texto explicando la razon.
        public string Put_movie(int id, MovieCreation movie_stats)
        {
            // VERIFICAR CLASIFICACION.
            if (!Check_movie_classification(movie_stats.classification_id)) return "No existe esta clasificacion";

            // OBTENER ID DIRECTOR.
            int director_id = Get_movie_director_id(movie_stats.director);

            // OBTENER ID ACTORES.
            int[] actors_ids = Get_movie_actors_id(movie_stats.actors);

            // GET MOVIE
            Movie movie = GetMovie_by_id(id);

            if (movie == null)
            {
                return "No existe la pelicula con este id";
            }

            if (GetMovie_by_name(movie_stats.name) != null || GetMovie_by_name(movie_stats.original_name) != null)
            {
                var temp = GetMovie_by_name(movie_stats.name);
                var temp2 = GetMovie_by_name(movie_stats.original_name);
                if ((temp != null && temp.id == movie.id) || (temp2 != null && temp2.id == movie.id))
                {
                    return update_Movie(movie, movie_stats, director_id, actors_ids);
                }
                return "No se puede modificar la pelicula debido a que ya existe una pelicula con este nombre";
            }

            return update_Movie(movie, movie_stats, director_id, actors_ids);

        }

        public string update_Movie(Movie movie, MovieCreation movie_stats, int director_id, int[] actors_ids)
        {

            // ELIMINAR RELACION ANTERIOR CON ACTORES.
            List<int> current_actors_ids = GetActors_ids(movie.id);
            foreach (int actor_id in current_actors_ids)
            {
                Delete_acts_background(movie.id, actor_id);
            }
            SaveChanges();

            movie.classification_id = movie_stats.classification_id;
            movie.original_name = movie_stats.original_name;
            movie.director_id = director_id;
            movie.name = movie_stats.name;
            movie.length = movie_stats.length;
            Movies.Update(movie);
            SaveChanges();

            // AGREGAR RELACION CON ACTORES en ACTS.
            foreach (int actor_id in actors_ids)
            {
                Post_acts(movie.id, actor_id);
            }
            return "";

        }

        public string Put_movie_by_name(string name, MovieCreation movie_stats)
        {
            // VERIFICAR CLASIFICACION.
            if (!Check_movie_classification(movie_stats.classification_id)) return "No existe esta clasificacion";

            // OBTENER ID DIRECTOR.
            int director_id = Get_movie_director_id(movie_stats.director);

            // OBTENER ID ACTORES.
            int[] actors_ids = Get_movie_actors_id(movie_stats.actors);

            // GET MOVIE

            Movie movie = GetMovie_by_name(name);

            if (movie == null)
            {
                return "No existe la pelicula con este nombre";
            }

            if (GetMovie_by_name(movie_stats.name) != null || GetMovie_by_name(movie_stats.original_name) != null)
            {
                var temp = GetMovie_by_name(movie_stats.name);
                var temp2 = GetMovie_by_name(movie_stats.original_name);
                if ((temp != null && temp.id == movie.id) || (temp2 != null && temp2.id == movie.id))
                {
                    return update_Movie(movie, movie_stats, director_id, actors_ids);
                }
                return "No se puede modificar la pelicula debido a que ya existe una pelicula con este nombre";
            }

            return update_Movie(movie, movie_stats, director_id, actors_ids);

        }

        // DELETE
        // Eliminacion especial de movie con el id tomando en cuenta si hay alguna referencia.
        // Si no se logra eliminar envia la razon por la cual no se logra.
        public string Delete_movie_by_id(int id)
        {
            var movie = GetMovie_by_id(id);
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


        // DELETE
        // Eliminacion especial de movie con el nombre tomando en cuenta si hay alguna referencia.
        // Si no se logra eliminar envia la razon por la cual no se logra.
        public string Delete_movie(string name)
        {
            var movie = GetMovie_by_name(name);
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

        // GET PROJECTION BY ROOM_ID, MOVIE_ID, DATE
        public Object GetProjections()
        {

             var query = (from p in Projections
                        select new
                        {
                            id = p.id,
                            movie = Movies.SingleOrDefault(x => x.id == p.movie_id).original_name,
                            date = p.FormattedDate,
                            schedule = p.schedule,
                            covid = p.covid,
                            room = p.room_id,
                            free_spaces = (from seat in Seats.Where(s => s.projection_id == p.id)
                                           where seat.status == "EMPTY"
                                           select seat).Count()
                        }).ToList();
            return query;
        }

        public Projection GetProjection_byRoom_Movie_Date(int movie_id, int room_id, DateTime date, string schedule)
            => Projections.Where(f => f.movie_id == movie_id && f.room_id == room_id && f.date == date && f.schedule == schedule).FirstOrDefault();

        // GET PROJECTION BY ROOM_ID
        public IEnumerable<Projection> GetProjections_byRoomId(int room_id) => Projections.Where(f => f.room_id == room_id);

        // GET PROJECTION BY MOVIE_ID
        public IEnumerable<Projection> GetProjections_byMovieId(int movie_id) => Projections.Where(f => f.movie_id == movie_id);

        // GET PROJECTIONS DATES BY BRANCH_NAME
        public List<string> GetProjections_dates_byBranch(string cinema_name)
        {
            List<string> query = (from b in Branches.Where(b => b.cinema_name == cinema_name)
                        join r in Rooms
                            on b.cinema_name equals r.branch_name
                        join p in Projections
                            on r.id equals p.room_id
                        select p.FormattedDate).ToList();

            query.Sort();

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
        // Inserta la proyeccion recibida como parametro a la tabla de Projections en la base de datos.

        public string Post_projection(Projection p)
        {
            // Verificar la existencia de otra proyeccion igual.
            Projection myList = GetProjection_byRoom_Movie_Date(p.movie_id, p.room_id, p.date, p.schedule);

            if (myList != null)
                return "Esa sala ya se encuentra asignada a otra proyeccion durante el horario ingresado.";

            Projections.Add(p);
            SaveChanges();

            Projection x = GetProjection_byRoom_Movie_Date(p.movie_id, p.room_id, p.date, p.schedule);
            int capacity = (from r in Rooms.Where(b => b.id == x.id)
                            join proj in Projections on r.id equals proj.room_id
                            select r.column_quantity*r.row_quantity).FirstOrDefault();

            Room room = GetRoom(x.room_id);


            Add_seats_for_proyection(x.id, room.row_quantity, room.column_quantity, x.covid);




            return ""; // Se logra agregar.
        }


        private int[] convert_matriz_to_array(int[,] mat, int row, int column)
        {

            int[] array = new int[row*column];
            int contador = 0;

            for (int i = 0; i < row; i++)
            {
                for (int e = 0; e < column; e++)
                {

                    array[contador] = mat[i, e];
                    contador++;

                }
            }

            return array;
        }

        private int[,] covid25(int[,] matriz, int row, int column) {

            int i = 0;
            int e = 0;
            int contador = 0;

            for(int f = 0; f < row; f++)
            {
                for (int c = 0; c < column; c++)
                {

                    if (i % 2 == 0 && (contador + 1) < column && (e + f) < row)
                    {
                        matriz[f + e,contador] = 1;
                        matriz[f + e,contador + 1] = 1;
                    }
                    contador += 2;
                    i += 1;


                }
                contador = 0;
                if (e + f < row && matriz[f + e,0] == 1 && i % 2 == 0){
                    i += 1;
                }
                else if(e + f < row && matriz[f + e,0] == 0 && i % 2 != 0)
                {
                    i += 1;
                }
                e += 1;
            }

            return matriz;
        }

        private int[,] covid50(int[,] matriz, int row, int column)
        {

            int i = 0;
            int contador = 0;

            for (int f = 0; f < row; f++)
            {
                for (int c = 0; c < column; c++)
                {
                    if (i % 2 == 0 && (contador + 1) < column && (f) < row)
                    {
                        matriz[f , contador] = 1;
                        matriz[f , contador + 1] = 1;
                    }
                    contador += 2;
                    i += 1;

                }
                contador = 0;
                if (matriz[f, 0] == 1 && i % 2 == 0)
                {
                    i += 1;
                }
                else if (matriz[f, 0] == 0 && i % 2 != 0)
                {
                    i += 1;
                }
            }

            return matriz;
        }

        private int[,] mat_ones(int[,] matriz, int row, int column)
        {

            for(int i=0; i < row; i++)
            {
                for(int e=0; e < column; e++)
                {
                    matriz[i, e] = 1;
                }
            }

            return matriz;

        }

        private int[,] covid75(int[,] matriz, int row, int column)
        {
            matriz = mat_ones(matriz,row,column);

            for (int f = 0; f < row; f++)
            {
                for (int c = 0; c < column; c++)
                {
                    if ((c + 1)% 3 == 0)
                    {
                        matriz[f,c] = 0;
                    }
                }
            }

            return matriz;
        }

        private int[] getArrayCovid(int row, int column, int covid)
        {

            int[,] mat = new int[row,column];

            foreach (int item in mat)
            {
                var a = item;
            }

            switch (covid)
            {

                case 0:
                    return convert_matriz_to_array(mat, row, column);

                case 25:
                    return convert_matriz_to_array(covid25(mat, row, column), row, column);

                case 50:
                    return convert_matriz_to_array(covid50(mat, row, column), row, column);

                case 75:
                    return convert_matriz_to_array(covid75(mat, row, column), row, column);

                case 100:
                    return convert_matriz_to_array(mat_ones(mat,row,column), row, column);

            }


            return null;
        }


        // Crear una cantidad de sillas para una projection. Todas estan vacias.
        public void Add_seats_for_proyection(int id, int row, int column, int covid)
        {
            var array = getArrayCovid(row, column, covid);

            int capacity = row * column;

            for (int i = 1; i < array.Length + 1; i++)
            {
                string stat;
                if (array[i-1] == 0)
                {
                    stat = "COVID";
                }
                else
                {
                    stat = "EMPTY";
                }

                Seat s = new Seat
                {
                    projection_id = id,
                    number = i,
                    status = stat
                };
                Seats.Add(s);
            }
            SaveChanges();
        }

        public bool is_any_seat_sold(Projection p)
        {
            List<Seat> seats = Get_all_seats_assgined_to_projection(p.id);

            foreach(Seat s in seats)
            {
                if (s.status == "TAKEN"){
                    return true;
                }
            }
            return false;


        }

        // PUT PROJECTION
        // Actualiza una proyeccion recibida como parametro en la tabla de projections
        // en la base de datos, si no se logra envia texto explicando la razon.
        // nota: No se puede modificar la sala de una projeccion. 
        public string Put_projection(Projection p)
        {
            // Verificar la existencia.
            Projection existing = GetProjection(p.id);
            if (existing == null)
                return "No se ha encontrado ninguna proyeccion que coincida con el ID ingresado.";

            // Verifica si se ha vendido una silla.
            if (is_any_seat_sold(p))
            {
                return "No se puede actualizar la proyeccion porque ya se han vendido sillas.";
            }

            // Verificar la existencia de otra proyeccion igual.
            Projection myList = GetProjection_byRoom_Movie_Date(p.movie_id, p.room_id, p.date, p.schedule);

            if (myList != null)
                return "Esa sala ya se encuentra asignada a otra proyeccion durante el horario ingresado.";
            existing.movie_id = p.movie_id;
            existing.date = p.date;
            existing.schedule = p.schedule;
 
            Projections.Update(existing);
            SaveChanges();
            return ""; // Se logra actualizar.
        }

        // DELETE
        // Eliminacion especial de projection tomando en cuenta si hay alguna referencia.
        // Si no se logra eliminar envia la razon por la cual no se logra.
        public string Delete_projection(int id)
        {
            var projection = GetProjection(id);
            if (projection == null)
                return "No existe ninguna proyeccion que coincida con el ID ingresado."; // No exite.

            // ELIMINAR LAS SILLAS ASOCIADAS A LA PROJECTION
            Delete_seats_of_a_projection(id);

            // ELIMINAR PROJECTION
            Projections.Remove(projection);
            SaveChanges();
            return ""; // Se elimina correctamente.
        }

        // GET especifico
        /// Retorna todas las sillas asignadas a una projeccion en especifico.
        public List<Seat> Get_all_seats_assgined_to_projection(int id)


        {
            var query = (from p in Projections.Where(b => b.id == id)
                         join seat in Seats
                            on p.id equals seat.projection_id
                         orderby seat.number ascending
                        select seat).ToList();



            return query;
        }



        /*
         *      ROOM
         */

        // GET ROOM BY ID
        public Room GetRoom(int id) => Rooms.Where(f => f.id == id).FirstOrDefault();

        // GET ROOM BY ID SPECIAL
        public Object GetRoom_special(int id)
        {
            // Obtener todas las salas de la sucursal que coincide con el cinema_name ingresado.
            var q = (from r in Rooms.Where(f => f.id == id)
                     select new
                     {
                         branch_name = r.branch_name,
                         id = r.id,
                         column_quantity = r.column_quantity,
                         row_quantity = r.row_quantity,
                         capacity = r.column_quantity * r.row_quantity,
                     }).FirstOrDefault();
            return q;
        }

        // GET ROOMS BY ID SPECIAL
        // Retorn
        public Object GetRooms_special()
        {
            // Obtener todas las salas de la sucursal que coincide con el cinema_name ingresado.
            var q = (from r in Rooms
                     select new
                     {
                         branch_name = r.branch_name,
                         id = r.id,
                         column_quantity = r.column_quantity,
                         row_quantity = r.row_quantity,
                         capacity = r.column_quantity * r.row_quantity,
                     }).ToList();
            return q;
        }

        // POST
        // Agrega una sala a la tabla Rooms de la base de datos.
        public void Post_room(Room room)
        {
            Rooms.Add(room);
            SaveChanges();            
        }
 
        // DELETE
        // Eliminacion especial de room tomando en cuenta si hay alguna referencia.
        // Si no se logra eliminar envia la razon por la cual no se logra.
        public string Delete_room(int id)
        {
            var room = GetRoom(id);
            if (room == null)
                return "No existe esta sala.";

            // verificar referencias.
            var proj = GetProjections_byRoomId(id);
            if (proj.Count() != 0)
                return "No se puede eliminar esta sala, tiene relacion con alguna(s) de las proyecciones.";

            // ELIMINAR ROOM
            Rooms.Remove(room);
            SaveChanges();
            return "";
        }


        /*
         *      SEAT
         */

        // GET SEAT BY PROJECTION_ID, NUMBER
        // Retorna la silla que coincida con el numero y el id de projection.
        public Seat GetSeat(int projection_id, int number)
        {
            return Seats.Where(f => f.number == number && f.projection_id == projection_id)
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
        public void Delete_seats_of_a_projection(int id)
        {
            Seats.RemoveRange(Seats.Where(x => x.projection_id == id));
            SaveChanges();
        }


    }

}
