import { Component, OnInit } from '@angular/core';
import { AngularFireAuth } from '@angular/fire/auth';
import { auth } from 'firebase/app';
import { Router } from '@angular/router';
import { AngularFireDatabase, AngularFireObject } from '@angular/fire/database';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  itemRef: AngularFireObject<any>;
  item: Observable<any>;

  database;

  user = {
    uid: '',
    // fName: 'Test First',
    // lName: 'Test Last',
    email: '',
    password: '',
    classCode: 'astronomy'
 };

  constructor(public afAuth: AngularFireAuth, private router: Router, db: AngularFireDatabase) {
    this.database = db;
    this.itemRef = db.object('users/buzbVbcYZwdjU04yT6l7xAkLkwk2');
    this.item = this.itemRef.valueChanges();
  }

  register() {
    this.afAuth.auth.createUserWithEmailAndPassword(this.user.email, this.user.password)
    // this.afAuth.auth.signInWithCustomToken(token)
      .then((res) => {
        console.log(res);
        // this.writeUserData(res.user.uid, this.user.fName, this.user.lName, this.user.email, this.user.password, this.user.classCode);
        this.router.navigate(['/dashboard']);
      })
      .catch((err) => {
        console.log('error: ' + err);
        alert('Email already exists');
        this.router.navigate(['/register']);
      });
  }

//   writeUserData(userId, fName, lName, email, password, classCode) {
//     this.itemRef = this.database.object('users/' + userId);
//     // firebase.database().ref('users/' + userId).update({
//       this.itemRef.update({
//       uid: userId,
//       firstName: fName,
//       lastName: lName,
//       email: email,
//       password: password,
//       class: classCode,
//   });
// }

  ngOnInit() {
  }

}
