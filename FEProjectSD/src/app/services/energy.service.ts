import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Energy } from '../models/energy';
@Injectable({
  providedIn: 'root'
})
export class EnergyService {

  constructor(private http: HttpClient) {}

  getEnergyForDevices(id: number, date: string){

    const params = new HttpParams().set('id', id).set('date', date);
    return this.http.get<Array<Energy>>(`${environment.BaseUrl}/api/Energy/get-for-deviceId`,{params});
  }


}
