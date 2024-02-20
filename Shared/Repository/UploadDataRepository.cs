using Microsoft.Extensions.Logging;
using Shared.Repository.Contracts;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Shared.Repository;
public class UploadDataRepository : IUploadDataRepository
{
    private readonly ILogger<UploadDataRepository> _logger;
    public UploadDataRepository(ILogger<UploadDataRepository> logger)
    {
        _logger = logger;
    }

    public void UploadDataJob()
    {
        try
        {
            using var conn = new SqlConnection(_connetionString);
            conn.Open();

            AddTableIfNotExists(conn);

            var dirList = Directory.GetDirectories(_directory);
            foreach (string dir in dirList)
            {
                foreach (string file in Directory.GetFiles(dir))
                {
                    string dirName = Path.GetFileName(dir);
                    UpdateData(conn, dirName, file);
                }
            }

            conn.Close();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ProcessDataToUpload");
        }
    }

    private static void AddTableIfNotExists(SqlConnection conn)
    {
        using SqlCommand cmd = new(_createTableQuery, conn);
        cmd.ExecuteNonQuery();
    }

    private static void UpdateData(SqlConnection conn, string dirName, string fname)
    {
        try
        {
            string fileName = Path.GetFileName(fname);
            string fileExtension = Path.GetExtension(fname);
            var fileBytes = File.ReadAllBytes(fname);

            using SqlCommand cmd = new(_insertQuery, conn);
            SqlParameter InitiationId = new()
            {
                ParameterName = "@InitiationId",
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input
            };

            SqlParameter FileName = new()
            {
                ParameterName = "@FileName",
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input
            };

            SqlParameter FileData = new()
            {
                ParameterName = "@FileData",
                SqlDbType = SqlDbType.VarBinary,
                Direction = ParameterDirection.Input
            };

            SqlParameter FileExtension = new()
            {
                ParameterName = "@FileExtension",
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input
            };

            SqlParameter CreateDate = new()
            {
                ParameterName = "@CreateDate",
                SqlDbType = SqlDbType.DateTime,
                Direction = ParameterDirection.Input
            };

            cmd.Parameters.Add(InitiationId);
            cmd.Parameters.Add(FileName);
            cmd.Parameters.Add(FileData);
            cmd.Parameters.Add(FileExtension);
            cmd.Parameters.Add(CreateDate);

            InitiationId.Value = dirName;
            FileName.Value = fileName;
            FileData.Value = fileBytes;
            FileExtension.Value = fileExtension;
            CreateDate.Value = DateTime.UtcNow;

            cmd.ExecuteNonQuery();
        }
        catch
        {
            throw;
        }
    }

    #region "Constant Data"
    private const string _connetionString = @"Data Source=LAPTOP-18TU6B2R; Initial Catalog=AKUMSDB1; User ID=sa; PWD=123456; Integrated Security=True; MultipleActiveResultSets=True";

    private const string _directory = @"D:\ItzTek-Projects\akumsApi\Presenters\Company.Api\Resources\Requests";

    private const string _createTableQuery = @"IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[FileLog]') AND type in (N'U'))
                BEGIN
                    CREATE TABLE [FileLog](
                     [Id] [bigint] IDENTITY(1,1) NOT NULL,
                     [InitiationId] [nvarchar](50) NULL,
                     [FileName] [nvarchar](250) NULL,
                     [FileData] [varbinary](max) NULL,
                     [Extension] [nvarchar](10) NULL,
                     [CreateDate] [datetime] NULL,
                        CONSTRAINT [PK_FileLog] PRIMARY KEY CLUSTERED 
                    (
                     [Id] ASC
                    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
                    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
                END";

    private const string _insertQuery =
      "IF NOT EXISTS(SELECT Id FROM [FileLog] WHERE [InitiationId] = @InitiationId AND [FileName] = @FileName)" +
      " BEGIN" +
      "  INSERT INTO [FileLog]([InitiationId],[FileName],[FileData],[Extension],[CreateDate]) " +
      "  VALUES (@InitiationId, @FileName, @FileData, @FileExtension, @CreateDate)" +
      " END";
    #endregion
}
