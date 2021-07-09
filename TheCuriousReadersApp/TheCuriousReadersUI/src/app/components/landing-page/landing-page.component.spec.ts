import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LandingPageComponent } from './landing-page.component';
import { RouterTestingModule } from '@angular/router/testing';
import { JwtModule } from '@auth0/angular-jwt';
import { JwtHelperService } from '@auth0/angular-jwt';

describe('LandingPageComponent', () => {
  let component: LandingPageComponent;
  let fixture: ComponentFixture<LandingPageComponent>;
  let jwtHelper: JwtHelperService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      providers: [JwtHelperService],
      imports: [RouterTestingModule,
                JwtModule.forRoot({
                config: {
                  tokenGetter: () => {
                    return '';
                    }
                  }
                })],
      declarations: [LandingPageComponent],
    })
      .compileComponents();

      jwtHelper = TestBed.inject(JwtHelperService);
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LandingPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
