import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SubscriptionService } from 'src/app/services/subscription.service';

@Component({
  selector: 'app-book-subscribers',
  templateUrl: './book-subscribers.component.html',
  styleUrls: ['./book-subscribers.component.scss']
})
export class BookSubscribersComponent implements OnInit {
  totalCount: number = 0;

  constructor(private subscriptionService: SubscriptionService, private activatedRoute: ActivatedRoute) {
   }

  ngOnInit(): void {
    this.getTotalSubscribers();
  }

  getTotalSubscribers(){
    const bookId = Number(this.activatedRoute.snapshot.paramMap.get('id'));;
    this.subscriptionService.getTotalSubscribersForABook(bookId)
    .subscribe(subscriptions => {
      this.totalCount =  subscriptions.totalSubscribers;
    })
  }
}
