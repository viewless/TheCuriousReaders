import { NewBookModel } from "./newbookmodel";

export class PaginatedNewBookModel {
    constructor(
    public newBooks: NewBookModel[],
    public totalCount: number
    ){}
}