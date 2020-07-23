namespace GameBox.Scheduler.Contracts
{
    public interface IQueueSenderService
    {
        void PostQueueMessage(string queueName, string message);
    }
}