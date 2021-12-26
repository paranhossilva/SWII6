create database TPFinal;

use TPFinal;

create table usuario(
	id int unique not null auto_increment, primary key(id),
    nome varchar(40) not null,
    senha char(32) not null,
    sts bool not null
);

create table produto(
	id int unique not null auto_increment, primary key(id),
    nome varchar(40) not null,
    preco float not null,
    sts bool not null,
    idCad int not null, foreign key (idCad) references usuario(id),
    idUp int, foreign key (idUp) references usuario(id)
);