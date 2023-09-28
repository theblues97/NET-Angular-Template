import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

const SERVER_API_URL = "https://localhost:7187/";
// const SERVER_API_URL = "http://localhost:43752/";

@Injectable()
export class ApiService {

    
    header = new HttpHeaders({
        'Content-Type': 'application/json',
    });

    constructor(private http: HttpClient) { }

    composeUrl(url: string) {
        if (url.indexOf('/') == 0) {
            url = url.substring(1);
        }
        return SERVER_API_URL + url;
    }

    get(url: string) {
        url = this.composeUrl(url);
        return this.http.get(url, { headers: this.header});
    }

    post(url: string, body: any) {
        url = this.composeUrl(url);
        return this.http.post(url, body, { headers: this.header});
    }

    put(url: string, body: any) {
        url = this.composeUrl(url);
        return this.http.put(url, body, { headers: this.header});
    }

    delete(url: string) {
        url = this.composeUrl(url);
        return this.http.delete(url, { headers: this.header});
    }
}