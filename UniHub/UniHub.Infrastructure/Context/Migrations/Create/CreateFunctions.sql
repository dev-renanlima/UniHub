
/*

===================== FUNCTION: InsertUser =====================


CREATE OR REPLACE FUNCTION "InsertUser"(
	p_Id UUID,
    p_InternalIdentifier VARCHAR(12),
	p_ExternalIdentifier VARCHAR(50),    
    p_Name VARCHAR(150),
    p_Email VARCHAR(150),
    p_Role VARCHAR(20),
	p_Status VARCHAR(20),
	p_ProfileUrl VARCHAR(2000),
	p_CreationDate TIMESTAMP,
    p_UpdateDate TIMESTAMP
)
RETURNS UUID
AS $$
DECLARE
    new_id UUID;
BEGIN
    INSERT INTO "User" (
		"Id",
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
		p_Id,
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


===================== FUNCTION: GetUserByIdentifier =====================


CREATE OR REPLACE FUNCTION "GetUserByIdentifier"(
    p_Identifier VARCHAR(50)
)
RETURNS TABLE (
    "Id" UUID,
	"InternalIdentifier" VARCHAR(12),
    "ExternalIdentifier" VARCHAR(50),    
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
			ue."InternalIdentifier",
			ue."ExternalIdentifier",			
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
			ui."InternalIdentifier",
			ui."ExternalIdentifier",			
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


===================== FUNCTION: GetUserById =====================


CREATE OR REPLACE FUNCTION "GetUserById"(
    p_Id UUID
)
RETURNS TABLE (
    "Id" UUID,
	"InternalIdentifier" VARCHAR(12),
    "ExternalIdentifier" VARCHAR(50),    
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
			u."InternalIdentifier",
			u."ExternalIdentifier",			
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


===================== FUNCTION: InsertCourse =====================


CREATE OR REPLACE FUNCTION "InsertCourse"(
	p_Id UUID,
    p_InternalIdentifier VARCHAR(12),
    p_UserId UUID,
    p_Name VARCHAR(150),
    p_Code VARCHAR(20),
    p_CreationDate TIMESTAMP,
    p_UpdateDate TIMESTAMP
)
RETURNS UUID
AS $$
DECLARE
    new_id UUID;
BEGIN
    INSERT INTO "Course" (
		"Id",
        "InternalIdentifier",
        "UserId",
        "Name",
        "Code",
        "CreationDate",
        "UpdateDate"
    )
    VALUES (
		p_Id,
        p_InternalIdentifier,
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


===================== FUNCTION: GetCourseByCode =====================


CREATE OR REPLACE FUNCTION "GetCourseByCode"(
    p_Code VARCHAR(20)
)
RETURNS TABLE (
    "Id" UUID,
    "InternalIdentifier" VARCHAR(12),
    "UserId" UUID,
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
        c."InternalIdentifier",
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


===================== FUNCTION: GetCourseMembersByCourseId =====================


CREATE OR REPLACE FUNCTION "GetCourseMembersByCourseId"(
    p_CourseId UUID
)
RETURNS TABLE (
    "CourseId" UUID,
	"CourseName" VARCHAR(150),
    "UserId" UUID,
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


===================== FUNCTION: GetCoursesByUserId =====================


CREATE OR REPLACE FUNCTION "GetCoursesByUserId"(
    p_UserId UUID
)
RETURNS TABLE (
    "CourseId" UUID,
    "CourseIdentifier" VARCHAR(12),
    "CourseName" VARCHAR(150),
    "CourseCode" VARCHAR(50),
    "UserId" UUID,
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
        c."InternalIdentifier" AS "CourseIdentifier",
        c."Name" AS "CourseName",
        c."Code" AS "CourseCode",
        uc."Id" AS "UserId",
        uc."Name" AS "UserName",
        uc."ExternalIdentifier" AS "UserIdentifier",
        COUNT(cm."Id") AS "NumberOfMembers",
        c."CreationDate",
        c."UpdateDate"
    FROM
        "Course" c
        INNER JOIN "User" uc ON uc."Id" = c."UserId" 
        LEFT JOIN "CourseMember" cm ON cm."CourseId" = c."Id"
    WHERE
        c."DeletionDate" IS NULL
        AND uc."DeletionDate" IS NULL
        AND (
            -- professor do curso
            c."UserId" = p_UserId
            -- ou aluno inscrito no curso
            OR EXISTS (
                SELECT 1
                FROM "CourseMember" cme
                INNER JOIN "User" u ON u."Id" = cme."UserId"
                WHERE cme."CourseId" = c."Id"
                  AND cme."UserId" = p_UserId
                  AND u."DeletionDate" IS NULL
            )
        )
    GROUP BY
        c."Id",
        c."InternalIdentifier",
        c."Name",
        c."Code",
        uc."Id",
        uc."Name",
        uc."ExternalIdentifier",
        c."CreationDate",
        c."UpdateDate";
END;
$$ LANGUAGE plpgsql;


===================== FUNCTION: InsertCourseMember =====================


CREATE OR REPLACE FUNCTION public."InsertCourseMember"(
	p_Id UUID,
    p_InternalIdentifier VARCHAR(12),
    p_CourseId UUID,
	p_UserId UUID,
	p_EnrollmentDate TIMESTAMP,
    p_CreationDate TIMESTAMP,
    p_UpdateDate TIMESTAMP
)
RETURNS UUID
AS $$
DECLARE
    new_id UUID;
BEGIN
    INSERT INTO public."CourseMember" (
		"Id",
		"InternalIdentifier",
        "CourseId",
        "UserId",
        "EnrollmentDate",
        "CreationDate",
        "UpdateDate"
    )
    VALUES (
		p_Id,
		p_InternalIdentifier,
		p_CourseId,
        p_UserId,
        p_EnrollmentDate,
        p_CreationDate,
        p_UpdateDate
    )
    RETURNING "Id" INTO new_id;

    RETURN new_id;
END;
$$ LANGUAGE plpgsql;


*/