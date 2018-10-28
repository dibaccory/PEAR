import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DashboardRoutingModule } from './dashboard-routing.module';

// Angularfire2
import { AngularFireFunctionsModule } from '@angular/fire/functions';

// My created components and modules
import { QuestionsComponent } from './dashboard-detail/questions.component';
import { DashboardComponent } from './dashboard-list/dashboard.component';
import { NgxChartsComponent } from './ngx-charts/ngx-charts.component';
import { NgxChartsModule } from '@swimlane/ngx-charts';

@NgModule({
  imports: [
    CommonModule,
    AngularFireFunctionsModule,
    NgxChartsModule,
    DashboardRoutingModule,
  ],
  declarations: [
    QuestionsComponent,
    NgxChartsComponent,
    DashboardComponent
  ]
})
export class DashboardModule { }
