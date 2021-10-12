------------------------------------------------------------------------------------------------------------------------------------
									
									-- SCRIPT DE POPULACION --
					
------------------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------------------

-- SUCURSALES
INSERT INTO public."Branches"( cinema_name, province, district) VALUES ('Del Este', 'San Jose', 'Curridabat');
INSERT INTO public."Branches"( cinema_name, province, district) VALUES ('Escazu', 'San Jose', 'Escazu');
INSERT INTO public."Branches"( cinema_name, province, district) VALUES ('Lincoln', 'San Jose', 'Moravia');
INSERT INTO public."Branches"( cinema_name, province, district) VALUES ('Terramall', 'Cartago', 'Tres Rios');

------------------------------------------------------------------------------------------------------------------------------------

-- ROOMS
--  1 al 5 en Del Este
--  6 al 9 en Lincoln
--  10 al 12 en Terramall
--  13 al 17 en Escazu
INSERT INTO public."Rooms"( branch_name, row_quantity, column_quantity) VALUES ('Del Este', 10, 26);
INSERT INTO public."Rooms"( branch_name, row_quantity, column_quantity) VALUES ('Del Este', 8, 24);
INSERT INTO public."Rooms"( branch_name, row_quantity, column_quantity) VALUES ('Del Este', 8, 22);
INSERT INTO public."Rooms"( branch_name, row_quantity, column_quantity) VALUES ('Del Este', 9, 20);
INSERT INTO public."Rooms"( branch_name, row_quantity, column_quantity) VALUES ('Del Este', 7, 20);

INSERT INTO public."Rooms"( branch_name, row_quantity, column_quantity) VALUES ('Lincoln', 7, 20);
INSERT INTO public."Rooms"( branch_name, row_quantity, column_quantity) VALUES ('Lincoln', 9, 20);
INSERT INTO public."Rooms"( branch_name, row_quantity, column_quantity) VALUES ('Lincoln', 9, 22);
INSERT INTO public."Rooms"( branch_name, row_quantity, column_quantity) VALUES ('Lincoln', 9, 20);

INSERT INTO public."Rooms"( branch_name, row_quantity, column_quantity) VALUES ('Terramall', 9, 22);
INSERT INTO public."Rooms"( branch_name, row_quantity, column_quantity) VALUES ('Terramall', 8, 24);
INSERT INTO public."Rooms"( branch_name, row_quantity, column_quantity) VALUES ('Terramall', 9, 20);

INSERT INTO public."Rooms"( branch_name, row_quantity, column_quantity) VALUES ('Escazu', 8, 20);
INSERT INTO public."Rooms"( branch_name, row_quantity, column_quantity) VALUES ('Escazu', 10, 26);
INSERT INTO public."Rooms"( branch_name, row_quantity, column_quantity) VALUES ('Escazu', 8, 24);
INSERT INTO public."Rooms"( branch_name, row_quantity, column_quantity) VALUES ('Escazu', 9, 26);
INSERT INTO public."Rooms"( branch_name, row_quantity, column_quantity) VALUES ('Escazu', 9, 22);


------------------------------------------------------------------------------------------------------------------------------------

-- EMPLOYEES
INSERT INTO public."Employees"(
	branch_id, cedula, first_name, middle_name, first_surname, second_surname, birth_date, phone_number, username, password)
	VALUES ('Del Este', 117920956, 'Michael', 'Shakime', 'Richards', 'Sparks', '10/21/2000', 22489340, 'Jeykime', 2100);

INSERT INTO public."Employees"(
	branch_id, cedula, first_name, middle_name, first_surname, second_surname, birth_date, phone_number, username, password)
	VALUES ('Lincoln', 604530340, 'Carlos', 'Adrian', 'Araya', 'Ramirez', '07/05/1999', 89573290, 'Heutlett', 1234);

INSERT INTO public."Employees"(
	branch_id, cedula, first_name, middle_name, first_surname, second_surname, birth_date, phone_number, username, password)
	VALUES ('Lincoln', 34543562, 'Jean', 'Pool', 'Tubito', 'Baez', '12/31/1999', 42948924, 'Tubito', 9988);

INSERT INTO public."Employees"(
	branch_id, cedula, first_name, middle_name, first_surname, second_surname, birth_date, phone_number, username, password)
	VALUES ('Terramall', 118190603, 'Jose', 'Alejandro', 'Chavarria', 'Madriz', '08/11/2001', 70372813, 'Jachm', 123);

