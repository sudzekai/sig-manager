create table roles (
    id int primary key auto_increment,
    name varchar(25) not null,

    constraint unique_idx_roles_name (name)
);

create table rights (
    id int primary key auto_increment,
    code varchar(50) not null,

    constraint unique_idx_rights_code (code)
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
    constraint unique_idx_users_email (email),
    constraint unique_idx_users_phone_number (phone_number),

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

create table positions (
    id int primary key auto_increment,
    `name` varchar(25) not null,
    price_per_hour decimal(5,2) not null,

    constraint unique_idx_positions_name unique (`name`)
)