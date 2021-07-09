import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DashboardComponent } from './dashboard.component';
import {HttpClientModule} from '@angular/common/http';
import { JwtModule } from '@auth0/angular-jwt';
import { JwtHelperService } from '@auth0/angular-jwt';
import { RouterTestingModule } from '@angular/router/testing';
import { NgxPaginationModule } from 'ngx-pagination';

describe('DashboardComponent', () => {
  let component: DashboardComponent;
  let fixture: ComponentFixture<DashboardComponent>;
  let jwtHelper: JwtHelperService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      providers: [JwtHelperService],
      imports: [ HttpClientModule,
                 RouterTestingModule,
                 NgxPaginationModule,
                 JwtModule.forRoot({
                  config: {
                    tokenGetter: () => {
                    return '';
                         }
                      }
                    }) ],
      declarations: [ DashboardComponent ]
    })
    .compileComponents();

    jwtHelper = TestBed.inject(JwtHelperService);
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
