import { AuthGuard } from './guards/auth.guard';
import { UserPageComponent } from './user-page/user-page.component';
import { LoginPageComponent } from './login-page/login-page.component';
import { AdminPageComponent } from './admin-page/admin-page.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path:'', component: LoginPageComponent, title: 'Login'
  },
  {
    path: "admin-page", component: AdminPageComponent, title: 'AdminPage', canActivate:[AuthGuard]
  },
  {
    path: "user-page/:id", component: UserPageComponent, title: 'UserPage', canActivate:[AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
