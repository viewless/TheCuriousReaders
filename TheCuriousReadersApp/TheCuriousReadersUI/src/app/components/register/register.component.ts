import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UserRegisterModel } from '../../models/user.registermodel';
import { AddressModel } from '../../models/addressmodel';
import { RegisterResponseModel } from '../../models/register.responsemodel'
import { environment } from 'src/environments/environment';
import { Errors } from '../../enums/errors';
import { Router } from "@angular/router";
import { AuthService } from "../../services/auth.service"
import { routePaths } from '../../constants/routes';
import { UserLoginModel } from 'src/app/models/user.loginmodel';
import { LoginResponseModel } from 'src/app/models/loignresponsemodel';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})

export class RegisterComponent implements OnInit {  
  addressModel = new AddressModel("", "", "", "", "", "", "")
  userModel = new UserRegisterModel("", "", "", "", "", "", this.addressModel);
  registerResponse = new RegisterResponseModel("", "");
  loginModel = new UserLoginModel("", "");

  errorMsg: string = '';
  successMsg: string = '';

  countries = ["Bulgaria", "Romania",
  "Germany", "Greece",
  "Turkey", "Serbia",
  "North Macedonia", "Moldova",
  "Croatia", "Montenegro"];

  constructor(private http: HttpClient, private router: Router, private authService: AuthService,
    private SpinnerService: NgxSpinnerService) {}

  ngOnInit(): void {
  }

  login(data:UserLoginModel){
    this.http.post(`${environment.baseUrl}/Login`, 
    data, 
    {headers:{'Content-Type':'application/json'}})
    .subscribe(res => {
      const token = (<LoginResponseModel>res).token;
      this.authService.setJWTInLocalStorage(token);
      this.router.navigate([routePaths.homePage]);
    }, error => {
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

  register(request: string, data: UserLoginModel): void {
    this.SpinnerService.show();
    this.http.post(`${environment.baseUrl}/Registration`, 
    request, 
    {headers:{'Content-Type':'application/json'}})
    .subscribe(res => {
      this.registerResponse.statusCode = "201";

      const forLogin = JSON.parse(request);

      this.loginModel.email = data.email;
      this.loginModel.password = data.password;
      this.SpinnerService.hide();

      this.login(this.loginModel);
    }, error => {
      this.SpinnerService.hide();
      if(error.status == Errors.Conflict){
        this.registerResponse.errorMessage = error.error.error;
        this.errorMsg = this.registerResponse.errorMessage;
      }
      else{
        this.errorMsg = "An error occured.";
      }
    })
  }

  onSubmit(): void {
    const data = {
      firstName: this.userModel.firstName,
      lastName: this.userModel.lastName,
      emailAddress: this.userModel.emailAddress,
      phoneNumber: this.userModel.phoneNumber,
      password: this.userModel.password,
      confirmPassword: this.userModel.confirmPassword,
      address: this.userModel.address,
      }      
      
    const jsonRequest = JSON.stringify(data);

    const forLogin = {
      email: this.userModel.emailAddress,
      password: this.userModel.password
    }

    this.register(jsonRequest, forLogin);
  }

  clearErrorMessage(): void {
    this.errorMsg = '';
  }

  clearSuccessMessage(): void {
    this.successMsg = '';
  }

  goBack(){
    this.router.navigate([routePaths.landingPage]);
  }

  loginRedirect(){
    this.router.navigate([routePaths.loginPage]);
  }
}
