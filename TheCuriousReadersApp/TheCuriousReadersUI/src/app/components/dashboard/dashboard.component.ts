import { Component, OnInit } from '@angular/core';
import { paginationParameters } from 'src/app/constants/paginationparameters';
import { SubscriptionService } from 'src/app/services/subscription.service';
import { AuthService } from 'src/app/services/auth.service';
import { ApprovedSubscriptionModel } from 'src/app/models/approvedsubscriptionmodel';
import { NgxSpinnerService } from 'ngx-spinner';


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  books: ApprovedSubscriptionModel[] = [];
  pageNumber: number = paginationParameters.pageNumber;
  pageSize: number = paginationParameters.pageSize;
  returnDate = new Date();
  totalItems: number = 0;
  userId: string = '';

  constructor(
    private subscriptionService: SubscriptionService,
    private authService: AuthService,
    private SpinnerService: NgxSpinnerService
  ) {}

  ngOnInit(): void {
    this.getCurrentPageData(this.pageNumber);
  }

  getCurrentPageData(pageNumber: number) {
    this.userId = this.authService.getUserId();
   
    this.subscriptionService
      .getInformationForDashboard(this.userId, this.pageNumber, this.pageSize)
      .subscribe(myBooks => {
        this.books = myBooks.approvedSubscriptions;
        this.totalItems = myBooks.totalCount;
      });

      
     
  }

  dateToReturn(bookDate: string) {
    return new Date(bookDate).toLocaleString();
  }

  requestAdditionalTime(id : number, requestedDays: number){
    this.SpinnerService.show();
    this.subscriptionService.requestMoreDays(id, requestedDays)
    .subscribe(myBooks => {
      this.getCurrentPageData(1);
      this.SpinnerService.hide();
    },(() => {
      this.SpinnerService.hide();
    }));
    }
  
  

  compareCurrentDateAndBookReturnDate(bookDate: string): boolean{
    let currentDate = new Date(Date.now());
    let bookReturnDate = new Date(bookDate);

    return currentDate > bookReturnDate;
  }

}