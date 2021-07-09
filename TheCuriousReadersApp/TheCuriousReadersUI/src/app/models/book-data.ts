import { AuthorModel } from "./authormodel";
import { GenreModel } from "./genremodel";

export interface BookData {
    genre: GenreModel;
    author: AuthorModel;
    id: string;
    title: string;
    quantity: number;
    description: string;
    coverUri: string;
    isAvailable: boolean;
  }
  