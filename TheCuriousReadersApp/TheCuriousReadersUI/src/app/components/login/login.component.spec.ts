import { ComponentFixture, TestBed } from '@angular/core/testing';
import { LoginComponent } from './login.component';
import { RouterTestingModule } from '@angular/router/testing';
import {HttpClientModule} from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { JwtModule} from '@auth0/angular-jwt';

describe('LoginComponent', () => {
  let component: LoginComponent;
  let fixture: ComponentFixture<LoginComponent>;
  let jwtHelper: JwtHelperService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      providers: [JwtHelperService],
      imports: [ RouterTestingModule,
                 HttpClientModule,
                 JwtModule.forRoot({
                  config: {
                    tokenGetter: () => {
                      return '';
                    }
                  }
                })],
      declarations: [ LoginComponent ]
    })
    .compileComponents();

    jwtHelper = TestBed.inject(JwtHelperService);
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LoginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
