import { BookModel } from "./bookmodel";

export class PaginatedBookModel {
    constructor(
    public bookResponse: BookModel[],
    public countOfNewBooks: number
    ){}
  }