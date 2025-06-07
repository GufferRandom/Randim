CREATE TABLE IF NOT EXISTS confirmed_friends (
    id serial PRIMARY KEY,
    user_id_1 integer NOT NULL REFERENCES appusers(id),
    user_id_2 integer NOT NULL REFERENCES appusers(id),
    confirmed boolean NOT NULL DEFAULT true,
    created_at timestamp NOT NULL,
    updated_at timestamp,
    is_deleted boolean NOT NULL DEFAULT false,
    UNIQUE (user_id_1, user_id_2),
    CHECK (user_id_1 > user_id_2)
);
