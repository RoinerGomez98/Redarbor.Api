CREATE DATABASE RedarborDB
GO
USE RedarborDB
GO

if not exists (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Redarbors')
begin
CREATE TABLE Redarbors(
	Id int identity(1,1) NOT NULL,
	CompanyId int NOT NULL,
	Email nvarchar(150) NOT NULL,
	Fax nvarchar(150) NULL,
	Name nvarchar(100) NULL,
	Username nvarchar(100) NULL,
	Password nvarchar(100) NOT NULL,
	Telephone nvarchar(20) NULL,
	Lastlogin smalldatetime NULL,
	CreatedOn smalldatetime NULL,
	DeletedOn smalldatetime NULL,
	UpdatedOn smalldatetime NULL,
	StatusId int NULL,
	PortalId int NOT NULL,
	RoleId int NOT NULL
	PRIMARY KEY (Id)
	)
end
go

CREATE OR ALTER PROC dbo.SP_GET_REDARBOR
	@Id int = 0,
	@Email nvarchar(150) = null,
	@Password nvarchar(100) = null
AS
BEGIN
	SELECT r.*
	FROM Redarbors r
	WHERE (r.Id = case when @Id=0 then r.Id else @Id end) 
	AND (r.Email = @Email OR COALESCE(@Email, '') = '')
	AND (r.Password  = @Password  OR COALESCE(@Password, '') = '')
	AND r.StatusId = 1
END
go

CREATE OR ALTER PROC SP_INSERT_UPDATE_REDARBORS
	@Id int,
	@CompanyId int,
	@Email nvarchar(150),
	@Fax nvarchar(150) ,
	@Name nvarchar(100) ,
	@Username nvarchar(100) ,
	@Password nvarchar(100)  ,
	@Telephone nvarchar(20) ,
	@Lastlogin smalldatetime ,
	@CreatedOn smalldatetime ,
	@DeletedOn smalldatetime ,
	@UpdatedOn smalldatetime ,
	@StatusId int ,
	@PortalId int,
	@RoleId int 
as
begin
	IF not exists( SELECT Id FROM Redarbors WHERE Id=@Id)
	begin
		INSERT INTO Redarbors (CompanyId,Email,Fax,Name,Username,Password,Telephone,Lastlogin,CreatedOn,DeletedOn,UpdatedOn,StatusId,PortalId,RoleId)
		VALUES (@CompanyId,@Email,@Fax,@Name,@Username,@Password,@Telephone,@Lastlogin,@CreatedOn,@DeletedOn,@UpdatedOn,@StatusId,@PortalId,@RoleId)
	end
	ELSE
	begin
		Update Redarbors set Name =@Name,Username = @Username,Email = @Email,Password=@Password,Fax = @Fax,Telephone = @Telephone
		,StatusId=@StatusId
		WHERE Id =@Id
	end
end
go

CREATE OR ALTER PROC SP_DELETE_REDARBORS
	@Id int
as
begin
	IF exists( SELECT Id FROM Redarbors WHERE Id=@Id)
	begin
		DELETE FROM  Redarbors WHERE Id=@Id
	end
end
go

SET IDENTITY_INSERT Redarbors ON 

GO

IF NOT EXISTS(SELECT * FROM Redarbors WHERE Id = 1)
BEGIN
	INSERT INTO Redarbors(Id,CompanyId, CreatedOn, DeletedOn, Email, Fax, Lastlogin,
	Name, Password, PortalId, RoleId, StatusId, Telephone, UpdatedOn, Username)
	VALUES(1,1, '2000-01-01 00:00:00','2000-01-01 00:00:00',
	'test1@test.test.tmp', '000.000.000','2000-01-01 00:00:00', 'test1', 
	'u1dP8aaRm5NwtJrDqnfvtA==',1,1,1, '000.000.000', '2000-01-01 00:00:00', 'test1'); 
END

GO

