create table if not exists friendrequests (
    id serial primary key,
    friend_receiver_id integer not null references appusers(id),
    friend_sender_id integer not null references appusers(id),
    accepted boolean not null,
    created_at timestamp not null,
    updated_at timestamp,
    is_deleted boolean not null default false,
    unique (friend_receiver_id, friend_sender_id)
);
