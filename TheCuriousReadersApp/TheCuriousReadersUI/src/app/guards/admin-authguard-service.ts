import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Injectable({
    providedIn: 'root'
})

export class AdminAuthGuard implements CanActivate{

    constructor(private router: Router, private authService: AuthService) {

    }
    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean{
        if(!(this.authService.isUserAdmin())){
            this.router.navigate([this.router.url]);
            return false;
        }

        return true;
    }
}