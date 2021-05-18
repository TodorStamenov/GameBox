import { Controller, Get, Query, UseGuards } from '@nestjs/common';

import { AuthGuard } from 'src/guards/auth.guard';
import { Order } from 'src/entities/order.schema';
import { OrderService } from '../services/order.service';

@Controller('api/orders')
export class OrdersController {
  constructor(private readonly orderService: OrderService) { }

  @Get()
  @UseGuards(AuthGuard('admin'))
  public async getOrders(
    @Query('startDate') startDate?: string,
    @Query('endDate') endDate?: string
  ): Promise<Order[]> {
    return await this.orderService.getOrdersAsync(startDate, endDate);
  }
}
