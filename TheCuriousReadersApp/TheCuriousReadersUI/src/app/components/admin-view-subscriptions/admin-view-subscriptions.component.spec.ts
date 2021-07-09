import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminViewSubscriptionsComponent } from './admin-view-subscriptions.component';
import {HttpClientTestingModule} from '@angular/common/http/testing';


describe('AdminViewSubscriptionsComponent', () => {
  let component: AdminViewSubscriptionsComponent;
  let fixture: ComponentFixture<AdminViewSubscriptionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      declarations: [ AdminViewSubscriptionsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminViewSubscriptionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
