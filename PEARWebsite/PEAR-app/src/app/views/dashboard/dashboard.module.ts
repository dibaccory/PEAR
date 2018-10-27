import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DashboardRoutingModule } from './dashboard-routing.module';

import { QuestionsComponent } from './dashboard-detail/questions.component';
import { DashboardComponent } from './dashboard-list/dashboard.component';

@NgModule({
  imports: [
    CommonModule,
    DashboardRoutingModule,
  ],
  declarations: [
    QuestionsComponent,
    DashboardComponent
  ]
})
export class DashboardModule { }
