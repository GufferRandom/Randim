create table if not exists friendrequests (
    id serial primary key,
    user_id_1 integer not null references appusers(id),
    user_id_2 integer not null references appusers(id),
    friend_requester_id integer not null references appusers(id),
    accepted boolean not null,
    created_at timestamp not null,
    updated_at timestamp,
    is_deleted boolean not null default false,
    unique (user_id_1, user_id_2),
    CHECK ( user_id_1 < user_id_2 )
);
