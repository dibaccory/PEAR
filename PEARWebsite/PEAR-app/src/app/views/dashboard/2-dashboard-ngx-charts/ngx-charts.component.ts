import { Component, OnInit } from '@angular/core';
import { single, single2, multi } from './sample-data';
import { StudentsService } from '../../../services/students.service';
import { ClassCodesService } from '../../../services/class-code.service';
import { AngularFireAuth } from '@angular/fire/auth';
import { AngularFireDatabase } from '@angular/fire/database';

@Component({
  selector: 'app-ngx-charts',
  templateUrl: './ngx-charts.component.html',
  styleUrls: ['./ngx-charts.component.css']
})

export class NgxChartsComponent implements OnInit {

  studentUIDs: string[] = [];
  classCodes: string[] = [];

  key;
  solarSystem;
  currentStudent;

  attemptBuild;
  attemptCollect;
  numBuildTimeSpent;
  numCollectTimeSpent;
  timeSpentBuild;
  timeSpentCollect;

  // ============ ngcx-charts ============== //
  single: any[];
  single2: any[];
  multi: any[];
  view: any[] = [700, 400];

  showXAxis = true;
  showYAxis = true;
  gradient = false;
  showLegend = true;
  showXAxisLabel = true;
  xAxisLabel = 'Planet';
  showYAxisLabel = true;
  yAxisLabel = '# of Attempts';
  yAxisLabel2 = 'Time (sec)';
  barPadding = 5;

  colorScheme = {
    domain: ['#5AA454', '#A10A28', '#C7B42C', '#AAAAAA']
  };
  // ======================================= //

  constructor(public afAuth: AngularFireAuth, public db: AngularFireDatabase, public students: StudentsService,
    codes: ClassCodesService) {

    this.studentUIDs = students.studentUIDs;
    this.classCodes = codes.classCodes;

    Object.assign(this, { single, single2, multi });
  }

  collectAttempts(uid) {
    this.currentStudent = uid;

    this.solarSystem = this.db.database.ref('answers/' + uid + '/astronomy/modules/solar system/').orderByKey();

    this.solarSystem.once('value')
      .then((snapshot) => {
        snapshot.forEach((childSnapshot) => {
          this.key = childSnapshot.key; // each key is a planet

          this.attemptCollect = childSnapshot.child('collect/totalAttempts').val();
          this.attemptBuild = childSnapshot.child('build/totalAttempts').val();
          this.numCollectTimeSpent = childSnapshot.child('collect/attempts').numChildren();
          this.numBuildTimeSpent = childSnapshot.child('build/attempts').numChildren();

          if (this.attemptBuild === null) {
            this.attemptBuild = 0;
          }
          if (this.attemptCollect === null) {
            this.attemptCollect = 0;
          }

          // single bar graph
          const entry = {
            name: this.key.toString(),
            value: this.attemptCollect
          };
          this.single = [...this.single, entry];

          // double bar graph
          const entry2 = {
            name: this.key.toString(),
            series: [
              {
                name: 'build mode',
                value: this.attemptBuild
              },
              {
                name: 'collect mode',
                value: this.attemptCollect
              }
            ]
          };
          this.single2 = [...this.single2, entry2];

          // tslint:disable-next-line:curly
          if (this.single.length > 9) this.single.splice(0, 1);
          // tslint:disable-next-line:curly
          if (this.single2.length > 9) this.single2.splice(0, 1);

          console.log('collect num:' + this.numCollectTimeSpent);
          // console.log('build num:' + this.numBuildTimeSpent);

          for (let j = 1; j <= this.numCollectTimeSpent; j++) {
            this.timeSpentCollect = childSnapshot.child('collect/attempts/' + j.toString() + '/time spent').val();

            if (this.timeSpentCollect === null) {
              this.timeSpentCollect = 0;
            }

            const name = 'Attempt' + ' ' + j.toString();

            const entry3 = {
              name: name,
              series: [
                {
                  name: this.key.toString(),
                  value: this.timeSpentCollect
                }
              ]
            };
            this.multi = [...this.multi, entry3];

            // tslint:disable-next-line:curly
            // if (this.multi.length > 9) this.multi.splice(0, 1);
          }
        });
      });
  }

  onSelect(event) {
    console.log(event);
  }

  ngOnInit() {
  }

}
