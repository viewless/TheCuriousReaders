import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewAllBooksComponent } from './view-all-books.component';

describe('ViewAllBooksComponent', () => {
  let component: ViewAllBooksComponent;
  let fixture: ComponentFixture<ViewAllBooksComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewAllBooksComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewAllBooksComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
