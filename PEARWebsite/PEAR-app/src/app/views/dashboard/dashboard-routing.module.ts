import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { DashboardComponent } from './dashboard-list/dashboard.component';
import { QuestionsComponent } from './dashboard-detail/questions.component';
import { NgxChartsComponent } from './ngx-charts/ngx-charts.component';

const routes: Routes = [
  { path: 'dashboard', component: DashboardComponent },
  { path: 'questions', component: QuestionsComponent },
  { path: 'charts', component: NgxChartsComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardRoutingModule { }
