import { Component, OnInit } from '@angular/core';
import { AngularFireAuth } from '@angular/fire/auth';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { auth } from 'firebase';


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
          this.router.navigate(['dashboard'])
        })
        .catch((err) => console.log('error: ' + err));

    // this.afAuth.auth.signInWithEmailAndPassword(this.user.email, this.user.password).catch(function(error) {
    //   // Handle Errors here.
    //   var errorCode = error.code;
    //   var errorMessage = error.message;

    //   if (errorCode === 'auth/wrong-password') {
    //     alert('Wrong password.');
    //   } else {
    //     alert(errorMessage);
    //   }
    //   console.log(error);
    };

  logout() {
    this.afAuth.auth.signOut();
  }

  ngOnInit() {
  }
}
