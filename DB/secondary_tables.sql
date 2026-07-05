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

    primary key (popcorn_shift_id, product_id),

    foreign key (user_id)
        references users(id)
        on delete cascade,
        on update cascade,
    
    foreign key (shift_id)
        references shifts(id)
        on delete cascade
        on update cascade
    
    foreign key (position_id)
        references positions(id)
        on delete restrict
        on update cascade
);