
/*
-- Table: public.User
CREATE TABLE "User" (
    "Id" BIGSERIAL PRIMARY KEY,
    "InternalIdentifier" VARCHAR(50) NOT NULL,
	"ExternalIdentifier" VARCHAR(50) NOT NULL,
    "Name" VARCHAR(150) NULL,
	"Email" VARCHAR(150) NOT NULL,
    "Role" VARCHAR(20) NOT NULL,
    "Status" VARCHAR(20) NOT NULL,
	"ProfileUrl" VARCHAR(2000),
    "CreationDate" TIMESTAMP NOT NULL,
    "UpdateDate" TIMESTAMP NULL,
    "DeletionDate" TIMESTAMP NULL
);

CREATE UNIQUE INDEX "UX_Email" ON "User" ("Email") WHERE "DeletionDate" IS NULL;
CREATE UNIQUE INDEX "UX_ExternalIdentifier" ON "User" ("ExternalIdentifier") WHERE "DeletionDate" IS NULL;
CREATE UNIQUE INDEX "UX_InternalIdentifier" ON "User" ("InternalIdentifier");

SELECT 'Table User as Created';


-- Table: public.Course
CREATE TABLE public."Course" (
    "Id" BIGSERIAL PRIMARY KEY,
    "UserId" BIGINT NOT NULL,
    "Name" VARCHAR(150) NOT NULL,
    "Code" VARCHAR(20) NOT NULL,
    "CreationDate" TIMESTAMP NOT NULL,
    "UpdateDate" TIMESTAMP NULL,
    "DeletionDate" TIMESTAMP NULL
);

ALTER TABLE public."Course" ADD CONSTRAINT "FK_Course_UserId" FOREIGN KEY ("UserId") REFERENCES public."User" ("Id");

CREATE UNIQUE INDEX "UX_Code" ON public."Course" ("Code") WHERE "DeletionDate" IS NULL;

SELECT 'Table Course as Created';

*/