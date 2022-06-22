import { Module } from '@nestjs/common';
import { AppController } from './app.controller';
import { AppService } from './app.service';
import { LibController } from './lib/lib.controller';
import { LibModule } from './lib/lib.module';
import { LibService } from './lib/lib.service';

@Module({
  imports: [LibModule],
  controllers: [AppController, LibController],
  providers: [AppService, LibService],
})
export class AppModule {}
