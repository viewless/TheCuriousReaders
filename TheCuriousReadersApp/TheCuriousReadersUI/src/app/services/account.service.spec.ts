import { TestBed } from '@angular/core/testing';

import { AccountService } from './account.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { of } from 'rxjs';
import { HttpClient } from "@angular/common/http";

describe('AccountService', () => {
    let service: AccountService;
    let httpMock: jasmine.Spy;
    let httpClient: HttpClient;

  beforeEach(() => {
    TestBed.configureTestingModule({
        providers: [AccountService,
                    HttpClient],
        imports: [ HttpClientTestingModule ]
    });
  
    httpClient = TestBed.inject(HttpClient);
    service = TestBed.inject(AccountService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should send request for getTotalAccounts', () => {
    httpMock = spyOn(httpClient,"get").and.returnValue(of("test"));

    service.getTotalAccounts().subscribe(x => {
        expect(x).toBeDefined();
        expect(httpMock).toHaveBeenCalled();
    });     
  });
});