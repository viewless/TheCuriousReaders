import { BookData } from "./book-data";

export class PaginatedBookDataModel {
    constructor(
    public books: BookData[],
    public totalCount: number
    ){}
}