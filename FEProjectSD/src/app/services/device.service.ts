import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Device } from '../models/device';

@Injectable({
  providedIn: 'root'
})
export class DeviceService {

  constructor(private http: HttpClient) { }

  getDevices(){
    return this.http.get<Array<Device>>(`${environment.BaseUrl}/api/Device/get`);
  }

  getUserDevices(id: number){
    return this.http.get<Array<Device>>(`${environment.BaseUrl}/api/Device/user-devices?id=${id}`);
  }

  addDevice(device: Device){
    return this.http.post(`${environment.BaseUrl}/api/Device/add`,device);
  }

  deleteDevice(id: number){
    return this.http.delete(`${environment.BaseUrl}/api/Device/remove?id=${id}`);
  }

  editDevice(device: Device){
    return this.http.put<any>(`${environment.BaseUrl}/api/Device/edit`, device);
  }
}
