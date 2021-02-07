using User.Services.Contracts.ServiceTypes;
using User.Services.Messages;

namespace User.Services.Contracts
{
    public interface IQueueSenderService : IScopedService
    {
        void PostQueueMessage(string queueName, string command);

        void PostQueueMessage<T>(string queueName, T command) where T : QueueMessageModel;
    }
}
