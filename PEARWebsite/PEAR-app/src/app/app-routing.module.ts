import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { TeachersComponent } from './teachers/teachers.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { TeacherDetailComponent } from './teacher-detail/teacher-detail.component';
import { LoginComponent } from './login/login.component';

const routes: Routes = [
  { path: 'teachers', component: TeachersComponent },
  { path: 'detail/:id', component: TeacherDetailComponent },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'login', component: LoginComponent},
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' }
];

@NgModule({
  exports: [RouterModule],
  imports: [RouterModule.forRoot(routes)]
})
export class AppRoutingModule { }
