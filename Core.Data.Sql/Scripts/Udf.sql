
CREATE FUNCTION [Split] (
      @InputString VARCHAR(8000),
      @Delimiter   VARCHAR(50)
)

RETURNS @Items TABLE (
      Item VARCHAR(8000)
)

AS
BEGIN
      IF @Delimiter = ' '
      BEGIN
            SET @Delimiter = ','
            SET @InputString = REPLACE(@InputString, ' ', @Delimiter)
      END

      IF (@Delimiter IS NULL OR @Delimiter = '')
            SET @Delimiter = ','

        --INSERT INTO @Items VALUES (@Delimiter) -- Diagnostic
        --INSERT INTO @Items VALUES (@InputString) -- Diagnostic

      DECLARE @Item VARCHAR(8000)
      DECLARE @ItemList VARCHAR(8000)
      DECLARE @DelimIndex INT

      SET @ItemList = @InputString
      SET @DelimIndex = CHARINDEX(@Delimiter, @ItemList, 0)
      WHILE (@DelimIndex != 0)
      BEGIN
            SET @Item = SUBSTRING(@ItemList, 0, @DelimIndex)
            INSERT INTO @Items VALUES (@Item)

            -- Set @ItemList = @ItemList minus one less item
            SET @ItemList = SUBSTRING(@ItemList, @DelimIndex+1, LEN(@ItemList)-@DelimIndex)
            SET @DelimIndex = CHARINDEX(@Delimiter, @ItemList, 0)
      END -- End WHILE

      IF @Item IS NOT NULL -- At least one delimiter was encountered in @InputString
      BEGIN
            SET @Item = @ItemList
            INSERT INTO @Items VALUES (@Item)
      END

      -- No delimiters were encountered in @InputString, so just return @InputString
      ELSE INSERT INTO @Items VALUES (@InputString)

      RETURN

END -- End Function
GO

CREATE FUNCTION [STRING_SPLIT] (  
    @list varchar(1000), 
	@delim varchar(1) = ','
)
RETURNS TABLE 
AS
RETURN (
    SELECT
        x.f.value( '.', 'varchar(50)' ) AS [value]
    FROM ( 
        SELECT CAST ( '<v><i>' + REPLACE ( @list, @delim, '</i><i>' ) + '</i></v>' AS xml ) AS x 
    ) AS d
    CROSS APPLY x.nodes( '//v/i' ) x( f )
)
GO

----For Performance use xml extraction instead loop
CREATE FUNCTION [split_string_XML]
(
    @in_string VARCHAR(MAX),
    @delimeter VARCHAR(1)
)
RETURNS @list TABLE(items VARCHAR(100))
AS
BEGIN
   DECLARE @sql_xml XML = Cast('<root><U>'+ Replace(@in_string, @delimeter, '</U><U>')+ '</U></root>' AS XML)
    
    INSERT INTO @list(items)
    SELECT f.x.value('.', 'VARCHAR(100)') AS items
    FROM @sql_xml.nodes('/root/U') f(x)
    WHERE f.x.value('.', 'BIGINT') <> 0
    
    RETURN 
END
GO

----For Requested count in client dashboard user in SP
CREATE FUNCTION [fnRequestedCount] (@UserId int)
RETURNS int
AS 
BEGIN 
    DECLARE @count int
	 IF @UserId=2
	BEGIN
	 select @count=count(1)  from [InitiationDetail] where CreatedById=2 AND RequestStatusId=3
	END
	ELSE IF @UserId=3
	BEGIN
	 select @count=count(1)  from [InitiationDetail] where CreatedById in (2,3) AND RequestStatusId in (3,4)
	END
	ELSE IF @UserId=4
	BEGIN
	 select @count=count(1) from [InitiationDetail] where CreatedById in (2,3,4) AND RequestStatusId in (3,4,5)
	END
    RETURN @count
END
