import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import { User } from '../models/user';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthentificationService {

  constructor(private http: HttpClient) {}

  authentificate(data: User){
    return this.http.post(`${environment.BaseUrl}/api/Auth/login`, data,
    {
      headers: new HttpHeaders({ "Content-Type": "application/json"})
    }
    );
  }
}
