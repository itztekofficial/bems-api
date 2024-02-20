using System;

namespace Core.Models.QueueModel
{
    /// <summary>
    /// The application error created event.
    /// </summary>
    public class ApplicationErrorModel
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        //public long Id { get; set; }

        ///// <summary>
        ///// Gets or sets the linkid.
        ///// </summary>
        //public long linkid { get; set; }

        ///// <summary>
        ///// Gets or sets the tenantid.
        ///// </summary>
        //public long tenantid { get; set; }

        /// <summary>
        /// Gets or sets the userid.
        /// </summary>
        public string userid { get; set; }

        /// <summary>
        /// Gets or sets the ip.
        /// </summary>
        public string ip { get; set; }

        /// <summary>
        /// Gets or sets the macaddress.
        /// </summary>
        public string macaddress { get; set; }

        ///// <summary>
        ///// Gets or sets the applicationtype.
        ///// </summary>
        //public string applicationtype { get; set; }

        /// <summary>
        /// Gets or sets the browsertype.
        /// </summary>
        public string browsertype { get; set; }

        /// <summary>
        /// Gets or sets the complinkid.
        /// </summary>
        public string compid { get; set; }

        /// <summary>
        /// Gets or sets the inner.
        /// </summary>
        public string inner { get; set; }

        /// <summary>
        /// Gets or sets the stacktrace.
        /// </summary>
        public string stacktrace { get; set; }

        /// <summary>
        /// Gets or sets the modulename.
        /// </summary>
        public string modulename { get; set; }

        /// <summary>
        /// Gets or sets the functionname.
        /// </summary>
        public string functionname { get; set; }

        /// <summary>
        /// Gets the data.
        /// </summary>
        public string Data { get; private set; }

        /// <summary>
        /// Gets or sets the createdon.
        /// </summary>
        public DateTime? createdon { get; set; }
    }
}