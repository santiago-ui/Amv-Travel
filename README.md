# Amv-Travel
#Ejercicio tecnico Resuelto. Aclaraciones:

## Se usa base de datos por lo cual, no se genera por codigo automaticamente la valores solicitados en el punto 1.
## Tampoco se implementaron los metodos MostrarInformacion de los modelos, pues no es su responsabilidad mostrar al usuario por pantalla.
## Esta decision se tomo al no hacer una aplicacion de consola, y manejar peticiones ajax con conexion a base de datos mediante el servicio GestorReservas.

###SCRIPTS SQL

CREATE DATABASE AMVTravel

use AMVTravel
go

CREATE TABLE Tour(
	Id int primary key identity,
	Nombre varchar(100),
	Destino varchar(150),
	FechaInicio datetime,
	FechaFin datetime,
	precio decimal(18,2)
)

CREATE TABLE Reserva(
	Id int primary key identity,
	Cliente varchar(100),
	FechaReserva Datetime,
	TourId int,
	foreign key (TourId) REFERENCES Tour(id)

)

CREATE TABLE Usuario(
IdUsuario int primary key identity,
NombreUsuario varchar(50),
Correo varchar(50),
Clave varchar(100)
)


INSERT INTO Tour (Nombre,Destino,FechaInicio,FechaFin,precio) 
VALUES ('Iguazu Full','Misiones', '20240331','20240407', 150000.34),
	   ('Tango-selva-Montañas','Bariloche','20240630','20240705',112244),
	   ('Aventura','Mendoza','20240912','20240918',205000.30);


