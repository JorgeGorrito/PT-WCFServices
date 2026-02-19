-- 1. Table Creation
CREATE TABLE [dbo].[Users] (
    [UserId]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]            VARCHAR (100)  NOT NULL,
    [BirthDate]       DATE           NOT NULL,
    [Gender]          CHAR (1)       NOT NULL, -- 'M' or 'F'
    [DeletedAt]       DATETIME       NULL,     -- For Soft Delete
    PRIMARY KEY CLUSTERED ([UserId] ASC),
    CONSTRAINT CHK_User_Gender CHECK ([Gender] IN ('M', 'F'))
);
GO

-- 2. Optimized Filtered Index
CREATE NONCLUSTERED INDEX IX_Users_Active 
ON [dbo].[Users] (UserId) 
INCLUDE (Name, BirthDate, Gender)
WHERE [DeletedAt] IS NULL;
GO

-- 3. Unified CRUD Stored Procedure
CREATE PROCEDURE [dbo].[sp_ManageUser]
    @Action VARCHAR(50),
    @UserId INT = NULL,
    @Name VARCHAR(100) = NULL,
    @BirthDate DATE = NULL,
    @Gender CHAR(1) = NULL,
    @OrderDesc BIT = 1,
    @PageSize INT = 10,
    @CurrentPage INT = 1
AS
BEGIN
    SET NOCOUNT ON;

    -- CREATE
    IF @Action = 'CREATE'
    BEGIN
        INSERT INTO Users (Name, BirthDate, Gender, DeletedAt)
        VALUES (@Name, @BirthDate, @Gender, NULL);
        SELECT SCOPE_IDENTITY() AS UserId;
    END

    -- READ (BY ID)
    ELSE IF @Action = 'GET_BY_ID'
    BEGIN
        SELECT UserId, Name, BirthDate, Gender 
        FROM Users WHERE UserId = @UserId AND DeletedAt IS NULL;
    END

    -- READ (PAGINATED)
    ELSE IF @Action = 'GET_PAGINATED'
    BEGIN
        SELECT 
            UserId, 
            Name, 
            BirthDate, 
            Gender,
            COUNT(*) OVER() AS TotalCount
        FROM Users
        WHERE DeletedAt IS NULL
        ORDER BY 
            CASE WHEN @OrderDesc = 1 THEN UserId END DESC,
            CASE WHEN @OrderDesc = 0 THEN UserId END ASC
        OFFSET (@CurrentPage - 1) * @PageSize ROWS
        FETCH NEXT @PageSize ROWS ONLY;
    END

    ELSE IF @Action = 'GET_BY_GENDER_PAGINATED'
    BEGIN
        SELECT 
            UserId, 
            Name, 
            BirthDate, 
            Gender,
            COUNT(*) OVER() AS TotalCount
        FROM Users
        WHERE Gender = @Gender AND DeletedAt IS NULL
        ORDER BY 
            CASE WHEN @OrderDesc = 1 THEN UserId END DESC,
            CASE WHEN @OrderDesc = 0 THEN UserId END ASC
        OFFSET (@CurrentPage - 1) * @PageSize ROWS
        FETCH NEXT @PageSize ROWS ONLY;
    END

    ELSE IF @Action = 'GET_BY_NAME_PAGINATED'
    BEGIN
        SELECT 
            UserId, 
            Name, 
            BirthDate, 
            Gender,
            COUNT(*) OVER() AS TotalCount
        FROM Users
        WHERE Name LIKE '%' + @Name + '%' AND DeletedAt IS NULL
        ORDER BY 
            CASE WHEN @OrderDesc = 1 THEN UserId END DESC,
            CASE WHEN @OrderDesc = 0 THEN UserId END ASC
        OFFSET (@CurrentPage - 1) * @PageSize ROWS
        FETCH NEXT @PageSize ROWS ONLY;
    END

    -- UPDATE
    ELSE IF @Action = 'UPDATE'
    BEGIN
        UPDATE Users
        SET Name = ISNULL(@Name, Name),
            BirthDate = ISNULL(@BirthDate, BirthDate),
            Gender = ISNULL(@Gender, Gender)
        WHERE UserId = @UserId AND DeletedAt IS NULL;
    END

    -- REMOVE (Soft Delete)
    ELSE IF @Action = 'REMOVE'
    BEGIN
        UPDATE Users SET DeletedAt = GETDATE() WHERE UserId = @UserId;
    END
END
GO