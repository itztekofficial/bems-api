--=============User Activity===================
CREATE PROC [usp_Activity]
@ActionType varchar(50) = '',
@Module varchar(100) = '',
@Action varchar(100) = '',
@Message varchar(250) = '',
@CreatedById int = 0,
@ModifiedById int =0
AS
BEGIN
IF(@ActionType = 'getAll')
BEGIN
 SELECT A.*,
 	U.[Name] As CreatedBy, U1.[Name] As UpdatedBy, (CASE WHEN A.IsActive = 1 THEN 'Active' ELSE 'In-Active' END) AS ActiveStr,
	CONVERT(VARCHAR(10), A.CreateDate, 105) AS CreateDateStr, CONVERT(VARCHAR(10), A.ModifyDate, 105) AS ModifyDateStr
 FROM [ActivityLog](nolock) A 
 LEFT JOIN [User](nolock) U ON A.CreatedById = U.Id
 LEFT JOIN [User](nolock) U1 ON A.ModifiedById = U1.Id
 where A.IsActive = 1 order by A.[Module] asc
END

IF(@ActionType = 'insert')
BEGIN
INSERT INTO [ActivityLog]([CompanyId] ,[Module] ,[Action] ,[Message] ,[IsActive] ,[CreatedById] ,[CreateDate] ,[ModifiedById] ,[ModifyDate])
     VALUES (1 , @Module , @Action , @Message , 1 , @CreatedById , GETUTCDATE() , @ModifiedById , GETUTCDATE())
END

END
GO

--================Lookups====================
CREATE PROC [usp_Lookups]
@ActionType varchar(50) = '',
@Id int=0,
@LookTypeId int=0,
@Name varchar(50) = '',
@Description varchar(50) = '',
@Value varchar(10) = '',
@Sequence int=0,
@IsActive bit =0,
@CreatedById int=0,
@CreateDate datetime= '',
@ModifiedById int=0,
@ModifyDate datetime= ''
AS
BEGIN
if(@ActionType = 'getAll')
begin
 SELECT * FROM [LookUp] (NOLOCK) WHERE IsActive = 1
end

if(@ActionType = 'getById')
begin
 SELECT * FROM [LookUp] (NOLOCK) WHERE [Id] = @Id
end

if(@ActionType = 'getBylookTypeId')
begin
 SELECT * FROM [LookUp] (NOLOCK) WHERE [LookTypeId] = @LookTypeId
end

if(@ActionType = 'insert')
begin
 INSERT INTO [LookUp] 
           ([LooktypeId],[Name],[Description],[Value],[Sequence],[IsActive],[CreatedById],[CreateDate])
     VALUES
           (@LooktypeId,@Name,@Description, @Value,@Sequence, @IsActive, @CreatedById, @CreateDate)
end

if(@ActionType = 'update')
begin
 UPDATE [LookUp]
   SET [LooktypeId] = @LooktypeId,[Name] = @Name,[Description] = @Description,[Value] = @Value,[Sequence] = @Sequence,[IsActive] = @IsActive
      ,[ModifiedById] = @ModifiedById,[ModifyDate] = @ModifyDate
   WHERE [Id] = @Id
end

if(@ActionType = 'delete')
begin
 Update [LookUp] set IsActive=0 WHERE Id = @Id
end

end
GO

--============Lookup Type===============
CREATE PROC [usp_LookupType]
@ActionType varchar(50) = '',
@Id int=0,
@Name varchar(50) = '',
@CreatedById int=0,
@CreateDate datetime =null,
@ModifiedById int =0,
@ModifyDate datetime =null,
@IsActive bit = 0
AS
BEGIN
if(@ActionType = 'getAll')
begin
 SELECT * FROM [LookUpType](nolock) where IsActive = 1
end

if(@ActionType = 'getById')
begin
 SELECT * FROM [LookUpType](nolock) where  IsActive=1 AND [Id] = @Id 
end

if(@ActionType = 'insert')
begin
 INSERT INTO [LookUpType]
       ([Name],[CreatedById],[CreateDate],[IsActive])
 VALUES
       (@Name,@CreatedById,@CreateDate, @IsActive)
end

if(@ActionType = 'update')
begin
 UPDATE [LookUpType]
   SET [Name] = @Name, [ModifiedById] = @ModifiedById, [ModifyDate] = @ModifyDate, [IsActive] = @IsActive
   WHERE Id = @Id
end

if(@ActionType = 'delete')
begin
  Update [LookUpType] set IsActive=0 WHERE Id = @Id
end

end
GO

--==============usp_Login================
CREATE PROC [usp_Login]
@ActionType VARCHAR(50) = '',
@Id INT = 0,
@EmailId VARCHAR(50) = '',
@UserPwd VARCHAR(50) = '',
@EntityId INT = 0,
@DepartmentId INT = 0
AS
BEGIN
SET NOCOUNT ON;

IF(@ActionType = 'ValidateUser')
BEGIN
	 SELECT * FROM [User] 
	 WHERE IsActive=1 AND EmailId = @EmailId and [UserPwd] = @UserPwd
END

IF(@ActionType = 'ValidateUserRole')
BEGIN
     DECLARE @IsMultiEntiry bit=0, @IsMultiDepartment bit = 0

	 SELECT @IsMultiEntiry= (CASE WHEN COUNT(DISTINCT EntityIds) > 1 THEN 1 ELSE 0  END),
	 @IsMultiDepartment= (CASE WHEN COUNT(DISTINCT DepartmentIds) > 1 THEN '1' ELSE 0 END) 
	 FROM [UserMapping] 
	 WHERE IsActive = 1 AND RefUserId = @Id AND IsActive = 1

	 SELECT  @IsMultiEntiry as IsMultiEntiry, @IsMultiDepartment as IsMultiDepartment

	SELECT e.[Id], e.[Name] FROM [Entity](nolock) e WHERE e.Id IN (
	SELECT EntityIds FROM [UserMapping] WHERE IsActive = 1 AND RefUserId = @Id) 
	and  e.IsActive = 1 ORDER BY [Sequence] ASC

	SELECT e.[Id], e.[Name] FROM department(nolock) e WHERE e.IsActive = 1 and e.Id IN (
	SELECT DepartmentIds FROM [UserMapping] WHERE IsActive = 1 AND RefUserId = @Id) 	
	ORDER BY [Sequence] ASC    
END

IF(@ActionType = 'UserDetailById')
BEGIN
	DECLARE @CompanyId INT, @RoleTypeId INT, @RoleIds VARCHAR(100), @tmpDepartmentId VARCHAR(100), @MenuIds VARCHAR(100), @ExtraMenuIds VARCHAR(100)  
	
	SELECT @CompanyId= CompanyId, @RoleIds = RoleIds, @ExtraMenuIds = MenuIds, @RoleTypeId = [RoleTypeId]
	 FROM [UserMapping] WHERE IsActive = 1 AND RefUserId = @Id AND EntityIds IN (@EntityId) AND DepartmentIds IN (@DepartmentId)
	
	SELECT Id, [Name], EmailId, MobileNo, UserType, @RoleTypeId AS RoleId, @DepartmentId AS DepartmentId, @EntityId AS EntityId  
	 FROM [User] WHERE IsActive = 1 AND CompanyId = @CompanyId AND Id = @Id
	
	SELECT [Name], Code, ContactPerson, EmailId, RegMobile, [Address], GSTNNo FROM [Company] WHERE IsActive = 1 AND Id = @CompanyId
	
	SELECT @MenuIds = MenuIds FROM [RoleMenu] WHERE IsActive = 1 AND CompanyId = @CompanyId AND Id IN (SELECT * FROM split_string_XML(@RoleIds,','))

	SET @MenuIds = (CASE WHEN (LEN(ISNULL(@ExtraMenuIds, '')) > 0) THEN  (@MenuIds + ',' + @ExtraMenuIds) ELSE @MenuIds END)
	SELECT * FROM [Menu] WHERE IsActive = 1 AND CompanyId = @CompanyId AND Id IN (SELECT * FROM split_string_XML(@MenuIds,','))
END

END
GO

--==================================usp_Menu==============================
CREATE PROC [usp_Menu]
@ActionType varchar(50) = '',
@Id int=0,
@CompanyId int=0,
@ParentId int=0,
@Label varchar(50) = '',
@Icon varchar(25) = '',
@RouterLink varchar(250) = '',
@Order int=0,
@IsParent bit = 0,
@ExpandedIcon varchar(25) = '',
@CollapsedIcon varchar(25) = '',
@IsActive bit = 0,
@CreatedById int=0,
@CreateDate datetime =null,
@ModifiedById int =0,
@ModifyDate datetime =null
AS
BEGIN

IF(@ActionType = 'getAll')
BEGIN
	SELECT * FROM [Menu](nolock) where IsActive = 1
END

if(@ActionType = 'getById')
BEGIN
	 SELECT * FROM [Menu](nolock) where  IsActive=1 AND [Id] = @Id 
END

IF(@ActionType = 'insert')
BEGIN
	 INSERT INTO [Menu]
		   ([CompanyId], [ParentId], [Label], [Icon], [RouterLink], [Order], [IsParent], [ExpandedIcon], [CollapsedIcon], [IsActive], [CreatedById], [CreateDate])
	 VALUES
		   (@CompanyId, @ParentId, @Label, @Icon, @RouterLink, @Order, @IsParent, @ExpandedIcon, @CollapsedIcon, @IsActive, @CreatedById, @CreateDate)
END

IF(@ActionType = 'update')
BEGIN
	 UPDATE [Menu]
	   SET [ParentId] = @ParentId, [Label] = @Label, [Icon] = @Icon, [RouterLink] = @RouterLink, [Order] = @Order, [IsParent] = @IsParent, [ExpandedIcon] = @ExpandedIcon,[CollapsedIcon] = @CollapsedIcon, [IsActive] = @IsActive, [ModifiedById] = @ModifiedById, [ModifyDate] = @ModifyDate
	 WHERE Id = @Id
END

IF(@ActionType = 'delete')
BEGIN
	Update [Menu] set [IsActive] = @IsActive, [ModifiedById] = @ModifiedById, [ModifyDate] = @ModifyDate WHERE Id = @Id
END

END
GO

--==================================usp_user==============================
CREATE PROC [usp_User]
@ActionType varchar(50) = '',
@Id int=0,
@CompanyId int=0,
@Name varchar(100) = '',
@EmailId varchar(100) = '',
@MobileNo varchar(15) = '',
@UserPwd varchar(250) = '',
@UserType int = 0,
@IsAdmin bit = 0, 
@IsPredefined bit = 0,
@IsLoggedIn	bit = 0,
@LogoutDateTime	datetime = null,
@IsActive bit = 0,
@CreatedById int = 0,
@CreateDate datetime = null,
@ModifiedById int = 0,
@ModifyDate datetime = null
AS
BEGIN

IF(@ActionType = 'getAll')
BEGIN
 SELECT E.*,
	U.[Name] As CreatedBy, U1.[Name] As UpdatedBy, (CASE WHEN E.IsActive = 1 THEN 'Active' ELSE 'In-Active' END) AS ActiveStr,
	FORMAT(E.CreateDate, 'dd/MM/yyyy hh:mm:ss tt') AS CreateDateStr, FORMAT(E.ModifyDate, 'dd/MM/yyyy hh:mm:ss tt') AS ModifyDateStr
 FROM [User] E (nolock)
 LEFT JOIN [User](nolock) U ON E.CreatedById = U.Id
 LEFT JOIN [User](nolock) U1 ON E.ModifiedById = U1.Id
 WHERE E.IsActive = 1   
END

if(@ActionType = 'getById')
BEGIN
 SELECT * FROM [User](nolock) where  IsActive=1 AND [Id] = @Id 
END

IF(@ActionType = 'insert')
BEGIN
 INSERT INTO [User]
       ([CompanyId], [Name], [EmailId], [MobileNo], [UserPwd], [UserType], [IsAdmin], [IsPredefined] ,[IsLoggedIn], [LogoutDateTime] ,[IsActive], [CreatedById],[CreateDate])
 VALUES
       (@CompanyId, @Name, @EmailId, @MobileNo, @UserPwd, @UserType, @IsAdmin, @IsPredefined, @IsLoggedIn, @LogoutDateTime, @IsActive, @CreatedById, @CreateDate)
END

IF(@ActionType = 'update')
BEGIN
 UPDATE [User]
   SET [Name] = @Name, [EmailId] = @EmailId, [MobileNo] = @MobileNo, [UserPwd] = @UserPwd, [IsActive]=@IsActive, [ModifiedById] = @ModifiedById, [ModifyDate] = @ModifyDate
   WHERE Id = @Id
END

IF(@ActionType = 'delete')
BEGIN
  Update [User] set [IsActive] = @IsActive, [ModifiedById] = @ModifiedById, [ModifyDate] = @ModifyDate WHERE Id = @Id
END

END
GO

--==================================usp_RoleMenu==============================
CREATE PROC [usp_RoleMenu]
@ActionType varchar(50) = '',
@Id int=0,
@CompanyId int=0,
@Name varchar(100) = '',
@IsAdmin bit = 0,
@IsPredefined bit = 0,
@MenuIds varchar(100) = '',
@IsActive bit = 0,
@CreatedById int=0,
@CreateDate datetime =null,
@ModifiedById int =0,
@ModifyDate datetime =null
AS
BEGIN

IF(@ActionType = 'getAll')
BEGIN
 --SELECT * FROM [RoleMenu](nolock) where IsActive = 1
	Select E.*, 
	U.[Name] As CreatedBy, U1.[Name] As UpdatedBy, (CASE WHEN E.IsActive = 1 THEN 'Active' ELSE 'In-Active' END) AS ActiveStr,
	FORMAT(E.CreateDate, 'dd/MM/yyyy hh:mm:ss tt') AS CreateDateStr, FORMAT(E.ModifyDate, 'dd/MM/yyyy hh:mm:ss tt') AS ModifyDateStr
	FROM [RoleMenu](nolock) E
	LEFT JOIN [User](nolock) U ON E.CreatedById = U.Id
	LEFT JOIN [User](nolock) U1 ON E.ModifiedById = U1.Id
	WHERE E.IsActive = 1 
	ORDER BY E.[Name] ASC
END

if(@ActionType = 'getById')
BEGIN
 SELECT * FROM [RoleMenu](nolock) where IsActive = 1 AND [Id] = @Id 
END

IF(@ActionType = 'insert')
BEGIN
 INSERT INTO [RoleMenu]
       ([CompanyId], [Name], [MenuIds], [IsAdmin], [IsPredefined], [IsActive], [CreatedById], [CreateDate])
 VALUES
       (@CompanyId, @Name, @MenuIds, @IsAdmin, @IsPredefined, @IsActive, @CreatedById, @CreateDate)

 EXEC [usp_Activity] @ActionType = 'insert', @Module = 'Role Menu', @Action = 'Insert', @Message = 'Record added successfully !', @CreatedById = @CreatedById, @ModifiedById = @CreatedById 
