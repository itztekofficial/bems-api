using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;

namespace Middlewares
{
    /// <summary>
    /// The base controller.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public abstract class BaseController : ControllerBase
    {
        /// <summary>
        /// Gets or sets the CompanyId.
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// Gets or sets the UserId.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the RoleId.
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// Gets or sets the UserTypeId.
        /// </summary>
        public int UserTypeId { get; set; }

        /// <summary>
        /// Gets or sets the UserName.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets the header data.
        /// </summary>
        [NonAction]
        public void GetHeaderData()
        {
            if (Request.Headers != null)
            {
                var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
                if (jwt != null && jwt.Claims.Any())
                {
                    CompanyId = Convert.ToInt32(jwt.Claims.First(c => c.Type == "companyId").Value);
                    UserId = Convert.ToInt32(jwt.Claims.First(c => c.Type == "userId").Value);
                    RoleId = Convert.ToString(jwt.Claims.First(c => c.Type == "roleId").Value);
                    UserTypeId = Convert.ToInt32(jwt.Claims.First(c => c.Type == "userTypeId").Value);
                    UserName = Convert.ToString(jwt.Claims.First(c => c.Type == "userName").Value);
                }

                //if (Request.Headers.TryGetValue("userId", out var values))
                //{
                //    try
                //    {
                //        CompanyId = Convert.ToInt32(values);
                //    }
                //    catch
                //    {
                //        CompanyId = 0;
                //    }
                //}

                //if (Request.Headers.TryGetValue("companyId", out values))
                //{
                //    try
                //    {
                //        UserId = Convert.ToInt32(values);
                //    }
                //    catch
                //    {
                //        UserId = 0;
                //    }
                //}

                //if (Request.Headers.TryGetValue("userName", out values))
                //{
                //    try
                //    {
                //        UserName = values;
                //    }
                //    catch
                //    {
                //        UserName = "";
                //    }
                //}
            }
        }

        public static DataTable CreateDataTable<T>(List<T> dataCollection)
        {
            DataTable dataTable = new(typeof(T).Name);
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                var type = prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType;

                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in dataCollection)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
    }

    /// <summary>
    /// The api action filter.
    /// </summary>
    //public class APIActionFilter : IActionFilter
    //{
    //    private readonly IConnectionProvider _connectionProvider;

    //    public APIActionFilter(IConnectionProvider connectionProvider)
    //    {
    //        _connectionProvider = connectionProvider;
    //    }

    //    public void OnActionExecuted(ActionExecutedContext actionExecutedContext)
    //    {
    //        System.Threading.Tasks.Task.Factory.StartNew(() =>
    //        {
    //            try
    //            {
    //                if (ConfigFile.appSettings.ApplicationType == 2)
    //                {
    //                    if (actionExecutedContext != null)
    //                    {
    //                        string[] data = actionExecutedContext.ActionDescriptor.AttributeRouteInfo.Template.Split("/");
    //                        var moduleName = data[0];
    //                        var action = data[1];

    //                        margerpmodel.ActivityDetailCreatedEvent appLogEvent = new margerpmodel.ActivityDetailCreatedEvent
    //                        {
    //                            linkid = margerputil.UtilFunction.GetLinkID(),
    //                            tenantid = actionExecutedContext != null && actionExecutedContext.HttpContext.Request != null && actionExecutedContext.HttpContext.Request.Headers != null ? Convert.ToInt64(actionExecutedContext.HttpContext.Request.Headers["dbinfolinkid"]) : 0,
    //                            userid = actionExecutedContext != null && actionExecutedContext.HttpContext.Request != null && actionExecutedContext.HttpContext.Request.Headers != null ? Convert.ToInt64(actionExecutedContext.HttpContext.Request.Headers["userlinkid"]) : 0,
    //                            complinkid = actionExecutedContext != null && actionExecutedContext.HttpContext.Request != null && actionExecutedContext.HttpContext.Request.Headers != null ? Convert.ToInt64(actionExecutedContext.HttpContext.Request.Headers["complinkid"]) : 0,
    //                            ip = "",
    //                            macaddress = "",
    //                            applicationtype = ConfigFile.appSettings.ApplicationType,
    //                            // browsertype = 0,
    //                            action = action,
    //                            modulename = moduleName,
    //                            createdon = DateTime.Now
    //                        };
    //                        if (ConfigFile.appSettings.useQueue == 1)
    //                        {
    //                            QueueManager queueManager = new QueueManager();
    //                            var message = Newtonsoft.Json.JsonConvert.SerializeObject(appLogEvent);
    //                            queueManager.SendDataInQueue("marg.activity.detail.created.event", message, EnumBusConnection.Log);
    //                        }
    //                        else
    //                        {
    //                            try
    //                            {
    //                                Exception ex = null;
    //                                var cdata = CommonData.GetCommonData(_connectionProvider.ConnectionString(DatabaseSQLDAL.LogginDatabase), ConnectionInfo.Provider);
    //                                string sql = $"INSERT INTO ActivityDetail (linkid,complinkid,tenantid,userid,ip,macaddress,applicationtype,browsertype,modulename,action,createdon)" +
    //                      $" VALUES(" + appLogEvent.linkid + "," + appLogEvent.complinkid + "," + appLogEvent.tenantid + "," + appLogEvent.userid + ",'" + appLogEvent.ip + "','" + appLogEvent.macaddress + "'," + appLogEvent.applicationtype + "," + appLogEvent.browsertype + ",'" + appLogEvent.modulename + "','" + appLogEvent.action + "','" + appLogEvent.createdon + "')";

    //                                var status = cdata.ExecuteNonQuery(sql, ref ex);
    //                            }
    //                            catch (Exception ex4)
    //                            {
    //                            }
    //                        }
    //                    }
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                // util.Log.WriteLog("BaseController", "OnActionExecuted", ex, 0, 0);
    //            }
    //        });
    //    }

    //    public void OnActionExecuting(ActionExecutingContext context)
    //    {
    //    }
    //}
}