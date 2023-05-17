using GameBox.Application.Model;

namespace GameBox.Application.Contracts.Services;

public interface IQueueSenderService : ITransientService
{
    void PostQueueMessage<T>(string queueName, T command) where T : QueueMessageModel;
}
