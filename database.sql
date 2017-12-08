

CREATE TABLE [dbo].[Contacts] (
    [Id]        INT          IDENTITY (1, 1) NOT NULL,
    [FirstName] VARCHAR (50) NULL,
    [LastName]  VARCHAR (50) NULL,
    [Email]     VARCHAR (50) NULL,
    [Company]   VARCHAR (50) NULL,
    [Title]     VARCHAR (50) NULL,
    CONSTRAINT [PK_Contacts] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[States] (
    [Id]        INT          NOT NULL,
    [StateName] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_States] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Addresses] (
    [Id]            INT          IDENTITY (1, 1) NOT NULL,
    [ContactId]     INT          NOT NULL,
    [AddressType]   VARCHAR (10) NOT NULL,
    [StreetAddress] VARCHAR (50) NOT NULL,
    [City]          VARCHAR (50) NOT NULL,
    [StateId]       INT          NOT NULL,
    [PostalCode]    VARCHAR (20) NOT NULL,
    CONSTRAINT [PK_Addresses] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Addresses_Contacts] FOREIGN KEY ([ContactId]) REFERENCES [dbo].[Contacts] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Addresses_States] FOREIGN KEY ([StateId]) REFERENCES [dbo].[States] ([Id])
);

GO

CREATE PROCEDURE [dbo].[DeleteAddress]
	@Id int
AS
BEGIN
	DELETE FROM Addresses WHERE Id = @Id;
END;
GO

CREATE PROCEDURE [dbo].[DeleteContact]
	@Id int
AS
BEGIN
	DELETE FROM Contacts
	WHERE Id = @Id;
END;

GO
CREATE procedure [dbo].[GetContact]
	@Id int
AS
BEGIN
	SELECT [Id]
		  ,[FirstName]
		  ,[LastName]
		  ,[Company]
		  ,[Title]
		  ,[Email]
	  FROM [dbo].[Contacts]
	WHERE Id = @Id;

	SELECT 
		Id,
		ContactId,
		AddressType,
		StreetAddress,
		City,
		StateId,
		PostalCode
	FROM [dbo].[Addresses] 
	WHERE ContactID = @Id;

END;
GO
CREATE procedure [dbo].[SaveAddress]
	@Id            int output,
	@ContactId     int,
	@AddressType   varchar(10),
	@StreetAddress varchar(50),
	@City          varchar(50),
	@StateId       int,
	@PostalCode    varchar(20)
AS
BEGIN
	UPDATE	Addresses
	SET		ContactId     = @ContactId,
	        AddressType   = @AddressType,
	        StreetAddress = @StreetAddress,
			City          = @City,
			StateId       = @StateId,
			PostalCode    = @PostalCode
	WHERE	Id            = @Id

	IF @@ROWCOUNT = 0
	BEGIN
		INSERT INTO Addresses
		(ContactId, AddressType, StreetAddress, City, StateId, PostalCode)
		VALUES (@ContactId, @AddressType, @StreetAddress, @City, @StateId, @PostalCode);
		
		SET @Id = cast(scope_identity() as int)
	END;
END;

GO
create procedure [dbo].[SaveContact]
	@Id     	int output,
	@FirstName	varchar(50),
	@LastName	varchar(50),	
	@Company	varchar(50),
	@Title		varchar(50),
	@Email		varchar(50)
AS
BEGIN
	UPDATE	Contacts
	SET		FirstName = @FirstName,
			LastName  = @LastName,
			Company   = @Company,
			Title     = @Title,
			Email     = @Email
	WHERE	Id        = @Id

	IF @@ROWCOUNT = 0
	BEGIN
		INSERT INTO [dbo].[Contacts]
           ([FirstName]
           ,[LastName]
           ,[Company]
           ,[Title]
           ,[Email])
		VALUES
           (@FirstName,
           @LastName, 
           @Company,
           @Title,
           @Email);
		SET @Id = cast(scope_identity() as int)
	END;
END;