import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ITable } from '../core/models/table.model';

@Injectable({
  providedIn: 'root'
})

export class ApiService {

  private _httpClient = inject(HttpClient);
  private baseURL: string = 'http://localhost:5217/api/Estadistica';

  getEstadistica(): Observable<ITable[]>{
    return this._httpClient.get<ITable[]>(this.baseURL);
  }

}
