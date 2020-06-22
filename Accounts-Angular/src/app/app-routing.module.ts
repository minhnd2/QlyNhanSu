import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { IndexComponent } from './index/index.component';
import { LoginComponent } from './login/login.component';
import { AccountsComponent } from './accounts/accounts.component';
import { AuthService } from './service/Auth.service';


const routes: Routes = [
  { path: "", component: IndexComponent, canActivate: [AuthService] },
  { path: "index", component: IndexComponent, canActivate: [AuthService] },
  { path: "login", component: LoginComponent },
  { path: "accounts", component: AccountsComponent, canActivate: [AuthService] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
