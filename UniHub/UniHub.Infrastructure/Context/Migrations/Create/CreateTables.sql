
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
*/