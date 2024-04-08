This is the output.

![image](https://github.com/jdsolivio/ASP.NET-Core-Web-API/assets/156039126/a873170b-184c-4136-974c-aa5ae8635f87)



And this is the response

[POST]
![image](https://github.com/jdsolivio/ASP.NET-Core-Web-API/assets/156039126/77ad2cbf-6fc4-49d8-9e9b-dfd82ca054cc)
[GET ALL DATA]
![image](https://github.com/jdsolivio/ASP.NET-Core-Web-API/assets/156039126/76e3bc48-a285-44df-97ee-bb5370806af2)
[GET SINGLE DATA]
![image](https://github.com/jdsolivio/ASP.NET-Core-Web-API/assets/156039126/30e04ce1-23f5-4269-bdf9-799b068aabc2)
[UPDATE]
![image](https://github.com/jdsolivio/ASP.NET-Core-Web-API/assets/156039126/653e165e-b17e-480d-a969-c1680f81f92e)
[DELETE]
![image](https://github.com/jdsolivio/ASP.NET-Core-Web-API/assets/156039126/03bb96ce-a14a-4780-b581-b60272b2d5c1)





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