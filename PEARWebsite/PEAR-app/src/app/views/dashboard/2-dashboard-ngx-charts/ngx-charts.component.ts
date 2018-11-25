import { Component, OnInit } from '@angular/core';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { single, timeSpent } from './sample-data';
import { Observable, Subscription, BehaviorSubject } from 'rxjs';
import { switchMap } from 'rxjs/operators';

import { StudentsService } from '../../../services/students.service';
import { ClassCodesService } from '../../../services/class-code.service';

import { AngularFireAuth } from '@angular/fire/auth';
import { AngularFireDatabase, AngularFireAction } from '@angular/fire/database';

@Component({
  selector: 'app-ngx-charts',
  templateUrl: './ngx-charts.component.html',
  styleUrls: ['./ngx-charts.component.css']
})

export class NgxChartsComponent implements OnInit {

  studentUIDs: string[] = [];
  classCodes: string[] = [];

  key;
  attemptCollect;
  attemptBuild;
  solarSystem;

  single: any[];
  multi: any[];

  temp: Observable<any[]>;

  // ============ ngcx-charts ============== //

  showXAxis = true;
  showYAxis = true;
  gradient = false;
  showLegend = true;
  showXAxisLabel = true;
  xAxisLabel = 'Planet';
  showYAxisLabel = true;
  yAxisLabel = '# of Attempts';
  yAxisLabel2 = 'Time (sec)';

  colorScheme = {
    domain: ['#5AA454', '#A10A28', '#C7B42C', '#AAAAAA']
  };
  // ======================================= //

  constructor(public afAuth: AngularFireAuth, public db: AngularFireDatabase, public students: StudentsService,
    codes: ClassCodesService) {

    this.studentUIDs = students.studentUIDs;
    this.classCodes = codes.classCodes;

    // tslint:disable-next-line:prefer-const
    // for (let uid of this.studentUIDs) {

    // this.solarSystem = this.db.list('answers/Cqgkn1hpngSOM01Q859LMg36gBE2/astronomy/modules/solar system/', ref => ref.orderByKey());
    // }

    this.collectAttempts(this.studentUIDs[0]);

    console.log(single);
    Object.assign(this, { single, timeSpent });
  }

  collectAttempts(uid) {

    console.log('uid:' + uid);

    this.solarSystem = this.db.database.ref('answers/' + uid.toString() + '/astronomy/modules/solar system/').orderByKey();

    this.solarSystem.once('value')
      .then((snapshot) => {
        snapshot.forEach((childSnapshot) => {
          this.key = childSnapshot.key; // each key is a planet

          this.attemptCollect = childSnapshot.child('collect/totalAttempts').val();
          // this.attemptBuild = childSnapshot.child('build/totalAttempts').val();

          console.log('total attemtpt collect' + '|' + this.attemptCollect);

          const entry = {
            name: this.key.toString(),
            value: this.attemptCollect
          };
          this.single = [...this.single, entry];

          // this.single.push({'name': this.key.toString(), 'value': this.attemptCollect});
          // tslint:disable-next-line:curly
          if (this.single.length > 9) this.single.splice(0, 1);
        });
      });

    // return this.single;
  }

  onSelect(event) {
    console.log(event);
  }

  ngOnInit() {
  }

}
