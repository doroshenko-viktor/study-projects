import { PartialType } from '@nestjs/mapped-types';
import { CreateLibDto } from './create-lib.dto';

export class UpdateLibDto extends PartialType(CreateLibDto) {}
