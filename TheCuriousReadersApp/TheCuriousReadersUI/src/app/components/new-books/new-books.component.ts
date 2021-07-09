import { Component, OnInit } from '@angular/core';
import { NewBookModel } from 'src/app/models/newbookmodel';
import { BookService } from 'src/app/services/book.service';
import { paginationParameters } from '../../constants/paginationparameters'

@Component({
  selector: 'app-new-books',
  templateUrl: './new-books.component.html',
  styleUrls: ['./new-books.component.scss']
})
export class NewBooksComponent implements OnInit {
  books: NewBookModel[] = [];
  pageNumber: number = paginationParameters.pageNumber;
  pageSize: number = paginationParameters.pageSize;
  totalItems: number = 0; 

  constructor(private bookService: BookService) { }

  ngOnInit(): void {
    this.getCurrentPageData(this.pageNumber);
  }

  getCurrentPageData(pageNumber: number){
    this.bookService.getNewBooks(this.pageNumber, this.pageSize)
    .subscribe(newBooks => {
      this.books = newBooks.newBooks;
      this.totalItems = newBooks.totalCount;
    })
  }
}
