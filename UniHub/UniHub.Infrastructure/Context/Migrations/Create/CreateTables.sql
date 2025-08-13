

/*===================== TABLE: USER =====================*/


CREATE TABLE "User" (
    "Id" UUID PRIMARY KEY,
    "InternalIdentifier" VARCHAR(12) NOT NULL,
	"ExternalIdentifier" VARCHAR(50) NOT NULL,
    "Name" VARCHAR(150) NOT NULL,
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
CREATE UNIQUE INDEX "UX_InternalIdentifier" ON "User" ("InternalIdentifier") WHERE "DeletionDate" IS NULL;


/*===================== TABLE: COURSE =====================*/


CREATE TABLE "Course" (
    "Id" UUID PRIMARY KEY,
	"InternalIdentifier" VARCHAR(12) NOT NULL,
    "UserId" UUID NOT NULL,
    "Name" VARCHAR(150) NOT NULL,
    "Code" VARCHAR(20) NOT NULL,
    "CreationDate" TIMESTAMP NOT NULL,
    "UpdateDate" TIMESTAMP NULL,
    "DeletionDate" TIMESTAMP NULL
);

ALTER TABLE "Course" ADD CONSTRAINT "FK_Course_UserId" FOREIGN KEY ("UserId") REFERENCES "User" ("Id");

CREATE UNIQUE INDEX "UX_Code" ON "Course" ("Code") WHERE "DeletionDate" IS NULL;


/*===================== TABLE: COURSE MEMBER =====================*/


CREATE TABLE "CourseMember" (
    "Id" UUID PRIMARY KEY,
	"InternalIdentifier" VARCHAR(12) NOT NULL,
    "CourseId" UUID NOT NULL,
	"UserId" UUID NOT NULL,
    "EnrollmentDate" TIMESTAMP NOT NULL,
    "CreationDate" TIMESTAMP NOT NULL,
    "UpdateDate" TIMESTAMP NULL,
    "DeletionDate" TIMESTAMP NULL
);

ALTER TABLE "CourseMember" ADD CONSTRAINT "FK_CourseMember_CourseId" FOREIGN KEY ("CourseId") REFERENCES "Course" ("Id");

ALTER TABLE "CourseMember" ADD CONSTRAINT "FK_CourseMember_UserId" FOREIGN KEY ("UserId") REFERENCES "User" ("Id");

CREATE UNIQUE INDEX "IX_CourseMember_CourseId_UserId_Unique_Active" ON "CourseMember" ("CourseId", "UserId") WHERE "DeletionDate" IS NULL;


