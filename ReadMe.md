This is the output

![image](https://github.com/jdsolivio/ASP.NET-Core-Web-API/assets/156039126/21863ba2-9d76-442f-8b89-13542a2fb839)


and this is the response

![image](https://github.com/jdsolivio/ASP.NET-Core-Web-API/assets/156039126/e711fa64-4490-4c11-ad6c-c9cbca323178)




================================
       Q U E R Y
================================
================================
Create a Table  TodoItems       
================================
                                                 
##                                                
    CREATE TABLE TodoItems (  
      Id INT IDENTITY(1,1) PRIMARY KEY,   
      Title NVARCHAR(MAX),
      Description NVARCHAR(MAX),
      IsCompleted BIT,
      CreatedAt DATETIME DEFAULT GETDATE(),
	  UniqueID INT
    );


================================
Create a Table  UserRegistrationTodoItems
================================

##
    CREATE TABLE UserRegistrationTodoItems (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(50), 
        LastName NVARCHAR(50), 
        MiddleInitial NVARCHAR(50), 
        BirtDate DATETIME, 
        Email NVARCHAR(50),
        Username NVARCHAR(50),
        Password NVARCHAR(50),
        TermsCondition BIT,
    );

================================
Create a Stored Procedure InsertTodoItems    
================================

##
    USE [JDSolivioDB]
        GO

        SET ANSI_NULLS ON
        GO
        SET QUOTED_IDENTIFIER ON
        GO


    ALTER PROCEDURE [dbo].[InsertTodoItems]
        @Title VARCHAR(255),
        @Description VARCHAR(MAX),
        @IsCompleted BIT,
        @UniqueID INT,
        @retVal INT OUTPUT
    AS
    BEGIN
        INSERT INTO TodoItems (Title, Description, IsCompleted, UniqueID)
        VALUES (@Title, @Description, @IsCompleted, @UniqueID);
        SET @retVal = 200;
    END;


================================
Create a Stored Procedure UpdateTodoItems  
================================

##
    USE [JDSolivioDB]
    GO


    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO


    ALTER PROCEDURE [dbo].[UpdateTodoItems]
        @Id INT,
        @Title NVARCHAR(100),
        @Description NVARCHAR(255),
	    @UniqueID INT,
        @IsCompleted BIT
    AS
    BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[TodoItems]
    SET 
        [Title] = @Title,
        [Description] = @Description,
        [IsCompleted] = @IsCompleted,
		[UniqueID] = @UniqueID
    WHERE
        [Id] = @Id;
    END;


================================
Create a Stored Procedure GetAllTodoItems     
================================

##
    USE [JDSolivioDB]
    GO


    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO


    ALTER PROCEDURE [dbo].[GetAllTodoItems]
    AS
    BEGIN
	    SET NOCOUNT ON;
	    SELECT * FROM dbo.TodoItems;
    END;


================================
Create a Stored Procedure DeleteTodoItem      
================================

#
    USE [JDSolivioDB]
    GO


    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO


    ALTER PROCEDURE [dbo].[DeleteTodoItem]
        @Id INT
    AS
    BEGIN
	    SET NOCOUNT ON;
        DELETE FROM [dbo].[TodoItems]
	    WHERE Id = @Id;
    END;


================================
Create a Stored Procedure GetSingleTodoItem   
================================

##
    USE [JDSolivioDB]
    GO


    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO


    ALTER PROCEDURE [dbo].[GetSingleTodoItem]
	    @UniqueID INT
    AS
    BEGIN
        SELECT *
        FROM TodoItems
        WHERE UniqueID = @UniqueID;
    END;

================================
Create a Stored Procedure InsertUserTodoItem   
================================

##
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO

    CREATE PROCEDURE InsertUserTodoItem
        @Name NVARCHAR(50),
        @LastName NVARCHAR(50),
        @MiddleInitial NVARCHAR(50),
        @BirthDate DATE,
        @Email NVARCHAR(50),
        @Username NVARCHAR(50),
        @Password NVARCHAR(50),
        @TermsCondition BIT,
        @retVal INT OUTPUT
	    AS 
    BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        INSERT INTO [JDSolivioDB].[dbo].[UserRegistrationTodoItems] (Name, LastName, MiddleInitial, BirtDate, Email, Username, Password, TermsCondition)
        VALUES (@Name, @LastName, @MiddleInitial, @BirthDate, @Email, @Username, @Password, @TermsCondition);      
        SET @retVal = 200;
    END TRY
    BEGIN CATCH
        SET @retVal = ERROR_NUMBER();
    END CATCH
    END;

================================
Create a Stored Procedure GetAllUserRegistrationTodoItems 
================================

##
    CREATE PROCEDURE GetAllUserRegistrationTodoItems
    AS
    BEGIN
    SET NOCOUNT ON;

    SELECT [Id],
           [Name],
           [LastName],
           [MiddleInitial],
           [BirtDate],
           [Email],
           [Username],
           [Password],
           [TermsCondition]
    FROM [JDSolivioDB].[dbo].[UserRegistrationTodoItems];
    END;

================================
Create a Stored Procedure GetUsernamePassword 
================================

##

    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO

    Create PROCEDURE GetUsernamePassword
	    @Username NVARCHAR(50),
	    @Password NVARCHAR(50)
    AS
    BEGIN
        SELECT *
        FROM UserRegistrationTodoItems
        WHERE 
	    Username = @Username
	    AND Password = @Password;
    END;


================================
Create a Stored Procedure UpdateUserDataTodoItem 
================================

##
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO

    Create PROCEDURE UpdateUserDataTodoItem
        @Id INT,
        @Name NVARCHAR(50),
        @LastName NVARCHAR(50),
        @MiddleInitial NVARCHAR(50),
        @BirthDate DATE,
        @Email NVARCHAR(50),
        @Username NVARCHAR(50),
        @Password NVARCHAR(50),
        @TermsCondition BIT
    AS
    BEGIN
        SET NOCOUNT ON;
        UPDATE [dbo].[UserRegistrationTodoItems]
        SET 
        [Name] = @Name,
        [LastName] = @LastName,
		[MiddleInitial] = @MiddleInitial,
		[BirtDate] = @BirthDate,
		[Email] = @Email,
		[Username] = @Username,
		[Password] = @Password,
		[TermsCondition] = @TermsCondition
    WHERE
        [Id] = @Id;
    END;