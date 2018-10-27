import { Component, OnInit } from '@angular/core';
import { AngularFireAuth } from '@angular/fire/auth';
import { AngularFireDatabase, AngularFireObject } from '@angular/fire/database';
// import { Question } from './questions';
import { Observable } from 'rxjs';
// import { ChangeDetectorRef } from '@angular/core';
// import { RouterOutlet } from '@angular/router';
// import { database, auth } from 'firebase';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-questions',
  templateUrl: './questions.component.html',
  styleUrls: ['./questions.component.css']
})
export class QuestionsComponent implements OnInit {

  // using this user id WON"T work because students have different uid!!!! Have to filter by class code? or reorder DB?
  answers: Observable<any>;
  loggedIn = this.afAuth.auth.onAuthStateChanged;
  report;
  // @Input() question: Question;

  constructor(public afAuth: AngularFireAuth, db: AngularFireDatabase) {
    this.answers = db.list<any>('answers/' + afAuth.auth.currentUser.uid + '/astronomy/modules/').valueChanges();

    // private route: ActivatedRoute,
    // private router: Router,
    // private service: HeroService

  }

  // json() {
  //   exports.csvJsonReport = functions.https.onRequest((request, response) => {
  //     // Your code goes here
  //     this.report = { 'a': 0, 'b': 1 }; // your object
  //   });
  // }

  ngOnInit() {
  }

}
