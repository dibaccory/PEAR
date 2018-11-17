import { Component, OnInit } from '@angular/core';
import { AngularFireAuth } from '@angular/fire/auth';
import { Router } from '@angular/router';
import { AngularFireDatabase } from '@angular/fire/database';
import { ClassCodesService } from '../../services/class-code.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  email;
  password;
  classCode;

  classCodes: string[] = [];
  isAValidClass = false;

  constructor(public afAuth: AngularFireAuth, private router: Router, public db: AngularFireDatabase,
    public classCodeService: ClassCodesService) {
      this.classCodes = classCodeService.classCodes;
  }

  register() {
    this.afAuth.auth.createUserWithEmailAndPassword(this.email, this.password)
      .then((res) => {
        this.checkClassCode();
      })
      .catch((err) => {
        console.log('error: ' + err);
        alert('Email already exists');
        this.router.navigate(['/register']);
      });
  }

  checkClassCode() {
    // tslint:disable-next-line:prefer-const
    for (let code of this.classCodes) {
      if (this.classCode === code) {
        this.isAValidClass = true;
      }
    }

    // console.log('isValidClassCode ' + this.isAValidClass);
    const user = this.afAuth.auth.currentUser;

    if (this.isAValidClass === true) {
      this.db.database.ref('teachers/' + user.uid).set({
        uid: user.uid,
        email: user.email,
        classCode: this.classCode,
      });

      this.router.navigate(['/dashboard']);
    } else {
      user.delete().then(function () {
        alert('Not a valid class code.');
      });
      this.router.navigate(['/register']);
    }
  }

  ngOnInit() {
  }

}
