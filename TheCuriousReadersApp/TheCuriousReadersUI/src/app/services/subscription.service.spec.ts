import { TestBed } from '@angular/core/testing';

import { SubscriptionService } from './subscription.service';
import { HttpClient } from '@angular/common/http';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { of } from 'rxjs';
import { ApproveSubscriptionModel } from '../models/approvesubscription.model';

describe('SubscriptionService', () => {
  let service: SubscriptionService;
  let httpMock: jasmine.Spy;
  let httpClient: HttpClient;
  let bookId: number;
  let pageNum: number;
  let pageSize: number;
  let id: number;
  let subModel: ApproveSubscriptionModel;
  let userId: string;

  beforeEach(() => {
    TestBed.configureTestingModule({
        providers: [SubscriptionService,
                    HttpClient],
        imports: [ HttpClientTestingModule ]
    });

    bookId = 1;
    pageNum = 1;
    pageSize = 5;
    id = 2;
    subModel = new ApproveSubscriptionModel(true, 14);
    userId = "userId";

    service = TestBed.inject(SubscriptionService);
    httpClient = TestBed.inject(HttpClient);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should send request for getTotalSubscribersForABook', () => {
    httpMock = spyOn(httpClient, "get").and.returnValue(of("test"));

    service.getTotalSubscribersForABook(bookId).subscribe(x => {
        expect(x).toBeDefined();
        expect(httpMock).toHaveBeenCalled();
    });
  });

  it('should send request for getNonReviewedSubscriptions', () => {
    httpMock = spyOn(httpClient, "get").and.returnValue(of("test"));

    service.getNonReviewedSubscriptions(pageNum, pageSize).subscribe(x => {
        expect(x).toBeDefined();
        expect(httpMock).toHaveBeenCalled();
    });
  });

  it('should send request for approveSubscription', () => {
    httpMock = spyOn(httpClient, "put").and.returnValue(of("test"));

    service.approveSubscription(id, subModel).subscribe(x => {
        expect(x).toBeDefined();
        expect(httpMock).toHaveBeenCalled();
    });
  });

  it('should send request for getInformationForDashboard', () => {
    httpMock = spyOn(httpClient, "get").and.returnValue(of("test"));

    service.getInformationForDashboard(userId, pageNum, pageSize).subscribe(x => {
        expect(x).toBeDefined();
        expect(httpMock).toHaveBeenCalled();
    });
  });

  it('should send request for requestMoreDays', () => {
    httpMock = spyOn(httpClient, "put").and.returnValue(of("test"));

    service.requestMoreDays(id).subscribe(x => {
        expect(x).toBeDefined();
        expect(httpMock).toHaveBeenCalled();
    });
  });
});
