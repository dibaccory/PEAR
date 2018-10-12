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
  title = 'PEAR';
  loggedIn = false;
  // user = this.afAuth.auth.currentUser;

  // user:
  // {
  //   email: string,
  //   password: string
  // };

  // constructor(public afAuth: AngularFireAuth) {
  //   this.afAuth.auth.onAuthStateChanged(function(user) {
  //     if (user) {
  //       // User is signed in.
  //       // const loggedIn = true;
  //     } else {
  //       // No user is signed in.
  //       // const loggedIn = false;
  //     }
  //   });
  // }
}
