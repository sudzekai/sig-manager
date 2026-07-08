create table products (
    id int primary key auto_increment,

    `name` varchar(50) not null,
    price decimal(6,2) not null,

    constraint unique_idx_products_name unique (`name`)
);

create table cars (
    id int primary key,
    
    `name` varchar(50) not null,
    `status` ENUM("working", "broken") not null default "working",
    plate varchar(150) not null default "-",

    constraint unique_idx_cars_name unique (`name`)
);

create table parks (
	id int primary key auto_increment,
    `name` varchar(25) not null,
    
    constraint unique_idx_parks_name unique (`name`)
);