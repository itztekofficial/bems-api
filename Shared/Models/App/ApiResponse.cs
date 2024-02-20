namespace Shared.Models.App
{
    using System;

    /// <summary>
    /// Defines the <see cref="ApiResponse" />.
    /// </summary>
    public class ApiResponse
    {
        /// <summary>
        /// Gets or sets the StatusCode.
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the ErrorCode.
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// Gets or sets the Message.
        /// </summary>
        public string Message { get; set; }

        public string InnerException { get; set; }
        public string StackTrace { get; set; }

        /// <summary>
        /// Gets or sets the Data.
        /// </summary>
        public Object Data { get; set; }

        public long TotalRows { get; set; } = 0;
    }

    public class ResultStatus1
    {
        public string status { get; set; }
        public Error1 error { get; set; }
        public string info { get; set; }
        public string errcode { get; set; }
    }

    public class Error1
    {
        public string message { get; set; }
        public int errorCodes { get; set; }
        public string Exception { get; set; }
        public string InnerException { get; set; }
        public string StackTrace { get; set; }
    }
}