INSERT INTO public."Employees"(
	branch_id, cedula, first_name, middle_name, first_surname, second_surname, birth_date, phone_number, username, password)
	VALUES ('Escazu', 117790130, 'Sebastian', null, 'Mora', 'Godinez', '08/11/2001', 83200846, 'Sebas', 123);

INSERT INTO public."Employees"(
	branch_id, cedula, first_name, middle_name, first_surname, second_surname, birth_date, phone_number, username, password)
	VALUES ('Del Este', 117677100, 'Alexandra', null, 'Jara', 'Zeledón', '01/07/2000', 87463210, 'ale_jze', 0701);

INSERT INTO public."Employees"(
	branch_id, cedula, first_name, middle_name, first_surname, second_surname, birth_date, phone_number, username, password)
	VALUES ('Terramall', 113764093, 'José', 'Andrés', 'Solano', 'Mora', '07/02/2000', 60927751, 'josesol_m', 0207);
	
------------------------------------------------------------------------------------------------------------------------------------

-- CLIENTS
INSERT INTO public."Clients"(
	cedula, first_name, middle_name, first_surname, second_surname, birth_date, phone_number, username, password)
	VALUES (117920956, 'Michael', 'Shakime', 'Richards', 'Sparks', '10/21/2000', 22489340, 'Jeykime', 2100);

INSERT INTO public."Clients"(
	cedula, first_name, middle_name, first_surname, second_surname, birth_date, phone_number, username, password)
	VALUES (118190603, 'Jose', 'Alejandro', 'Chavarria', 'Madriz', '08/11/2001', 70372813, 'Jachm', 123);

INSERT INTO public."Clients"(
	cedula, first_name, middle_name, first_surname, second_surname, birth_date, phone_number, username, password)
	VALUES (604530340, 'Carlos', 'Adrian', 'Araya', 'Ramirez', '07/05/1999', 89573290, 'Heutlett', 1234);

INSERT INTO public."Clients"(
	cedula, first_name, middle_name, first_surname, second_surname, birth_date, phone_number, username, password)
	VALUES (117790130, 'Sebastian', null, 'Mora', 'Godinez', '05/28/2000', 83200846, 'Sebas', 123);

------------------------------------------------------------------------------------------------------------------------------------

-- CLASSIFICATION
INSERT INTO public."Classifications"( code, details, age_rating) VALUES ('G','General Audiences', -1);
INSERT INTO public."Classifications"( code, details, age_rating) VALUES ('PG','Parental Guidance Suggested', 15);
INSERT INTO public."Classifications"( code, details, age_rating) VALUES ('PG-13','Parents Strongly Cautioned', 13);
INSERT INTO public."Classifications"( code, details, age_rating) VALUES ('R','Restricted', 17);
INSERT INTO public."Classifications"( code, details, age_rating) VALUES ('NC-17','Adults Only', 18);

------------------------------------------------------------------------------------------------------------------------------------

-- DIRECTORS
INSERT INTO public."Directors"(name) VALUES ('Andy Serkis');
INSERT INTO public."Directors"(name) VALUES ('Robert Zemeckis');
INSERT INTO public."Directors"(name) VALUES ('Andrew Stanton');
INSERT INTO public."Directors"(name) VALUES ('Roger Allers');
INSERT INTO public."Directors"(name) VALUES ('Barry Jenkins');
INSERT INTO public."Directors"(name) VALUES ('Lana Wachowski');
INSERT INTO public."Directors"(name) VALUES ('George Miller');
INSERT INTO public."Directors"(name) VALUES ('Hayao Miyazaki');
INSERT INTO public."Directors"(name) VALUES ('Bong Joon-ho');
INSERT INTO public."Directors"(name) VALUES ('Gareth Edwards');
	
------------------------------------------------------------------------------------------------------------------------------------

-- MOVIES
INSERT INTO public."Movies"(classification_id, director_id, original_name, name, length) 
    VALUES ('PG-13', 1, 'Venom Let There Be Carnage', 'Venom Carnage Liberado', '97 min');
INSERT INTO public."Movies"(classification_id, director_id, original_name, name, length) 
	VALUES ('G', 2, 'Back to the Future', 'Regreso al futuro', '116 min');
