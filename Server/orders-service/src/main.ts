import { NestFactory } from '@nestjs/core';

import { AppModule } from './app.module';
import { config } from './config/config';

async function bootstrap() {
  const app = await NestFactory.create(AppModule, { cors: true });
  await app.listen(config.nodePort);
}

bootstrap();
