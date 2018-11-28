import { Injectable } from '@angular/core';
import { AngularFireAuth } from '@angular/fire/auth';
import { AngularFireDatabase } from '@angular/fire/database';

@Injectable({
  providedIn: 'root'
})
export class PlanetKeyService {

  solarSystem;
  key;

  constructor(public afAuth: AngularFireAuth, public db: AngularFireDatabase) {

    // this.solarSystem = this.db.database.ref('answers/' + uid + '/astronomy/modules/solar system/').orderByKey();

    // this.solarSystem.once('value')
    //   .then((snapshot) => {
    //     snapshot.forEach((childSnapshot) => {
    //       this.key = childSnapshot.key; // each key is a planet
    //     });
    //   });
  }
}
