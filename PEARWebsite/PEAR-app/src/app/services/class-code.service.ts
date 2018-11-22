import { Injectable } from '@angular/core';
import { AngularFireAuth } from '@angular/fire/auth';
import { AngularFireDatabase } from '@angular/fire/database';

@Injectable({
  providedIn: 'root'
})
export class ClassCodesService {

  classCodes: string[] = [];

  constructor(public afAuth: AngularFireAuth, public db: AngularFireDatabase) {
    const module = this.db.database.ref('classrooms').orderByKey();

    module.once('value')
      .then((snapshot) => {
        snapshot.forEach((childSnapshot) => {
          const key = childSnapshot.key; // classCode name
          this.classCodes.push(key.toString());
        });
      });
    console.log(this.classCodes);
  }
}
