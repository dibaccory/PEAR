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
  studentEmails: string[] = [];
  studentsInfo;
  classCodes: string[] = [];

  key;
  solarSystem;
  currentStudent;
  key2;
  solarSystem2;
  currentStudent2;

  attemptBuild;
  attemptCollect;
  timeSpentBuild;
  timeSpentCollect = [];
  data = [];

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
    this.studentsInfo = students.studentsInfo;
    this.classCodes = codes.classCodes;

    Object.assign(this, { single, single2, multi });
  }

  getAttempts(uid, email) {
    this.currentStudent = email;

    this.solarSystem = this.db.database.ref('answers/' + uid + '/astronomy/modules/solar system/').orderByKey();

    this.single2 = []; // reset drawing graph
    this.solarSystem.once('value')
      .then((snapshot) => {
        snapshot.forEach((childSnapshot) => {
          this.key = childSnapshot.key; // each key is a planet

          if (childSnapshot.child('collect/totalAttempts').exists()) {
            this.attemptCollect = childSnapshot.child('collect/totalAttempts').val();
          } else { this.attemptCollect = 0; }

          if (childSnapshot.child('build/totalAttempts').exists()) {
            this.attemptBuild = childSnapshot.child('build/totalAttempts').val();
          } else { this.attemptBuild = 0; }

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
        });
      });

      this.getTimeSpent(uid);
  }

  getTimeSpent(uid) {

    const planets = ['earth', 'jupiter', 'mars', 'mercury', 'neptune', 'saturn', 'sun', 'uranus', 'venus'];
    let pathExist = true;

    this.multi = [];
    this.timeSpentCollect = [];
    this.key2 = [];
    for (let planet of planets) {
      this.solarSystem2 = this.db.database.ref('answers/' + uid + '/astronomy/modules/solar system/' + planet + '/collect/attempts/')
        .orderByKey();

      if (this.solarSystem2 === null) {
        pathExist = false;
      }

      let sum = 0;
      let counter = 0;
      this.solarSystem2.once('value')
      .then((snapshot) => {
        snapshot.forEach((childSnapshot) => {
          this.key2 = childSnapshot.key; // each key is attempt key 1, 2, 3...
          console.log('bottom key:' + this.key2);

          sum += childSnapshot.child('/time spent').val();
          let te: number = childSnapshot.child('/time spent').val();
          this.timeSpentCollect.push({user: uid, planet: planet, attempt: this.key2.toString(), value: te});
          counter++;
        });
      });
      console.log(this.timeSpentCollect);
      console.log(sum);

      let average: number;

      if (counter > 1) {
        average = (sum / counter);
      }

      console.log(average);
      this.data.push({name: planet, value: average});
      console.log(this.data);
    }

    const entry3 = {
      'name': uid,
      'series': [
        {
          'name': 'earth2',
          'value': 0,
        },
        {
          'name': 'jupiter',
          'value': 10,
        },
        {
          'name': 'mars',
          'value': 5,
        },
        {
          'name': 'mercury',
          'value': 7,
        },
        {
          'name': 'neptune',
          'value': 20,
        },
        {
          'name': 'saturn',
          'value': 30,
        },
        {
          'name': 'sun',
          'value': 0,
        },
        {
          'name': 'uranus',
          'value': 0,
        },
        {
          'name': 'venus',
          'value': 10,
        },
      ]
    };
    this.multi = [...this.multi, entry3];
  }

  onSelect(event) {
    console.log(event);
  }

  ngOnInit() {
  }

}
