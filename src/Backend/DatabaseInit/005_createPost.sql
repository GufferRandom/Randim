create table if not exists "Posts"(
    "Id" serial primary key,
    "UserId" integer not null references "AppUser"("Id"),
    "Content" text not null,
    "CreatedAt" timestamp not null,
    "UpdatedAt" timestamp,
    "IsDeleted" boolean not null Default false,
    "ReactionsId" integer[],
    "CommentsId" integer[]
)