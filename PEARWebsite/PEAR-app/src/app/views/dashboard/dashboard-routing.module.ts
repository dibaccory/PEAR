import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { DashboardComponent } from './dashboard-list/dashboard.component';
import { DefaultComponent } from './1-dashboard-modules/default.component';
import { QuestionsComponent } from './3-dashboard-questions/questions.component';
import { NgxChartsComponent } from './2-dashboard-ngx-charts/ngx-charts.component';
import { DbOutputComponent } from './4-dashboard-export/db-output.component';

const routes: Routes = [
  {
    path: 'dashboard',
    component: DashboardComponent,
    children: [
      { path: 'default', component: DefaultComponent },
      { path: 'test', component: QuestionsComponent },
      { path: 'test2', component: NgxChartsComponent },

      // { path: 'test', component: DefaultComponent },
      // { path: 'test', component: QuestionsComponent, outlet: 'questions' },
      // { path: 'test', component: NgxChartsComponent, outlet: 'stats' },
      // { path: '', component: DbOutputComponent },
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardRoutingModule { }
