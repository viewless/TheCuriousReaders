import { Component, OnInit } from '@angular/core';
import { GenreBooksCountModel } from 'src/app/models/genrebookscountmodel';
import { BookService } from 'src/app/services/book.service';

@Component({
  selector: 'app-genres-books',
  templateUrl: './genres-books.component.html',
  styleUrls: ['./genres-books.component.scss']
})
export class GenresBooksComponent implements OnInit {
  genres: GenreBooksCountModel[] = [];

  constructor(private bookService: BookService ) { }

  ngOnInit(): void {
    this.getGenreAndBookCount()
  }

  getGenreAndBookCount(){
    this.bookService.getGenres()
    .subscribe( genresWithBooksCount => {
      this.genres = genresWithBooksCount
    });
  }
}
