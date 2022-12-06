
import { AuthentificationService } from './../services/authentification.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../models/user';
import { HttpErrorResponse } from '@angular/common/http';
import { AuthenticatedResponse } from '../models/AuthenticatedResponse';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.scss']
})
export class LoginPageComponent implements OnInit {
  username!: string;
  password!: string;
  invalidLogin?: boolean;

  constructor(
    private router: Router,
    private authentification: AuthentificationService,
    ) { }

  ngOnInit(): void {

  }
  login(){
    let userCredentials: User ={
      name:'a',
      role:'b',
      userName: this.username,
      password: this.password
    }

    this.authentification.authentificate(userCredentials).subscribe({
      next: (response: any) => {
        const token: AuthenticatedResponse = response.token;
        const role: string = response.role;
        const id: string = response.id;
        console.log(response.role);
        localStorage.setItem("jwt", token.token);
        localStorage.setItem('role', role);
        this.invalidLogin = false;
        if(role === 'admin'){
          this.router.navigate(["/"+role+"-page"]);
        }
        else{
          this.router.navigate(["/"+role+"-page/"+id]);
        }
      },
      error: (err: HttpErrorResponse) => this.invalidLogin = true
    })
  }

}
