import { Injectable } from '@nestjs/common';
import { RabbitSubscribe } from '@golevelup/nestjs-rabbitmq';

import { OrderService } from './order.service';
import { OrderMessageModel } from 'src/models/orderMessage.model';

@Injectable()
export class MessagingService {
  constructor(private readonly orderService: OrderService) { }

  @RabbitSubscribe({
    exchange: 'amq.direct',
    routingKey: '',
    queue: 'orders',
    queueOptions: {
      durable: false,
      exclusive: false,
      autoDelete: true
    }
  })
  public async pubSubHandler(message: OrderMessageModel) {
    await this.orderService.addOrderAsync(message);
  }
}