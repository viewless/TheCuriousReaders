import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GenresBooksComponent } from './genres-books.component';
import {HttpClientTestingModule} from '@angular/common/http/testing';

describe('GenresBooksComponent', () => {
  let component: GenresBooksComponent;
  let fixture: ComponentFixture<GenresBooksComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ HttpClientTestingModule ],
      declarations: [ GenresBooksComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GenresBooksComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
