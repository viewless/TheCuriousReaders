import { Component, OnInit } from '@angular/core';
import { AuthorModel } from 'src/app/models/authormodel';
import { BookModel } from 'src/app/models/bookmodel';
import { BookResponseModel } from 'src/app/models/book.responsemodel';
import { BookService } from 'src/app/services/book.service';
import { GenreModel } from 'src/app/models/genremodel';
import { Errors } from '../../enums/errors';
import { switchMap } from 'rxjs/operators';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.scss']
})

export class BookComponent implements OnInit {  
  authorModel = new AuthorModel("")
  genreModel = new GenreModel("")
  bookModel = new BookModel("", "", 1, this.authorModel, this.genreModel)
  bookResponse = new BookResponseModel(0,"", "");
  cover = new File([], "");
  errorMsg: string = '';
  successMsg: string = '';

  genres = ["Fantasy","Mystery",
  "Thriller","Romance",
  "Dystopian","Sci-Fi",
  "Horror","Crime"];

  constructor(private bookService: BookService, private SpinnerService: NgxSpinnerService) {}

  ngOnInit(): void {
  }

  onSelectedFile($event: any){
    this.cover = $event.target.files[0]  
  }

  onSubmit(): void {

    const data = {
      title: this.bookModel.title,
      description: this.bookModel.description,
      quantity: this.bookModel.quantity,
      author: this.authorModel,
      genre: this.genreModel
      }

    const jsonRequest = JSON.stringify(data);
    
    this.SpinnerService.show();
    this.bookService.addBook(jsonRequest).pipe(
      switchMap((res: BookResponseModel) => this.bookService.addCover(this.cover, res.id)))
    .subscribe(newBook => {
      this.SpinnerService.hide();
      this.successMsg = 'Book has successfully been created.'
    }, (error => {
      this.SpinnerService.hide();
      if(error.status == Errors.BadRequest){
        this.errorMsg = 'Book fields are required.'
      }else if(error.status == Errors.Conflict){
        this.errorMsg = 'Title with this author already exists.'
      }
    }));
  }

  clearErrorMessage(): void {
    this.errorMsg = '';
  }

  clearSuccessMessage(): void {
    this.successMsg = '';
  }
}

