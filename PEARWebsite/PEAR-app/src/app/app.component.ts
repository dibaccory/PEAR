import { Component } from '@angular/core';
import { AngularFireAuth } from '@angular/fire/auth';
import { auth } from 'firebase/app';
// import { AngularFireDatabase } from '@angular/fire/database';
// import { AngularFireAuth } from '@angular/fire/auth';
// import { auth } from 'firebase/app';
// import { Observable } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {

  constructor(public afAuth: AngularFireAuth) {
  }

  title = 'PEAR';
  loggedIn = this.afAuth.auth.currentUser;

  // loggedInF() {
  //   this.afAuth.auth.onAuthStateChanged(function(user) {
  //     if (user) {
  //       // User is logged in
  //     } else {
  //       // User is logged out
  //     }
  //   });
  // }
}