END

IF(@ActionType = 'update')
BEGIN
 UPDATE [RoleMenu]
   SET [Name] = @Name, [MenuIds] = @MenuIds, [IsActive] = @IsActive, [ModifiedById] = @ModifiedById, [ModifyDate] = @ModifyDate
   WHERE Id = @Id

EXEC [usp_Activity] @ActionType = 'insert', @Module = 'Role Menu', @Action = 'Update', @Message = 'Record updated successfully !', @CreatedById = @CreatedById, @ModifiedById = @CreatedById
END

IF(@ActionType = 'delete')
BEGIN
  Update [RoleMenu] set [IsActive] = @IsActive, [ModifiedById] = @ModifiedById, [ModifyDate] = @ModifyDate WHERE Id = @Id
  
EXEC [usp_Activity] @ActionType = 'insert', @Module = 'RoleMenu', @Action = 'Delete', @Message = 'Record deleted successfully !', @CreatedById = @CreatedById, @ModifiedById = @CreatedById
END

END
GO

--==================================usp_UserMapping==============================
CREATE PROC [usp_UserMapping]      
@ActionType varchar(50) = '',      
@Id int=0,      
@CompanyId int=0,      
@RefUserId int=0,      
@RoleIds varchar(100) = '',      
@EntityIds varchar(100) = '',      
@DepartmentIds varchar(100) = '', 
@RoleTypeId int=0,
@MenuIds varchar(100) = '',      
@IsActive bit = 0,      
@CreatedById int=0,      
@CreateDate datetime =null,      
@ModifiedById int =0,      
@ModifyDate datetime =null      
AS      
BEGIN      
      
IF(@ActionType = 'getAll')      
BEGIN      
 SELECT         
   ur.[Name] AS 'UserName',        
   ISNULL(STUFF((SELECT ',' + e.[Name] FROM [Entity] e WHERE e.Id IN (SELECT * FROM split_string_XML(u.EntityIds, ','))         
   FOR XML PATH(''), TYPE).value('text()[1]', 'nvarchar(max)') , 1, LEN(','), ''), '') AS [Entitys],        
   ISNULL(STUFF((SELECT ',' + d.[Name] FROM [Department] d WHERE d.Id IN (SELECT * FROM split_string_XML(u.DepartmentIds, ','))         
   FOR XML PATH(''), TYPE).value('text()[1]', 'nvarchar(max)') , 1, LEN(','), ''), '') AS [Departments],        
   ISNULL(STUFF((SELECT ',' + r.[Name] FROM [RoleMenu] r WHERE r.Id IN (SELECT * FROM split_string_XML(u.RoleIds, ','))         
   FOR XML PATH(''), TYPE).value('text()[1]', 'nvarchar(max)') , 1, LEN(','), ''), '') AS [Roles], u.*,  
   U1.[Name] As CreatedBy, U2.[Name] As UpdatedBy, (CASE WHEN u.IsActive = 1 THEN 'Active' ELSE 'In-Active' END) AS ActiveStr,  
   FORMAT(U.CreateDate, 'dd/MM/yyyy hh:mm:ss tt') AS CreateDateStr, FORMAT(U.ModifyDate, 'dd/MM/yyyy hh:mm:ss tt') AS ModifyDateStr  
  FROM [UserMapping](nolock) u        
  INNER JOIN [User](nolock) ur ON ur.Id = u.RefUserId  
  LEFT JOIN [User](nolock) U1 ON u.CreatedById = U1.Id  
  LEFT JOIN [User](nolock) U2 ON u.ModifiedById = U2.Id  
  WHERE u.IsActive = 1 AND u.RoleTypeId <> 14     
      
  SELECT u.[Id], u.[Name] FROM [User](nolock) u WHERE u.IsActive = 1 AND u.Id NOT IN (SELECT ur.RefUserId FROM [UserMapping] ur WHERE ur.IsActive = 1)      
  SELECT r.[Id], r.[Name] FROM [RoleMenu](nolock) r WHERE r.IsActive = 1 AND r.IsAdmin = 0       
  SELECT e.[Id], e.[Name] FROM [Entity](nolock) e WHERE e.IsActive = 1      
  SELECT d.[Id], d.[Name] FROM [Department](nolock) d WHERE d.IsActive = 1 
  SELECT l.[Id], l.[Name] FROM [LookUp](nolock) l WHERE l.IsActive = 1 AND l.[LooktypeId] = 3 AND [Id] <> 14
  SELECT m.[Id], m.[ParentId], m.[Label], m.[IsParent] FROM [Menu](nolock) m WHERE m.IsActive = 1 AND m.[Label] != 'Dashboard' ORDER BY [Order]      
END     
    
IF(@ActionType = 'getMenuByRoleId')      
BEGIN      
 DECLARE @tmp VARCHAR(250) SET @tmp = ''    
 SELECT @tmp = @tmp + r.[MenuIds] + ', ' FROM [RoleMenu](nolock) r WHERE r.IsActive = 1 AND r.Id IN (SELECT * FROM split_string_XML('4,5', ','))    
 SELECT m.* FROM [Menu](nolock) m WHERE m.IsActive = 1 AND m.[Label] != 'Dashboard'     
 AND m.Id IN (SELECT * FROM split_string_XML(SUBSTRING(@tmp, 0, LEN(@tmp)), ',') )     
END     
    
IF(@ActionType = 'getById')      
BEGIN      
 SELECT * FROM [UserMapping](nolock) where IsActive = 1 AND [Id] = @Id       
END      
      
IF(@ActionType = 'insert')      
BEGIN      
 INSERT INTO [UserMapping]      
       ([CompanyId], [RefUserId], [RoleIds], [EntityIds], [DepartmentIds], [RoleTypeId], [MenuIds], [IsActive], [CreatedById], [CreateDate])      
 VALUES      
       (@CompanyId, @RefUserId, @RoleIds, @EntityIds, @DepartmentIds, @RoleTypeId, @MenuIds, @IsActive, @CreatedById, @CreateDate)      
END      
      
IF(@ActionType = 'update')      
BEGIN      
 UPDATE [UserMapping]      
   SET [RoleIds] = @RoleIds, [EntityIds] = @EntityIds, [DepartmentIds] = @DepartmentIds, [RoleTypeId] = @RoleTypeId, [MenuIds] = @MenuIds, [IsActive] = @IsActive, [ModifiedById] = @ModifiedById, [ModifyDate] = @ModifyDate      
   WHERE [RefUserId] = @RefUserId    
END      
      
IF(@ActionType = 'delete')      
BEGIN      
  Update [UserMapping] set [IsActive] = @IsActive, [ModifiedById] = @ModifiedById, [ModifyDate] = @ModifyDate WHERE Id = @Id      
END      
      
END  
GO

--=========================Department master==============================
CREATE PROC [usp_Department]
@ActionType varchar(50) = '',
@Id int = 0,
@CompanyId int = 0,
@Name varchar(100) = '',
@Sequence int = 0,
@IsActive bit = 0,
@CreatedById int = 0,
@CreateDate datetime = null,
@ModifiedById int = 0,
@ModifyDate datetime = null
AS
BEGIN

IF(@ActionType = 'getAll')
BEGIN
  --SELECT * FROM [Department](nolock) where IsActive = 1 order by [Sequence] asc
	Select D.*, 
	U.[Name] As CreatedBy, U1.[Name] As UpdatedBy, (CASE WHEN D.IsActive = 1 THEN 'Active' ELSE 'In-Active' END) AS ActiveStr,
    FORMAT(D.CreateDate, 'dd/MM/yyyy hh:mm:ss tt') AS CreateDateStr, FORMAT(D.ModifyDate, 'dd/MM/yyyy hh:mm:ss tt') AS ModifyDateStr	
	FROM [Department](nolock) D
	LEFT JOIN [User](nolock) U ON D.CreatedById = U.Id
	LEFT JOIN [User](nolock) U1 ON D.ModifiedById = U1.Id
	WHERE D.IsActive = 1 
	ORDER BY D.[Sequence] ASC
END

if(@ActionType = 'getById')
BEGIN
  SELECT * FROM [Department](nolock) where  IsActive = 1 AND [Id] = @Id 
END

IF(@ActionType = 'insert')
BEGIN
	INSERT INTO [Department]
		([CompanyId], [Name], [Sequence], [IsActive], [CreatedById], [CreateDate])
	VALUES
		(@CompanyId, @Name, @Sequence, @IsActive, @CreatedById, @CreateDate)

 EXEC [usp_Activity] @ActionType = 'insert', @Module = 'Department', @Action = 'Insert', @Message = 'Record added successfully !', @CreatedById = @CreatedById, @ModifiedById = @CreatedById 
END

IF(@ActionType = 'update')
BEGIN
	UPDATE [Department]
	 SET [Name] = @Name, [Sequence] = @Sequence, [IsActive] = @IsActive, [ModifiedById] = @ModifiedById, [ModifyDate] = @ModifyDate
	WHERE Id = @Id

 EXEC [usp_Activity] @ActionType = 'insert', @Module = 'Department', @Action = 'Update', @Message = 'Record updated successfully !', @CreatedById = @CreatedById, @ModifiedById = @CreatedById
END

IF(@ActionType = 'delete')
BEGIN
  Update [Department] set [IsActive] = @IsActive, [ModifiedById] = @ModifiedById, [ModifyDate] = @ModifyDate WHERE Id = @Id

 EXEC [usp_Activity] @ActionType = 'insert', @Module = 'Department', @Action = 'Delete', @Message = 'Record deleted successfully !', @CreatedById = @CreatedById, @ModifiedById = @CreatedById
END

END
GO

--=========================Entity master==============================
CREATE PROC [usp_Entity]
@ActionType varchar(50) = '',
@Id int = 0,
@CompanyId int = 0,
@Code varchar(15) = '',
@Name varchar(100) = '',
@Sequence int = 0,
@IsActive bit = 0,
@CreatedById int = 0,
@CreateDate datetime =null,
@ModifiedById int =0,
@ModifyDate datetime =null
AS
BEGIN

IF(@ActionType = 'getAll')
BEGIN
 --SELECT * FROM [Entity](nolock) where IsActive = 1 order by [Sequence] asc
	Select E.*, 
	U.[Name] As CreatedBy, U1.[Name] As UpdatedBy, (CASE WHEN E.IsActive = 1 THEN 'Active' ELSE 'In-Active' END) AS ActiveStr,
	FORMAT(E.CreateDate, 'dd/MM/yyyy hh:mm:ss tt') AS CreateDateStr, FORMAT(E.ModifyDate, 'dd/MM/yyyy hh:mm:ss tt') AS ModifyDateStr
	FROM [Entity](nolock) E
	LEFT JOIN [User](nolock) U ON E.CreatedById = U.Id
	LEFT JOIN [User](nolock) U1 ON E.ModifiedById = U1.Id
	WHERE E.IsActive = 1 
	ORDER BY E.[Sequence] ASC
END

if(@ActionType = 'getById')
BEGIN
  SELECT * FROM [Entity](nolock) where  IsActive = 1 AND [Id] = @Id 
END

IF(@ActionType = 'insert')
BEGIN
	INSERT INTO [Entity]
		([CompanyId], [Code], [Name], [Sequence], [IsActive], [CreatedById], [CreateDate])
	VALUES
		(@CompanyId, @Code, @Name, @Sequence, @IsActive, @CreatedById, @CreateDate)

 EXEC [usp_Activity] @ActionType = 'insert', @Module = 'Entity', @Action = 'Insert', @Message = 'Record added successfully !', @CreatedById = @CreatedById, @ModifiedById = @CreatedById 
END

IF(@ActionType = 'update')
BEGIN
	UPDATE [Entity]
	 SET [Code] = @Code, [Name] = @Name, [Sequence] = @Sequence, [IsActive] = @IsActive, [ModifiedById] = @ModifiedById, [ModifyDate] = @ModifyDate
	WHERE Id = @Id

 EXEC [usp_Activity] @ActionType = 'insert', @Module = 'Entity', @Action = 'Update', @Message = 'Record updated successfully !', @CreatedById = @CreatedById, @ModifiedById = @CreatedById 
END

IF(@ActionType = 'delete')
BEGIN
  Update [Entity] set [IsActive] = @IsActive, [ModifiedById] = @ModifiedById, [ModifyDate] = @ModifyDate WHERE Id = @Id

  EXEC [usp_Activity] @ActionType = 'insert', @Module = 'Entity', @Action = 'Delete', @Message = 'Record deleted successfully !', @CreatedById = @CreatedById, @ModifiedById = @CreatedById
END

END
GO

--=========================Entity Type master==============================
CREATE  PROC [usp_EntityType]
@ActionType varchar(50) = '',
@Id int = 0,
@CompanyId int = 0,
@Name varchar(100) = '',
@Sequence int = 0,
@IsActive bit = 0,
@CreatedById int = 0,
@CreateDate datetime =null,
@ModifiedById int =0,
@ModifyDate datetime =null
AS
BEGIN

IF(@ActionType = 'getAll')
BEGIN
 --SELECT * FROM [EntityType](nolock) where IsActive = 1 order by [Sequence] asc
 	Select E.*, 
	U.[Name] As CreatedBy, U1.[Name] As UpdatedBy, (CASE WHEN E.IsActive = 1 THEN 'Active' ELSE 'In-Active' END) AS ActiveStr,
	FORMAT(E.CreateDate, 'dd/MM/yyyy hh:mm:ss tt') AS CreateDateStr, FORMAT(E.ModifyDate, 'dd/MM/yyyy hh:mm:ss tt') AS ModifyDateStr
	FROM [EntityType](nolock) E
	LEFT JOIN [User](nolock) U ON E.CreatedById = U.Id
	LEFT JOIN [User](nolock) U1 ON E.ModifiedById = U1.Id
	WHERE E.IsActive = 1 
	ORDER BY E.[Sequence] ASC
END

if(@ActionType = 'getById')
BEGIN
  SELECT * FROM [EntityType](nolock) where  IsActive = 1 AND [Id] = @Id 
END

IF(@ActionType = 'insert')
BEGIN
	INSERT INTO [EntityType]
		([CompanyId], [Name], [Sequence], [IsActive], [CreatedById], [CreateDate])
	VALUES
		(@CompanyId, @Name, @Sequence, @IsActive, @CreatedById, @CreateDate)

 EXEC [usp_Activity] @ActionType = 'insert', @Module = 'Entity Type', @Action = 'Insert', @Message = 'Record added successfully !', @CreatedById = @CreatedById, @ModifiedById = @CreatedById 
END

IF(@ActionType = 'update')
BEGIN
	UPDATE [EntityType]
	 SET [Name] = @Name, [Sequence] = @Sequence, [IsActive] = @IsActive, [ModifiedById] = @ModifiedById, [ModifyDate] = @ModifyDate
	WHERE Id = @Id

 EXEC [usp_Activity] @ActionType = 'insert', @Module = 'Entity Type', @Action = 'Update', @Message = 'Record updated successfully !', @CreatedById = @CreatedById, @ModifiedById = @CreatedById
