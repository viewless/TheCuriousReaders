import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookSubscribersComponent } from './book-subscribers.component';
import {HttpClientTestingModule} from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';

describe('BookSubscribersComponent', () => {
  let component: BookSubscribersComponent;
  let fixture: ComponentFixture<BookSubscribersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ HttpClientTestingModule,
                 RouterTestingModule ],
      declarations: [ BookSubscribersComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BookSubscribersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
