import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewBooksComponent } from './new-books.component';
import {HttpClientModule} from '@angular/common/http';
import { NgxPaginationModule } from 'ngx-pagination';

describe('NewBooksComponent', () => {
  let component: NewBooksComponent;
  let fixture: ComponentFixture<NewBooksComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ HttpClientModule,
                 NgxPaginationModule ],
      declarations: [ NewBooksComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NewBooksComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
