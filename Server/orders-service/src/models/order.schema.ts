import { Prop, Schema, SchemaFactory } from '@nestjs/mongoose';
import { Document } from 'mongoose';

export type OrderDocument = Order & Document;

@Schema()
export class Order {
  @Prop({ required: true })
  username: string;

  @Prop({ required: true })
  timeStamp: string;

  @Prop({ required: true })
  price: number;

  @Prop({ required: true })
  gamesCount: number;
}

export const OrderSchema = SchemaFactory.createForClass(Order);
