create database TP02;

use TP02;

create table usuario(
  id int not null unique auto_increment, primary key(id),
  nome varchar(40) not null,
  senha char(32) not null);
  
create table categoria(
  id int not null unique auto_increment, primary key(id),
  nome varchar(40) not null,
  descricao varchar(40));

create table produto(
 id int not null unique auto_increment, primary key(id),
 nome varchar(40) not null,
 preco float not null,
 descricao varchar(40),
 qtd int not null,
 idCateg int not null,
 foreign key (idCateg) references categoria(id));

