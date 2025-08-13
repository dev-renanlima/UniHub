
/*


CREATE OR REPLACE FUNCTION public."InsertUser"(
    p_InternalIdentifier VARCHAR(50),
	p_ExternalIdentifier VARCHAR(50),    
    p_Name VARCHAR(150),
    p_Email VARCHAR(150),
    p_Role VARCHAR(20),
	p_Status VARCHAR(20),
	p_ProfileUrl VARCHAR(2000),
	p_CreationDate TIMESTAMP,
    p_UpdateDate TIMESTAMP
)
RETURNS BIGINT
AS $$
DECLARE
    new_id BIGINT;
BEGIN
    INSERT INTO "User" (
		"InternalIdentifier",
        "ExternalIdentifier",
        "Name",
		"Email",
        "Role",
		"Status",
		"ProfileUrl",
        "CreationDate",
        "UpdateDate"
    )
    VALUES (
		p_InternalIdentifier,
        p_ExternalIdentifier,
        p_Name,
		p_Email,
        p_Role,
		p_Status,
		p_ProfileUrl,
        p_CreationDate,
        p_UpdateDate
    )
    RETURNING "Id" INTO new_id;

    RETURN new_id;
END;
$$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION "GetUserByIdentifier"(
    p_Identifier VARCHAR(50)
)
RETURNS TABLE (
    "Id" BIGINT,
    "ExternalIdentifier" VARCHAR(50),
    "InternalIdentifier" VARCHAR(50),
    "Name" VARCHAR(150),
	"Email" VARCHAR(150),
    "Role" VARCHAR(20),
	"Status" VARCHAR(20),
	"ProfileUrl" VARCHAR(2000),
    "CreationDate" TIMESTAMP,
    "UpdateDate" TIMESTAMP
)
AS $$
BEGIN
    RETURN QUERY
		SELECT
			ue."Id",
			ue."ExternalIdentifier",
			ue."InternalIdentifier",
			ue."Name",
			ue."Email",
			ue."Role",
			ue."Status",
			ue."ProfileUrl",
			ue."CreationDate",
			ue."UpdateDate"
		FROM 
			"User" ue
		WHERE 
			ue."ExternalIdentifier" = p_Identifier AND ue."DeletionDate" IS NULL
		UNION ALL
		SELECT
			ui."Id",
			ui."ExternalIdentifier",
			ui."InternalIdentifier",
			ui."Name",
			ui."Email",
			ui."Role",
			ui."Status",
			ui."ProfileUrl",
			ui."CreationDate",
			ui."UpdateDate"
		FROM 
			"User" ui
		WHERE 
			ui."InternalIdentifier" = p_Identifier AND ui."DeletionDate" IS NULL
			AND NOT EXISTS (
				SELECT 1
				FROM "User" u
				WHERE u."ExternalIdentifier" = p_Identifier AND u."DeletionDate" IS NULL
		  	);
END;
$$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION "GetUserById"(
    p_Id BIGINT
)
RETURNS TABLE (
    "Id" BIGINT,
    "ExternalIdentifier" VARCHAR(50),
    "InternalIdentifier" VARCHAR(50),
    "Name" VARCHAR(150),
	"Email" VARCHAR(150),
    "Role" VARCHAR(20),
	"Status" VARCHAR(20),
	"ProfileUrl" VARCHAR(2000),
    "CreationDate" TIMESTAMP,
    "UpdateDate" TIMESTAMP
)
AS $$
BEGIN
    RETURN QUERY
		SELECT
			u."Id",
			u."ExternalIdentifier",
			u."InternalIdentifier",
			u."Name",
			u."Email",
			u."Role",
			u."Status",
			u."ProfileUrl",
			u."CreationDate",
			u."UpdateDate"
		FROM 
			"User" u
		WHERE 
			u."Id" = p_Id AND u."DeletionDate" IS NULL;		
END;
$$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION public."InsertCourse"(
    p_UserId BIGINT,
    p_Name VARCHAR(150),
    p_Code VARCHAR(20),
    p_CreationDate TIMESTAMP,
    p_UpdateDate TIMESTAMP
)
RETURNS BIGINT
AS $$
DECLARE
    new_id BIGINT;
BEGIN
    INSERT INTO public."Course" (
        "UserId",
        "Name",
        "Code",
        "CreationDate",
        "UpdateDate"
    )
    VALUES (
        p_UserId,
        p_Name,
        p_Code,
        p_CreationDate,
        p_UpdateDate
    )
    RETURNING "Id" INTO new_id;

    RETURN new_id;
END;
$$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION "GetCourseByCode"(
    p_Code VARCHAR(20)
)
RETURNS TABLE (
    "Id" BIGINT,
    "UserId" BIGINT,
    "Name" VARCHAR(150),
    "Code" VARCHAR(20),
    "CreationDate" TIMESTAMP,
    "UpdateDate" TIMESTAMP
)
AS $$
BEGIN
    RETURN QUERY
    SELECT
        c."Id",
        c."UserId",
        c."Name",
        c."Code",
        c."CreationDate",
        c."UpdateDate"
    FROM
        "Course" c
    WHERE
        c."DeletionDate" IS NULL AND		
        c."Code" = p_Code;
END;
$$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION "GetCourseMembersByCourseId"(
    p_CourseId BIGINT
)
RETURNS TABLE (
    "CourseId" BIGINT,
	"CourseName" VARCHAR(150),
    "UserId" BIGINT,
    "UserName" VARCHAR(150),
    "UserIdentifier" VARCHAR(50),
    "EnrollmentDate" TIMESTAMP
)
AS $$
BEGIN
    RETURN QUERY
    SELECT
        cm."CourseId",
		c."Name" AS "CourseName",
        cm."UserId",
        u."Name" AS "UserName",
        u."ExternalIdentifier" AS "UserIdentifier",
        cm."EnrollmentDate"
    FROM
        "CourseMember" cm
		INNER JOIN "Course" c ON c."Id" = cm."CourseId"
		INNER JOIN "User" u ON u."Id" = cm."UserId"
    WHERE
        cm."DeletionDate" IS NULL AND 
		c."DeletionDate" IS NULL AND
		u."DeletionDate" IS NULL AND
        cm."CourseId" = p_CourseId;
END;
$$ LANGUAGE plpgsql;



CREATE OR REPLACE FUNCTION "GetCoursesByUserId"(
    p_UserId BIGINT
)
RETURNS TABLE (
    "CourseId" BIGINT,
    "CourseName" VARCHAR(150),
    "CourseCode" VARCHAR(50),
    "UserId" BIGINT,
    "UserName" VARCHAR(150),
    "UserIdentifier" VARCHAR(50),
    "NumberOfMembers" BIGINT,
    "CreationDate" TIMESTAMP,
    "UpdateDate" TIMESTAMP
)
AS $$
BEGIN
    RETURN QUERY
    SELECT
        c."Id" AS "CourseId",
        c."Name" AS "CourseName",
        c."Code" AS "CourseCode",
        u."Id" AS "UserId",
        u."Name" AS "UserName",
        u."ExternalIdentifier" AS "UserIdentifier",
        COUNT(cm."Id") AS "NumberOfMembers",
        c."CreationDate",
        c."UpdateDate"
    FROM
        "Course" c
        INNER JOIN "User" u ON u."Id" = p_UserId
        LEFT JOIN "CourseMember" cm ON cm."CourseId" = c."Id"
    WHERE
        c."DeletionDate" IS NULL
        AND u."DeletionDate" IS NULL
        AND (
            c."UserId" = p_UserId -- ADMIN
            OR EXISTS (
                SELECT 1
                FROM "CourseMember" cme
                WHERE cme."CourseId" = c."Id"
                  AND cme."UserId" = p_UserId
            ) -- MEMBER
        )
    GROUP BY
        c."Id",
        c."Name",
        c."Code",
        u."Id",
        u."Name",
        u."ExternalIdentifier",
        c."CreationDate",
        c."UpdateDate";
END;
$$ LANGUAGE plpgsql;

*/