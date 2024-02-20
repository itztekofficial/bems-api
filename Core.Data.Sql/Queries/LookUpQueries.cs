namespace Admin.Data.Sql.Queries;

public static class LookUpQueries
{
    public static string AllLookUp => "SELECT * FROM [LookUp] (NOLOCK)";

    public static string LookUpById => "SELECT * FROM [LookUp] (NOLOCK) WHERE [Id] = @Id";

    public static string AddLookUp =>
        @"INSERT INTO [LookUp] ([LooktypeId] ,[Name] ,[Description] ,[Value] ,[Sequence] ,[CreatedById] ,[CreateDate] ,[ModifiedById] ,[ModifyDate])
            VALUES (@LooktypeId, @Name, @Description, @Value, @Sequence, @CreatedById, @CreateDate, @ModifiedById, @ModifyDate)";

    public static string UpdateLookUp =>
        @"UPDATE [LookUp]
        SET [LooktypeId] = @LooktypeId,
            [Name] = @Name,
            [Description] = @Description,
            [Value] = @Value
        WHERE [Id] = @Id";

    public static string DeleteLookUp => "DELETE FROM [LookUp] WHERE [Id] = @Id";
}