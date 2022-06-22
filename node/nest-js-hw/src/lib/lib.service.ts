import { Injectable } from '@nestjs/common';
import { CreateLibDto } from './dto/create-lib.dto';
import { UpdateLibDto } from './dto/update-lib.dto';

@Injectable()
export class LibService {
  create(createLibDto: CreateLibDto) {
    return 'This action adds a new lib';
  }

  findAll() {
    return `This action returns all lib`;
  }

  findOne(id: number) {
    return `This action returns a #${id} lib`;
  }

  update(id: number, updateLibDto: UpdateLibDto) {
    return `This action updates a #${id} lib`;
  }

  remove(id: number) {
    return `This action removes a #${id} lib`;
  }
}
