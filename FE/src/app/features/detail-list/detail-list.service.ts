import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { ApiService } from 'src/app/core/api.service';
import { OrderDto } from './detail-list.model';
import { BaseDataResponseModel } from 'src/app/core/models/response.model';

@Injectable()
export class DetailListService {

    constructor(private api: ApiService){}

    getOrder() {
        return this.api.get(`Product/GetOrder`)
        .pipe(
            map(rs => rs as BaseDataResponseModel<OrderDto[]>)
        )
    }
}