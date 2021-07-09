import { SearchResponseModel } from "./searchResponseModel";

export class PaginatedNewSearchResultsModel {
    constructor(
    public books: SearchResponseModel[],
    public totalCount: number
    ){}
}