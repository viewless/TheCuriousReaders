import { Component, OnInit } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { UserLoginModel } from '../../models/user.loginmodel';
import { environment } from 'src/environments/environment';
import { Router } from "@angular/router";
import { AuthService } from "../../services/auth.service"
import { Errors } from "../../enums/errors";
import { routePaths } from '../../constants/routes';
import { LoginResponseModel } from 'src/app/models/loignresponsemodel';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})

export class LoginComponent implements OnInit {
  loginModel = new UserLoginModel("", "");

  constructor(private router: Router, private http: HttpClient, private authService: AuthService,
    private SpinnerService: NgxSpinnerService) { }

  errorMsg: string = '';

  ngOnInit(): void {
  }

  login(data:UserLoginModel){
    this.SpinnerService.show();
    this.http.post(`${environment.baseUrl}/Login`, 
    data, 
    {headers:{'Content-Type':'application/json'}})
    .subscribe(res => {
      this.SpinnerService.hide();
      const token = (<LoginResponseModel>res).token;
      this.authService.setJWTInLocalStorage(token);
      this.router.navigate([routePaths.homePage]);
    }, error => {
      this.SpinnerService.hide();
      if(error.status == Errors.Unauthenticated){
        this.errorMsg = "Invalid email address or password.";
      }
      else if(error.status == Errors.BadRequest){
        this.errorMsg = "Please enter all the required credentials.";
      }
      else {
        this.errorMsg = "An error occured.";
      }
    })
  }

  goBack(){
    this.router.navigate([routePaths.landingPage]);
  }

  registerRedirect(){
    this.router.navigate([routePaths.registrationPage]);
  }

  onSubmit(){
    this.login(this.loginModel);
  }
  
  clearErrorMessage(): void {
    this.errorMsg = '';
  }
}
