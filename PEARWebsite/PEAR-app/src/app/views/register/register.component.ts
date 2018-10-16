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

  user = {
    fName: 'Test First',
    lName: 'Test Last',
    email: '',
    password: '',
    classCode: 'Astronomy'
 };

    constructor(public afAuth: AngularFireAuth, private router: Router, db: AngularFireDatabase) {
      this.itemRef = db.object('users');
      this.item = this.itemRef.valueChanges();
  }

  register() {
    this.afAuth.auth.createUserWithEmailAndPassword(this.user.email, this.user.password)
    // this.afAuth.auth.signInWithCustomToken(token)
      .then((res) => {
        console.log(res);
        this.writeUserData(Math.floor(Math.random() * 10) + 1,
          this.user.fName, this.user.lName, this.user.email, this.user.password, this.user.classCode);
        this.router.navigate(['/dashboard']);
      });
        // .catch((err) => {
        //   console.log('error: ' + err);
        //   alert('Wrong password.');
        //   this.router.navigate(['/login']);
        // });
  }

  writeUserData(userId, fName, lName, email, password, classCode) {
    // firebase.database().ref('users/' + userId).set({
      this.itemRef.set({
      firstName: fName,
      lastName: lName,
      email: email,
      password: password,
      class: classCode,
  });
}

  ngOnInit() {
  }

}
