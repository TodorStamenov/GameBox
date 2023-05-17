using System.Text.Json;
using System.Text.Json.Serialization;

namespace Message.DataAccess.Models;

public class Message
{
    public string serializedData;

    private Message() { }

    public Message(string queueName, object data)
    {
        this.QueueName = queueName;
        this.Data = data;
    }

    public Guid Id { get; set; }

    public string QueueName { get; private set; }

    public Type Type { get; private set; }

    public bool Published { get; private set; }

    public void MarkAsPublished()
    {
        this.Published = true;
    }

    public object Data
    {
        get
        {
            return JsonSerializer.Deserialize(
                this.serializedData,
                this.Type,
                new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                });
        }

        set
        {
            this.Type = value.GetType();

            this.serializedData = JsonSerializer.Serialize(
                value,
                new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                });
        }
    }
}
