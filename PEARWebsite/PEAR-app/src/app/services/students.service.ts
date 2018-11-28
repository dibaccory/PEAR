import { Injectable } from '@angular/core';
import { AngularFireAuth } from '@angular/fire/auth';
import { AngularFireDatabase } from '@angular/fire/database';

@Injectable({
  providedIn: 'root'
})
export class StudentsService {

  studentUIDs: string[] = [];
  studentEmails: string[] = [];
  studentsInfo = [];

  constructor(public afAuth: AngularFireAuth, public db: AngularFireDatabase) {
    const module = this.db.database.ref('users').orderByKey();

    module.once('value')
      .then((snapshot) => {
        snapshot.forEach((childSnapshot) => {
          const key = childSnapshot.key; // UID
          this.studentUIDs.push(key.toString());
          this.studentEmails.push(childSnapshot.child('email').val());
          this.studentsInfo.push({uid: key.toString(), email: childSnapshot.child('email').val()});
        });
      });
    // console.log(this.studentUIDs);
    // console.log(this.studentEmails);
    console.log(this.studentsInfo);
  }
}
