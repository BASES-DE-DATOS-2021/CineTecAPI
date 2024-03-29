DROP SCHEMA public CASCADE;
CREATE SCHEMA public;


/*
 *	 *	 *	 *	 TABLAS	  *	  *	  *	  *
 */
-- Employees
CREATE TABLE "Employees"
(
	branch_id 			VARCHAR(30) NOT NULL,
    cedula 				INT NOT NULL,
	first_name	 		VARCHAR(30) NOT NULL,
	middle_name	 		VARCHAR(30),
	first_surname	 	VARCHAR(30) NOT NULL,
	second_surname	 	VARCHAR(30),
	birth_date		 	DATE NOT NULL,
    phone_number 		VARCHAR(30),
	start_date			DATE NOT NULL,
	username			VARCHAR(30) NOT NULL,
	password			VARCHAR(30) NOT NULL,
	UNIQUE(username),
	PRIMARY KEY(cedula)
);

-- Branches
CREATE TABLE "Branches"
(
    cinema_name VARCHAR(30) NOT NULL,
    province VARCHAR(30) NOT NULL,
    district VARCHAR(30) NOT NULL,
    PRIMARY KEY(cinema_name)
);

-- Rooms
CREATE TABLE "Rooms"
(
	branch_name 		VARCHAR(30) NOT NULL,
    id 					INT NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 999 CACHE 1 ),
	row_quantity 		INT NOT NULL,
	column_quantity 	INT NOT NULL,
	PRIMARY KEY(id)
);

-- Seats
CREATE TABLE "Seats"
(
	projection_id 		INT NOT NULL,
    number 				INT NOT NULL,
	status		 		VARCHAR(30) NOT NULL,
	PRIMARY KEY(projection_id, number)
);

-- Directors
CREATE TABLE "Directors"
(
	id				INT NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 999 CACHE 1 ),
	name 			VARCHAR(30) NOT NULL,
	UNIQUE(name),
    PRIMARY KEY(id)
);

-- Classifications
CREATE TABLE "Classifications"
(
	code 			VARCHAR(30) NOT NULL,
	details 		VARCHAR(30) NOT NULL,
	age_rating		INT NOT NULL,
	UNIQUE(age_rating),
    PRIMARY KEY(code)
);

-- Actors
CREATE TABLE "Actors"
(
	id				INT NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 999 CACHE 1 ),
	name 			VARCHAR(30) NOT NULL,
	UNIQUE(name),
    PRIMARY KEY(id)
);

-- Acts
CREATE TABLE "Acts"
(
	movie_id		INT NOT NULL,
	actor_id		INT NOT NULL,
	PRIMARY KEY(movie_id, actor_id)
);

-- Movies
CREATE TABLE "Movies"
( 
	classification_id 		VARCHAR(30),
	director_id 			INT NOT NULL,
	id 						INT NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 999 CACHE 1 ),
    original_name 			VARCHAR(30) NOT NULL,
    name 					VARCHAR(30) NOT NULL,
	length 					VARCHAR(30) NOT NULL,
    PRIMARY KEY(id)
);

-- Projections
CREATE TABLE "Projections"
(
	room_id 			INT NOT NULL,
    movie_id	 		INT NOT NULL,
	id					INT NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 999 CACHE 1 ),
	date				DATE NOT NULL,
	schedule			CHAR(5) NOT NULL,
	covid				INT NOT NULL,
	PRIMARY KEY(id)
);

-- Clients
CREATE TABLE "Clients"
(
	cedula 				INT NOT NULL,
	first_name 			VARCHAR(30) NOT NULL,
	middle_name 		VARCHAR(30),
	first_surname 		VARCHAR(30) NOT NULL,
	second_surname 		VARCHAR(30),
	birth_date 			DATE NOT NULL,
	phone_number 		VARCHAR(30),
	username 			VARCHAR(30) NOT NULL,
	password 			VARCHAR(30) NOT NULL,
	UNIQUE(username),
	PRIMARY KEY (cedula)
);

-- Bills
--CREATE TABLE "Bills"
--(
	--client_id 			INT NOT NULL,
    --projection_id 		INT NOT NULL,
	--id					INT NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 999 CACHE 1 ),
	--detail				VARCHAR(255),
	--PRIMARY KEY(id)
--);


/*
 *	 *	 *	 *	 FOREIGN KEYS	*	*	*	*
 */
-- Employees
ALTER TABLE "Employees"
ADD CONSTRAINT EMPLOYEE_BRANCH_FK FOREIGN KEY(branch_id)
REFERENCES "Branches"(cinema_name);

-- Rooms
ALTER TABLE "Rooms"
ADD CONSTRAINT ROOM_BRANCH_FK FOREIGN KEY(branch_name)
REFERENCES "Branches"(cinema_name);

-- Seats
ALTER TABLE "Seats"
ADD CONSTRAINT SEAT_PROJECTION_FK FOREIGN KEY(projection_id)
REFERENCES "Projections"(id);

-- Movies
ALTER TABLE "Movies"
ADD CONSTRAINT MOVIE_CLASSIFICATION_FK FOREIGN KEY(director_id)
REFERENCES "Directors"(id);

ALTER TABLE "Movies"
ADD CONSTRAINT MOVIE_DIRECTOR_FK FOREIGN KEY(classification_id)
REFERENCES "Classifications"(code);

-- Acts
ALTER TABLE "Acts"
ADD CONSTRAINT ACTS_MOVIE_FK FOREIGN KEY(movie_id)
REFERENCES "Movies"(id);

ALTER TABLE "Acts"
ADD CONSTRAINT ACTS_ACTOR_FK FOREIGN KEY(actor_id)
REFERENCES "Actors"(id);

-- Projections
ALTER TABLE "Projections"
ADD CONSTRAINT PROJECTION_ROOM_FK FOREIGN KEY(room_id)
REFERENCES "Rooms"(id);

ALTER TABLE "Projections"
ADD CONSTRAINT PROJECTION_MOVIE_FK FOREIGN KEY(movie_id)
REFERENCES "Movies"(id);

-- Bills
--ALTER TABLE "Bills"
--ADD CONSTRAINT BILL_CLIENT_FK FOREIGN KEY(client_id)
--REFERENCES "Clients"(cedula);

-- ALTER TABLE "Bills"
-- ADD CONSTRAINT BILL_PROJECTION_FK FOREIGN KEY(projection_id)
-- REFERENCES "Projections"(id);
