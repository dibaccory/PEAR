import { Component, OnInit } from '@angular/core';
import { AngularFireAuth } from '@angular/fire/auth';
import { AngularFireDatabase } from '@angular/fire/database';
import { Router } from '@angular/router';
import { TeachersService } from 'src/app/services/teachers.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  module;
  UID: string;
  key;
  isATeacher = false;
  teacherUIDs: string[] = [];

  email;
  password;

  constructor(public afAuth: AngularFireAuth, public db: AngularFireDatabase, private router: Router,
    public teacherService: TeachersService) {
      this.teacherUIDs = teacherService.teacherUIDs;
  }

  login() {
    this.afAuth.auth.signInWithEmailAndPassword(this.email, this.password)
      .then((res) => {
        this.UID = res.user.uid;
        this.checkIfTeacher();
      })
      .catch((err) => {
        console.log('error: ' + err);
        alert('Wrong email or password.');
        this.router.navigate(['/login']);
      });
  }

  checkIfTeacher() {
    // tslint:disable-next-line:prefer-const
    for (let uid of this.teacherUIDs) {
      if (this.UID === uid) {
        this.isATeacher = true;
      }
    }

    const user = this.afAuth.auth.currentUser;

    if (this.isATeacher === true) {
      this.router.navigate(['/dashboard']);
    } else {
      this.afAuth.auth.signOut()
        .then((res) => {
          console.log(res);
          alert('This is not a registered teacher account');
          this.router.navigate(['/login']);
        });
    }
  }

  ngOnInit() {
  }
}
