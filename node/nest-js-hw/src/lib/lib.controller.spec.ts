import { Test, TestingModule } from '@nestjs/testing';
import { LibController } from './lib.controller';
import { LibService } from './lib.service';

describe('LibController', () => {
  let controller: LibController;

  beforeEach(async () => {
    const module: TestingModule = await Test.createTestingModule({
      controllers: [LibController],
      providers: [LibService],
    }).compile();

    controller = module.get<LibController>(LibController);
  });

  it('should be defined', () => {
    expect(controller).toBeDefined();
  });
});
