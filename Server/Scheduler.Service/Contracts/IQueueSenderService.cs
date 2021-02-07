namespace Scheduler.Service.Contracts
{
    public interface IQueueSenderService
    {
        void PostQueueMessage(string queueName, string message);
    }
}