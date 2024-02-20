using Core.Models.QueueModel;
using System;

namespace Core.Util
{
    /// <summary>
    /// The log.
    /// </summary>
    public static class Log
    {
        //private static readonly object objLock = new object();

        /// <summary>
        /// WriteErrorLog
        /// </summary>
        /// <param name="ClassName"></param>
        /// <param name="FuncName"></param>
        /// <param name="ExpMsg"></param>
        /// <param name="InrExpMsg"></param>
        /// <param name="stackTrace"></param>
        /// <param name="compId"></param>
        public static void WriteErrorLog(string ClassName, string FuncName, string ExpMsg, string InrExpMsg, string stackTrace = "", string compId = "")
        {
            InrExpMsg ??= "";
            stackTrace ??= "";

            Serilog.Log.Error("Exception-" + ExpMsg + "|InrException-" + InrExpMsg + "|Stack Trace- " + stackTrace + "|Class-" + ClassName + "|Function-" + FuncName + Environment.NewLine + "|CompId-" + compId + Environment.NewLine);
        }

        /// <summary>
        /// WriteLog
        /// </summary>
        /// <param name="ClassName"></param>
        /// <param name="FuncName"></param>
        /// <param name="msg"></param>
        /// <param name="compid"></param>
        /// <param name="userid"></param>
        public static void WriteLog(string ClassName, string FuncName, string msg, string compid = "", string userid = "")
        {
            //ApplicationErrorModel appLogEvent = new()
            //{
            //    //linkid = UtilFunction.GetLinkID(),
            //    //tenantid = dblinkid,
            //    userid = userid,
            //    compid = compid,
            //    ip = "",
            //    macaddress = "",
            //    //applicationtype = "",
            //    browsertype = "",
            //    inner = msg,
            //    stacktrace = "",
            //    modulename = ClassName,
            //    functionname = FuncName,
            //    createdon = DateTime.Now
            //};
            //QueueManager queueManager = new();
            //var message = Newtonsoft.Json.JsonConvert.SerializeObject(appLogEvent);
            //queueManager.SendDataInQueue("application.error.created.event", message, EnumBusConnection.Log);
            WriteErrorLog(ClassName, FuncName, msg, "", "", compid);
        }
    }
}