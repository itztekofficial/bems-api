using System.Text.RegularExpressions;

namespace MargConnect.Core.Util
{
    /// <summary>
    /// The logger.
    /// </summary>
    public static class Logger
    {
        /// <summary>
        /// Creates the log.
        /// </summary>
        /// <param name="eventObject">The event object.</param>
        public static void CreateLog<T>(T eventObject)
        {
            string QueueName = "marg." + MakeQueueName(eventObject.GetType().Name).Replace(" ", ".").ToLower();
            var message = Newtonsoft.Json.JsonConvert.SerializeObject(eventObject);
            QueueManager queueManager = new QueueManager();
            queueManager.SendDataInQueue(QueueName, message, EnumBusConnection.Log);
        }

        /// <summary>
        /// Makes the queue name.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A string.</returns>
        private static string MakeQueueName(string value)
        {
            return Regex.Replace(value, "([a-z])([A-Z])", "$1 $2");
        }
    }
}