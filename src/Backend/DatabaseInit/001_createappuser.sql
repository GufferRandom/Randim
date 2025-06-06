create table if not exists "AppUser" (
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
)