import { Injectable } from '@angular/core';
import { AngularFireAuth } from '@angular/fire/auth';
import { AngularFireDatabase } from '@angular/fire/database';

@Injectable({
  providedIn: 'root'
})
export class TeachersService {

  teacherUIDs: string[] = [];

  constructor(public afAuth: AngularFireAuth, public db: AngularFireDatabase) {
    const module = this.db.database.ref('teachers').orderByChild('uid');

    module.once('value')
      .then((snapshot) => {
        snapshot.forEach((childSnapshot) => {
          const key = childSnapshot.key; // UID
          this.teacherUIDs.push(key.toString());
        });
      });
    console.log(this.teacherUIDs);
  }
}
