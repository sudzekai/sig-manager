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

create table users (
    id int primary key auto_increment,
    role_id int not null,

    username varchar(25) not null,
    full_name varchar(255) not null,
    email varchar(255) not null,
    phone_number varchar(12) not null,
    password_hash text not null,

    phone_number_last_four varchar(4) not null,
    
    verification_code varchar(6) not null,

    created_at datetime not null,
    updated_at datetime not null,

    foreign key (role_id)
        references roles(id)
        on delete restrict
        on update cascade,

    index idx_users_role_id (role_id),
    constraint unique_idx_users_username unique (username),
    index idx_users_full_name (full_name),
    constraint unique_idx_users_email unique (email),
    constraint unique_idx_users_phone_number unique (phone_number),

    index idx_users_created_at (created_at)

);

create table push_subscriptions (
    id int primary key auto_increment,
    user_id int not null,
    
    `endpoint` text not null,
    p256dh_key text not null,
    auth_key text not null,

    created_at datetime not null,
    updated_at datetime not null,

    foreign key (user_id) 
        references users(id)
        on delete cascade
        on update cascade,
    
    constraint unique_idx_push_subscriptions_user_id unique (user_id),
    
    index idx_push_subscriptions_created_at (created_at)
);

create table shifts (
    id int primary key auto_increment,

    `type` ENUM("car", "train", "popcorn", "bouncer", "carousel", "admin") not null,
    `status` ENUM("opened", "closed") not null default "opened",

    created_at datetime not null,
    updated_at datetime not null,
    closed_at datetime,

    index idx_shifts_created_at (created_at),
    index idx_shifts_status_closed_at (`status`, closed_at)
);

create table info_shifts (
    shift_id int primary key,

    cash decimal(18,2),
    cashless decimal(18,2),
    receipt_photo_file_name varchar(255),

    foreign key (shift_id)
        references shifts(id)
        on delete cascade
        on update cascade
);

create table ticket_shifts (
    shift_id int primary key,

    first_ticket int not null,
    last_ticket int,

    ticket_price decimal(5,2),

    foreign key (shift_id)
        references shifts(id)
        on delete cascade
        on update cascade
);

create table car_shifts (
    shift_id int primary key,

    foreign key (shift_id)
        references shifts(id)
        on delete cascade
        on update cascade
);

create table bouncer_shifts (
    shift_id int primary key,

    foreign key (shift_id)
        references shifts(id)
        on delete cascade
        on update cascade
);

create table carousel_shifts (
    shift_id int primary key,

    foreign key (shift_id)
        references shifts(id)
        on delete cascade
        on update cascade
);


create table train_shifts (
    shift_id int primary key,

    first_ticket_alternative int not null,
    last_ticket_alternative int,
    ticket_price_alternative decimal(5,2),

    foreign key (shift_id)
        references shifts(id)
        on delete cascade
        on update cascade
);

create table popcorn_shifts (
    shift_id int primary key,

    foreign key (shift_id)
        references shifts(id)
        on delete cascade
        on update cascade
);

create table admin_shifts (
    shift_id int primary key,

    foreign key (shift_id)
        references shifts(id)
        on delete cascade
        on update cascade
);

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

create table role_rights (
    role_id int not null,
    right_id int not null,

    primary key (role_id, right_id),

    foreign key (role_id)
        references roles(id)
        on delete cascade
        on update cascade,
    
    foreign key (right_id)
        references rights(id)
        on delete cascade
        on update cascade
);

create table car_shift_cars (
    car_shift_id int not null,
    car_id int not null,

    primary key (car_shift_id, car_id),

    foreign key (car_shift_id)
        references car_shifts(shift_id)
        on delete cascade
        on update cascade,
    
    foreign key (car_id)
        references cars(id)
        on delete cascade
        on update cascade
);

create table popcorn_shift_products (
    popcorn_shift_id int not null,
    product_id int not null,
    
    quantity int not null default 0, 

    primary key (popcorn_shift_id, product_id),

    foreign key (popcorn_shift_id)
        references popcorn_shifts(shift_id)
        on delete cascade
        on update cascade,
    
    foreign key (product_id)
        references products(id)
        on delete cascade
        on update cascade
);

create table user_shifts (
    user_id int not null,
    shift_id int not null,
    position_id int not null,

    primary key (user_id, shift_id),

    foreign key (user_id)
        references users(id)
        on delete cascade
        on update cascade,
    
    foreign key (shift_id)
        references shifts(id)
        on delete cascade
        on update cascade,
    
    foreign key (position_id)
        references positions(id)
        on delete restrict
        on update cascade
);