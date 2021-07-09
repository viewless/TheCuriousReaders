import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { paginationParameters } from 'src/app/constants/paginationparameters';
import { NotReviewedSubscriptionsModel } from 'src/app/models/notreviewed.subscriptions.model';
import { SubscriptionService } from 'src/app/services/subscription.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ApproveSubscriptionModel } from 'src/app/models/approvesubscription.model';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-admin-view-subscriptions',
  templateUrl: './admin-view-subscriptions.component.html',
  styleUrls: ['./admin-view-subscriptions.component.scss']
})
export class AdminViewSubscriptionsComponent implements OnInit {
  notReviewedSubscriptions: NotReviewedSubscriptionsModel[] = [];
  totalItems: number = 0;
  pageNumber: number = paginationParameters.pageNumber;
  pageSize: number = paginationParameters.pageSize;
  @ViewChild('modalApprove') modalApprove : TemplateRef<any> | undefined;
  @ViewChild('modalReject') modalReject : TemplateRef<any> | undefined;
  errorMsg: string = '';
  successMsg: string = '';
  approveSubscriptionModel: ApproveSubscriptionModel = new ApproveSubscriptionModel(false, 0);

  constructor(private subscriptionService: SubscriptionService, private modalService: NgbModal,
    private SpinnerService: NgxSpinnerService) { }

  ngOnInit(): void {
    this.getNonReviewedSubscriptionsPage(this.pageNumber);
  }

  getNonReviewedSubscriptionsPage(pageNumber: number){
    this.subscriptionService.getNonReviewedSubscriptions(pageNumber, this.pageSize)
      .subscribe(subscriptions => {
        this.notReviewedSubscriptions = subscriptions.nonReviewedSubscriptions;
        this.totalItems = subscriptions.totalCount;
    })
  }

  approveSubscription(id: number){
    const modalResult = this.modalService.open(this.modalApprove).result;
    var isAccepted: boolean;

    modalResult.then((value) => { 
      isAccepted = value;
      const daysAsString = (<HTMLInputElement>document.querySelector(`#request-days-id${id}`)).innerHTML;
      if(isAccepted){
      if(daysAsString){
        const days = parseInt(daysAsString);
        if(days > 0){
        this.approveSubscriptionModel.isApproved = true;
        this.approveSubscriptionModel.subscriptionDaysAccepted = days;
        this.SpinnerService.show();
        this.subscriptionService.approveSubscription(id, this.approveSubscriptionModel)
        .subscribe(() => {
          this.SpinnerService.hide();
          this.successMsg = "The subscription has successfully been approved."
          this.getNonReviewedSubscriptionsPage(1);
        }, () => {
          this.SpinnerService.hide();
          this.errorMsg = "An error occured while updating the subscription."; 
        })
      }else{
        this.errorMsg = "Requested days for approved subscription can't be zero.";
      }
      }
      else{
        this.errorMsg = "Requested days value can't be empty.";
      }
    }
    }
    );
  }

  rejectSubscription(id: number){
    const modalResult = this.modalService.open(this.modalReject).result;
    var isRejected: boolean;

    modalResult.then((value) => { 
      isRejected = value;
      const daysAsString = (<HTMLInputElement>document.querySelector(`#request-days-id${id}`)).innerHTML;
      if(isRejected){
        const days = parseInt(daysAsString);
        this.approveSubscriptionModel.isApproved = false;
        this.SpinnerService.show();
        this.subscriptionService.approveSubscription(id, this.approveSubscriptionModel)
        .subscribe(() => {
          this.SpinnerService.hide();
          this.successMsg = "The subscription has successfully been rejected."
          this.getNonReviewedSubscriptionsPage(1);
        }, () => {
          this.SpinnerService.hide();
          this.errorMsg = "An error occured while updating the subscription.";  
        })
    }
    }
    );
  }

  rejectSubscriptionModal(): boolean{
    return true;
  }

  approveSubscriptionModal(): boolean{
    return true;
  }

  clearErrorMessage() : void{
    this.errorMsg = '';
  }

  clearSuccessMessage() : void{
    this.successMsg = '';
  }
}
