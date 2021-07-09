import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { routePaths } from 'src/app/constants/routes';
import { Location } from '@angular/common'

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  constructor(private router: Router, private location: Location) { }

  
  goBack(){
    this.location.back();
  }

  goHomePage(){
    this.router.navigate([routePaths.landingPage]);
  }

  ngOnInit(): void {
  }

}
