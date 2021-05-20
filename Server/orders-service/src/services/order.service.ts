import { Injectable } from '@nestjs/common';
import { InjectModel } from '@nestjs/mongoose';
import { Model } from 'mongoose';

import { Order, OrderDocument } from 'src/entities/order.schema';

@Injectable()
export class OrderService {
  constructor(
    @InjectModel(Order.name) private orderModel: Model<OrderDocument>,
  ) {}

  public async getOrdersAsync(
    startDate?: string,
    endDate?: string,
  ): Promise<Order[]> {
    if (!startDate) {
      startDate = new Date(1970, 1, 1).toISOString();
    } else {
      startDate = new Date(startDate).toISOString();
    }

    if (!endDate) {
      endDate = new Date(2050, 1, 1).toISOString();
    } else {
      endDate = new Date(endDate).toISOString();
    }

    return await this.orderModel
      .find({
        timeStamp: {
          $gte: startDate,
          $lte: endDate,
        },
      })
      .sort({ timeStamp: 'desc' })
      .limit(20)
      .exec();
  }
}
