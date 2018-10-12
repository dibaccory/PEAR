import { Component, OnInit } from '@angular/core';
import { AngularFireAuth } from '@angular/fire/auth';
import { auth } from 'firebase/app';
import { Router } from '@angular/router';

// import { AngularFireDatabase } from '@angular/fire/database';
// import { Observable } from 'rxjs';
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

// login() {
  // this.afAuth.auth.signInWithEmailAndPassword(this.user.email, this.user.password);
      // .then((res) => {
      //   console.log(res);
      //   this.router.navigate(['/splashpage']);
      // })
      // .catch((err) => {
      //   console.log('error: ' + err);
      //   alert('Wrong password.');
      //   // this.router.navigate(['teachers'])
      // });
  // }

login() {
  this.afAuth.auth.signInWithEmailAndPassword(this.user.email, this.user.password)
    .then((res) => {
      console.log(res);
      this.router.navigate(['dashboard']);
    })
    .catch(function(error) {
      // Handle Errors here.
      const errorCode = error.code;
      const errorMessage = error.message;
      if (errorCode === 'auth/wrong-password') {
        alert('Wrong password.');
      } else {
        alert(errorMessage);
      }
      console.log(error);
      // ...

    // this.afAuth.auth.signInWithEmailAndPassword(this.user.email, this.user.password)
    //   .then((res) => {
    //     console.log(res);
    //     this.router.navigate(['dashboard'])
    //   })
    //   .catch((err) => {
    //     console.log('error: ' + err);
    //     alert('Wrong password.');
    //     // this.router.navigate(['teachers'])
    //   }
  });
}

// logout() {
//   this.afAuth.auth.signOut();
// }

  ngOnInit() {
  }
}
