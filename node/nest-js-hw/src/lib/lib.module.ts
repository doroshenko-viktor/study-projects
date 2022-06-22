import { Module } from '@nestjs/common';
import { LibService } from './lib.service';
import { LibController } from './lib.controller';

@Module({
  controllers: [LibController],
  providers: [LibService],
})
export class LibModule {}
