import { Component, OnInit } from '@angular/core';
// import { AngularFireAuth } from '@angular/fire/auth';
// import { Router } from '@angular/router';
// import { AuthService } from '../../services/auth.service';
// import { auth } from 'firebase';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

//   user = {
//     email: '',
//     password: ''
//  };

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

//   logout() {
//     this.afAuth.auth.signOut();
//   }

  ngOnInit() {
  }
}
