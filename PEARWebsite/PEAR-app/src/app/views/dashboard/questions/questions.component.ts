import { Component, OnInit } from '@angular/core';
import { AngularFireAuth } from '@angular/fire/auth';
import { AngularFireDatabase, AngularFireObject } from '@angular/fire/database';
// import { Question } from './questions';
import { Observable } from 'rxjs';
import { ChangeDetectorRef } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { database, auth } from 'firebase';

@Component({
  selector: 'app-questions',
  templateUrl: './questions.component.html',
  styleUrls: ['./questions.component.css']
})
export class QuestionsComponent implements OnInit {

  // using this user id WON"T work because students have different uid!!!! Have to filter by class code? or reorder DB?
  answers: Observable<any>;
  loggedIn = this.afAuth.auth.currentUser;
  // @Input() question: Question;

  // sun: Observable<any>;
  // mercury: Observable<any>;
  // venus: Observable<any>;
  // earth: Observable<any>;
  // mars: Observable<any>;
  // jupiter: Observable<any>;
  // saturn: Observable<any>;
  // uranus: Observable<any>;
  // neptune: Observable<any>;

  constructor(public afAuth: AngularFireAuth, db: AngularFireDatabase) {
    this.answers = db.list<any>('answers/' + afAuth.auth.currentUser.uid + '/astronomy/modules/solar system/').valueChanges();

    // this.sun = db.list<any>('answers/' + afAuth.auth.currentUser.uid + '/astronomy/modules/solar system/sun/').valueChanges();
    // this.mercury = db.list<any>('answers/' + afAuth.auth.currentUser.uid + '/astronomy/modules/solar system/mercury').valueChanges();
    // this.venus = db.list<any>('answers/' + afAuth.auth.currentUser.uid + '/astronomy/modules/solar system/venus').valueChanges();
    // this.earth = db.list<any>('answers/' + afAuth.auth.currentUser.uid + '/astronomy/modules/solar system/earth').valueChanges();
    // this.mars = db.list<any>('answers/' + afAuth.auth.currentUser.uid + '/astronomy/modules/solar system/mars').valueChanges();
    // this.jupiter = db.list<any>('answers/' + afAuth.auth.currentUser.uid + '/astronomy/modules/solar system/jupiter').valueChanges();
    // this.saturn = db.list<any>('answers/' + afAuth.auth.currentUser.uid + '/astronomy/modules/solar system/saturn').valueChanges();
    // this.uranus = db.list<any>('answers/' + afAuth.auth.currentUser.uid + '/astronomy/modules/solar system/uranus').valueChanges();
    // this.neptune = db.list<any>('answers/' + afAuth.auth.currentUser.uid + '/astronomy/modules/solar system/neptune').valueChanges();
  }

  ngOnInit() {
  }

}
