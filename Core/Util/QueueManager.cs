using Core.Enums;
using Microsoft.Azure.ServiceBus;
using System;
using System.Linq;
using System.Text;
using ServiceMessage = Microsoft.Azure.ServiceBus.Message;

namespace Core.Util
{
    /// <summary>
    /// The queue manager.
    /// </summary>
    public class QueueManager
    {
        private readonly QueueSettings _queueSettings;
        private static IQueueClient _queueClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueueManager"/> class.
        /// </summary>
        public QueueManager()
        {
            _queueSettings = new QueueSettings
            {
                QueueType = (QueueType)Enum.Parse(typeof(QueueType), ConfigFile.busSettings.QueueType.ToString(), true),
                ServiceBusConnection = ConfigFile.busSettings.ServiceBusConnection.ToString(),
                ServiceBusLogConnection = ConfigFile.busSettings.ServiceBusLogConnection.ToString(),
                ServiceBusNotificationConnection = ConfigFile.busSettings.ServiceBusNotificationConnection.ToString()
            };
        }

        /// <summary>
        /// Sends the data in queue.
        /// </summary>
        /// <param name="queueName">The queue name.</param>
        /// <param name="message">The message.</param>
        public void SendDataInQueue(string queueName, string message, EnumBusConnection busConnection)
        {
            try
            {
                if (ConfigFile.appSettings.UseQueue == EnmUseQueue.Yes && _queueSettings.QueueType == QueueType.ServiceBus)
                {
                    if (busConnection == EnumBusConnection.Log)
                    {
                        _queueClient = new QueueClient(_queueSettings.ServiceBusLogConnection, queueName);
                    }
                    else if (busConnection == EnumBusConnection.Notification)
                    {
                        _queueClient = new QueueClient(_queueSettings.ServiceBusNotificationConnection, queueName);
                    }
                    else
                    {
                        _queueClient = new QueueClient(_queueSettings.ServiceBusConnection, queueName);
                    }
                    var body = Encoding.UTF8.GetBytes(message);
                    var serviceMessage = new ServiceMessage(body);
                    var eventName = queueName.Replace("", "");
                    serviceMessage.PartitionKey = ReverseMap(eventName);
                    _queueClient.SendAsync(serviceMessage);
                }
            }
            catch (Exception)
            {
                //Log.Error(ex, ex.Message);
                throw new ArgumentNullException("SomethingWentWrong");
            }
        }

        /// <summary>
        /// Reverses the map.
        /// </summary>
        /// <param name="queueName">The queue name.</param>
        /// <returns>A string.</returns>
        private static string ReverseMap(string queueName)
        {
            string eventName = string.Empty;
            var arrParts = queueName.Split('.').ToList();

            arrParts.ForEach(p =>
            {
                eventName += p.Substring(0, 1).ToUpper() + p.Substring(1, p.Length - 1) + ".";
            });

            eventName = eventName.Replace(".", "");

            return eventName;
        }
    }

    /// <summary>
    /// The queue settings.
    /// </summary>
    public class QueueSettings
    {
        /// <summary>
        /// Gets or sets the queue type.
        /// </summary>
        public QueueType QueueType { get; set; }

        /// <summary>
        /// Gets or sets the queue path.
        /// </summary>
        public string QueuePath { get; set; }

        /// <summary>
        /// Gets or sets the service bus connection.
        /// </summary>
        public string ServiceBusConnection { get; set; }

        public string ServiceBusLogConnection { get; set; }

        public string ServiceBusNotificationConnection { get; set; }

        /// <summary>
        /// Gets or sets the queue user.
        /// </summary>
        public string QueueUser { get; set; }

        /// <summary>
        /// Gets or sets the queue pwd.
        /// </summary>
        public string QueuePwd { get; set; }

        /// <summary>
        /// Gets or sets the queue port.
        /// </summary>
        public int QueuePort { get; set; }
    }

    /// <summary>
    /// The queue type.
    /// </summary>
    public enum QueueType
    {
        ServiceBus
    }

    /// <summary>
    /// The message type.
    /// </summary>
    public enum MessageType
    {
        Email,
        Sms,
        Log
    }

    public enum EnumBusConnection
    {
        General = 0,
        Log = 1,
        Notification = 2
    }
}