END

IF(@ActionType = 'delete')
BEGIN
  Update [EntityType] set [IsActive] = @IsActive, [ModifiedById] = @ModifiedById, [ModifyDate] = @ModifyDate WHERE Id = @Id
  
 EXEC [usp_Activity] @ActionType = 'insert', @Module = 'Entity Type', @Action = 'Delete', @Message = 'Record deleted successfully !', @CreatedById = @CreatedById, @ModifiedById = @CreatedById
END

END
GO

--=========================Agreement master==============================
CREATE PROC [usp_Agreement]
@ActionType varchar(50) = '',
@Id int = 0,
@CompanyId int = 0,
@Name varchar(100) = '',
@Sequence int = 0,
@IsActive bit = 0,
@CreatedById int = 0,
@CreateDate datetime =null,
@ModifiedById int =0,
@ModifyDate datetime =null
AS
BEGIN

IF(@ActionType = 'getAll')
BEGIN
 --SELECT * FROM [Agreement](nolock) where IsActive = 1 order by [Sequence] asc
	Select A.*, 
	U.[Name] As CreatedBy, U1.[Name] As UpdatedBy, (CASE WHEN A.IsActive = 1 THEN 'Active' ELSE 'In-Active' END) AS ActiveStr,
	FORMAT(A.CreateDate, 'dd/MM/yyyy hh:mm:ss tt') AS CreateDateStr, FORMAT(A.ModifyDate, 'dd/MM/yyyy hh:mm:ss tt') AS ModifyDateStr
	FROM [Agreement](nolock) A
	LEFT JOIN [User](nolock) U ON A.CreatedById = U.Id
	LEFT JOIN [User](nolock) U1 ON A.ModifiedById = U1.Id
	WHERE A.IsActive = 1 
	ORDER BY A.[Sequence] ASC
END

if(@ActionType = 'getById')
BEGIN
  SELECT * FROM [Agreement](nolock) where  IsActive = 1 AND [Id] = @Id 
END

IF(@ActionType = 'insert')
BEGIN
	INSERT INTO [Agreement]
		([CompanyId], [Name], [Sequence], [IsActive], [CreatedById], [CreateDate])
	VALUES
		(@CompanyId, @Name, @Sequence, @IsActive, @CreatedById, @CreateDate)

 EXEC [usp_Activity] @ActionType = 'insert', @Module = 'Agreement', @Action = 'Insert', @Message = 'Record added successfully !', @CreatedById = @CreatedById, @ModifiedById = @CreatedById 
END

IF(@ActionType = 'update')
BEGIN
	UPDATE [Agreement]
	 SET [Name] = @Name, [Sequence] = @Sequence, [IsActive] = @IsActive, [ModifiedById] = @ModifiedById, [ModifyDate] = @ModifyDate
	WHERE Id = @Id

 EXEC [usp_Activity] @ActionType = 'insert', @Module = 'Agreement', @Action = 'Update', @Message = 'Record updated successfully !', @CreatedById = @CreatedById, @ModifiedById = @CreatedById
END

IF(@ActionType = 'delete')
BEGIN
  Update [Agreement] set [IsActive] = @IsActive, [ModifiedById] = @ModifiedById, [ModifyDate] = @ModifyDate WHERE Id = @Id

 EXEC [usp_Activity] @ActionType = 'insert', @Module = 'Agreement', @Action = 'Delete', @Message = 'Record deleted successfully !', @CreatedById = @CreatedById, @ModifiedById = @CreatedById
END

END
GO

--=========================SubAgreement master==============================
CREATE PROC [usp_SubAgreement]
@ActionType varchar(50) = '',
@Id int = 0,
@CompanyId int = 0,
@Name varchar(100) = '',
@Sequence int = 0,
@IsActive bit = 0,
@CreatedById int = 0,
@CreateDate datetime =null,
@ModifiedById int =0,
@ModifyDate datetime =null
AS
BEGIN

IF(@ActionType = 'getAll')
BEGIN
 --SELECT * FROM [SubAgreement](nolock) where IsActive = 1 order by [Sequence] asc
	Select SA.*, 
	U.[Name] As CreatedBy, U1.[Name] As UpdatedBy, (CASE WHEN SA.IsActive = 1 THEN 'Active' ELSE 'In-Active' END) AS ActiveStr,
	FORMAT(SA.CreateDate, 'dd/MM/yyyy hh:mm:ss tt') AS CreateDateStr, FORMAT(SA.ModifyDate, 'dd/MM/yyyy hh:mm:ss tt') AS ModifyDateStr
	FROM [SubAgreement](nolock) SA
	LEFT JOIN [User](nolock) U ON SA.CreatedById = U.Id
	LEFT JOIN [User](nolock) U1 ON SA.ModifiedById = U1.Id
	WHERE SA.IsActive = 1 
	ORDER BY SA.[Sequence] ASC
END

if(@ActionType = 'getById')
BEGIN
  SELECT * FROM [SubAgreement](nolock) where  IsActive = 1 AND [Id] = @Id 
END

IF(@ActionType = 'insert')
BEGIN
	INSERT INTO [SubAgreement]
		([CompanyId], [Name], [Sequence], [IsActive], [CreatedById], [CreateDate])
	VALUES
		(@CompanyId, @Name, @Sequence, @IsActive, @CreatedById, @CreateDate)
 
 EXEC [usp_Activity] @ActionType = 'insert', @Module = 'Sub Agreement', @Action = 'Insert', @Message = 'Record added successfully !', @CreatedById = @CreatedById, @ModifiedById = @CreatedById
END

IF(@ActionType = 'update')
BEGIN
	UPDATE [SubAgreement]
	 SET [Name] = @Name, [Sequence] = @Sequence, [IsActive] = @IsActive, [ModifiedById] = @ModifiedById, [ModifyDate] = @ModifyDate
	WHERE Id = @Id

 EXEC [usp_Activity] @ActionType = 'insert', @Module = 'Sub Agreement', @Action = 'Update', @Message = 'Record updated successfully !', @CreatedById = @CreatedById, @ModifiedById = @CreatedById
END

IF(@ActionType = 'delete')
BEGIN
  Update [SubAgreement] set [IsActive] = @IsActive, [ModifiedById] = @ModifiedById, [ModifyDate] = @ModifyDate WHERE Id = @Id
  
 EXEC [usp_Activity] @ActionType = 'insert', @Module = 'Sub Agreement', @Action = 'Delete', @Message = 'Record deleted successfully !', @CreatedById = @CreatedById, @ModifiedById = @CreatedById
END

END
GO

--=========================PaymentTerm master==============================
CREATE PROC [usp_PaymentTerm]
@ActionType varchar(50) = '',
@Id int = 0,
@CompanyId int = 0,
@Name varchar(100) = '',
@Sequence int = 0,
@IsActive bit = 0,
@CreatedById int = 0,
@CreateDate datetime =null,
@ModifiedById int =0,
@ModifyDate datetime =null
AS
BEGIN

IF(@ActionType = 'getAll')
BEGIN
	Select PT.*, 
	U.[Name] As CreatedBy, U1.[Name] As UpdatedBy, (CASE WHEN PT.IsActive = 1 THEN 'Active' ELSE 'In-Active' END) AS ActiveStr,
	FORMAT(PT.CreateDate, 'dd/MM/yyyy hh:mm:ss tt') AS CreateDateStr, FORMAT(PT.ModifyDate, 'dd/MM/yyyy hh:mm:ss tt') AS ModifyDateStr
	FROM [PaymentTerm](nolock) PT
	LEFT JOIN [User](nolock) U ON PT.CreatedById = U.Id
	LEFT JOIN [User](nolock) U1 ON PT.ModifiedById = U1.Id
	WHERE PT.IsActive = 1 
	ORDER BY PT.[Sequence] ASC
END

if(@ActionType = 'getById')
BEGIN
  SELECT * FROM [PaymentTerm](nolock) where  IsActive = 1 AND [Id] = @Id 
END

IF(@ActionType = 'insert')
BEGIN
	INSERT INTO [PaymentTerm]
		([CompanyId], [Name], [Sequence], [IsActive], [CreatedById], [CreateDate])
	VALUES
		(@CompanyId, @Name, @Sequence, @IsActive, @CreatedById, @CreateDate)
  
  EXEC [usp_Activity] @ActionType = 'insert', @Module = 'Payment Term', @Action = 'Insert', @Message = 'Record added successfully !', @CreatedById = @CreatedById, @ModifiedById = @CreatedById 
END

IF(@ActionType = 'update')
BEGIN
	UPDATE [PaymentTerm]
	 SET [Name] = @Name, [Sequence] = @Sequence, [IsActive] = @IsActive, [ModifiedById] = @ModifiedById, [ModifyDate] = @ModifyDate
	WHERE Id = @Id

  EXEC [usp_Activity] @ActionType = 'insert', @Module = 'Payment Term', @Action = 'Update', @Message = 'Record updated successfully !', @CreatedById = @CreatedById, @ModifiedById = @CreatedById
END

IF(@ActionType = 'delete')
BEGIN
  Update [PaymentTerm] set [IsActive] = @IsActive, [ModifiedById] = @ModifiedById, [ModifyDate] = @ModifyDate WHERE Id = @Id
  
  EXEC [usp_Activity] @ActionType = 'insert', @Module = 'PaymentTerm', @Action = 'Delete', @Message = 'Record deleted successfully !', @CreatedById = @CreatedById, @ModifiedById = @CreatedById
END

END
GO

--=========================Document master==============================
CREATE PROC [usp_Document]
@ActionType varchar(50) = '',
@Id int = 0,
@CompanyId int = 0,
@EntityTypeId int = 0,
@Name varchar(100) = '',
@Sequence int = 0,
@IsActive bit = 0,
@CreatedById int = 0,
@CreateDate datetime =null,
@ModifiedById int =0,
@ModifyDate datetime =null
AS
BEGIN

IF(@ActionType = 'getAll')
BEGIN
 	Select D.*, E.[Name] As EntityTypeName,
	U.[Name] As CreatedBy, U1.[Name] As UpdatedBy, (CASE WHEN D.IsActive = 1 THEN 'Active' ELSE 'In-Active' END) AS ActiveStr,
	FORMAT(E.CreateDate, 'dd/MM/yyyy hh:mm:ss tt') AS CreateDateStr, FORMAT(E.ModifyDate, 'dd/MM/yyyy hh:mm:ss tt') AS ModifyDateStr
	FROM [Document](nolock) D
	LEFT JOIN [EntityType](nolock) E ON E.Id = D.EntityTypeId
	LEFT JOIN [User](nolock) U ON D.CreatedById = U.Id
	LEFT JOIN [User](nolock) U1 ON D.ModifiedById = U1.Id
	WHERE D.IsActive = 1 
	ORDER BY E.Id, D.[Sequence] ASC
END

if(@ActionType = 'getById')
BEGIN
  SELECT * FROM [Document](nolock) where  IsActive = 1 AND [Id] = @Id 
END

IF(@ActionType = 'insert')
BEGIN
	INSERT INTO [Document]
		([CompanyId], [EntityTypeId], [Name], [Sequence], [IsActive], [CreatedById], [CreateDate])
	VALUES
		(@CompanyId, @EntityTypeId, @Name, @Sequence, @IsActive, @CreatedById, @CreateDate)
	
   EXEC [usp_Activity] @ActionType = 'insert', @Module = 'Document', @Action = 'Insert', @Message = 'Record added successfully !', @CreatedById = @CreatedById, @ModifiedById = @CreatedById
END

IF(@ActionType = 'update')
BEGIN
	UPDATE [Document]
	 SET [EntityTypeId] = @EntityTypeId, [Name] = @Name, [Sequence] = @Sequence, [IsActive] = @IsActive, [ModifiedById] = @ModifiedById, [ModifyDate] = @ModifyDate
	WHERE Id = @Id

  EXEC [usp_Activity] @ActionType = 'insert', @Module = 'Document', @Action = 'Edit', @Message = 'Record updated successfully !', @CreatedById = @CreatedById, @ModifiedById = @CreatedById
END

IF(@ActionType = 'delete')
BEGIN
  Update [Document] set [IsActive] = @IsActive, [ModifiedById] = @ModifiedById, [ModifyDate] = @ModifyDate WHERE Id = @Id
   
  EXEC [usp_Activity] @ActionType = 'insert', @Module = 'Document', @Action = 'Delete', @Message = 'Record deleted successfully !', @CreatedById = @CreatedById, @ModifiedById = @CreatedById
END

END
GO

--=========================Product master==============================
CREATE PROC [usp_Product]
@ActionType varchar(50) = '',
@Id int = 0,
@CompanyId int = 0,
@Code varchar(50) = '',
@Name varchar(250) = '',
@Sequence int = 0,
@IsActive bit = 0,
@CreatedById int = 0,
@CreateDate datetime =null,
@ModifiedById int =0,
@ModifyDate datetime =null
AS
BEGIN

IF(@ActionType = 'getAll')
BEGIN
	Select E.*, 
	U.[Name] As CreatedBy, U1.[Name] As UpdatedBy, (CASE WHEN E.IsActive = 1 THEN 'Active' ELSE 'In-Active' END) AS ActiveStr,
	FORMAT(E.CreateDate, 'dd/MM/yyyy hh:mm:ss tt') AS CreateDateStr, FORMAT(E.ModifyDate, 'dd/MM/yyyy hh:mm:ss tt') AS ModifyDateStr
	FROM [Product](nolock) E
	LEFT JOIN [User](nolock) U ON E.CreatedById = U.Id
	LEFT JOIN [User](nolock) U1 ON E.ModifiedById = U1.Id
	WHERE E.IsActive = 1 
	ORDER BY E.[Sequence] ASC
END

if(@ActionType = 'getById')
BEGIN
  SELECT * FROM [Product](nolock) where  IsActive = 1 AND [Id] = @Id 
END

IF(@ActionType = 'insert')
BEGIN
	INSERT INTO [Product]
		([CompanyId], [Code], [Name], [Sequence], [IsActive], [CreatedById], [CreateDate])
	VALUES
		(@CompanyId, @Code, @Name, @Sequence, @IsActive, @CreatedById, @CreateDate)

 EXEC [usp_Activity] @ActionType = 'insert', @Module = 'Product', @Action = 'Insert', @Message = 'Record added successfully !', @CreatedById = @CreatedById, @ModifiedById = @CreatedById 
END

IF(@ActionType = 'update')
BEGIN
	UPDATE [Product]
	 SET [Code] = @Code, [Name] = @Name, [Sequence] = @Sequence, [IsActive] = @IsActive, [ModifiedById] = @ModifiedById, [ModifyDate] = @ModifyDate
	WHERE Id = @Id

 EXEC [usp_Activity] @ActionType = 'insert', @Module = 'Product', @Action = 'Update', @Message = 'Record updated successfully !', @CreatedById = @CreatedById, @ModifiedById = @CreatedById 
