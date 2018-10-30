import { Component, OnInit } from '@angular/core';
import { AngularFireAuth } from '@angular/fire/auth';
import { Router } from '@angular/router';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  constructor(public afAuth: AngularFireAuth, private router: Router) {
  }

  userEmail = this.afAuth.auth.currentUser.email;
  user = this.afAuth.auth.currentUser;
  // email = this.afAuth.auth.currentUser.email;

  updateEmail() {
      this.user.updateEmail('test2@test.com');
        // .then(function () {
        //   // Update successful
        //   // this.email = this.afAuth.auth.currentUser.email;
        //   this.router.navigate(['/dashboard']);
        // }).catch(function (error) {
        //   // Error happened
        // });
  }

  updatePassword() {
  //   this.afAuth.auth.onAuthStateChanged(function(user) {

  //     user.reauthenticateAndRetrieveDataWithCredential(this.afAuth.auth.credential)
  //       .then(function() {
  //         // User re-authed
  //         // this.user.email = user.email;
  //       }).catch(function() {
  //         // Error happened
  //       });

      // user.updatePassword(ere)
      //   .then(function () {
      //     // Update successful
      //   }).catch(function (error) {
      //     // Error happened
      //   });
    // });
  }

  ngOnInit() {
  }

}
