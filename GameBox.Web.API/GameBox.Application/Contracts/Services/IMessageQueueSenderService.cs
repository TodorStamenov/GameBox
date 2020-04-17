namespace GameBox.Application.Contracts.Services
{
    public interface IMessageQueueSenderService : ITransientService
    {
        void Send<T>(string queueName, T command) where T : class;
    }
}
