import { AuthorModel } from './authormodel';
import { GenreModel } from './genremodel';

export class SearchResponseModel {
    constructor(
        public id: number,
        public title: string,
        public coverUri: string, 
        public author: AuthorModel,
        public genre: GenreModel
    ){}
}