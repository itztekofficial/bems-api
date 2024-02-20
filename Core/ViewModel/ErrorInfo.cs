namespace Core.ViewModel
{
    /// <summary>
    /// The error info.
    /// </summary>
    public class ErrorInfo
    {
        private string _message;
        private string _Exception;
        private string _InnerException;
        private string _StackTrace;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorInfo"/> class.
        /// </summary>
        public ErrorInfo()
        {
            _message = "";
            _Exception = "";
            _InnerException = "";
            _StackTrace = "";
        }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message
        { get { return _message; } set { _message = value; } }//General Message

        /// <summary>
        /// Gets or sets the exception.
        /// </summary>
        public string Exception
        { get { return _Exception; } set { _Exception = value; } }

        /// <summary>
        /// Gets or sets the inner exception.
        /// </summary>
        public string InnerException
        { get { return _InnerException; } set { _InnerException = value; } }

        /// <summary>
        /// Gets or sets the stack trace.
        /// </summary>
        public string StackTrace
        { get { return _StackTrace; } set { _StackTrace = value; } }
    }
}