END

IF(@ActionType = 'delete')
BEGIN
  Update [Product] set [IsActive] = @IsActive, [ModifiedById] = @ModifiedById, [ModifyDate] = @ModifyDate WHERE Id = @Id

  EXEC [usp_Activity] @ActionType = 'insert', @Module = 'Product', @Action = 'Delete', @Message = 'Record deleted successfully !', @CreatedById = @CreatedById, @ModifiedById = @CreatedById
END

END
GO

--=========================TermValidity master==============================
CREATE PROC [usp_TermValidity]
@ActionType varchar(50) = '',
@Id int = 0,
@CompanyId int = 0,
@Name varchar(250) = '',
@Sequence int = 0,
@IsActive bit = 0,
@CreatedById int = 0,
@CreateDate datetime =null,
@ModifiedById int =0,
@ModifyDate datetime =null
AS
BEGIN

IF(@ActionType = 'getAll')
BEGIN
	Select E.*, 
	U.[Name] As CreatedBy, U1.[Name] As UpdatedBy, (CASE WHEN E.IsActive = 1 THEN 'Active' ELSE 'In-Active' END) AS ActiveStr,
	FORMAT(E.CreateDate, 'dd/MM/yyyy hh:mm:ss tt') AS CreateDateStr, FORMAT(E.ModifyDate, 'dd/MM/yyyy hh:mm:ss tt') AS ModifyDateStr
	FROM [TermValidity](nolock) E
	LEFT JOIN [User](nolock) U ON E.CreatedById = U.Id
	LEFT JOIN [User](nolock) U1 ON E.ModifiedById = U1.Id
	WHERE E.IsActive = 1 
	ORDER BY E.[Sequence] ASC
END

if(@ActionType = 'getById')
BEGIN
  SELECT * FROM [TermValidity](nolock) where  IsActive = 1 AND [Id] = @Id 
END

IF(@ActionType = 'insert')
BEGIN
	INSERT INTO [TermValidity]
		([CompanyId], [Name], [Sequence], [IsActive], [CreatedById], [CreateDate])
	VALUES
		(@CompanyId, @Name, @Sequence, @IsActive, @CreatedById, @CreateDate)

 EXEC [usp_Activity] @ActionType = 'insert', @Module = 'TermValidity', @Action = 'Insert', @Message = 'Record added successfully !', @CreatedById = @CreatedById, @ModifiedById = @CreatedById 
END

IF(@ActionType = 'update')
BEGIN
	UPDATE [TermValidity]
	 SET [Name] = @Name, [Sequence] = @Sequence, [IsActive] = @IsActive, [ModifiedById] = @ModifiedById, [ModifyDate] = @ModifyDate
	WHERE Id = @Id

 EXEC [usp_Activity] @ActionType = 'insert', @Module = 'TermValidity', @Action = 'Update', @Message = 'Record updated successfully !', @CreatedById = @CreatedById, @ModifiedById = @CreatedById 
END

IF(@ActionType = 'delete')
BEGIN
  Update [TermValidity] set [IsActive] = @IsActive, [ModifiedById] = @ModifiedById, [ModifyDate] = @ModifyDate WHERE Id = @Id

  EXEC [usp_Activity] @ActionType = 'insert', @Module = 'TermValidity', @Action = 'Delete', @Message = 'Record deleted successfully !', @CreatedById = @CreatedById, @ModifiedById = @CreatedById
END

END
GO

--=========================Initiation Detail==============================
create PROC [usp_InitiationDetail]  
@ActionType varchar(50) = '',    
@Id int = 0,
@RoleTypeId int = 0,
@RefId varchar(50)= '',    
@DepartmentId int = 0,    
@CustomerName varchar(150) = '',    
@EntityTypeId int = 0,    
@OtherCustomerType varchar(100)='',
@EntityId int = 0,    
@customerTypeId int = 0,    
@AgreementId int = 0,    
@AgreementOthers varchar(100) = '',    
@PaymentTermId int = 0,    
@PaymentTermOthers varchar(100) = '',    
@OfficeAddress varchar(50) = '',    
@OfficeAddress1 varchar(50) = '',    
@OfficeCountryId int= 0,    
@OfficeOtherCountry varchar(100) = '',    
@OfficeStateId int = 0,    
@OfficeOtherState varchar(100) = '',    
@OfficeCityId varchar(50) = '',    
@OfficeOtherCity varchar(100) = '',
@OfficePinNo varchar(10) = '',
@DLNumber varchar(50) = '',    
@IsOfficeDLAddressSame bit = 0,
@DLAddress varchar(50) = '',    
@DLAddress1 varchar(50) = '',    
@DLCountryId int = 0,    
@DLOtherCountry varchar(100) = '',    
@DLStateId int = 0,    
@DLOtherState varchar(100) = '',    
@DLCityId int = 0,    
@DLOtherCity varchar(100) = '',
@DLPinNo varchar(10) = '',
@PANNumber varchar(50) = '',    
@GSTNumber varchar(50) = '',    
@IsDLBillingAddressSame bit = 0,    
@BillingAddress varchar(50) = '',    
@BillingAddress1 varchar(50) = '',    
@BillingCountryId int = 0,    
@BillingOtherCountry varchar(100) = '',    
@BillingStateId int = 0,    
@BillingOtherState varchar(100) = '',    
@BillingCityId int = 0,    
@BillingOtherCity varchar(100) = '',
@BillingPinNo varchar(10) = '',
@IsBillingDeliveryAddressSame bit = 0,   
@DeliveryAddress varchar(50) = '',    
@DeliveryAddress1 varchar(50) = '',    
@DeliveryCountryId int = 0,    
@DeliveryOtherCountry varchar(100) = '',    
@DeliveryStateId int = 0,    
@DeliveryOtherState varchar(100) = '',    
@DeliveryCityId int = 0,    
@DeliveryOtherCity varchar(100) = '',
@DeliveryPinNo varchar(10) = '',
@AuthorisedSignatoryName varchar(100) = '',    
@ContactPersonName varchar(100) = '',    
@ContactPersonMobile varchar(15) = '',    
@ContactPersonEmail varchar(100) = '',
@PartyProfileSheet_FileName varchar(250) = '',    
@PartyProfileSheet_Extension varchar(10) = '',    
@DraftAgreement_FileName varchar(250) = '',    
@DraftAgreement_Extension varchar(10) = '',
@Comment varchar(500) = '',    
@RequestStatusId int = 0,  
@Remarks varchar(500) = '',   
@IsActive bit = 0,    
@CreatedById int = 0,    
@CreateDate datetime = null,    
@ModifiedById int = 0,    
@ModifyDate datetime = null    
AS    
BEGIN    
    
IF(@ActionType = 'getAll')    
BEGIN    
 SELECT * FROM [InitiationDetail](nolock) where IsActive = 1 order by [CreateDate] asc    
END    
    
if(@ActionType = 'getById')    
BEGIN    
  DECLARE @tmpInitiationId VARCHAR(50)  
  SELECT @tmpInitiationId = [RefId] FROM [InitiationDetail](nolock) where IsActive = 1 AND [Id] = @Id    
    
  SELECT i.*, 
  ISNULL((SELECT um.[RoleTypeId] FROM [UserMapping] um WHERE um.[IsActive] = 1 AND um.[RefUserId] = i.[ModifiedById]), 0) AS ApproverRoleType
  FROM [InitiationDetail](nolock) i where i.IsActive = 1 AND i.[Id] = @Id
  
  SELECT * FROM [InitiationAttachment](nolock) where IsActive = 1 AND [InitiationId] = @tmpInitiationId 

  SELECT * FROM [LegalHead] (nolock) where IsActive = 1 AND [InitiationId] = @tmpInitiationId 
  
  SELECT lu.*, FORMAT(lu.CreateDate, 'dd/MM/yyyy hh:mm:ss tt') AS CreateDateStr FROM [LegalUser](nolock) lu where lu.IsActive = 1 AND lu.[InitiationId] = @tmpInitiationId
  
  SELECT CONVERT(VARCHAR(10), AgreementDate, 105) AS 'AgreementDateStr', CONVERT(VARCHAR(10), EffectiveDate, 105) AS 'EffectiveDateStr', 
  CONVERT(VARCHAR(10), RenewalDueDate, 105) AS 'RenewalDueDateStr', * 
  FROM [ExecutionDetails](nolock) where IsActive = 1 AND [InitiationId] = @tmpInitiationId
END    
    
IF(@ActionType = 'insert')    
BEGIN    
 DECLARE @tempRefId VARCHAR(10)    
 SELECT @tempRefId = MAX(CONVERT(INT, ISNULL([RefId],''))) FROM [InitiationDetail]    
    IF(@tempRefId IS NULL)    
 BEGIN    
  SET @RefId = '100001'    
 END    
 ELSE     
 BEGIN    
  SET @RefId = @tempRefId + 1    
 END    

INSERT INTO [InitiationDetail]    
    ([RefId],[DepartmentId],[RoleTypeId],[CustomerName],[EntityTypeId],[OtherCustomerType],[EntityId],[CustomerTypeId],[AgreementId],[AgreementOthers],[PaymentTermId],[PaymentTermOthers] ,[OfficeAddress],[OfficeAddress1],[OfficeCountryId],[OfficeOtherCountry],[OfficeStateId],[OfficeOtherState],[OfficeCityId],[OfficeOtherCity],[OfficePinNo],[DLNumber],[IsOfficeDLAddressSame],[DLAddress],[DLAddress1],[DLCountryId],[DLOtherCountry],[DLStateId],[DLOtherState],[DLCityId],[DLOtherCity],[DLPinNo],[PANNumber],[GSTNumber],[IsDLBillingAddressSame],[BillingAddress],[BillingAddress1],[BillingCountryId],[BillingOtherCountry],[BillingStateId],[BillingOtherState],[BillingCityId],[BillingOtherCity],[BillingPinNo],[IsBillingDeliveryAddressSame],[DeliveryAddress],[DeliveryAddress1],[DeliveryCountryId],[DeliveryOtherCountry],[DeliveryStateId],[DeliveryOtherState],[DeliveryCityId],[DeliveryOtherCity],[DeliveryPinNo],[AuthorisedSignatoryName],[ContactPersonName],[ContactPersonMobile],[ContactPersonEmail],[PartyProfileSheet_FileName],[PartyProfileSheet_Extension],[DraftAgreement_FileName],[DraftAgreement_Extension],[Comment],[RequestStatusId],[IsActive],[CreatedById],[CreateDate])    
VALUES  (@RefId,@DepartmentId,@RoleTypeId,@CustomerName,@EntityTypeId,@OtherCustomerType,@EntityId,@CustomerTypeId,@AgreementId,@AgreementOthers,@PaymentTermId,@PaymentTermOthers,@OfficeAddress,@OfficeAddress1,@OfficeCountryId,@OfficeOtherCountry,@OfficeStateId,@OfficeOtherState,@OfficeCityId,@OfficeOtherCity,@OfficePinNo,@DLNumber,@IsOfficeDLAddressSame,@DLAddress,@DLAddress1,@DLCountryId,@DLOtherCountry,@DLStateId,@DLOtherState,@DLCityId,@DLOtherCity,@DLPinNo,@PANNumber,@GSTNumber,@IsDLBillingAddressSame,@BillingAddress,@BillingAddress1,@BillingCountryId,@BillingOtherCountry,@BillingStateId,@BillingOtherState,@BillingCityId,@BillingOtherCity,@BillingPinNo,@IsBillingDeliveryAddressSame,@DeliveryAddress,@DeliveryAddress1,@DeliveryCountryId,@DeliveryOtherCountry,@DeliveryStateId,@DeliveryOtherState,@DeliveryCityId,@DeliveryOtherCity,@DeliveryPinNo,@AuthorisedSignatoryName,@ContactPersonName,@ContactPersonMobile,@ContactPersonEmail,@PartyProfileSheet_FileName,@PartyProfileSheet_Extension,@DraftAgreement_FileName,@DraftAgreement_Extension,@Comment,@RequestStatusId,@IsActive,@CreatedById,@CreateDate)
	
   IF(@@IDENTITY > 0)    
   BEGIN    
    SELECT @RefId AS InitiationId    
     
    INSERT INTO [InitiationRequestHistory] ([InitiationId] ,[RequestStatusId] ,[Comment] ,[CreatedById] ,[CreateDate])    
     VALUES (@RefId ,@RequestStatusId ,'Request initiatiated !' , @CreatedById ,@CreateDate)    
    
    EXEC [usp_Activity] @Module = 'New Request', @Action = 'Insert', @Message = 'Record added successfully !', @CreatedById = 1, @ModifiedById = 1     
   END    
   ELSE    
   BEGIN    
       SELECT '' AS InitiationId    
   END    
END    
    
