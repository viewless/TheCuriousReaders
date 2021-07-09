import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { UserRoles } from '../enums/userroles';
import { routePaths } from '../constants/routes';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private jwtHelper: JwtHelperService, private router: Router) { }

  public setJWTInLocalStorage(token: string): void {
    localStorage.setItem("jwt", token);
  }

  public isUserAdmin(): boolean{
    const JWTtoken = localStorage.getItem("jwt");

    if(JWTtoken && !this.jwtHelper.isTokenExpired(JWTtoken)){
      var decodedToken = this.jwtHelper.decodeToken(JWTtoken);
      if(decodedToken.roles === UserRoles.Librarian){
      return true;
      }
    }

    return false;
  }

  isUserAuthenticated(): boolean{
    const token = localStorage.getItem("jwt");
    
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      return true;
    }
    else {
      return false;
    }
  }

  logOut() {
    localStorage.removeItem("jwt");
    this.router.navigate([routePaths.landingPage]);
  }

  getUserId() {
    const JWTtoken = localStorage.getItem("jwt");

    if(JWTtoken && !this.jwtHelper.isTokenExpired(JWTtoken)){
      var decodedToken = this.jwtHelper.decodeToken(JWTtoken);

      return decodedToken.id;
    } 
  }
}
