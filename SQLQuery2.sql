use UniversidaDos;
go
IF OBJECT_ID('EstudianteMateria', 'U') IS NOT NULL
    DROP TABLE EstudianteMateria;

IF OBJECT_ID('materias', 'U') IS NOT NULL
    DROP TABLE materias;

IF OBJECT_ID('estudiante', 'U') IS NOT NULL
    DROP TABLE estudiante;
use UniversidaDos;
go
create table estudiante
	(
		id int identity (1,1) primary key,
		Nombre varchar(50) not null,
		Apellido varchar(50) not null,
		correo varchar (30),
		FechaNacimiento date 
	);

go

create table materias
	(
		id int identity (1,1) primary key,
		NombreMateria varchar (20)
	);

go

create table EstudianteMateria
	(
		id int identity (1,1) primary key,
		nota1 int,
		nota2 int,
		nota3 int,
		EstuId int not null,
		MateId int not null,
		constraint UQ_Estu_Mate unique (EstuId, MateId)
	);

go

alter table EstudianteMateria
add constraint fk_estu
foreign key (EstuId)
references estudiante(id)
on delete cascade;
go

alter table EstudianteMateria
add constraint fk_mate
foreign key (MateId)
references materias(id)
on delete cascade 
go 