INSERT INTO public."Movies"(classification_id, director_id, original_name, name, length) 
	VALUES ('G', 3, 'Finding Nemo', 'Encontrando a Nemo', '100 min');
INSERT INTO public."Movies"(classification_id, director_id, original_name, name, length) 
	VALUES ('G', 4, 'The Lion King', 'El Rey Leon', '88 min');
INSERT INTO public."Movies"(classification_id, director_id, original_name, name, length) 
	VALUES ('R', 5, 'Moonlight', 'Moonlight', '111 min');
INSERT INTO public."Movies"( classification_id, director_id, original_name, name, length) 
    VALUES ('PG-13', 6, 'The Matrix', 'La Matrix', '136 min');
INSERT INTO public."Movies"( classification_id, director_id, original_name, name, length) 
    VALUES ('R', 7, 'Mad max Fury Road', 'Mad max Furia en la carretera', '120 min');
INSERT INTO public."Movies"( classification_id, director_id, original_name, name, length) 
    VALUES ('G', 8, 'Spirited Away', 'El viaje de Chihiro', '125 min');
INSERT INTO public."Movies"( classification_id, director_id, original_name, name, length) 
    VALUES ('PG', 9, 'Parasite', 'Parasitos', '132min');
INSERT INTO public."Movies"( classification_id, director_id, original_name, name, length) 
    VALUES ('G', 10, 'Rogue One', 'Rogue One', '133min');

------------------------------------------------------------------------------------------------------------------------------------
-- ACTORS
INSERT INTO public."Actors"(name) VALUES ('Tom Hardy');
INSERT INTO public."Actors"(name) VALUES ('Michelle Williams');
INSERT INTO public."Actors"(name) VALUES ('Naomie Harris');
INSERT INTO public."Actors"(name) VALUES ('Reid Scott');
INSERT INTO public."Actors"(name) VALUES ('Stephen Graham');
INSERT INTO public."Actors"(name) VALUES ('Woody Harrelson');

INSERT INTO public."Actors"(name) VALUES ('Michael J. Fox');
INSERT INTO public."Actors"(name) VALUES ('Christopher Lloyd');
INSERT INTO public."Actors"(name) VALUES ('Lea Thompson');
INSERT INTO public."Actors"(name) VALUES ('Crispin Glover');

INSERT INTO public."Actors"(name) VALUES ('Albert Brooks');
INSERT INTO public."Actors"(name) VALUES ('Ellen DeGeneres');
INSERT INTO public."Actors"(name) VALUES ('Alexander Gould');
INSERT INTO public."Actors"(name) VALUES ('Willem Dafoe');

INSERT INTO public."Actors"(name) VALUES ('Matthew Broderick');
INSERT INTO public."Actors"(name) VALUES ('Jonathan Taylor Thomas');
INSERT INTO public."Actors"(name) VALUES ('James Earl Jones');
INSERT INTO public."Actors"(name) VALUES ('Jeremy Irons');

INSERT INTO public."Actors"(name) VALUES ('Trevante Rhodes');
INSERT INTO public."Actors"(name) VALUES ('André Holland');
INSERT INTO public."Actors"(name) VALUES ('Ashton Sanders');
INSERT INTO public."Actors"(name) VALUES ('Jharrel Jerome');
INSERT INTO public."Actors"(name) VALUES ('Mahershala Ali');

INSERT INTO public."Actors"(name) VALUES ('Keanu Reeves');
INSERT INTO public."Actors"(name) VALUES ('Carrie-Anne Moss');
INSERT INTO public."Actors"(name) VALUES ('Laurence Fishburne');
INSERT INTO public."Actors"(name) VALUES ('Hugo Weaving');

INSERT INTO public."Actors"(name) VALUES ('Charlize Theron');
INSERT INTO public."Actors"(name) VALUES ('Hugh Keays-Byrne');
INSERT INTO public."Actors"(name) VALUES ('Nicholas Hoult');

INSERT INTO public."Actors"(name) VALUES ('Rumi Hiiragi');
INSERT INTO public."Actors"(name) VALUES ('Miyu Irino');
INSERT INTO public."Actors"(name) VALUES ('Mari Natsuki');

INSERT INTO public."Actors"(name) VALUES ('Song Kang-Ho');
INSERT INTO public."Actors"(name) VALUES ('Park So-dam');
INSERT INTO public."Actors"(name) VALUES ('Cho Yeo-jeong');

