create table roles (
    id int primary key auto_increment,
    name varchar(25) not null,

    constraint unique_idx_roles_name unique (name)
);

create table rights (
    id int primary key auto_increment,
    code varchar(50) not null,

    constraint unique_idx_rights_code unique (code)
);

create table positions (
    id int primary key auto_increment,
    `name` varchar(25) not null,
    price_per_hour decimal(5,2) not null,

    constraint unique_idx_positions_name unique (`name`)
);