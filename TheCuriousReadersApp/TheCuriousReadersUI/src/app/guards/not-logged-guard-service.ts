import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { routePaths } from '../constants/routes';
import { AuthService } from '../services/auth.service';

@Injectable()
export class NotLoggedGuard implements CanActivate{

    constructor(private router: Router, private authService: AuthService) {

    }
    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean{
        if(!(this.authService.isUserAuthenticated())){
            return true;
        }

        this.router.navigate([routePaths.homePage]);
        return false;
    }
}