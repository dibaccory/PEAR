import { Component, OnInit } from '@angular/core';
import { multiBar, multiLine } from './sample-data';
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

  planets;
  data = {
    earth: [],
    jupiter: [],
    mars: [],
    mercury: [],
    neptune: [],
    saturn: [],
    sun: [],
    uranus: [],
    venus: [],
  };

  // ============ ngcx-charts ============== //
  multiBar: any[];
  multiLine: any[];
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

    this.studentsInfo = students.studentsInfo;
    this.classCodes = codes.classCodes;

    Object.assign(this, { multiBar, multiLine });
  }

  getAttempts(uid, email) {
    this.currentStudent = email;

    this.solarSystem = this.db.database.ref('answers/' + uid + '/astronomy/modules/solar system/').orderByKey();

    this.multiBar = []; // reset drawing graph
    this.solarSystem.once('value')
      .then((snapshot) => {
        snapshot.forEach((childSnapshot) => {
          this.key = childSnapshot.key; // key - planet

          if (childSnapshot.child('collect/totalAttempts').exists()) {
            this.attemptCollect = childSnapshot.child('collect/totalAttempts').val();
          } else { this.attemptCollect = 0; }

          if (childSnapshot.child('build/totalAttempts').exists()) {
            this.attemptBuild = childSnapshot.child('build/totalAttempts').val();
          } else { this.attemptBuild = 0; }

          // double bar graph
          const entry = {
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
          this.multiBar = [...this.multiBar, entry];
        });
      });

      this.getTimeSpent(uid);
  }

  getTimeSpent(uid) {

    this.planets = ['earth', 'jupiter', 'mars', 'mercury', 'neptune', 'saturn', 'sun', 'uranus', 'venus'];

    this.multiLine = []; // reset drawing graph
    for (let planet of this.planets) {
      this.solarSystem2 = this.db.database.ref('answers/' + uid + '/astronomy/modules/solar system/' + planet + '/collect/attempts/')
        .orderByKey();

      this.solarSystem2.once('value')
        .then((snapshot) => {
          snapshot.forEach((childSnapshot) => {
            this.key2 = childSnapshot.key; // ekey is attempt - key 1, 2, 3...

            this.timeSpentCollect = [{
              attempt: this.key2.toString(),
              value: childSnapshot.child('time spent').val()
            }];

            this.data[planet] = this.timeSpentCollect;
          });
        });
    }

    console.log(this.data);
    console.log(this.data.earth);

    const entry2 = {
      'name': 'collect mode',
      'series': [
        {
          'name': 'earth2',
          'value': this.getValue(this.data.earth),
        },
        {
          'name': 'jupiter',
          'value': this.getValue(this.data.jupiter),
        },
        {
          'name': 'mars',
          'value': this.getValue(this.data.mars),
        },
        {
          'name': 'mercury',
          'value': this.getValue(this.data.mercury),
        },
        {
          'name': 'neptune',
          'value': this.getValue(this.data.neptune),
        },
        {
          'name': 'saturn',
          'value': this.getValue(this.data.saturn),
        },
        {
          'name': 'sun',
          'value': this.getValue(this.data.sun),
        },
        {
          'name': 'uranus',
          'value': this.getValue(this.data.uranus),
        },
        {
          'name': 'venus',
          'value': this.getValue(this.data.venus),
        },
      ]
    };
    this.multiLine = [...this.multiLine, entry2];
  }

  getValue(planet) {

    if (planet.length === 0) {
      return 0;
    }

    let value: number;
    for (let p of planet) {
      value = p.value;
    }
    return value;
  }

  onSelect(event) {
    console.log(event);
  }

  ngOnInit() {
  }

}
