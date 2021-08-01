import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Observable } from "rxjs";
import {catchError, map} from 'rxjs/operators';

@Injectable()
export class ProductService {
    constructor(
        private httpClient: HttpClient
      ) { }

      public getProdcuts(url:string): Observable<any>{
        const _jsonURL = url+'/assets/json/products.json';      
        return this.httpClient.get(_jsonURL);
      }
}