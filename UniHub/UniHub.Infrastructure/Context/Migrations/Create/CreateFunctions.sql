
/*

-- Stored Procedure: InsertUser
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


-- Stored Procedure: GetUserByIdentifier
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

*/