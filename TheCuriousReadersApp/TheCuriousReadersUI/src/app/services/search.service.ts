import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { SearchModel } from '../models/searchmodel';
import { PaginatedNewSearchResultsModel } from '../models/paginatednewsearchresultsmodel';

@Injectable({
  providedIn: 'root'
})

export class SearchService {
  constructor(private http: HttpClient) {}

  search(searchModel: SearchModel, pageNumber: number, pageSize: number) : Observable<PaginatedNewSearchResultsModel>{    
    let queryString: string = `${environment.baseUrl}/Books/search?PageNumber=${pageNumber}&PageSize=${pageSize}`;

    if (searchModel.parameters.indexOf("Book title") > - 1){
      queryString += `&BookTitle=${searchModel.text}`;
    } 
    if (searchModel.parameters.indexOf("Book author") > - 1){
      queryString += `&AuthorName=${searchModel.text}`;
    } 
    if(searchModel.parameters.indexOf("Book genre") > - 1){
      queryString += `&GenreName=${searchModel.text}`;
    }
    if (searchModel.parameters.indexOf("Description keywords") > - 1){
      queryString += `&BookDescription=${searchModel.text}`;
    } 
    if (searchModel.parameters.indexOf("Comments keywords") > - 1){
      queryString += `&CommentBody=${searchModel.text}`;
    }

    return this.http.get<PaginatedNewSearchResultsModel>(
      queryString,
      { headers: { 'Content-Type': 'application/json' } }
    );
  }
}
