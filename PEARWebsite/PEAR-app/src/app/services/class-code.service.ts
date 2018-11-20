import { Injectable } from '@angular/core';
import { AngularFireAuth } from '@angular/fire/auth';
import { Router } from '@angular/router';
import { AngularFireDatabase } from '@angular/fire/database';

@Injectable({
  providedIn: 'root'
})
export class ClassCodesService {

  classCodes: string[] = [];
  key;
  module;

  constructor(public afAuth: AngularFireAuth, private router: Router, public db: AngularFireDatabase) {
    this.module = this.db.database.ref('classrooms').orderByKey();

    this.module.once('value')
      .then((snapshot) => {
        snapshot.forEach((childSnapshot) => {
          this.key = childSnapshot.key; // classCode name
          this.classCodes.push(this.key.toString());
        });
      });
    console.log(this.classCodes);
  }
}
