using System;

namespace Core.Models.QueueModel
{
    /// <summary>
    /// The application login log created event.
    /// </summary>
    public class ApplicationLoginLogModel
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        //public long Id { get; set; }

        ///// <summary>
        ///// Gets or sets the linkid.
        ///// </summary>
        //public long Linkid { get; set; }

        ///// <summary>
        ///// Gets or sets the tenant id.
        ///// </summary>
        //public long TenantId { get; set; }

        /// <summary>
        /// Gets or sets the complink id.
        /// </summary>
        public string CompId { get; set; }

        /// <summary>
        /// Gets or sets the user link id.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the comp name.
        /// </summary>
        public string CompName { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the ip.
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// Gets or sets the device.
        /// </summary>
        public string Device { get; set; }

        /// <summary>
        /// Gets or sets the login time.
        /// </summary>
        public DateTime? LoginTime { get; set; }

        /// <summary>
        /// Gets or sets the logout time.
        /// </summary>
        public DateTime? LogoutTime { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        public string Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        public string Longitude { get; set; }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        public byte Source { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is update.
        /// </summary>
        public bool IsUpdate { get; set; }

        /// <summary>
        ///  Gets or sets a IsLoggedIn.
        /// </summary>
        public bool IsLoggedIn { get; set; }
    }
}