CREATE TYPE ReactType AS ENUM ('Like', 'Love', 'Care', 'Haha', 'Wow', 'Sad', 'Angry');
create table if not exists "Reactions"(
    "Id" serial primary key,
    "UserId" integer not null references "AppUser"("Id"),
    "ReactType" ReactType NOT NULL,
    "CreatedAt" timestamp not null,
    "UpdatedAt" timestamp,
    "IsDeleted" boolean not null Default false
)