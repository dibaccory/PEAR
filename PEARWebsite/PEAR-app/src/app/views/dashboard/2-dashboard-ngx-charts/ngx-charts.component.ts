import { Component, OnInit } from '@angular/core';
import { NgxChartsModule } from '@swimlane/ngx-charts';
// import { BrowserModule } from '@angular/platform-browser';
import { attempts, timeSpent } from './sample-data';
import { StudentsService } from '../../../services/students.service';

import { AngularFireAuth } from '@angular/fire/auth';
import { AngularFireDatabase } from '@angular/fire/database';

@Component({
  selector: 'app-ngx-charts',
  templateUrl: './ngx-charts.component.html',
  styleUrls: ['./ngx-charts.component.css']
})
export class NgxChartsComponent implements OnInit {

  totalAttempts: number[] = [];
  studentUIDs: string[] = [];
  key;
  solarSystem;
  userID;

  planetAttempt = {
    earth: '',
    jupiter: '',
    mars: '',
    mercury: '',
    neptune: '',
    saturn: '',
    sun: '',
    uranus: '',
    venus: '',
  };

  // ngcx-charts ==========================

  // Options
  showXAxis = true;
  showYAxis = true;
  gradient = false;
  showLegend = true;
  showXAxisLabel = true;
  xAxisLabel = 'Student';
  showYAxisLabel = true;
  yAxisLabel = '# of Attempts';

  colorScheme = {
    domain: ['#5AA454', '#A10A28', '#C7B42C', '#AAAAAA']
  };
  // =======================================

  constructor(public afAuth: AngularFireAuth, db: AngularFireDatabase, public studentService: StudentsService) {

    this.studentUIDs = studentService.studentUIDs;

    this.userID = afAuth.auth.currentUser.uid;
    this.solarSystem = db.database.ref('answers/' + this.userID + '/astronomy/modules/solar system/').orderByKey();

    this.solarSystem.once('value')
      .then((snapshot) => {
        snapshot.forEach((childSnapshot) => {
          this.key = childSnapshot.key; // each key is a planet
          const attempt = childSnapshot.child('collect/totalAttempts').val(); // totalAttempts value

          this.totalAttempts.push(attempt);
          this.planetAttempt[this.key] = attempt;
        });
      });

    Object.assign(this, { attempts, timeSpent });
  }

  onSelect(event) {
    console.log(event);
  }

  ngOnInit() {
  }

}
