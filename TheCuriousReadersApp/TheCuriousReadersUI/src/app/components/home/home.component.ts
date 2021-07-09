import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { routePaths } from '../../constants/routes';
import { SearchModel } from 'src/app/models/searchmodel';
import { SearchService } from "../../services/search.service";
import { SearchResponseModel } from "../../models/searchResponseModel";
import { PaginatedNewSearchResultsModel } from '../../models/paginatednewsearchresultsmodel';
import { paginationSearch } from 'src/app/constants/paginationsearch';
import { IDropdownSettings } from 'ng-multiselect-dropdown';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})

export class HomeComponent implements OnInit {
  textToSearchFor: string = '';
  errorMsg: string = '';
  searchResults: SearchResponseModel[] = [];
  successMsg: string = '';
  searchPageNumber: number = paginationSearch.pageNumber;
  searchPageSize: number = paginationSearch.pageSize;
  searchData = new SearchModel(new Array<string>(), "");
  bookCountInResponse = new PaginatedNewSearchResultsModel(this.searchResults, 0);

  dropdownList: Array<string> = [];
  selectedItems: Array<string> = [];
  dropdownSettings!: IDropdownSettings;

  constructor(private authService: AuthService, private router: Router, private searchService: SearchService) {}

  ngOnInit(): void {
    this.dropdownList = [
     'Book title',
     'Book author',
     'Book genre',
     'Description keywords',
     'Comments keywords',
    ];

    this.dropdownSettings = {
      singleSelection: false,
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      itemsShowLimit: 0,
      allowSearchFilter: true,
    };
  }

  logOut() {
    this.authService.logOut();
  }

  openBookDetails(id: number): void {    
    this.router.navigate([routePaths.bookDetails.concat(id.toString())]);
  }

  openAdminPage(): void {
    this.router.navigate([routePaths.adminPanel]);
  }
  
  openDashboard(): void {
    this.router.navigate([routePaths.dashboardPage]);
  }

  onSubmit(): void {
    const data: SearchModel = {
      parameters: this.selectedItems,
      text: this.textToSearchFor
    }

    if (!data.text) {
      this.errorMsg = "Please enter text to search for.";
    } else if (!data.text.trim()) {
      this.errorMsg = "You can't search only for whitespaces!";
    } else if (!data.parameters) {
      this.errorMsg = "Please enter a parameter to search for."
    } else {
      this.searchData = data;
      this.getSearchResults(data);
    }
  }

  clearErrorMessage(): void {
    this.errorMsg = '';
  }

  clearSuccessMessage(): void {
    this.successMsg = '';
  }

  getSearchResults(data: SearchModel) {
    const {
      searchPageNumber,
      searchPageSize
    } = this;

    this.searchService.search(data, searchPageNumber, searchPageSize)
      .subscribe(res => {
        this.bookCountInResponse.totalCount = res.totalCount
        this.bookCountInResponse.books = res.books

      }, error => {
        this.errorMsg = "No results.";
      });
  }

  getCurrentPageData(newPage: number) {
    this.searchPageNumber = newPage;

    const {
      searchPageNumber,
      searchPageSize
    } = this;

    this.searchService.search(this.searchData, searchPageNumber, searchPageSize)
      .subscribe(newBooks => {
        this.bookCountInResponse.totalCount = newBooks.totalCount
        this.bookCountInResponse.books = newBooks.books
      })
  }

  isUserAdmin() : boolean {
    return this.authService.isUserAdmin();
  }
}
