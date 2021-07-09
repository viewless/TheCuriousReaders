import {AuthorModel} from "./authormodel"
import {GenreModel} from "./genremodel";

export class NewBookModel {
    constructor(
    public id: number,
    public title: string,
    public description: string,
    public coverUri: string,
    public createdAt: Date,
    public quantity: number,
    public author: AuthorModel,
    public genre: GenreModel
    ){}
  }