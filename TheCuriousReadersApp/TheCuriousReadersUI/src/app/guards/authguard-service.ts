import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { routePaths } from '../constants/routes';

@Injectable()
export class AuthGuard implements CanActivate{

    constructor(private router: Router, private authService: AuthService) {

    }
    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean{
        if(!(this.authService.isUserAuthenticated())){
            this.router.navigate([routePaths.landingPage]);
            return false;
        }

        return true;
    }
}