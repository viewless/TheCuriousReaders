import {AuthorModel} from "./authormodel"
import { GenreModel } from "./genremodel";

export class BookModel {
    constructor(
    public title: string,
    public description: string,
    public quantity: number,
    public author: AuthorModel,
    public genre: GenreModel
    ){}
}