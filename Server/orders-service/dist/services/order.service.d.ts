import { Model } from 'mongoose';
import { Order, OrderDocument } from 'src/models/order.schema';
export declare class OrderService {
    private orderModel;
    constructor(orderModel: Model<OrderDocument>);
    getOrders(startDate?: string, endDate?: string): Promise<Order[]>;
}
