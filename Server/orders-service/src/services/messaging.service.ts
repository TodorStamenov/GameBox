import { Injectable } from '@nestjs/common';
import { RabbitSubscribe } from '@golevelup/nestjs-rabbitmq';
import { InjectModel } from '@nestjs/mongoose';
import { Model } from 'mongoose';

import { OrderMessageModel } from 'src/models/orderMessage.model';
import { Order, OrderDocument } from 'src/entities/order.schema';

@Injectable()
export class MessagingService {
  constructor(
    @InjectModel(Order.name) private orderModel: Model<OrderDocument>,
  ) {}

  @RabbitSubscribe({
    exchange: 'amq.direct',
    routingKey: '',
    queue: 'orders',
    queueOptions: {
      durable: false,
      exclusive: false,
      autoDelete: true,
    },
  })
  public async pubSubHandler(order: OrderMessageModel) {
    await this.orderModel.create(order);
  }
}
