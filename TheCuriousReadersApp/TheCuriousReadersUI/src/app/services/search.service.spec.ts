import { TestBed } from '@angular/core/testing';

import { SearchService } from './search.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { HttpClient } from '@angular/common/http';
import { of } from 'rxjs';
import { SearchModel } from '../models/searchmodel';
import { PaginatedNewSearchResultsModel } from '../models/paginatednewsearchresultsmodel';
import { SearchResponseModel } from '../models/searchResponseModel';

describe('SearchService', () => {
  let service: SearchService;
  let httpMock: jasmine.Spy;
  let httpClient: HttpClient;
  let searchModel: SearchModel;
  let searchMock: jasmine.Spy;
  let pageNum: number;
  let pageSize: number;
  let paginatedBookResult: PaginatedNewSearchResultsModel;
  let searchResults: SearchResponseModel[];

  beforeEach(() => {
    TestBed.configureTestingModule({
        providers: [SearchService,
                    HttpClient],
        imports: [ HttpClientTestingModule ]
    });

    searchModel = new SearchModel(new Array<string>(), "");
    pageNum = 1;
    pageSize = 5;
    searchResults = [];

    paginatedBookResult = new PaginatedNewSearchResultsModel(searchResults,10);

    service = TestBed.inject(SearchService);
    httpClient = TestBed.inject(HttpClient);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should send request for search', () => {
    httpMock = spyOn(httpClient, "get").and.returnValue(of("test"));

    service.search(searchModel,pageNum ,pageSize).subscribe(x => {
        expect(x).toBeDefined();
        expect(httpMock).toHaveBeenCalled();
    });
  });

  it('should return data', () => {
    searchMock = spyOn(service, "search").and.returnValue(of(paginatedBookResult));

    service.search(searchModel,pageNum ,pageSize).subscribe(x => {
        expect(x).toBe(paginatedBookResult);
    });
  });
});
