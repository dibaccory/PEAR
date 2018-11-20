import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DashboardRoutingModule } from './dashboard-routing.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

// Angularfire2
import { AngularFireFunctionsModule } from '@angular/fire/functions';

// My created components and modules
import { QuestionsComponent } from './3-dashboard-questions/questions.component';
import { DashboardComponent } from './dashboard-list/dashboard.component';
import { NgxChartsComponent } from './2-dashboard-ngx-charts/ngx-charts.component';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { DefaultComponent } from './1-dashboard-modules/default.component';
import { DbOutputComponent } from './4-dashboard-export/db-output.component';

@NgModule({
  imports: [
    CommonModule,
    NgbModule,
    AngularFireFunctionsModule,
    NgxChartsModule,
    DashboardRoutingModule,
  ],
  declarations: [
    QuestionsComponent,
    NgxChartsComponent,
    DashboardComponent,
    DefaultComponent,
    DbOutputComponent
  ]
})
export class DashboardModule { }
