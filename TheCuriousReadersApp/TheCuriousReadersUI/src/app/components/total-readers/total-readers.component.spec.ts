import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TotalReadersComponent } from './total-readers.component';
import {HttpClientTestingModule} from '@angular/common/http/testing';

describe('TotalReadersComponent', () => {
  let component: TotalReadersComponent;
  let fixture: ComponentFixture<TotalReadersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ HttpClientTestingModule ],
      declarations: [ TotalReadersComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TotalReadersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
