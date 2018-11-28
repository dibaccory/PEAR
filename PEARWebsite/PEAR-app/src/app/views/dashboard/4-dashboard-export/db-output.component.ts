import { Component, OnInit } from '@angular/core';
import { StudentsService } from '../../../services/students.service';
import { AngularFireAuth } from '@angular/fire/auth';
import { AngularFireDatabase } from '@angular/fire/database';

@Component({
  selector: 'app-db-output',
  templateUrl: './db-output.component.html',
  styleUrls: ['./db-output.component.css']
})
export class DbOutputComponent implements OnInit {

  allStudents;
  key;
  result;

  constructor(public afAuth: AngularFireAuth, public db: AngularFireDatabase, public students: StudentsService) {
    this.allStudents = students.studentsInfo;
  }

  getOutput(uid) {
    const module = this.db.database.ref('answers/' + uid + '/astronomy/modules/solar system/').orderByKey();

    module.once('value')
      .then((snapshot) => {
        snapshot.forEach((childSnapshot) => {
          this.key = childSnapshot.key; // planet
          this.result = childSnapshot.toJSON();

        });
      });
    console.log(this.result);
  }

  ngOnInit() {
  }

}
