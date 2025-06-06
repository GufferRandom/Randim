create table if not exists "Comments"(
    "Id" serial primary key,
    "Content" text not null,
    "UserId" integer not null references "AppUser"("Id"),
    "CreatedAt" timestamp not null,
    "UpdatedAt" timestamp,
    "IsDeleted" boolean not null Default false,
    "ReactionsId" integer[]
)