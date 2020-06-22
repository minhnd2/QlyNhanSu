import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService implements CanActivate {

  constructor(private router: Router) { }
  
  isLogged(): boolean {
    if (localStorage.getItem("ID") === null) {
      return false;
    } else {
      return true;
    }
  }

  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    if (!this.isLogged()) {
      this.router.navigate(['/login']);
      return false;
    } else {
      return true;
    }
  }

}
