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

  user;
  email;
  password;
  name;
  classCode = '';

//   user = {
//     fName: 'Test First',
//     lName: 'Test Last',
//     email: '',
//     password: '',
//     classCode: 'astronomy'
//  };

  constructor(public afAuth: AngularFireAuth, private router: Router, public db: AngularFireDatabase) {
    // this.itemRef = db.object('teachers/');
    // this.item = this.itemRef.valueChanges();
    // this.user = this.afAuth.user;
  }

  register(name, email, password) {
    this.user = null;
    this.afAuth.auth.createUserWithEmailAndPassword(email, password)
      .then((res) => {
        // console.log(name);
        // console.log(email);
        // console.log(password);

        this.afAuth.auth.currentUser.updateProfile({ displayName: name, photoURL: '' });
        console.log(res);
        this.router.navigate(['/dashboard']);
      })
      .catch((err) => {
        console.log('error: ' + err);
        alert('Email already exists');
        this.router.navigate(['/register']);
      });
  }

  ngOnInit() {
  }

}
