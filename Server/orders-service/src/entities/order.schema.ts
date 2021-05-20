import { Prop, Schema, SchemaFactory } from '@nestjs/mongoose';
import { Document } from 'mongoose';

import { Game, GameSchema } from './game.schema';

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

  @Prop({ type: [GameSchema], default: [], required: true })
  games: Game[];
}

export const OrderSchema = SchemaFactory.createForClass(Order);
