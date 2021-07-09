import { TestBed } from '@angular/core/testing';

import { AuthService } from './auth.service';
import { JwtModule } from '@auth0/angular-jwt';
import { JwtHelperService } from '@auth0/angular-jwt';
import { RouterTestingModule } from '@angular/router/testing';

describe('AuthService', () => {
  let service: AuthService;
  let jwtHelper: JwtHelperService;
  let serviceMock: jasmine.Spy;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [JwtHelperService,
                  AuthService],
      imports: [RouterTestingModule,
                JwtModule.forRoot({
                  config: {
                    tokenGetter: () => {
                      return '';
                    }
                  }
                })]
    });

    service = TestBed.inject(AuthService);
    jwtHelper = TestBed.inject(JwtHelperService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should return false if user is not admin', () => {
    serviceMock = spyOn(service, "isUserAdmin").and.returnValue(false);

    expect(service.isUserAdmin()).toBe(false, 'service returned false');
  });

  it('should return true if user is admin', () => {
    serviceMock = spyOn(service, "isUserAdmin").and.returnValue(true);

    expect(service.isUserAdmin()).toBe(true, 'service returned true');
  });

  it('should return false if user not authenticated', () => {
    serviceMock = spyOn(service, "isUserAuthenticated").and.returnValue(false);

    expect(service.isUserAuthenticated()).toBe(false, 'service returned false');
  });
});
