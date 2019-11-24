using System.Threading;
using System.Threading.Tasks;
using GameBox.Application.Contracts.Services;
using MediatR;

namespace GameBox.Application.Orders.Commands.CreateOrder
{
    public class OrderCreatedHandler : INotificationHandler<OrderCreated>
    {
        private readonly IMessageQueueSenderService sender;

        public OrderCreatedHandler(IMessageQueueSenderService sender)
        {
            this.sender = sender;
        }

        public async Task Handle(OrderCreated notification, CancellationToken cancellationToken)
        {
            this.sender.Send("orders", notification);
        }
    }
}
