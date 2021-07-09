import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-total-readers',
  templateUrl: './total-readers.component.html',
  styleUrls: ['./total-readers.component.scss']
})
export class TotalReadersComponent implements OnInit {
  totalCount: number = 0;

  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
    this.getCountOfReaders();
  }

  getCountOfReaders(){
    this.accountService.getTotalAccounts()
    .subscribe(countOfReaders => {
      this.totalCount =  countOfReaders;
    })
  }

}
