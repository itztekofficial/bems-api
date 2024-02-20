using Microsoft.Extensions.Logging;
using Shared.Repository.Contracts;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Shared.Repository;
public class DownloadDataRepository : IDownloadDataRepository
{
    private readonly ILogger<DownloadDataRepository> _logger;
    public DownloadDataRepository(ILogger<DownloadDataRepository> logger)
    {
        _logger = logger;
    }

    public void DownloadDataJob()
    {
        try
        {
            var ds = new DataSet();
            using var conn = new SqlConnection(_connetionString);
            conn.Open();

            using SqlCommand cmd = new(_selectQuery, conn);
            SqlDataAdapter da = new(cmd);
            da.Fill(ds);

            if (ds != null && ds.Tables.Count > 0)
            {
                var dt = ds.Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    string initiationId = Convert.ToString(row["InitiationId"]);
                    string fileName = Convert.ToString(row["FileName"]);
                    byte[] fileData = (byte[])row["FileData"];

                    var pathToSave = Path.Combine(_downloadDirectory, initiationId);
                    if (!Directory.Exists(pathToSave))
                        Directory.CreateDirectory(pathToSave);

                    var fullPath = Path.Combine(pathToSave, fileName);
                    File.WriteAllBytes(fullPath, fileData);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ProcessDataToUpload");
        }
    }

    #region "Constant Data"
    private const string _connetionString = @"Data Source=LAPTOP-18TU6B2R; Initial Catalog=AKUMSDB1; User ID=sa; PWD=123456; Integrated Security=True; MultipleActiveResultSets=True";

    private const string _downloadDirectory = @"D:\ItzTek-Projects\choptaweb";
    private const string _selectQuery = "Select [InitiationId], [FileName], [FileData] From [FileLog] ORDER BY [InitiationId] ASC";
    #endregion
}
