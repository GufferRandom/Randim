create table if not exists appusers (
    id serial primary key,
    entra_id uuid not null,
    user_name varchar(255) not null,
    first_name varchar(255) not null,
    last_name varchar(255) not null,
    email varchar(255) not null,
    created_at timestamp not null,
    updated_at timestamp,
    is_deleted boolean not null default false,
    friend_request_send integer[],
    friend_request_received integer[],
    confirmed_friends integer[]
);

insert into appusers (
    entra_id, user_name, first_name, last_name, email,
    created_at, updated_at, is_deleted,
    friend_request_send, friend_request_received, confirmed_friends
) values
-- User 1
('550e8400-e29b-41d4-a716-446655440000', 'jdoe', 'John', 'Doe', 'jdoe@example.com',
 '2025-06-01 10:00:00', '2025-06-05 14:30:00', false,
 null, null, null),

-- User 2
('550e8400-e29b-41d4-a716-446655440001', 'asmith', 'Alice', 'Smith', 'asmith@example.com',
 '2025-06-02 09:15:00', null, false,
 null, null, null),

-- User 3
('550e8400-e29b-41d4-a716-446655440002', 'bchan', 'Bob', 'Chan', 'bchan@example.com',
 '2025-06-02 11:45:00', '2025-06-04 16:00:00', false,
 null, null, null),

-- User 4
('550e8400-e29b-41d4-a716-446655440003', 'cmiller', 'Charlie', 'Miller', 'cmiller@example.com',
 '2025-06-03 13:20:00', null, false,
 null, null, null),

-- User 5
('550e8400-e29b-41d4-a716-446655440004', 'dlee', 'Diana', 'Lee', 'dlee@example.com',
 '2025-06-04 08:50:00', '2025-06-06 09:00:00', false,
 null, null, null);