IF(@ActionType = 'update')    
BEGIN    
    DECLARE @tempUpdateRefId VARCHAR(10)  

    UPDATE [InitiationDetail]    
     SET [CustomerName] =@CustomerName    
      ,[EntityTypeId] = @EntityTypeId    
	  ,[OtherCustomerType] = @OtherCustomerType  
      ,[EntityId] = @EntityId    
      ,[CustomerTypeId] = @CustomerTypeId    
      ,[AgreementId] = @AgreementId    
      ,[AgreementOthers] = @AgreementOthers    
      ,[PaymentTermId] = @PaymentTermId    
      ,[PaymentTermOthers] = @PaymentTermOthers    
      ,[OfficeAddress] = @OfficeAddress    
      ,[OfficeAddress1] = @OfficeAddress1    
      ,[OfficeCountryId] = @OfficeCountryId    
      ,[OfficeOtherCountry] = @OfficeOtherCountry    
      ,[OfficeStateId] = @OfficeStateId    
      ,[OfficeOtherState] = @OfficeOtherState    
      ,[OfficeCityId] = @OfficeCityId    
      ,[OfficeOtherCity] = @OfficeOtherCity    
	  ,[OfficePinNo]=@OfficePinNo
      ,[DLNumber] = @DLNumber    
      ,[IsOfficeDLAddressSame] = @IsOfficeDLAddressSame    
      ,[DLAddress] = @DLAddress    
      ,[DLAddress1] = @DLAddress1    
      ,[DLCountryId] = @DLCountryId    
      ,[DLOtherCountry] = @DLOtherCountry    
      ,[DLStateId] = @DLStateId    
      ,[DLOtherState] = @DLOtherState    
      ,[DLCityId] = @DLCityId    
      ,[DLOtherCity] = @DLOtherCity    
	  ,[DLPinNo]=@DLPinNo
      ,[PANNumber] = @PANNumber    
      ,[GSTNumber] = @GSTNumber    
      ,[IsDLBillingAddressSame] = @IsDLBillingAddressSame    
      ,[BillingAddress] = @BillingAddress    
      ,[BillingAddress1] = @BillingAddress1    
      ,[BillingCountryId] = @BillingCountryId    
      ,[BillingOtherCountry] = @BillingOtherCountry    
      ,[BillingStateId] = @BillingStateId    
      ,[BillingOtherState] = @BillingOtherState    
      ,[BillingCityId] =@BillingCityId    
      ,[BillingOtherCity] = @BillingOtherCity    
	  ,[BillingPinNo]=@BillingPinNo
	  ,[IsBillingDeliveryAddressSame]=@IsBillingDeliveryAddressSame
	  ,[DeliveryAddress] = @DeliveryAddress    
      ,[DeliveryAddress1] = @DeliveryAddress1    
      ,[DeliveryCountryId] = @DeliveryCountryId    
      ,[DeliveryOtherCountry] = @DeliveryOtherCountry    
      ,[DeliveryStateId] = @DeliveryStateId    
      ,[DeliveryOtherState] = @DeliveryOtherState    
      ,[DeliveryCityId] =@DeliveryCityId    
      ,[DeliveryOtherCity] = @DeliveryOtherCity    
	  ,[DeliveryPinNo]=@DeliveryPinNo
      ,[AuthorisedSignatoryName] = @AuthorisedSignatoryName    
      ,[ContactPersonName] = @ContactPersonName    
      ,[ContactPersonMobile] = @ContactPersonMobile    
      ,[ContactPersonEmail] = @ContactPersonEmail    
      ,[PartyProfileSheet_FileName] = @PartyProfileSheet_FileName    
      ,[PartyProfileSheet_Extension] = @PartyProfileSheet_Extension    
      ,[DraftAgreement_FileName] = @DraftAgreement_FileName    
      ,[DraftAgreement_Extension] = @DraftAgreement_Extension    
      ,[Comment] = @Comment    
      ,[RequestStatusId] = @RequestStatusId         
      ,[ModifiedById] = @ModifiedById    
      ,[ModifyDate] =@ModifyDate    
    WHERE Id = @Id    
  
    SELECT @tempUpdateRefId = [RefId] FROM [InitiationDetail](nolock) where IsActive = 1 AND [Id] = @Id   

   IF(@RoleTypeId = 16) --FOR Department HOD  
   BEGIN  
     INSERT INTO [InitiationRequestHistory] ([InitiationId] ,[RequestStatusId] ,[Comment] ,[CreatedById] ,[CreateDate])      
     VALUES (@tempUpdateRefId , @RequestStatusId ,'Request submitted by HOD !', @ModifiedById ,@ModifyDate)      
    END 
	
   IF(@RoleTypeId = 15 AND @RequestStatusId = 10) --FOR SEND TO CUSTOMER  
   BEGIN  
     INSERT INTO [InitiationRequestHistory] ([InitiationId] ,[RequestStatusId] ,[Comment] ,[CreatedById] ,[CreateDate])      
     VALUES (@tempUpdateRefId , @RequestStatusId ,'Request shared with customer !', @ModifiedById ,@ModifyDate)      
    END  

    EXEC [usp_Activity] @Module = 'Update Request', @Action = 'Update', @Message = 'Record updated successfully !', @CreatedById = @ModifiedById, @ModifiedById = @ModifiedById   
   
   SELECT @tempUpdateRefId
END    
    
IF(@ActionType = 'delete')    
BEGIN    
  Update [InitiationDetail] set [IsActive] = @IsActive, [ModifiedById] = @ModifiedById, [ModifyDate] = @ModifyDate WHERE Id = @Id    
END    
  
IF(@ActionType = 'updateStatus') 
 BEGIN    
  DECLARE @ReqStatus VARCHAR(50), @RemReq VARCHAR(100) , @tmpRefId VARCHAR(100);    
  UPDATE [InitiationDetail] SET [RequestStatusId] = @RequestStatusId , [Remarks] = @Remarks, [ModifiedById] = @ModifiedById, [ModifyDate] = @ModifyDate 
  WHERE Id = @Id      
   
   SELECT @tmpRefId = [RefId] FROM [InitiationDetail] WHERE [IsActive] = 1 AND [Id] = @Id 

   SELECT @ReqStatus = [Name] FROM [Lookup] WHERE [IsActive] = 1 AND [Id] = 
   (CASE WHEN (@RoleTypeId IN (16,17,18) AND @RequestStatusId IN (3,4,6)) THEN 19 ELSE @RequestStatusId END)

   SET @RemReq = CASE WHEN (@RoleTypeId = 16 and @RequestStatusId = 3) THEN 'Request has been ' + @ReqStatus + ' by Dept HOD !'
                      WHEN (@RoleTypeId = 17 and @RequestStatusId = 4) THEN 'Request has been ' + @ReqStatus + ' by Legal HOD !'
					  WHEN (@RoleTypeId = 18 and @RequestStatusId = 6) THEN 'Request has been ' + @ReqStatus + ' by Legal User !' 

					  WHEN (@RoleTypeId = 18 and @RequestStatusId = 4) THEN 'Request has been send back by Legal User !'
					  WHEN (@RoleTypeId = 17 and @RequestStatusId = 3) THEN 'Request has been send back by Legal HOD !'
					  WHEN (@RoleTypeId = 16 and @RequestStatusId = 11) THEN 'Request has been send back by Department HOD !'
				ELSE 'Request has been ' + @ReqStatus + ' !' END;  	      
  
   INSERT INTO [InitiationRequestHistory] ([InitiationId] ,[RequestStatusId] ,[Comment] ,[Remarks], [CreatedById] ,[CreateDate])    
    VALUES (@tmpRefId ,@RequestStatusId , @RemReq , @Remarks, @ModifiedById , @ModifyDate)    
    
   EXEC [usp_Activity] @Module = 'Initiation Request', @Action = 'Update Status', @Message = @RemReq, @CreatedById = @ModifiedById, @ModifiedById = @ModifiedById  
END  
    
END

GO

--=========================Initiation Attachment Detail==============================
CREATE PROC [usp_InitiationAttachment]
@ActionType varchar(50) = '',
@Id INT = 0,
@DocumentId INT = 0,
@InitiationId VARCHAR(50) = '',
@AttachmentName VARCHAR(100) = '',
@FileName VARCHAR(250) = '',
@Extension VARCHAR(10) = '',
@Sequence INT = 0,
@IsApproved bit = 0,
@IsActive bit = 0,
@CreatedById int = 0,
@CreateDate datetime = null,
@ModifiedById int = 0,
@ModifyDate datetime = null
AS
BEGIN

IF(@ActionType = 'getById')
BEGIN
  SELECT * FROM [dbo].[InitiationAttachment](nolock) where IsActive = 1 AND [Id] = @Id 
END

IF(@ActionType = 'getByInitiationId')
BEGIN
  SELECT * FROM [dbo].[InitiationAttachment](nolock) where IsActive = 1 AND [InitiationId] = @Id 
END

IF(@ActionType = 'insert')
BEGIN
	Declare @VersionNo varchar(10)='1.0'
	INSERT INTO [dbo].[InitiationAttachment] ([InitiationId],[DocumentId] ,[AttachmentName] ,[FileName] ,[Extension],[Sequence],[VersionNo],[IsApproved],[IsActive], [CreatedById] ,[CreateDate] ,[ModifiedById] ,[ModifyDate])
     VALUES (@InitiationId,@DocumentId, @AttachmentName, @FileName, @Extension, @Sequence,@VersionNo, @IsApproved, @IsActive, @CreatedById, @CreateDate, @ModifiedById, @ModifyDate)
	 
	 INSERT INTO [dbo].[InitiationAttachmentHistory] ([InitiationId],[DocumentId] ,[AttachmentName] ,[FileName] ,[Extension],[Sequence],[VersionNo] ,[IsActive], [CreatedById] ,[CreateDate] ,[ModifiedById] ,[ModifyDate])
     VALUES (@InitiationId,@DocumentId, @AttachmentName, @FileName, @Extension, @Sequence,@VersionNo, @IsActive, @CreatedById, @CreateDate, @ModifiedById, @ModifyDate)
END

IF(@ActionType = 'update')
BEGIN
	Declare @InitiationId1 varchar(10), @VersionNo1 varchar(10)
	sELECT @InitiationId1=InitiationId,@VersionNo1=CAST(VersionNo AS DECIMAL(5,1))+0.1 from  [InitiationAttachment] WHERE [Id] = @Id 

 UPDATE [dbo].[InitiationAttachment]
     SET [DocumentId]=@DocumentId
	    ,[AttachmentName] = @AttachmentName
	    ,[FileName] = @FileName
		,[Extension] = @Extension
		,[Sequence] = @Sequence
		,[VersionNo]=@VersionNo1
		,[IsApproved] = @IsApproved
		,[IsActive] = @IsActive
		,[ModifiedById] = @ModifiedById
		,[ModifyDate] = @ModifyDate
	WHERE [Id] = @Id 
	
	INSERT INTO [dbo].[InitiationAttachmentHistory] ([InitiationId],[DocumentId] ,[AttachmentName] ,[FileName] ,[Extension],[Sequence],[VersionNo] ,[IsActive], [CreatedById] ,[CreateDate] ,[ModifiedById] ,[ModifyDate])
    VALUES (@InitiationId1,@DocumentId, @AttachmentName, @FileName, @Extension, @Sequence,@VersionNo1, @IsActive, @CreatedById, @CreateDate, @ModifiedById, @ModifyDate)
END

IF(@ActionType = 'delete')
BEGIN
  Update [dbo].[InitiationAttachment] SET [IsActive] = @IsActive, [ModifiedById] = @ModifiedById, [ModifyDate] = @ModifyDate WHERE Id = @Id
END

END
GO

--=========================Utility Detail==============================
CREATE PROC [usp_Utility]
@ActionType varchar(50) = '',
@Id int = 0
AS
BEGIN

IF(@ActionType = 'getAllMasters')
BEGIN 
  SELECT e.[Id], e.[Name] FROM [Country](nolock) e WHERE e.IsActive = 1 ORDER BY [Sequence] ASC
  SELECT e.[Id], e.[Name] FROM [Entity](nolock) e WHERE e.IsActive = 1 ORDER BY [Sequence] ASC
  SELECT e.[Id], e.[Name] FROM [EntityType](nolock) e WHERE e.IsActive = 1 ORDER BY [Sequence] ASC    
  SELECT e.[Id], e.[Name] FROM [CustomerType](nolock) e WHERE e.IsActive = 1 ORDER BY [Sequence] ASC
  SELECT d.[Id], d.[Name] FROM [Agreement](nolock) d WHERE d.IsActive = 1 ORDER BY [Sequence] ASC
  SELECT d.[Id], d.[Name] FROM [PaymentTerm](nolock) d WHERE d.IsActive = 1 ORDER BY [Sequence] ASC

  SELECT d.[Id], d.[Name] FROM [SubAgreement](nolock) d WHERE d.IsActive = 1 ORDER BY [Sequence] ASC 
  SELECT d.[Id], d.[Name] FROM [User](nolock) d WHERE d.IsActive = 1 AND d.Id IN (SELECT [RefUserId] FROM [UserMapping] WHERE [RoleTypeId] = 18) --Legal User
  SELECT d.[Id], d.[EntityTypeId], d.[Name] FROM [Document](nolock) d WHERE d.IsActive = 1 ORDER BY [Sequence] ASC
  SELECT d.[Id], d.[Name] FROM [Product](nolock) d WHERE d.IsActive = 1 ORDER BY [Sequence] ASC 
  SELECT d.[Id], d.[Name] FROM [TermValidity](nolock) d WHERE d.IsActive = 1 ORDER BY [Sequence] ASC
  SELECT d.[Id], d.[Name] FROM [Lookup](nolock) d WHERE d.IsActive = 1 AND [LookTypeId] = 4 ORDER BY [Sequence] ASC
END

IF(@ActionType = 'getStateByCountryId')
BEGIN 
  SELECT e.[Id], e.[Name] FROM [State](nolock) e WHERE e.IsActive = 1 AND e.CountryId = @Id ORDER BY [Sequence] ASC
END

IF(@ActionType = 'getCityByStateId')
BEGIN 
  SELECT e.[Id], e.[Name] FROM [City](nolock) e WHERE e.IsActive = 1 AND e.StateId = @Id ORDER BY [Sequence] ASC 
END

END
GO

--==================================usp_Settings==============================
CREATE PROC [usp_Settings]    
@ActionType varchar(50) = '',    
@Id int = 0,
@UserPwd varchar(250) = '',
@ModifiedById int = 0,    
@ModifyDate datetime = null 
AS    
BEGIN    
    
IF(@ActionType = 'getUserDetailsById')    
BEGIN    
  SELECT       
   ur.[Name] AS 'UserName', ur.[EmailId], ur.[MobileNo],     
   ISNULL(STUFF((SELECT ',' + e.[Name] FROM [Entity] e WHERE e.Id IN (SELECT * FROM split_string_XML(u.EntityIds, ','))       
   FOR XML PATH(''), TYPE).value('text()[1]', 'nvarchar(max)') , 1, LEN(','), ''), 'NA') AS [Entitys],      
   ISNULL(STUFF((SELECT ',' + d.[Name] FROM [Department] d WHERE d.Id IN (SELECT * FROM split_string_XML(u.DepartmentIds, ','))       
   FOR XML PATH(''), TYPE).value('text()[1]', 'nvarchar(max)') , 1, LEN(','), ''), 'NA') AS [Departments],      
   ISNULL(STUFF((SELECT ',' + r.[Name] FROM [RoleMenu] r WHERE r.Id IN (SELECT * FROM split_string_XML(u.RoleIds, ','))       
   FOR XML PATH(''), TYPE).value('text()[1]', 'nvarchar(max)') , 1, LEN(','), ''), 'NA') AS [Roles],
   '' AS 'DeptHOD'     
  FROM [UserMapping](nolock) u      
  INNER JOIN [User](nolock) ur ON ur.Id = u.RefUserId      
  WHERE u.IsActive = 1 AND u.Id = @Id
END

IF(@ActionType = 'getUserPWDById')    
BEGIN 
 SELECT UserPwd FROM [User](nolock) u WHERE u.IsActive = 1 AND u.Id = @Id
END

IF(@ActionType = 'updateUserPWDById')    
BEGIN 
 UPDATE [User] SET UserPwd = @UserPwd, ModifiedById = @ModifiedById, ModifyDate = @ModifyDate WHERE Id = @Id
END

END
GO

--==================================CustomerType Master==============================
CREATE  PROC [usp_CustomerType]
@ActionType varchar(50) = '',
@Id int = 0,
@CompanyId int = 0,
@Name varchar(100) = '',
@Sequence int = 0,
@IsActive bit = 0,
@CreatedById int = 0,
@CreateDate datetime =null,
@ModifiedById int =0,
@ModifyDate datetime =null
AS
BEGIN

