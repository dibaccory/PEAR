import { Component, OnInit } from '@angular/core';
import { AngularFireAuth } from '@angular/fire/auth';
import { auth } from 'firebase/app';

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

  model = {
    left: true,
    middle: false,
    right: false
};

//   constructor(public afAuth: AngularFireAuth, private router: Router) {
//   }
//   login() {
//     this.afAuth.auth.signInWithEmailAndPassword(this.user.email, this.user.password)
//         .then((res) => {
//           console.log(res);
//           this.router.navigate(['dashboard'])
//         })
//         .catch((err) => {
//           console.log('error: ' + err);
//           alert('Wrong password.');
//           // this.router.navigate(['teachers'])
//         })
//     };


  // constructor(public afAuth: AngularFireAuth) {
  // }
  // constructor(public afAuth: AngularFireAuth) {
  // }

  // login() {
  //   // this.afAuth.auth.signInWithEmailAndPassword(this.user.email, this.user.password).catch(function(error) {
  //   //   const errorCode = error.code;
  //   //   const errorMessage: string = error.message;
  //   this.afAuth.auth.signInWithEmailAndPassword(this.user.email, this.user.password)
  //       .then((res) => {
  //         console.log(res);
  //       })
  //       .catch((err) => {
  //         console.log('error: ' + err);
  //         alert('Wrong password.');
  //   });
  // }

  // logout() {
  //   this.afAuth.auth.signOut();
  // }

  ngOnInit() {
  }
}
