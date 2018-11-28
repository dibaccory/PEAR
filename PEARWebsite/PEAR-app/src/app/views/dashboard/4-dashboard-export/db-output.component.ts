import { Component, OnInit } from '@angular/core';
import { StudentsService } from '../../../services/students.service';
import { AngularFireAuth } from '@angular/fire/auth';
import { AngularFireDatabase } from '@angular/fire/database';
import * as json2csv from 'json2csv';

@Component({
  selector: 'app-db-output',
  templateUrl: './db-output.component.html',
  styleUrls: ['./db-output.component.css']
})
export class DbOutputComponent implements OnInit {

  allStudents;
  key;
  result;
  result2;

  constructor(public afAuth: AngularFireAuth, public db: AngularFireDatabase, public students: StudentsService) {
    this.allStudents = students.studentsInfo;
  }

  getOutput(uid) {
    const module = this.db.database.ref('answers/' + uid + '/astronomy/modules/solar system/').orderByKey();

    module.once('child_added')
      .then((snapshot) => {
        snapshot.forEach((childSnapshot) => {
          this.key = childSnapshot.key; // planet
          this.result = childSnapshot.toJSON();
          this.result2 = snapshot.toJSON();
          console.log(this.key);
        });
      });
    console.log(this.result);

    const link = 'https://pear-f60a2.firebaseio.com/answers/' + uid + '/astronomy/modules/solar system/.json';
    window.open('https://json-csv.com?u=' + link);
  }

  getOutputAll() {
    const link = 'https://pear-f60a2.firebaseio.com/answers/.json';
    window.open('https://json-csv.com?u=' + link);
  }

  ngOnInit() {
  }

}
