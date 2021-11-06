create database TP03;

use TP03;

create table bl(
	num int unique not null auto_increment, primary key(num),
    consignee int not null,
    navio varchar(50) not null);
    
create table container(
	num int unique not null auto_increment, primary key(num),
    tipo varchar(50) not null,
    tamanho float not null,
    numBL int not null, foreign key (numBL) references bl(num));