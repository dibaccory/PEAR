import { Component, OnInit } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { attempts, timeSpent } from './sample-data';

import { AngularFireAuth } from '@angular/fire/auth';
import { AngularFireDatabase, AngularFireAction, snapshotChanges } from '@angular/fire/database';
import { Observable, Subscription, BehaviorSubject } from 'rxjs';
import { switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-ngx-charts',
  templateUrl: './ngx-charts.component.html',
  styleUrls: ['./ngx-charts.component.css']
})
export class NgxChartsComponent implements OnInit {

  totalAttempts: number[] = [];
  key;
  solarSystem;
  userID;
  testEarth;

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
  attempts: any[];
  timeSpent: any[];

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

  constructor(public afAuth: AngularFireAuth, db: AngularFireDatabase) {

    this.userID = afAuth.auth.currentUser.uid;
    this.solarSystem = db.database.ref('answers/' + this.userID + '/astronomy/modules/solar system/').orderByKey();

    this.solarSystem.once('value')
      .then((snapshot) => {
        // this.testEarth = snapshot.child('earth/collect').exists(); // check path exists; debugging

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