IF(@ActionType = 'getAll')
BEGIN
	Select C.*, 
	U.[Name] As CreatedBy, U1.[Name] As UpdatedBy, (CASE WHEN C.IsActive = 1 THEN 'Active' ELSE 'In-Active' END) AS ActiveStr,
	FORMAT(C.CreateDate, 'dd/MM/yyyy hh:mm:ss tt') AS CreateDateStr, FORMAT(C.ModifyDate, 'dd/MM/yyyy hh:mm:ss tt') AS ModifyDateStr
	FROM [CustomerType](nolock) C
	LEFT JOIN [User](nolock) U ON C.CreatedById = U.Id
	LEFT JOIN [User](nolock) U1 ON C.ModifiedById = U1.Id
	WHERE C.IsActive = 1 
	ORDER BY C.[Sequence] ASC
END

if(@ActionType = 'getById')
BEGIN
  SELECT * FROM [CustomerType](nolock) where  IsActive = 1 AND [Id] = @Id 
END

IF(@ActionType = 'insert')
BEGIN
	INSERT INTO [CustomerType]
		([CompanyId], [Name], [Sequence], [IsActive], [CreatedById], [CreateDate])
	VALUES
		(@CompanyId, @Name, @Sequence, @IsActive, @CreatedById, @CreateDate)
		
  EXEC [usp_Activity] @ActionType = 'insert', @Module = 'Customer Type', @Action = 'Insert', @Message = 'Record added successfully !', @CreatedById = @CreatedById, @ModifiedById = @CreatedById
END

IF(@ActionType = 'update')
BEGIN
	UPDATE [CustomerType]
	 SET [Name] = @Name, [Sequence] = @Sequence, [IsActive] = @IsActive, [ModifiedById] = @ModifiedById, [ModifyDate] = @ModifyDate
	WHERE Id = @Id
	
   EXEC [usp_Activity] @ActionType = 'insert', @Module = 'Customer Type', @Action = 'Update', @Message = 'Record updated successfully !', @CreatedById = @CreatedById, @ModifiedById = @CreatedById
END

IF(@ActionType = 'delete')
BEGIN
  Update [CustomerType] set [IsActive] = @IsActive, [ModifiedById] = @ModifiedById, [ModifyDate] = @ModifyDate WHERE Id = @Id
  
  EXEC [usp_Activity] @ActionType = 'insert', @Module = 'Customer Type', @Action = 'Delete', @Message = 'Record deleted successfully !', @CreatedById = @CreatedById, @ModifiedById = @CreatedById
END

END
GO

--=========================Admin DashBoard Detail==============================
CREATE PROC [usp_AdminDashBoard]
@ActionType varchar(50) = '',
@Id int = 0
AS
BEGIN

IF(@ActionType = 'getDashBoardDetail')
BEGIN 
SELECT
  (SELECT COUNT(e.[Id]) FROM [Department](nolock) e WHERE e.IsActive = 1) TDepartment,
  (SELECT COUNT(e.[Id]) FROM [Document](nolock) e WHERE e.IsActive = 1) TDocument,
  (SELECT COUNT(e.[Id]) FROM [Entity](nolock) e WHERE e.IsActive = 1) TEntity,
  (SELECT COUNT(e.[Id]) FROM [EntityType](nolock) e WHERE e.IsActive = 1) TEntityType,    
  (SELECT COUNT(e.[Id]) FROM [Agreement](nolock) e WHERE e.IsActive = 1) TAgreement,
  (SELECT COUNT(e.[Id]) FROM [SubAgreement](nolock) e WHERE e.IsActive = 1) TSubAgreement,
  (SELECT COUNT(e.[Id]) FROM [PaymentTerm](nolock) e WHERE e.IsActive = 1) TPaymentTerm,
  (SELECT COUNT(e.[Id]) FROM [User](nolock) e WHERE e.IsActive = 1) TUser

  EXEC [usp_Activity] @ActionType = 'getAll'
END

END
GO

--=========================Company DashBoard Detail==============================
CREATE PROC [usp_CompanyDashBoard]
@ActionType varchar(50) = '',
@Id int = 0
AS
BEGIN

IF(@ActionType = 'getDashBoardDetail')
BEGIN 
DECLARE @CurrYear INT 
SET @CurrYear = YEAR(GETDATE())

SELECT
  (SELECT count(1) FROM [InitiationDetail](nolock) e WHERE e.IsActive = 1 AND ((@Id=2 AND  RequestStatusId in (9,11)) 
	OR ( @Id=3 AND  RequestStatusId in (3,12)) OR (@Id=4 AND  RequestStatusId in (4,12)) OR (@Id=5 AND RequestStatusId in (6,7,8,12))
 )) PendingAction,
  (SELECT dbo.fnRequestedCount(@Id)) Requested,
  (SELECT ISNULL(SUM(CASE WHEN RequestStatusId=6 AND (e.CreatedById = @Id OR e.CreatedById < @Id) THEN 1 ELSE 0 END), 0)
	FROM [InitiationDetail](nolock) e WHERE e.IsActive = 1 and  e.CreatedById in (2,3,4)) Drafts,
  (SELECT ISNULL(SUM(CASE WHEN RequestStatusId=7 AND (e.CreatedById = @Id OR e.CreatedById <= @Id) THEN 1 ELSE 0 END), 0)
	FROM [InitiationDetail](nolock) e WHERE e.IsActive = 1 and  e.CreatedById <= @Id) Negotiation,
  (SELECT ISNULL(SUM(CASE WHEN RequestStatusId=8 AND (e.CreatedById = @Id OR e.CreatedById < @Id) THEN 1 ELSE 0 END), 0)
	FROM [InitiationDetail](nolock) e WHERE e.IsActive = 1 and  e.CreatedById in (2,3,4)) Execution,    
  (SELECT ISNULL(SUM(CASE WHEN RequestStatusId=9 AND (e.CreatedById = @Id OR e.CreatedById < @Id) THEN 1 ELSE 0 END), 0)
	FROM [InitiationDetail](nolock) e WHERE e.IsActive = 1 and  e.CreatedById = @Id) Executed,
  (SELECT ISNULL(SUM(CASE WHEN RequestStatusId=10 AND (e.CreatedById = @Id OR e.CreatedById < @Id) THEN 1 ELSE 0 END), 0)
	FROM [InitiationDetail](nolock) e WHERE e.IsActive = 1 and  e.CreatedById = @Id) SharedWithCustomer

SELECT
  (SELECT COUNT(e.[Id]) FROM [InitiationDetail](nolock) e WHERE e.IsActive = 1 AND e.CreatedById = @Id
    AND [CreateDate] BETWEEN DATEFROMPARTS(@CurrYear, '01', '01') AND DATEFROMPARTS(@CurrYear, '01', '31') ) [JAN],
  (SELECT COUNT(e.[Id]) FROM [InitiationDetail](nolock) e WHERE e.IsActive = 1 AND e.CreatedById = @Id
    AND [CreateDate] BETWEEN DATEFROMPARTS(@CurrYear, '02', '01') AND DATEFROMPARTS(@CurrYear, '02', '28') ) [FEB],
  (SELECT COUNT(e.[Id]) FROM [InitiationDetail](nolock) e WHERE e.IsActive = 1 AND e.CreatedById = @Id
    AND [CreateDate] BETWEEN DATEFROMPARTS(@CurrYear, '03', '01') AND DATEFROMPARTS(@CurrYear, '03', '31') ) [MAR],
  (SELECT COUNT(e.[Id]) FROM [InitiationDetail](nolock) e WHERE e.IsActive = 1 AND e.CreatedById = @Id
    AND [CreateDate] BETWEEN DATEFROMPARTS(@CurrYear, '04', '01') AND DATEFROMPARTS(@CurrYear, '04', '30') ) [APR],
  (SELECT COUNT(e.[Id]) FROM [InitiationDetail](nolock) e WHERE e.IsActive = 1 AND e.CreatedById = @Id
    AND [CreateDate] BETWEEN DATEFROMPARTS(@CurrYear, '05', '01') AND DATEFROMPARTS(@CurrYear, '05', '31') ) [MAY],
  (SELECT COUNT(e.[Id]) FROM [InitiationDetail](nolock) e WHERE e.IsActive = 1 AND e.CreatedById = @Id
    AND [CreateDate] BETWEEN DATEFROMPARTS(@CurrYear, '06', '01') AND DATEFROMPARTS(@CurrYear, '06', '30') ) [JUN],
  (SELECT COUNT(e.[Id]) FROM [InitiationDetail](nolock) e WHERE e.IsActive = 1 AND e.CreatedById = @Id
    AND [CreateDate] BETWEEN DATEFROMPARTS(@CurrYear, '07', '01') AND DATEFROMPARTS(@CurrYear, '07', '31') ) [JUL],
  (SELECT COUNT(e.[Id]) FROM [InitiationDetail](nolock) e WHERE e.IsActive = 1 AND e.CreatedById = @Id
    AND [CreateDate] BETWEEN DATEFROMPARTS(@CurrYear, '08', '01') AND DATEFROMPARTS(@CurrYear, '08', '31') ) [AUG],
  (SELECT COUNT(e.[Id]) FROM [InitiationDetail](nolock) e WHERE e.IsActive = 1 AND e.CreatedById = @Id
    AND [CreateDate] BETWEEN DATEFROMPARTS(@CurrYear, '09', '01') AND DATEFROMPARTS(@CurrYear, '09', '30') ) [SEP],
  (SELECT COUNT(e.[Id]) FROM [InitiationDetail](nolock) e WHERE e.IsActive = 1 AND e.CreatedById = @Id
    AND [CreateDate] BETWEEN DATEFROMPARTS(@CurrYear, '10', '01') AND DATEFROMPARTS(@CurrYear, '10', '31') ) [OCT],
  (SELECT COUNT(e.[Id]) FROM [InitiationDetail](nolock) e WHERE e.IsActive = 1 AND e.CreatedById = @Id
    AND [CreateDate] BETWEEN DATEFROMPARTS(@CurrYear, '11', '01') AND DATEFROMPARTS(@CurrYear, '11', '30') ) [NOV],
  (SELECT COUNT(e.[Id]) FROM [InitiationDetail](nolock) e WHERE e.IsActive = 1 AND e.CreatedById = @Id
    AND [CreateDate] BETWEEN DATEFROMPARTS(@CurrYear, '12', '01') AND DATEFROMPARTS(@CurrYear, '12', '31') ) [DEC]

 SELECT i.Id, i.RefId, i.CustomerName, e.[Name] AS Entity, et.[Name] AS EntityType, c.[Name] AS CustomerType, 
  (CASE WHEN a.[Name] = 'OTHERS' THEN (a.[Name] + ' - ' + i.[AgreementOthers]) ELSE a.[Name] END) AS Agreement, 
  (CASE WHEN pt.[Name] = 'OTHERS' THEN (pt.[Name] + ' - ' + i.[PaymentTermOthers]) ELSE pt.[Name] END) AS PaymentTerm, 
  i.[RequestStatusId], l.[Name] RequestStatus, 
  (SELECT COUNT([Id]) FROM [InitiationAttachment](nolock) ia where ia.IsActive = 1 AND ia.[IsApproved] = 0 AND ia.[InitiationId] = i.[RefId]) AS 'PendingReply'
  FROM [InitiationDetail](nolock) i
  LEFT JOIN [Entity](nolock) e ON e.Id = i.EntityId
  LEFT JOIN [EntityType](nolock) et ON et.Id = i.EntityTypeId
  LEFT JOIN [CustomerType](nolock) c ON c.Id = i.CustomerTypeId
  LEFT JOIN [Agreement](nolock) a ON a.Id = i.AgreementId
  LEFT JOIN [PaymentTerm](nolock) pt ON pt.Id = i.PaymentTermId
  LEFT JOIN [Lookup](nolock) l ON l.Id = i.[RequestStatusId] AND l.[LookTypeId] = 2
  WHERE i.IsActive = 1  AND i.CreatedById = @Id  --AND i.[RequestStatusId] <> 13
  ORder by i.Id desc
END

IF(@ActionType = 'getRequestHistory')
BEGIN 
  SELECT i.[InitiationId] ,l.[Name] AS RequestStatus ,i.[Comment],isnull(i.[Remarks],'') as [Remarks] ,u.[Name] AS CreatedBy ,CONVERT(VARCHAR(12), i.[CreateDate], 106) AS CreateDate
  FROM [InitiationRequestHistory] i
  LEFT JOIN [User](nolock) u ON u.Id = i.CreatedById
  LEFT JOIN [Lookup](nolock) l ON l.Id = i.[RequestStatusId] AND l.[LookTypeId] = 2
  WHERE i.InitiationId = @Id
END

END

GO
--=========================Request Approval==============================
CREATE PROC [usp_RequestApproval]  
@ActionType VARCHAR(50) = '',  
@Id INT = 0,  
@EntityId int = 0,
@DepartmentId INT = 0, 
@RoleTypeId INT = 0, 
@RequestStatusIds VARCHAR(20) = ''  
AS  
BEGIN  
IF(@ActionType = 'getApprovalList')  
BEGIN  
 SELECT i.Id, i.RefId, i.CustomerName, e.[Name] AS Entity, et.[Name] AS EntityType, c.[Name] AS CustomerType,   
  (CASE WHEN a.[Name] = 'OTHERS' THEN (a.[Name] + ' - ' + i.[AgreementOthers]) ELSE a.[Name] END) AS Agreement,   
  (CASE WHEN pt.[Name] = 'OTHERS' THEN (pt.[Name] + ' - ' + i.[PaymentTermOthers]) ELSE pt.[Name] END) AS PaymentTerm,   
  i.[RequestStatusId], l.[Name] RequestStatus, u.[Name] As RequestBy, CONVERT(VARCHAR(10), i.[CreateDate], 105) AS RequestOn  
  FROM [InitiationDetail](nolock) i  
  LEFT JOIN [Entity](nolock) e ON e.Id = i.EntityId  
  LEFT JOIN [EntityType](nolock) et ON et.Id = i.EntityTypeId  
  LEFT JOIN [CustomerType](nolock) c ON c.Id = i.CustomerTypeId  
  LEFT JOIN [Agreement](nolock) a ON a.Id = i.AgreementId  
  LEFT JOIN [PaymentTerm](nolock) pt ON pt.Id = i.PaymentTermId  
  LEFT JOIN [Lookup](nolock) l ON l.Id = i.[RequestStatusId] AND l.[LookTypeId] = 2  
  LEFT JOIN [User](nolock) u ON u.Id = i.CreatedById  
  WHERE i.IsActive = 1 AND i.DepartmentId = @DepartmentId --AND i.EntityId = @EntityId 
  AND i.RequestStatusId IN (SELECT * FROM split_string_XML(@RequestStatusIds, ',')) 

 -- AND ISNULL((SELECT um.[RoleTypeId] FROM [UserMapping] um WHERE um.[IsActive] = 1 AND um.[RefUserId] = i.[ModifiedById]), 0) = @RoleTypeId
END  
  
END
GO

