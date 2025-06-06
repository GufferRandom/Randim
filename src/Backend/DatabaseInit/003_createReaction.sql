CREATE TYPE react_type AS ENUM ('Like', 'Love', 'Care', 'Haha', 'Wow', 'Sad', 'Angry');

CREATE TABLE IF NOT EXISTS reactions (
    id serial PRIMARY KEY,
    user_id integer NOT NULL REFERENCES appusers(id),
    react_type react_type NOT NULL,
    created_at timestamp NOT NULL,
    updated_at timestamp,
    is_deleted boolean NOT NULL DEFAULT false
);
