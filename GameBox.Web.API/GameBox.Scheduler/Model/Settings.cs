namespace GameBox.Scheduler.Model
{
    public class Settings
    {
        public string ConnectionString { get; set; }

        public string RabbitMQHost { get; set; }

        public int RabbitMQPort { get; set; }

        public string RabbitMQUsername { get; set; }

        public string RabbitMQPassword { get; set; }
    }
}