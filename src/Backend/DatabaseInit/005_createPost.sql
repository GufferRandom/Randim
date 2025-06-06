CREATE TABLE IF NOT EXISTS posts (
    id serial PRIMARY KEY,
    user_id integer NOT NULL REFERENCES appusers(id),
    content text NOT NULL,
    created_at timestamp NOT NULL,
    updated_at timestamp,
    is_deleted boolean NOT NULL DEFAULT false,
    reactions_id integer[],
    comments_id integer[]
);
