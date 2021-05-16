import { Document } from 'mongoose';
export declare type OrderDocument = Order & Document;
export declare class Order {
    username: string;
    timeStamp: string;
    price: number;
    gamesCount: number;
}
export declare const OrderSchema: import("mongoose").Schema<Document<Order, any>, import("mongoose").Model<any, any, any>, undefined>;
