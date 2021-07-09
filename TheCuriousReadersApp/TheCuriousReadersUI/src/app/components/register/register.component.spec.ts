import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterComponent } from './register.component';
import {HttpClientTestingModule} from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { JwtModule } from '@auth0/angular-jwt';
import { JwtHelperService } from '@auth0/angular-jwt';
import { FormsModule }   from '@angular/forms';

describe('RegisterComponent', () => {
  let component: RegisterComponent;
  let fixture: ComponentFixture<RegisterComponent>;
  let jwtHelper: JwtHelperService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      providers: [JwtHelperService],
      imports: [ HttpClientTestingModule,
                 RouterTestingModule,
                 FormsModule,
                 JwtModule.forRoot({
                  config: {
                    tokenGetter: () => {
                      return '';
                      }
                    }
                  })],
      declarations: [ RegisterComponent ]
    })
    .compileComponents();

    jwtHelper = TestBed.inject(JwtHelperService);
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RegisterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
