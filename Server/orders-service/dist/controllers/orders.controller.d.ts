import { Order } from 'src/models/order.schema';
import { OrderService } from '../services/order.service';
export declare class OrdersController {
    private readonly orderService;
    constructor(orderService: OrderService);
    getOrders(startDate?: string, endDate?: string): Promise<Order[]>;
}
