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
  buildAttempt;
  attemptBuild;
  attemptCollect;
  solarSystem;
  currentStudent;
  time: any[] = [];

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

    // this.collectAttempts(this.studentUIDs[0]);

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

          if (this.attemptBuild === null) {
            this.attemptBuild = 0;
          }
          if (this.attemptCollect === null) {
            this.attemptCollect = 0;
          }

          const entry = {
            name: this.key.toString(),
            value: this.attemptCollect
          };
          this.single = [...this.single, entry];

          const entry2 = {
            name: this.key.toString(),
            series: [
              {
                name: 'collect mode',
                value: this.attemptCollect
              },
              {
                name: 'build mode',
                value: this.attemptBuild
              }
            ]
          };
          this.single2 = [...this.single2, entry2];

          // tslint:disable-next-line:curly
          if (this.single.length > 9) this.single.splice(0, 1);
          // tslint:disable-next-line:curly
          if (this.single2.length > 9) this.single2.splice(0, 1);
        });
      });
  }

  // timeSpent(uid) {

  //   const ref = this.db.database.ref('answers/' + uid + '/astronomy/modules/solar system/').orderByKey();

  //   ref.once('value')
  //     .then((snapshot) => {
  //       snapshot.forEach((childSnapshot) => {
  //         const key = childSnapshot.key; // each key is a planet

  //         const newRef = this.db.database.ref(key).once('value')
  //           .then((snapshot2) => {
  //             snapshot2.forEach((childSnapshot2) => {
  //               const key2 = childSnapshot2.key;
  //               console.log('test:' + key2.toString());
  //               // this.time = childSnapshot.child('attempts');
  //             });
  //           });
  //       });
  //     });
  // }

  onSelect(event) {
    console.log(event);
  }

  ngOnInit() {
  }

}
