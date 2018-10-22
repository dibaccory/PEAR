import { Component, OnInit } from '@angular/core';
import { AngularFireAuth } from '@angular/fire/auth';
import { auth } from 'firebase/app';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  user = {
    email: '',
    password: ''
  };

  constructor(public afAuth: AngularFireAuth, private router: Router) {
  }

login() {
  this.afAuth.auth.signInWithEmailAndPassword(this.user.email, this.user.password)
      .then((res) => {
        console.log(res);
        this.router.navigate(['/dashboard']);
      })
      .catch((err) => {
        console.log('error: ' + err);
        alert('Wrong email or password.');
        this.router.navigate(['/login']);
      });
  }

  ngOnInit() {
  }
}
