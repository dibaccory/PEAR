import { Component, OnInit } from '@angular/core';
import { AngularFireAuth } from '@angular/fire/auth';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  constructor(public afAuth: AngularFireAuth, private router: Router) {
  }

  title = 'PEAR';
  loggedIn = this.afAuth.auth.onAuthStateChanged;

  logout() {
    this.afAuth.auth.signOut()
      .then((res) => {
        console.log(res);
        this.router.navigate(['/splashpage']);
      });
  }

  ngOnInit() {
  }

}
