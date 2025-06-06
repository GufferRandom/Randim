CREATE TABLE IF NOT EXISTS comments (
    id serial PRIMARY KEY,
    content text NOT NULL,
    user_id integer NOT NULL REFERENCES appusers(id),
    created_at timestamp NOT NULL,
    updated_at timestamp,
    is_deleted boolean NOT NULL DEFAULT false,
    reactions_id integer[]
);
