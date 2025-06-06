create table if not exists "AppUsers" (
    "Id" serial primary key,
    "EntraId" uuid not null,
    "UserName" varchar(255) not null,
    "FirstName" varchar(255) not null,
    "LastName" varchar(255) not null,
    "Email" varchar(255) not null,
    "CreatedAt" timestamp not null,
    "UpdatedAt" timestamp,
    "IsDeleted" boolean not null Default false,
    "FriendRequestSend" integer[],
    "FriendRequestReceived" integer[],
    "ConfirmedFriends" integer[] 
);

INSERT INTO "AppUsers" (
    "EntraId", "UserName", "FirstName", "LastName", "Email",
    "CreatedAt", "UpdatedAt", "IsDeleted",
    "FriendRequestSend", "FriendRequestReceived", "ConfirmedFriends"
) VALUES
-- User 1
('550e8400-e29b-41d4-a716-446655440000', 'jdoe', 'John', 'Doe', 'jdoe@example.com',
 '2025-06-01 10:00:00', '2025-06-05 14:30:00', false,
 NULL, NULL, NULL),

-- User 2
('550e8400-e29b-41d4-a716-446655440001', 'asmith', 'Alice', 'Smith', 'asmith@example.com',
 '2025-06-02 09:15:00', NULL, false,
 NULL, NULL, NULL),

-- User 3
('550e8400-e29b-41d4-a716-446655440002', 'bchan', 'Bob', 'Chan', 'bchan@example.com',
 '2025-06-02 11:45:00', '2025-06-04 16:00:00', false,
 NULL, NULL, NULL),

-- User 4
('550e8400-e29b-41d4-a716-446655440003', 'cmiller', 'Charlie', 'Miller', 'cmiller@example.com',
 '2025-06-03 13:20:00', NULL, false,
 NULL, NULL, NULL),
-- User 5
('550e8400-e29b-41d4-a716-446655440004', 'dlee', 'Diana', 'Lee', 'dlee@example.com',
 '2025-06-04 08:50:00', '2025-06-06 09:00:00', false,
 NULL, NULL, NULL);


