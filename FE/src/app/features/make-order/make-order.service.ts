import { Injectable,  } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from 'src/app/core/api.service';
import { OrderDto } from '../detail-list/detail-list.model';
import { map } from 'rxjs';
import { BaseResponseModel } from 'src/app/core/models/response.model';

@Injectable()
export class MakeOrderService {

    constructor(private api: ApiService){}

    register(orderList: OrderDto[]) {
        return this.api.post("Product/SaveOrder", orderList)
        .pipe(
            map(rs => rs as BaseResponseModel)
        )
    }
}