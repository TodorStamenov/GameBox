import { JwtModule } from '@nestjs/jwt';
import { Module } from '@nestjs/common';
import { MongooseModule } from '@nestjs/mongoose';

import { OrdersController } from './controllers/orders.controller';
import { OrderService } from './services/order.service';
import { Order, OrderSchema } from './entities/order.schema';

import { config } from './config/config';

@Module({
  imports: [
    JwtModule.register({ secret: 'superSecretKey@345' }),
    MongooseModule.forRoot(
      `mongodb://${config.dockerHostIp}:${config.mongoPort}/game-box`, {
      useNewUrlParser: true,
      useUnifiedTopology: true
    }),
    MongooseModule.forFeature([{ name: Order.name, schema: OrderSchema }])
  ],
  controllers: [OrdersController],
  providers: [OrderService]
})
export class AppModule { }
