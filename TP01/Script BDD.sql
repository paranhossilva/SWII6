create database TP02;

use TP02;

create table autor(
  id int unique not null, primary key(id),
  nome varchar(50) not null,
  email varchar(30) not null,
  sexo char(1) not null
);

create table livro(
  id int unique not null, primary key(id),
  nome varchar(50) not null,
  preco float not null,
  qtd int
  );

create table autorlivro(
  id int unique not null, primary key(id),
  idAutor int not null,
  idLivro int not null,
  foreign key (idAutor) references autor(id),
  foreign key (idLivro) references livro(id));
  
insert into autor values (1, "Robert Jordan", "RobertJordan@email.com", 'M'),
						 (2, "Brandon Sanderson", "BrandonSanderson@email.com", 'M');

insert into livro values (1, "Roda do Tempo Vol. 12", 80, 1),
						 (2, "Roda do Tempo Vol. 13", 85, 2),
						 (3, "Roda do Tempo Vol. 14", 90, 3);
                         
insert into autorlivro values (1, 1, 1),
							  (2, 1, 2),
                              (3, 1, 3),
                              (4, 2, 1),
                              (5, 2, 2),
                              (6, 2, 3);
                              
select * from autorlivro 
inner join autor on autor.id = idAutor 
inner join livro on livro.id = idLivro
where idlivro = 3;
