namespace GameBox.Application.Contracts.Services
{
    public interface IMessageQueueSenderService
    {
        void Send<T>(string queueName, T command) where T : class;
    }
}
