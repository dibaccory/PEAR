import { Injectable } from '@angular/core';
import { AngularFireAuth } from '@angular/fire/auth';
import { AngularFireDatabase } from '@angular/fire/database';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class StudentsService {

  studentUIDs: string[] = [];
  module;
  key;

  constructor(public afAuth: AngularFireAuth, public db: AngularFireDatabase, private router: Router) {
    this.module = this.db.database.ref('users').orderByKey();

    this.module.once('value')
      .then((snapshot) => {
        snapshot.forEach((childSnapshot) => {
          this.key = childSnapshot.key; // UID
          this.studentUIDs.push(this.key.toString());
        });
      });
    console.log(this.studentUIDs);
  }
}
