import { Module } from '@nestjs/common';
import { JwtModule } from '@nestjs/jwt';
import { MongooseModule } from '@nestjs/mongoose';
import { RabbitMQModule } from '@golevelup/nestjs-rabbitmq';

import { OrdersController } from './controllers/orders.controller';
import { OrderService } from './services/order.service';
import { MessagingService } from './services/messaging.service';
import { Order, OrderSchema } from './entities/order.schema';

import { config } from './config/config';

@Module({
  imports: [
    JwtModule.register({ secret: '*^Super_Secret_Symmetric_Security_Key^*' }),
    MongooseModule.forFeature([{ name: Order.name, schema: OrderSchema }]),
    MongooseModule.forRoot(
      `mongodb://${config.dockerHostIp}:${config.mongoPort}/game-box`,
      {
        useNewUrlParser: true,
        useUnifiedTopology: true,
      },
    ),
    RabbitMQModule.forRoot(RabbitMQModule, {
      uri: `amqp://guest:guest@${config.dockerHostIp}:${config.rabbitMqPort}`,
      connectionInitOptions: { wait: true },
    }),
  ],
  controllers: [OrdersController],
  providers: [OrderService, MessagingService],
})
export class AppModule {}
