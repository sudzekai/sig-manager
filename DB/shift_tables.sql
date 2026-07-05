-- wrappers

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

-- types

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
