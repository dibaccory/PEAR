import { Component } from '@angular/core';
import { AngularFireAuth } from '@angular/fire/auth';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {

  constructor(public afAuth: AngularFireAuth) {
  }

  title = 'PEAR';
  loggedIn;

  loggedInF() {
    this.afAuth.auth.onAuthStateChanged(function(user) {
      if (user) {
        this.loggedIn = true;
      } else {
        this.loggedIn = false;
      }
    });
  }
}
