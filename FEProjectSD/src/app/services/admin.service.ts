import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Device } from '../models/device';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  constructor(private http: HttpClient) { }

  getUsers(){
    return this.http.get<Array<User>>(`${environment.BaseUrl}/api/User/get`);
  }

  addUser(user: User){
    return this.http.post(`${environment.BaseUrl}/api/User/add`,user);
  }

  deleteUser(id: number){
    return this.http.delete(`${environment.BaseUrl}/api/User/remove?userId=${id}`);
  }

  editUser(user: User){
    return this.http.put<any>(`${environment.BaseUrl}/api/User/edit`, user);
  }

  getDevices(){
    return this.http.get<Array<Device>>(`${environment.BaseUrl}/api/Device/get`);
  }


}
