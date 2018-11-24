import { Component, OnInit } from '@angular/core';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { attempts2, timeSpent } from './sample-data';

import { StudentsService } from '../../../services/students.service';
import { ClassCodesService } from '../../../services/class-code.service';

import { AngularFireAuth } from '@angular/fire/auth';
import { AngularFireDatabase } from '@angular/fire/database';
import { constants } from 'perf_hooks';

@Component({
  selector: 'app-ngx-charts',
  templateUrl: './ngx-charts.component.html',
  styleUrls: ['./ngx-charts.component.css']
})

export class NgxChartsComponent implements OnInit {

  studentUIDs: string[] = [];
  classCodes: string[] = [];

  // key;
  // attemptCollect;
  // attemptBuild;
  // solarSystem;

  attempts2: any[];

  // attempts3: Array<{ 'name': string, 'value': number }> = [];

  // attempts = [
  //   { 'name': 't1', 'value': 1 },
  //   { 'name': 't2', 'value': 2 },
  //   { 'name': 't3', 'value': 3 }
  // ];

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
  intervalID;

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
    // this.solarSystem = this.db.database.ref('answers/Cqgkn1hpngSOM01Q859LMg36gBE2/astronomy/modules/solar system/').orderByKey();

    // this.solarSystem.once('value')
    //   .then((snapshot) => {
    //     snapshot.forEach((childSnapshot) => {
    //       this.key = childSnapshot.key; // each key is a planet

    //       this.attemptCollect = childSnapshot.child('collect/totalAttempts').val();
    //       this.attemptBuild = childSnapshot.child('build/totalAttempts').val();

    //       console.log('total attemtpt collect' + '|' + this.attemptCollect);

    //       this.attempts.push({'name': this.key.toString(), 'value': this.attemptCollect});
    //       // this.temp.push(this.key.toString(), this.attemptCollect);
    //     });
    //   });
    // }

    // this.attempts.push({ 'name': 'temp', 'value': 5 });

    // console.log(this.attempts);
    console.log(attempts2);

    // const att = this.attempts;
    Object.assign(this, {attempts2, timeSpent });
    // Object.assign(this, { attempts, timeSpent });

    this.intervalID = setInterval(() => {
      this.attempts2 = [...this.addRandomValue()];
    }, 2000);
  }

  addRandomValue() {
    const value = Math.random() * 1000000;
    this.attempts2.push({ 'name': new Date, 'value': value });
    // tslint:disable-next-line:curly
    if (this.attempts2.length > 9) this.attempts2.splice(0, 1);

    return this.attempts2;
  }

  onSelect(event) {
    console.log(event);
  }

  ngOnInit() {
  }

}
