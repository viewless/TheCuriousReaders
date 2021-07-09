import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";

@Injectable({
    providedIn: 'root',
  })
  
export class AccountService {
    constructor(private http: HttpClient) {}

    getTotalAccounts(): Observable<number>{
        return this.http.get<number>(`${environment.baseUrl}/Accounts/total`);
    }
}