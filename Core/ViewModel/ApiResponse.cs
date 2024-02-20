namespace Core.ViewModel
{
    /// <summary>
    /// Api Response
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResponse<T>
    {
        private string _httpstatuscode;
        private string _errcode;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiResponse"/> class.
        /// </summary>
        public ApiResponse()
        {
            _httpstatuscode = "";
            _errcode = "";
            Error = new ErrorInfo();
        }

        /// <summary>
        /// Gets or sets the Message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the ErrorLavel.
        /// </summary>
        public ErrorLabel ErrorLavel { get; set; } //10-Info,20-Warning,30-Error

        /// <summary>
        /// Gets or sets the Status.
        /// </summary>
        public EnumStatus Status { get; set; } //1-Success,0-Error,-1-datavalidation Error,-2 Duplicate record

        /// <summary>
        /// Gets or sets the HttpStatusCode.
        /// </summary>
        public string HttpStatusCode
        { get { return _httpstatuscode; } set { _httpstatuscode = value; } }

        /// <summary>
        /// Gets or sets the Errcode.
        /// </summary>
        public string Errcode
        { get { return _errcode; } set { _errcode = value; } }

        /// <summary>
        /// Gets or sets the Error.
        /// </summary>
        public ErrorInfo Error { get; set; }

        /// <summary>
        /// Gets or sets the Data.
        /// </summary>
        public T Data { get; set; }
    }

    public enum EnumStatus
    {
        Error = 0,
        Success = 1,
        DataValidationError = 2,
        Duplicate = 3
    }

    public enum ErrorLabel
    {
        Info = 10,
        Warning = 20,
        Error = 30
    }
}