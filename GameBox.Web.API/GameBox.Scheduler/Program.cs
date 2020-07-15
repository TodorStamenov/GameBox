using Dapper;
using Hangfire;
using RabbitMQ.Client;
using System;
using System.Data.SqlClient;
using System.Text;

namespace GameBox.Scheduler
{
    public class Program
    {
        public static void Main()
        {
            var connectionString = "Server=.;Database=GameBoxCore;Integrated Security=True;Trusted_Connection=True;MultipleActiveResultSets=true";

            GlobalConfiguration.Configuration.UseSqlServerStorage(connectionString);

            using (var server = new BackgroundJobServer())
            {
                RecurringJob.AddOrUpdate(
                    nameof(Program),
                    () => ProcessMessages(connectionString),
                    Cron.Minutely);

                Console.ReadKey();
            }
        }

        public static void ProcessMessages(string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var messages = connection.Query<Message>("SELECT Id, QueueName, SerializedData [serializedData] FROM Messages WHERE Published = 0");

                foreach (var message in messages)
                {
                    Send(message.QueueName, message.SerializedData);
                    connection.Execute("UPDATE Messages SET Published = 1 WHERE Id = @MessageId", new { MessageId = message.Id });
                }
            }
        }

        private static void Send(string queueName, string message)
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = "172.17.0.1",
                UserName = "guest",
                Password = "guest",
                Port = 5672,
                RequestedConnectionTimeout = TimeSpan.FromMilliseconds(3000)
            };

            using (var rabbitConnection = connectionFactory.CreateConnection())
            {
                using (var channel = rabbitConnection.CreateModel())
                {
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.QueueDeclare(
                        queue: queueName,
                        durable: false,
                        exclusive: false,
                        autoDelete: true,
                        arguments: null);

                    channel.BasicPublish(
                        exchange: string.Empty,
                        routingKey: queueName,
                        basicProperties: null,
                        body: body);
                }
            }
        }
    }

    public class Message
    {
        public Guid Id { get; set; }

        public string QueueName { get; set; }

        public string SerializedData { get; set; }
    }
}