INSERT INTO public."Actors"(name) VALUES ('Felicity Jones');
INSERT INTO public."Actors"(name) VALUES ('Diego Luna');
INSERT INTO public."Actors"(name) VALUES ('Donnie Yen');
INSERT INTO public."Actors"(name) VALUES ('Forest Whitaker');

-- ACTS IN
INSERT INTO public."Acts"(movie_id, actor_id) VALUES (1, 1);
INSERT INTO public."Acts"(movie_id, actor_id) VALUES (1, 2);
INSERT INTO public."Acts"(movie_id, actor_id) VALUES (1, 3);
INSERT INTO public."Acts"(movie_id, actor_id) VALUES (1, 4);
INSERT INTO public."Acts"(movie_id, actor_id) VALUES (1, 5);
INSERT INTO public."Acts"(movie_id, actor_id) VALUES (1, 6);

INSERT INTO public."Acts"(movie_id, actor_id) VALUES (2, 7);
INSERT INTO public."Acts"(movie_id, actor_id) VALUES (2, 8);
INSERT INTO public."Acts"(movie_id, actor_id) VALUES (2, 9);
INSERT INTO public."Acts"(movie_id, actor_id) VALUES (2, 10);

INSERT INTO public."Acts"(movie_id, actor_id) VALUES (3, 11);
INSERT INTO public."Acts"(movie_id, actor_id) VALUES (3, 12);
INSERT INTO public."Acts"(movie_id, actor_id) VALUES (3, 13);
INSERT INTO public."Acts"(movie_id, actor_id) VALUES (3, 14);

INSERT INTO public."Acts"(movie_id, actor_id) VALUES (4, 15);
INSERT INTO public."Acts"(movie_id, actor_id) VALUES (4, 16);
INSERT INTO public."Acts"(movie_id, actor_id) VALUES (4, 17);
INSERT INTO public."Acts"(movie_id, actor_id) VALUES (4, 18);

INSERT INTO public."Acts"(movie_id, actor_id) VALUES (5, 19);
INSERT INTO public."Acts"(movie_id, actor_id) VALUES (5, 20);
INSERT INTO public."Acts"(movie_id, actor_id) VALUES (5, 21);
INSERT INTO public."Acts"(movie_id, actor_id) VALUES (5, 22);
INSERT INTO public."Acts"(movie_id, actor_id) VALUES (5, 23);
INSERT INTO public."Acts"(movie_id, actor_id) VALUES (5, 3);

INSERT INTO public."Acts"(movie_id, actor_id) VALUES (6, 24);
INSERT INTO public."Acts"(movie_id, actor_id) VALUES (6, 25);
INSERT INTO public."Acts"(movie_id, actor_id) VALUES (6, 26);
INSERT INTO public."Acts"(movie_id, actor_id) VALUES (6, 27);

INSERT INTO public."Acts"(movie_id, actor_id) VALUES (7, 28);
INSERT INTO public."Acts"(movie_id, actor_id) VALUES (7, 29);
INSERT INTO public."Acts"(movie_id, actor_id) VALUES (7, 30);
INSERT INTO public."Acts"(movie_id, actor_id) VALUES (7, 1);

INSERT INTO public."Acts"(movie_id, actor_id) VALUES (8, 31);
INSERT INTO public."Acts"(movie_id, actor_id) VALUES (8, 32);
INSERT INTO public."Acts"(movie_id, actor_id) VALUES (8, 33);

INSERT INTO public."Acts"(movie_id, actor_id) VALUES (9, 34);
INSERT INTO public."Acts"(movie_id, actor_id) VALUES (9, 35);
INSERT INTO public."Acts"(movie_id, actor_id) VALUES (9, 36);

INSERT INTO public."Acts"(movie_id, actor_id) VALUES (10, 37);
INSERT INTO public."Acts"(movie_id, actor_id) VALUES (10, 38);
INSERT INTO public."Acts"(movie_id, actor_id) VALUES (10, 39);
INSERT INTO public."Acts"(movie_id, actor_id) VALUES (10, 40);


------------------------------------------------------------------------------------------------------------------------------------

---- PROJECTIONS

--  1 al 5 en Del Este
--  6 al 9 en Lincoln
--  10 al 12 en Terramall
--  13 al 17 en Escazu