--=========================Legal Approval HOD==============================
CREATE PROC [usp_LegalHeadApproval]
@ActionType varchar(50) = '',
@Id INT = 0,
@RoleTypeId int = 0,
@InitiationId VARCHAR(50) = '',
@TypeOfAgreementId INT = 0,
@CommentAgreementType VARCHAR(250) = '',
@SubCategoryAgreementId INT = 0,
@CommentSubCategoryAgreement VARCHAR(250) = '',
@AssignLegalUserId INT = 0,
@CommentAssignLegalUser VARCHAR(250) = '',
@IsActive bit = 0,
@RequestStatusId int = 0, 
@CreatedById int = 0,
@CreateDate datetime = null,
@ModifiedById int = 0,
@ModifyDate datetime = null
AS
BEGIN

IF(@ActionType = 'insert')
	BEGIN
	IF EXISTS(SELECT 1 FROM [LegalHead] WHERE [IsActive] = 1 AND [InitiationId] = @InitiationId) 
		BEGIN
	      UPDATE [LegalHead]
		  SET [InitiationId] = @InitiationId
			,[TypeOfAgreementId] = @TypeOfAgreementId
			,[CommentAgreementType] = @CommentAgreementType
			,[SubCategoryAgreementId] = @SubCategoryAgreementId
			,[CommentSubCategoryAgreement] = @CommentSubCategoryAgreement
			,[AssignLegalUserId] = @AssignLegalUserId
			,[CommentAssignLegalUser] = @CommentAssignLegalUser			
			,[IsActive] = @IsActive
			,[ModifiedById] = @ModifiedById
			,[ModifyDate] = @ModifyDate
			WHERE [InitiationId] = @InitiationId 
		END
	ELSE 
		BEGIN
		INSERT INTO [LegalHead]
		([InitiationId], [TypeOfAgreementId], [CommentAgreementType], [SubCategoryAgreementId], [CommentSubCategoryAgreement], [AssignLegalUserId]
		 ,[CommentAssignLegalUser], [IsActive], [CreatedById], [CreateDate])
		VALUES (@InitiationId, @TypeOfAgreementId, @CommentAgreementType, @SubCategoryAgreementId, @CommentSubCategoryAgreement,
		@AssignLegalUserId, @CommentAssignLegalUser, @IsActive, @CreatedById, @CreateDate)
		END
	END
	
	IF(@RoleTypeId = 17 AND @RequestStatusId = 6) --FOR Legal HOD
	BEGIN
     INSERT INTO [InitiationRequestHistory] ([InitiationId] ,[RequestStatusId] ,[Comment] ,[CreatedById] ,[CreateDate])    
     VALUES (@InitiationId , @RequestStatusId ,'Request submitted by Legal HOD !', @ModifiedById ,@ModifyDate)    
    END

    EXEC [usp_Activity] @Module = 'Update Request', @Action = 'Update', @Message = 'Record updated successfully !', @CreatedById = @ModifiedById, @ModifiedById = @ModifiedById     
END
GO
--=========================Legal User Approval==============================
CREATE PROC [usp_LegalUserApproval]
@ActionType varchar(50) = '',
@Id INT = 0,
@RoleTypeId int = 0,
@RequestStatusId int = 0, 
@InitiationId VARCHAR(50) = '',
@FileName VARCHAR(250) = '',
@Extension VARCHAR(10) = '',
@Comment VARCHAR(250) = '',
@PendingDocIds VARCHAR(50) = '',
@Remarks VARCHAR(250) = '',
@IsActive bit = 0,
@CreatedById int = 0,
@CreateDate datetime = null,
@ModifiedById int = 0,
@ModifyDate datetime = null
AS
BEGIN
IF(@ActionType = 'insert')
	BEGIN
	IF(LEN(ISNULL(@PendingDocIds,'')) > 0)
	BEGIN
		INSERT INTO [LegalUser]([InitiationId], [FileName], [Extension], [Comment], [PendingDocIds], [Remarks], [IsActive], [CreatedById], [CreateDate])
			VALUES (@InitiationId, @FileName, @Extension, @Comment,@PendingDocIds, @Remarks, @IsActive, @CreatedById, @CreateDate)
	END

	UPDATE [InitiationAttachment] SET [IsApproved] = 1, [ModifiedById] = @ModifiedById, [ModifyDate] = @ModifyDate WHERE [IsActive] = 1 AND [InitiationId] = @InitiationId
	
	IF(@RoleTypeId = 18 AND @RequestStatusId = 7) --FOR Legal HOD NEGOTIATION
	BEGIN
     INSERT INTO [InitiationRequestHistory] ([InitiationId] ,[RequestStatusId] ,[Comment] ,[CreatedById] ,[CreateDate])    
     VALUES (@InitiationId , @RequestStatusId ,'Negotiation start by Legal User !', @ModifiedById ,@ModifyDate)    
    END

	IF(@RoleTypeId = 18 AND @RequestStatusId = 8) --FOR Legal User Execution
	BEGIN
		INSERT INTO [InitiationRequestHistory] ([InitiationId] ,[RequestStatusId] ,[Comment] ,[CreatedById] ,[CreateDate])    
		VALUES (@InitiationId , @RequestStatusId ,'Request submitted by Legal User for Execution !', @ModifiedById ,@ModifyDate)    
	END

	EXEC [usp_Activity] @Module = 'Update Request', @Action = 'Update', @Message = 'Record updated successfully !', @CreatedById = @ModifiedById, @ModifiedById = @ModifiedById     
 END
END

GO
--==================================usp_FieldMapping==============================
CREATE PROC [usp_FieldMapping]
@ActionType varchar(50) = '',
@Id int = 0,
@CompanyId int = 0,
@Name varchar(100) = '',
@IsVisible bit = 0,
@IsRequired bit = 0,
@IsActive bit = 0,
@Sequence int=0,
@CreatedById int = 0,
@CreateDate datetime =null,
@ModifiedById int =0,
@ModifyDate datetime =null
AS
BEGIN

IF(@ActionType = 'getAll')
BEGIN
	Select E.*, 
	U.[Name] As CreatedBy, U1.[Name] As UpdatedBy,
	(CASE WHEN E.IsActive = 1 THEN 'Active' ELSE 'In-Active' END) AS ActiveStr,
	FORMAT(E.CreateDate, 'dd/MM/yyyy hh:mm:ss tt') AS CreateDateStr, FORMAT(E.ModifyDate, 'dd/MM/yyyy hh:mm:ss tt') AS ModifyDateStr
	FROM [FieldMapping](nolock) E
	LEFT JOIN [User](nolock) U ON E.CreatedById = U.Id
	LEFT JOIN [User](nolock) U1 ON E.ModifiedById = U1.Id
	WHERE E.IsActive = 1 
	ORDER BY E.[Sequence] ASC
END

if(@ActionType = 'getById')
BEGIN
  SELECT * FROM [FieldMapping](nolock) where  IsActive = 1 AND [Id] = @Id 
END

IF(@ActionType = 'insert')
BEGIN
	INSERT INTO [FieldMapping]
		([CompanyId], [Name],[IsVisible],[IsRequired], [Sequence], [IsActive], [CreatedById], [CreateDate])
	VALUES
		(@CompanyId, @Name,@IsVisible,@IsRequired, @Sequence, @IsActive, @CreatedById, @CreateDate)

 EXEC [usp_Activity] @ActionType = 'insert', @Module = 'FieldMapping', @Action = 'Insert', @Message = 'Record added successfully !', @CreatedById = @CreatedById, @ModifiedById = @CreatedById 
END

IF(@ActionType = 'update')
BEGIN
	UPDATE [FieldMapping]
	SET [CompanyId] = @CompanyId, [Name] = @Name, [IsVisible]=@IsVisible, [IsRequired]=@IsRequired, [Sequence] = @Sequence, [IsActive] = @IsActive, [ModifiedById] = @ModifiedById, [ModifyDate] = @ModifyDate
	WHERE Id = @Id

 EXEC [usp_Activity] @ActionType = 'insert', @Module = 'FieldMapping', @Action = 'Update', @Message = 'Record updated successfully !', @CreatedById = @CreatedById, @ModifiedById = @CreatedById 
END

IF(@ActionType = 'delete')
BEGIN
  Update [FieldMapping] set [IsActive] = @IsActive, [ModifiedById] = @ModifiedById, [ModifyDate] = @ModifyDate WHERE Id = @Id

  EXEC [usp_Activity] @ActionType = 'insert', @Module = 'FieldMapping', @Action = 'Delete', @Message = 'Record deleted successfully !', @CreatedById = @CreatedById, @ModifiedById = @CreatedById
END

END
GO

--=========================Execution Details==============================
ALTER PROC [usp_ExecutionDetails]
@ActionType varchar(50) = '',  
@Id INT = 0,  
@RoleTypeId int = 0,  
@InitiationId VARCHAR(50) = '',  
@AgreementDate datetime = null,  
@EffectiveDate datetime = null,  
@TermValidityId INT = 0,  
@TermValidityNo INT = 0,  
@RenewalDueDate datetime = null,  
@YearNotice INT = 0, 
@MonthNotice INT = 0, 
@DayNotice INT = 0,  
@CountryId INT = 0,  
@StateId INT = 0,  
@CityId INT = 0, 
@OtherExecutionState VARCHAR(250) = '',
@OtherExecutionCity VARCHAR(250) = '',  
@CommercialApprovedBy VARCHAR(250) = '',  
@LegalApprovedBy VARCHAR(250) = '',  
@ProductIds VARCHAR(100) = '',   
@Comment VARCHAR(250) = '',  
@OtherRemarks VARCHAR(250) = '',  
@AttachmentFileName VARCHAR(250) = '',  
@AttachmentExtension VARCHAR(10) = '',  
@FinalAgreementFileName VARCHAR(250) = '',  
@FinalAgreementExtension VARCHAR(10) = '',  
@IsActive bit = 0,  
@RequestStatusId int = 0,   
@CreatedById int = 0,  
@CreateDate datetime = null,  
@ModifiedById int = 0,  
@ModifyDate datetime = null   
AS  
BEGIN  
IF(@ActionType = 'insert')  
 BEGIN  
 IF EXISTS(SELECT 1 FROM [ExecutionDetails] WHERE [IsActive] = 1 AND [InitiationId] = @InitiationId)   
  BEGIN  
     UPDATE [ExecutionDetails]  
     SET [AgreementDate] = @AgreementDate  
     ,[EffectiveDate] = @EffectiveDate  
     ,[TermValidityId] = @TermValidityId 
	 ,[TermValidityNo]=@TermValidityNo 
     ,[RenewalDueDate] = @RenewalDueDate  
	 ,[YearNotice] = @YearNotice 
	 ,[MonthNotice] = @MonthNotice
     ,[DayNotice] = @DayNotice  
     ,[CountryId] = @CountryId  
     ,[StateId] = @StateId  
     ,[OtherExecutionState] = @OtherExecutionState  
	 ,[CityId] = @CityId
	 ,[OtherExecutionCity] = @OtherExecutionCity
	 ,[CommercialApprovedBy] = @CommercialApprovedBy
	 ,[LegalApprovedBy] = @LegalApprovedBy
     ,[ProductIds] = @ProductIds  
     ,[Comment] = @Comment  
     ,[OtherRemarks] = @OtherRemarks  
     ,[AttachmentFileName] = @AttachmentFileName  
     ,[AttachmentExtension] = @AttachmentExtension  
     ,[FinalAgreementFileName] = @FinalAgreementFileName  
     ,[FinalAgreementExtension] = @FinalAgreementExtension  
     ,[IsActive] = @IsActive       
     ,[ModifiedById] =@ModifiedById  
     ,[ModifyDate] = @ModifyDate  
   WHERE [InitiationId] = @InitiationId   
  END  
 ELSE   
  BEGIN  
  INSERT INTO [ExecutionDetails]([InitiationId],[AgreementDate],[EffectiveDate],[TermValidityId],[TermValidityNo],[RenewalDueDate],[YearNotice],[MonthNotice],[DayNotice]  
    ,[CountryId],[StateId],[OtherExecutionState],[CityId],[OtherExecutionCity],[CommercialApprovedBy],[LegalApprovedBy],[ProductIds],[Comment]
	,[OtherRemarks],[AttachmentFileName],[AttachmentExtension] ,[FinalAgreementFileName],[FinalAgreementExtension],[IsActive],[CreatedById],[CreateDate])  
  VALUES (@InitiationId, @AgreementDate, @EffectiveDate, @TermValidityId,@TermValidityNo, @RenewalDueDate, @YearNotice, @MonthNotice,@DayNotice, @CountryId, @StateId,  
    @OtherExecutionState,@CityId,@OtherExecutionCity, @CommercialApprovedBy, @LegalApprovedBy, @ProductIds, @Comment, @OtherRemarks, 
	@AttachmentFileName, @AttachmentExtension, @FinalAgreementFileName, @FinalAgreementExtension, @IsActive, @CreatedById, @CreateDate)  
  END  
 END  
   
  -- IF(@RoleTypeId = 18 AND @RequestStatusId < 9) --FOR Execution Details    Need to check and confirm with Rupesh
    --IF(@RoleTypeId  in (15,16,17,18) AND @RequestStatusId < 9) --FOR Execution Details  
	IF (@RequestStatusId < 9) --FOR Execution Details 
    BEGIN  
     INSERT INTO [InitiationRequestHistory] ([InitiationId] ,[RequestStatusId] ,[Comment] ,[CreatedById] ,[CreateDate])      
     VALUES (@InitiationId , @RequestStatusId ,'Request submitted for execution.', @ModifiedById ,@ModifyDate)      
    END  

  -- IF(@RoleTypeId = 18 AND @RequestStatusId = 9) --FOR Execution Details  
  --   IF(@RoleTypeId IN (15,16,17,18) AND @RequestStatusId = 9) --FOR Execution Details   Need to check and confirm with Rupesh
   
   IF (@RequestStatusId = 9) --FOR Execution Details   Need to check and confirm with Rupesh
    BEGIN  
	 INSERT INTO [InitiationRequestHistory] ([InitiationId] ,[RequestStatusId] ,[Comment] ,[CreatedById] ,[CreateDate])      
     VALUES (@InitiationId , @RequestStatusId ,'Request Executed.', @ModifiedById ,@ModifyDate)          
    END 
  
    EXEC [usp_Activity] @Module = 'Update Request', @Action = 'Update', @Message = 'The record has been updated successfully.', @CreatedById = @ModifiedById, @ModifiedById = @ModifiedById       
END   
GO

--=========================UserHelpFileMapping==============================
CREATE PROC [usp_UserHelpFileMapping]
@ActionType varchar(50) = '',
@Id int = 0,
@CompanyId int = 0,
@RoleTypeId int = 0,
@FileName varchar(250) = '',
@FileExt varchar(10) = '',
@Sequence int = 0,
@IsActive bit = 0,
@CreatedById int = 0,
@CreateDate datetime = null,
@ModifiedById int =0,
@ModifyDate datetime = null
AS
BEGIN

