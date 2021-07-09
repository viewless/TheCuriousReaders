import { TestBed } from '@angular/core/testing';

import { BookService } from './book.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { HttpClient } from "@angular/common/http";
import { of } from 'rxjs';

describe('BookService', () => {
    let service: BookService;
    let httpMock: jasmine.Spy;
    let httpClient: HttpClient;
    let bookId: string;
    let pageNum: number;
    let pageSize: number;
    let isAvailable: boolean;
    
  beforeEach(() => {
    TestBed.configureTestingModule({
        providers: [BookService,
                    HttpClient],
        imports: [ HttpClientTestingModule ]
    });

    bookId = "1";
    pageNum = 1;
    pageSize = 5;
    isAvailable = true;
    
    httpClient = TestBed.inject(HttpClient);
    service = TestBed.inject(BookService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should send request for addBook', () => {
    httpMock = spyOn(httpClient,"post").and.returnValue(of("test"));

    service.addBook({}).subscribe(x => {
        expect(x).toBeDefined();
        expect(httpMock).toHaveBeenCalled();
    });     
  }); 

  it('should send request for addCover', () => {
    httpMock = spyOn(httpClient,"post").and.returnValue(of("test"));
    const file = new File([],"");
    const bookId = 1;

    service.addCover(file,bookId).subscribe(x => {
        expect(x).toBeDefined();
        expect(httpMock).toHaveBeenCalled();
    });     
  });
  
  it('should send request for getBookData', () => {
    httpMock = spyOn(httpClient,"get").and.returnValue(of("test"));

    service.getBookData(bookId).subscribe(x => {
        expect(x).toBeDefined();
        expect(httpMock).toHaveBeenCalled();
    });     
  });

  it('should send request for subscribeToBook', () => {
    httpMock = spyOn(httpClient,"post").and.returnValue(of("test"));

    service.subscribeToBook({BookId: "1", Copies: 1}).subscribe(x => {
        expect(x).toBeDefined();
        expect(httpMock).toHaveBeenCalled();
    });     
  });

  it('should send request for getNewBooks', () => {
    httpMock = spyOn(httpClient,"get").and.returnValue(of("test"));

    service.getNewBooks(pageNum, pageSize).subscribe(x => {
        expect(x).toBeDefined();
        expect(httpMock).toHaveBeenCalled();
    });     
  });

  it('should send request for getGenres', () => {
    httpMock = spyOn(httpClient,"get").and.returnValue(of("test"));

    service.getGenres().subscribe(x => {
        expect(x).toBeDefined();
        expect(httpMock).toHaveBeenCalled();
    });     
  });

  it('should send request for addReview', () => {
    httpMock = spyOn(httpClient,"post").and.returnValue(of("test"));

    service.addReview({rating: 5, commentBody: "test"}, bookId).subscribe(x => {
        expect(x).toBeDefined();
        expect(httpMock).toHaveBeenCalled();
    });     
  });

  it('should send request for getComments', () => {
    httpMock = spyOn(httpClient,"get").and.returnValue(of("test"));

    service.getComments(pageNum, pageSize, bookId).subscribe(x => {
        expect(x).toBeDefined();
        expect(httpMock).toHaveBeenCalled();
    });     
  });

  it('should send request for deleteBook', () => {
    httpMock = spyOn(httpClient,"delete").and.returnValue(of("test"));

    service.deleteBook(bookId).subscribe(x => {
        expect(x).toBeDefined();
        expect(httpMock).toHaveBeenCalled();
    });     
  });

  it('should send request for changeBookAvailability', () => {
    httpMock = spyOn(httpClient,"patch").and.returnValue(of("test"));

    service.changeBookAvailability(1,isAvailable).subscribe(x => {
        expect(x).toBeDefined();
        expect(httpMock).toHaveBeenCalled();
    });     
  });

  it('should send request for updateBook', () => {
    httpMock = spyOn(httpClient,"put").and.returnValue(of("test"));

    service.updateBook("test", 1).subscribe(x => {
        expect(x).toBeDefined();
        expect(httpMock).toHaveBeenCalled();
    });     
  });
});