-- Dia 24

-- lincoln
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (6, 1, '10/24/2021','16:10');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (7, 2, '10/24/2021','15:20');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (8, 1, '10/24/2021','17:35');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (9, 2, '10/24/2021','08:40');

INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (6, 3, '10/24/2021','16:15');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (7, 7, '10/24/2021','16:00');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (8, 3, '10/24/2021','17:00');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (9, 7, '10/24/2021','11:40');

INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (9, 6, '10/24/2021','14:15');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (7, 6, '10/24/2021','16:50');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (6, 2, '10/24/2021','17:45');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (6, 2, '10/24/2021','14:25');

-- escazu
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (13, 8, '10/24/2021','17:10');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (17, 8, '10/24/2021','11:20');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (15, 8, '10/24/2021','12:40');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (14, 9, '10/24/2021','09:55');

INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (15, 9, '10/24/2021','14:15');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (13, 10, '10/24/2021','18:15');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (17, 10, '10/24/2021','13:50');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (13, 10, '10/24/2021','15:30');

-- del este
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (1, 5, '10/24/2021','13:55');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (2, 5, '10/24/2021','14:15');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (3, 5, '10/24/2021','18:50');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (5, 6, '10/24/2021','17:35');

INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (2, 6, '10/24/2021','16:20');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (3, 6, '10/24/2021','19:00');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (1, 6, '10/24/2021','13:30');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (4, 7, '10/24/2021','15:50');

INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (1, 7, '10/24/2021','19:05');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (5, 7, '10/24/2021','18:15');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (2, 8, '10/24/2021','13:50');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (4, 8, '10/24/2021','16:55');

-- terramall
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (12, 10, '10/24/2021','12:55');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (10, 10, '10/24/2021','18:20');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (12, 3, '10/24/2021','13:35');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (11, 3, '10/24/2021','09:50');

INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (10, 4, '10/24/2021','13:10');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (10, 4, '10/24/2021','15:45');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (11, 4, '10/24/2021','17:40');


--  1 al 5 en Del Este
--  6 al 9 en Lincoln
--  10 al 12 en Terramall
--  13 al 17 en Escazu


-- Dia 25

-- lincoln
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (9, 5, '10/25/2021','16:10');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (7, 2, '10/25/2021','15:20');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (8, 9, '10/25/2021','17:35');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (9, 2, '10/25/2021','08:40');

INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (6, 3, '10/25/2021','16:15');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (6, 4, '10/25/2021','16:00');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (7, 5, '10/25/2021','17:00');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (7, 8, '10/25/2021','11:40');

INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (9, 9, '10/25/2021','14:15');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (7, 5, '10/25/2021','16:50');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (8, 8, '10/25/2021','17:45');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (9, 2, '10/25/2021','14:25');

-- escazu
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (13, 9, '10/25/2021','17:10');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (10, 9, '10/25/2021','11:20');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (15, 10, '10/25/2021','12:40');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (14, 10, '10/25/2021','09:55');

INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (15, 10, '10/25/2021','14:15');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (13, 8, '10/25/2021','18:15');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (10, 8, '10/25/2021','13:50');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (13, 9, '10/25/2021','15:30');

-- del este
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (1, 7, '10/25/2021','13:55');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (2, 7, '10/25/2021','14:15');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (3, 6, '10/25/2021','18:50');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (5, 6, '10/25/2021','17:35');

INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (2, 8, '10/25/2021','16:20');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (3, 4, '10/25/2021','19:00');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (1, 4, '10/25/2021','13:30');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (4, 1, '10/25/2021','15:50');

INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (1, 1, '10/25/2021','19:05');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (5, 1, '10/25/2021','18:15');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (2, 3, '10/25/2021','13:50');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (4, 3, '10/25/2021','16:55');

-- terramall
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (12, 6, '10/25/2021','12:55');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (10, 6, '10/25/2021','18:20');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (12, 1, '10/25/2021','13:35');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (11, 1, '10/25/2021','09:50');

INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (10, 4, '10/25/2021','13:10');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (10, 4, '10/25/2021','15:45');
INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (11, 4, '10/25/2021','17:40');


-- -- Dia 26
-- INSERT INTO public."Projections"(room_id, movie_id, date, schedule) VALUES (, , '10/26/2021','');