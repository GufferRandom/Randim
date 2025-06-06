create table if not exists "Friends" (
    "Id" serial primary key,
    "FriendReceiverId" integer not null references "AppUser"("Id"),
    "FriendSenderId" integer not null references "AppUser"("Id"),
    "Accepted" boolean not null,
    "CreatedAt" timestamp not null,
    "UpdatedAt" timestamp,
    "IsDeleted" boolean not null Default false
)