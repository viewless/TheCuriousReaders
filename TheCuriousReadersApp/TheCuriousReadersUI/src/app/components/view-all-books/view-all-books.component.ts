import { Component, OnInit } from '@angular/core';
import { paginationParameters } from 'src/app/constants/paginationparameters';
import { BookData } from 'src/app/models/book-data';
import { BookService } from 'src/app/services/book.service';

@Component({
  selector: 'app-view-all-books',
  templateUrl: './view-all-books.component.html',
  styleUrls: ['./view-all-books.component.scss']
})
export class ViewAllBooksComponent implements OnInit {
  bookData: BookData[] = [];
  pageNumber: number = paginationParameters.pageNumber;
  pageSize: number = paginationParameters.pageSize;
  totalItems: number = 0; 

  constructor(private bookService: BookService) { }

  ngOnInit(): void {
    this.getCurrentPageData(this.pageNumber);
  }

  getCurrentPageData(pageNumber: number){
    this.bookService.getAllBooksWithPagination(pageNumber, this.pageSize)
    .subscribe(books => {
      this.bookData = books.books;
      this.totalItems = books.totalCount;
    })
  }
}