IF(@ActionType = 'getAll')
BEGIN
  Select E.*, 
	L.[Name] AS RoleType, U.[Name] As CreatedBy, U1.[Name] As UpdatedBy,
	(CASE WHEN E.IsActive = 1 THEN 'Active' ELSE 'In-Active' END) AS ActiveStr,
	FORMAT(E.CreateDate, 'dd/MM/yyyy hh:mm:ss tt') AS CreateDateStr, FORMAT(E.ModifyDate, 'dd/MM/yyyy hh:mm:ss tt') AS ModifyDateStr
	FROM [UserHelpFileMapping] (nolock) E
	LEFT JOIN [User](nolock) U ON E.CreatedById = U.Id
	LEFT JOIN [User](nolock) U1 ON E.ModifiedById = U1.Id
	LEFT JOIN [Lookup](nolock) L ON L.Id = E.RoleTypeId AND [LookTypeId] = 3
	WHERE E.IsActive = 1 
	ORDER BY E.[Sequence] ASC
END

if(@ActionType = 'getById')
BEGIN
  SELECT * FROM [UserHelpFileMapping](nolock) where  IsActive = 1 AND [Id] = @Id 
END

IF(@ActionType = 'insert')
BEGIN
	INSERT INTO [UserHelpFileMapping]
		([CompanyId], [RoleTypeId], [FileName], [FileExt], [Sequence], [IsActive], [CreatedById], [CreateDate])
	VALUES
		(@CompanyId, @RoleTypeId, @FileName, @FileExt, @Sequence, @IsActive, @CreatedById, @CreateDate)

 EXEC [usp_Activity] @ActionType = 'insert', @Module = 'UserHelpFileMapping', @Action = 'Insert', @Message = 'Record added successfully !', @CreatedById = @CreatedById, @ModifiedById = @CreatedById 
END

IF(@ActionType = 'update')
BEGIN
	UPDATE [UserHelpFileMapping]
	 SET [RoleTypeId] = @RoleTypeId, [FileName] = @FileName, [FileExt] = @FileExt, [Sequence] = @Sequence, [IsActive] = @IsActive, [ModifiedById] = @ModifiedById, [ModifyDate] = @ModifyDate
	WHERE Id = @Id
 EXEC [usp_Activity] @ActionType = 'insert', @Module = 'UserHelpFileMapping', @Action = 'Update', @Message = 'Record updated successfully !', @CreatedById = @CreatedById, @ModifiedById = @CreatedById 

END

IF(@ActionType = 'delete')
BEGIN
  Update [UserHelpFileMapping] set [IsActive] = @IsActive, [ModifiedById] = @ModifiedById, [ModifyDate] = @ModifyDate WHERE Id = @Id
  
  EXEC [usp_Activity] @ActionType = 'insert', @Module = 'UserHelpFileMapping', @Action = 'Delete', @Message = 'Record deleted successfully !', @CreatedById = @CreatedById, @ModifiedById = @CreatedById
END

END

GO
--========================= Contract Template ==============================
CREATE PROC [usp_ContractTemplate]
@ActionType varchar(50) = '',
@Id int = 0,
@CompanyId int = 0,
@FileName varchar(250) = '',
@FileExt varchar(10) = '',
@Sequence int = 0,
@IsActive bit = 0,
@CreatedById int = 0,
@CreateDate datetime = null,
@ModifiedById int =0,
@ModifyDate datetime = null
AS
BEGIN
IF(@ActionType = 'getAll')
BEGIN
   Select E.*, 
	U.[Name] As CreatedBy, U1.[Name] As UpdatedBy,
	(CASE WHEN E.IsActive = 1 THEN 'Active' ELSE 'In-Active' END) AS ActiveStr,
	FORMAT(E.CreateDate, 'dd/MM/yyyy hh:mm:ss tt') AS CreateDateStr, FORMAT(E.ModifyDate, 'dd/MM/yyyy hh:mm:ss tt') AS ModifyDateStr
	FROM [ContractTemplate] (nolock) E
	LEFT JOIN [User](nolock) U ON E.CreatedById = U.Id
	LEFT JOIN [User](nolock) U1 ON E.ModifiedById = U1.Id
	WHERE E.IsActive = 1 
   ORDER BY E.[Sequence] ASC
END

if(@ActionType = 'getById')
BEGIN
  SELECT * FROM [ContractTemplate](nolock) WHERE IsActive = 1 AND [Id] = @Id 
END

IF(@ActionType = 'insert')
BEGIN
	INSERT INTO [ContractTemplate]
		([CompanyId], [FileName], [FileExt], [Sequence], [IsActive], [CreatedById], [CreateDate])
	VALUES
		(@CompanyId, @FileName, @FileExt, @Sequence, @IsActive, @CreatedById, @CreateDate)

	EXEC [usp_Activity] @ActionType = 'insert', @Module = 'ContractTemplates', @Action = 'Insert', @Message = 'Record added successfully !', @CreatedById = @CreatedById, @ModifiedById = @CreatedById 
END

IF(@ActionType = 'update')
BEGIN
	UPDATE [ContractTemplate]
	SET [FileName] = @FileName, [FileExt] = @FileExt, [Sequence] = @Sequence, [IsActive] = @IsActive, [ModifiedById] = @ModifiedById, [ModifyDate] = @ModifyDate
	WHERE Id = @Id

	EXEC [usp_Activity] @ActionType = 'insert', @Module = 'ContractTemplates', @Action = 'Update', @Message = 'Record updated successfully !', @CreatedById = @CreatedById, @ModifiedById = @CreatedById 
END

IF(@ActionType = 'delete')
BEGIN
  Update [ContractTemplate] set [IsActive] = @IsActive, [ModifiedById] = @ModifiedById, [ModifyDate] = @ModifyDate WHERE Id = @Id

  EXEC [usp_Activity] @ActionType = 'insert', @Module = 'ContractTemplates', @Action = 'Delete', @Message = 'Record deleted successfully !', @CreatedById = @CreatedById, @ModifiedById = @CreatedById
END
END
GO
--========================= Repository Template ==============================
CREATE  PROC [usp_RepositoryTemplate]
@ActionType varchar(50) = '',
@Id int = 0,
@CompanyId int = 0,
@FileName varchar(250) = '',
@FileExt varchar(10) = '',
@Sequence int = 0,
@IsActive bit = 0,
@CreatedById int = 0,
@CreateDate datetime = null,
@ModifiedById int =0,
@ModifyDate datetime = null
AS
BEGIN
IF(@ActionType = 'getAll')
BEGIN
   Select E.*, 
	U.[Name] As CreatedBy, U1.[Name] As UpdatedBy,
	(CASE WHEN E.IsActive = 1 THEN 'Active' ELSE 'In-Active' END) AS ActiveStr,
	FORMAT(E.CreateDate, 'dd/MM/yyyy hh:mm:ss tt') AS CreateDateStr, FORMAT(E.ModifyDate, 'dd/MM/yyyy hh:mm:ss tt') AS ModifyDateStr
	FROM [RepositoryTemplate] (nolock) E
	LEFT JOIN [User](nolock) U ON E.CreatedById = U.Id
	LEFT JOIN [User](nolock) U1 ON E.ModifiedById = U1.Id
	WHERE E.IsActive = 1 
   ORDER BY E.[Sequence] ASC
END

if(@ActionType = 'getById')
BEGIN
  SELECT * FROM [RepositoryTemplate](nolock) WHERE IsActive = 1 AND [Id] = @Id 
END

IF(@ActionType = 'insert')
BEGIN
	INSERT INTO [RepositoryTemplate]
		([CompanyId], [FileName], [FileExt], [Sequence], [IsActive], [CreatedById], [CreateDate])
	VALUES
		(@CompanyId, @FileName, @FileExt, @Sequence, @IsActive, @CreatedById, @CreateDate)

	EXEC [usp_Activity] @ActionType = 'insert', @Module = 'RepositoryTemplate', @Action = 'Insert', @Message = 'Record added successfully !', @CreatedById = @CreatedById, @ModifiedById = @CreatedById 
END

IF(@ActionType = 'update')
BEGIN
	UPDATE [RepositoryTemplate]
	SET [FileName] = @FileName, [FileExt] = @FileExt, [Sequence] = @Sequence, [IsActive] = @IsActive, [ModifiedById] = @ModifiedById, [ModifyDate] = @ModifyDate
	WHERE Id = @Id

	EXEC [usp_Activity] @ActionType = 'insert', @Module = 'RepositoryTemplate', @Action = 'Update', @Message = 'Record updated successfully !', @CreatedById = @CreatedById, @ModifiedById = @CreatedById 
END

IF(@ActionType = 'delete')
BEGIN
  Update [RepositoryTemplate] set [IsActive] = @IsActive, [ModifiedById] = @ModifiedById, [ModifyDate] = @ModifyDate WHERE Id = @Id

  EXEC [usp_Activity] @ActionType = 'insert', @Module = 'RepositoryTemplate', @Action = 'Delete', @Message = 'Record deleted successfully !', @CreatedById = @CreatedById, @ModifiedById = @CreatedById
END
END
GO


--========================= Email Setup  ==============================
create PROC [dbo].[usp_EmailSetup]
@ActionType varchar(50) = '',
@Id int = 0,
@CompanyId int = 0,
@IPAddress varchar(100) = '',
@EmailId varchar(100) = '',
@EmailPWD varchar(100) = '',
@SMTPPort varchar(100) = '',
@isCurrentActive bit = 0,
@IsActive bit = 0,
@CreatedById int = 0,
@CreateDate datetime =null,
@ModifiedById int =0,
@ModifyDate datetime =null
AS
BEGIN

IF(@ActionType = 'getAll')
BEGIN
 --SELECT * FROM [Entity](nolock) where IsActive = 1 order by [Sequence] asc
	Select E.*, 
	U.[Name] As CreatedBy, U1.[Name] As UpdatedBy, (CASE WHEN E.IsActive = 1 THEN 'Active' ELSE 'In-Active' END) AS ActiveStr,
	FORMAT(E.CreateDate, 'dd/MM/yyyy hh:mm:ss tt') AS CreateDateStr, FORMAT(E.ModifyDate, 'dd/MM/yyyy hh:mm:ss tt') AS ModifyDateStr
	FROM [EmailSetup](nolock) E
	LEFT JOIN [User](nolock) U ON E.CreatedById = U.Id
	LEFT JOIN [User](nolock) U1 ON E.ModifiedById = U1.Id

END

if(@ActionType = 'getById')
BEGIN
  SELECT * FROM [EmailSetup](nolock) where  IsActive = 1 AND [Id] = @Id 
END

IF(@ActionType = 'insert')
BEGIN
	INSERT INTO [EmailSetup]
		([CompanyId], [IPAddress], [EmailId], [EmailPWD], [SMTPPort], [IsActive], [CreatedById], [CreateDate])
	VALUES
		(@CompanyId, @IPAddress, @EmailId, @EmailPWD, @SMTPPort, @IsActive, @CreatedById, @CreateDate)

 EXEC [usp_Activity] @ActionType = 'insert', @Module = 'EmailSetup', @Action = 'Insert', @Message = 'Record added successfully.', @CreatedById = @CreatedById, @ModifiedById = @CreatedById 
END

IF(@ActionType = 'update')
BEGIN
	UPDATE [EmailSetup]
	 SET [IPAddress] = @IPAddress, [EmailId] = @EmailId, [EmailPWD] = @EmailPWD,[SMTPPort] = @SMTPPort, [IsActive] = @IsActive, [ModifiedById] = @ModifiedById, [ModifyDate] = @ModifyDate
	WHERE Id = @Id

 EXEC [usp_Activity] @ActionType = 'insert', @Module = 'EmailSetup', @Action = 'Update', @Message = 'Record updated successfully.', @CreatedById = @CreatedById, @ModifiedById = @CreatedById 
END

IF(@ActionType = 'delete')
BEGIN
  Update [EmailSetup] set [IsActive] = @IsActive, [ModifiedById] = @ModifiedById, [ModifyDate] = @ModifyDate WHERE Id = @Id

  EXEC [usp_Activity] @ActionType = 'insert', @Module = 'EmailSetup', @Action = 'Delete', @Message = 'Record deleted successfully.', @CreatedById = @CreatedById, @ModifiedById = @CreatedById
END
END
GO
--=========================Email Template ==============================
create PROC [dbo].[usp_EmailTemplate]
@ActionType varchar(50) = '',
@Id int = 0,
@CompanyId int = 0,
@MailTypeId int = 0,
@Template nvarchar(MAX) = '',
@IsActive bit = 0,
@CreatedById int = 0,
@CreateDate datetime =null,
@ModifiedById int =0,
@ModifyDate datetime =null
AS
BEGIN

IF(@ActionType = 'getAll')
BEGIN
	Select E.*, 
	U.[Name] As CreatedBy, U1.[Name] As UpdatedBy, (CASE WHEN E.IsActive = 1 THEN 'Active' ELSE 'In-Active' END) AS ActiveStr,
	FORMAT(E.CreateDate, 'dd/MM/yyyy hh:mm:ss tt') AS CreateDateStr, FORMAT(E.ModifyDate, 'dd/MM/yyyy hh:mm:ss tt') AS ModifyDateStr
	FROM [EmailTemplate](nolock) E
	LEFT JOIN [User](nolock) U ON E.CreatedById = U.Id
	LEFT JOIN [User](nolock) U1 ON E.ModifiedById = U1.Id

END

if(@ActionType = 'getById')
BEGIN
  SELECT * FROM [EmailTemplate](nolock) where  IsActive = 1 AND [Id] = @Id 
END

IF(@ActionType = 'insert')
BEGIN
	INSERT INTO [EmailTemplate]
		([CompanyId], [MailTypeId], [Template],[IsActive], [CreatedById], [CreateDate])
	VALUES
		(@CompanyId,@MailTypeId, @Template, @IsActive, @CreatedById, @CreateDate)

 EXEC [usp_Activity] @ActionType = 'insert', @Module = 'EmailTemplate', @Action = 'Insert', @Message = 'Record added successfully.', @CreatedById = @CreatedById, @ModifiedById = @CreatedById 
END

IF(@ActionType = 'update')
BEGIN
	UPDATE [EmailTemplate]
	 SET [MailTypeId] = @MailTypeId, [Template] = @Template, [IsActive] = @IsActive, [ModifiedById] = @ModifiedById, [ModifyDate] = @ModifyDate
	WHERE Id = @Id

 EXEC [usp_Activity] @ActionType = 'insert', @Module = 'EmailTemplate', @Action = 'Update', @Message = 'Record updated successfully.', @CreatedById = @CreatedById, @ModifiedById = @CreatedById 
END

IF(@ActionType = 'delete')
BEGIN
  Update [EmailTemplate] set [IsActive] = @IsActive, [ModifiedById] = @ModifiedById, [ModifyDate] = @ModifyDate WHERE Id = @Id

  EXEC [usp_Activity] @ActionType = 'insert', @Module = 'EmailTemplate', @Action = 'Delete', @Message = 'Record deleted successfully.', @CreatedById = @CreatedById, @ModifiedById = @CreatedById
END

END

