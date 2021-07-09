import { Component, OnInit } from '@angular/core';
import { Router } from "@angular/router";
import { AuthService } from "../../services/auth.service"
import { routePaths } from '../../constants/routes';

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.scss']
})
export class LandingPageComponent implements OnInit {

  constructor(private router: Router, private authService: AuthService) { }

  ngOnInit(): void {
  }

  registerRedirect(){
    this.router.navigate([routePaths.registrationPage]);
  }

  loginRedirect(){
    this.router.navigate([routePaths.loginPage]);
  }
}
