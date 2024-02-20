namespace MargConnect.Core.Util
{
    /// <summary>
    /// The app settings.
    /// </summary>
    public class AppSettings
    {
        public int DefaultSMSCount { get; set; } // Free SMS Values
        /// <summary>
        /// Gets or sets the secret.
        /// </summary>
        public string Secret { get; set; }
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        /// <summary>
        /// Gets or sets the margpayurl.
        /// </summary>
        public string margpayurl { get; set; }
        /// <summary>
        /// Gets or sets the websiteurl.
        /// </summary>
        public string websiteurl { get; set; }
        /// <summary>
        /// Gets or sets the margapibaseurl.
        /// </summary>
        public string margapibaseurl { get; set; }
        /// <summary>
        /// Gets or sets the pharmanxtapibaseurl.
        /// </summary>
        public string pharmanxtapibaseurl { get; set; }
        /// <summary>
        /// Gets or sets the new marg s m s u r l.
        /// </summary>
        public string NewMargSMSURL { get; set; }
        /// <summary>
        /// Gets or sets the new marg support url.
        /// </summary>
        public string NewMargSupportUrl { get; set; }        
        /// <summary>
        /// Gets or sets the marg support.
        /// </summary>
        public string MargSupport { get; set; }
        /// <summary>
        /// Gets or sets the coll commerce a p i.
        /// </summary>
        public string CollCommerceAPI { get; set; }
        /// <summary>
        /// Gets or sets the noreply mail.
        /// </summary>
        public string noreplyMail { get; set; }
        /// <summary>
        /// Gets or sets the partnersupport mail.
        /// </summary>
        public string partnersupportMail { get; set; }
        /// <summary>
        /// Gets or sets the support mail.
        /// </summary>
        public string supportMail { get; set; }
        /// <summary>
        /// Gets or sets the webpages version.
        /// </summary>
        public string webpagesVersion { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether webpages enabled.
        /// </summary>
        public bool webpagesEnabled { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether preserve login url.
        /// </summary>
        public bool PreserveLoginUrl { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether client validation enabled.
        /// </summary>
        public bool ClientValidationEnabled { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether unobtrusive java script enabled.
        /// </summary>
        public bool UnobtrusiveJavaScriptEnabled { get; set; }
        /// <summary>
        /// Gets or sets the margbooks logo.
        /// </summary>
        public string margbooksLogo { get; set; }
        /// <summary>
        /// Gets or sets the demo to main d b backup path.
        /// </summary>
        public string DemoToMainDBBackupPath { get; set; }
        /// <summary>
        /// Gets or sets the application type.
        /// </summary>
        public int ApplicationType { get; set; } //1-Desktop,2-Web,3-mobile
        /// <summary>
        /// Gets or sets the database type.
        /// </summary>
        public int DatabaseType { get; set; } //1-SQL,2-Azure DB
        /// <summary>
        /// Gets or sets the main database pool.
        /// </summary>
        public string MainDatabasePool { get; set; } //Set Pool for azure main DB
        /// <summary>
        /// Gets or sets the max pool size.
        /// </summary>
        public int MaxPoolSize { get; set; } //Set Pool for azure main DB
        /// <summary>
        /// Gets or sets the allowsameuserlogin.
        /// </summary>
        public int allowsameuserlogin { get; set; } //0- Not allow,1 Allow same user login
        /// <summary>
        /// Gets or sets the trial period days.
        /// </summary>
        public int trialPeriodDays { get; set; } //web trial period allowed
        /// <summary>
        /// Gets or sets the skip mobile email o t p.
        /// </summary>
        public int skipMobileEmailOTP { get; set; } //1 For send emailotp 2 for skip
        /// <summary>
        /// Gets or sets the db version.
        /// </summary>
        public double dbVersion { get; set; } //Database version use for update existing database
        public double mobiledbVersion { get; set; } //Database version use for update existing database for mobile only
        /// <summary>
        /// Gets or sets the use queue.
        /// </summary>
        public int useQueue { get; set; } //0 For not to use queue and 1 for use Queue
        /// <summary>
        /// Gets or sets the database setting use.
        /// </summary>
        public int DatabaseSettingUse { get; set; } //1-XML file,2-Config file
        /// <summary>
        /// Gets or sets the is multi schema on.
        /// </summary>
        public int IsMultiSchemaOn { get; set; } //0-Off,1-On
        /// <summary>
        /// Gets or sets the max schema in d b.
        /// </summary>
        public int MaxSchemaInDB { get; set; } //Define maximum schema created in db
        /// <summary>
        /// Gets or sets the is connection pooling on.
        /// </summary>
        public int IsConnectionPoolingOn { get; set; } //0-Off,1-On DB Connection string pooling
        /// <summary>
        /// Gets or sets the datasource.
        /// </summary>
        public string Datasource { get; set; }
        /// <summary>
        /// Gets or sets the user i d.
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Gets or sets the whatsup.
        /// </summary>
        public string Whatsup { get; set; }
        /// <summary>
        /// Gets or sets the w user name.
        /// </summary>
        public string wUserName { get; set; }
        /// <summary>
        /// Gets or sets the w password.
        /// </summary>
        public string wPassword { get; set; }
        /// <summary>
        /// Gets or sets the whatsapp from.
        /// </summary>
        public string whatsappFrom { get; set; }
        /// <summary>
        /// Gets or sets the access key.
        /// </summary>
        public string AccessKey { get; set; }
        /// <summary>
        /// Gets or sets the queue type.
        /// </summary>
        public string QueueType { get; set; }
        /// <summary>
        /// Gets or sets the r b m q host name.
        /// </summary>
        public string RBMQHostName { get; set; }
        /// <summary>
        /// Gets or sets the r b m q port.
        /// </summary>
        public int RBMQPort { get; set; }
        /// <summary>
        /// Gets or sets the r b m q user name.
        /// </summary>
        public string RBMQUserName { get; set; }
        /// <summary>
        /// Gets or sets the r b m q password.
        /// </summary>
        public string RBMQPassword { get; set; }
        /// <summary>
        /// Gets or sets the mobileno.
        /// </summary>
        public string mobileno { get; set; }
        /// <summary>
        /// Gets or sets the service bus connection.
        /// </summary>
        public string ServiceBusConnection { get; set; }
        public string ServiceBusLogConnection { get; set; }
        public string ServiceBusNotificationConnection { get; set; }
        /// <summary>
        /// Gets or sets the provider.
        /// </summary>
        public string Provider { get; set; }
        /// <summary>
        /// Gets or sets the user procedure.
        /// </summary>
        public int UserProcedure { get; set; } //0-Not Use Procedure,1-Use Procedure
        /// <summary>
        /// Gets or sets the storage type.
        /// </summary>
        public string StorageType { get; set; }
        /// <summary>
        /// Gets or sets the storage connection.
        /// </summary>
        public string StorageConnection { get; set; }
        /// <summary>
        /// Gets or sets the storage folder.
        /// </summary>
        public string StorageFolder { get; set; }
        /// <summary>
        /// Gets or sets the setting version.
        /// </summary>
        public double settingVersion { get; set; }
        public double APIVersion { get; set; }
        /// <summary>
        /// Use to check application startup path for offline exe.
        /// </summary>
        public int IsElectron { get; set; }
        /// <summary>
        /// Use to update company profile forcefully if 1 or normal update 0
        /// </summary>
        public int UpdateControlRoomForcefully { get; set; }
    }
}