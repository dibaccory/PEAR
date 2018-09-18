import { Component } from '@angular/core';
import { AngularFireDatabase } from '@angular/fire/database';
import { AngularFireAuth } from '@angular/fire/auth';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  // template: `
  // <h1>hi</h1>
  // <ul>
  //   <li *ngFor="let item of items | async">
  //     {{ item.name }}
  //   </li>
  // </ul>
  // `,
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'PEAR';
  items: Observable<any>;
  constructor(db: AngularFireDatabase) {
    this.items = db.object('items').valueChanges();
  }

  // writeUserData(db: AngularFireDatabase, userId, name, email) {
  //   firebase.database().ref('users/' + userId).set({
  //     username: name,
  //     email: email
  //   });
  // }
  
  // readUserData(db: AngularFireDatabase) {
  //   db.database.ref('/users/' + "Cqgkn1hpngSOM01Q859LMg36gBE2");
    // .once('value').then(function(snapshot) {
      // let user/name = (snapshot.val() && snapshot.val().username) || 'Anonymous';
    // }
    // return db.database.ref('/users/' + name).once('value').then(function(snapshot) {
    //   let username = (snapshot.val() && snapshot.val().username) || 'Anonymous';
    //   // ...
    // });
  // }
}
