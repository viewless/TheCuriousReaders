import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { routePaths } from 'src/app/constants/routes';

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.scss']
})
export class AdminPanelComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  goBack(){
    this.router.navigate([routePaths.landingPage]);
  }

}
