import { Prop, Schema, SchemaFactory } from '@nestjs/mongoose';

@Schema()
export class Game {
  @Prop({ required: true })
  id: string;

  @Prop({ required: true })
  title: string;

  @Prop({ required: true })
  viewCount: number;

  @Prop({ required: true })
  price: number;

  @Prop({ required: true })
  orderCount: number;
}

export const GameSchema = SchemaFactory.createForClass(Game);
