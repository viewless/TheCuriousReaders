import { NgModule } from '@angular/core';
import { Routes, RouterModule, Router } from '@angular/router';
import { AdminPanelComponent } from './components/admin-panel/admin-panel.component'; 
import { AdminAuthGuard } from './guards/admin-authguard-service';
import { AuthGuard } from './guards/authguard-service';
import { BookComponent } from './components/book/book.component';
import { BookDetailsComponent } from './components/book-details/book-details.component';
import { HomeComponent } from './components/home/home.component';
import { LandingPageComponent } from './components/landing-page/landing-page.component';
import { NotLoggedGuard } from './guards/not-logged-guard-service';
import { AdminViewSubscriptionsComponent } from './components/admin-view-subscriptions/admin-view-subscriptions.component';
import { RegisterComponent } from './components/register/register.component';
import { LoginComponent } from './components/login/login.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { ViewAllBooksComponent } from './components/view-all-books/view-all-books.component';



const routes: Routes = [
  { path: 'admin-panel', canActivate: [AdminAuthGuard],
  children: [
    { path: '', component: AdminPanelComponent},  
    { path: 'book-form', component: BookComponent},
    { path: 'subscriptions', component: AdminViewSubscriptionsComponent},
    { path: 'view-books', component: ViewAllBooksComponent}
  ]},
  { path: 'books/:id', component: BookDetailsComponent},
  { path: 'home', component: HomeComponent, canActivate: [AuthGuard]},
  { path: 'register', component: RegisterComponent, canActivate: [NotLoggedGuard]},
  { path: 'login', component: LoginComponent, canActivate: [NotLoggedGuard]},
  { path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard]}, 
  { path: '', component: LandingPageComponent, canActivate: [NotLoggedGuard] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [AuthGuard, AdminAuthGuard, NotLoggedGuard]
})
export class AppRoutingModule {

  constructor(private router: Router) {
    this.router.errorHandler = (error: any) => {
        this.router.navigate(['']);
    }
  }

}
