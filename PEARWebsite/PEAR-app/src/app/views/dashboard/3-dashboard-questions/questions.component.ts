import { Component, OnInit } from '@angular/core';
import { AngularFireAuth } from '@angular/fire/auth';
import { AngularFireDatabase, AngularFireObject } from '@angular/fire/database';
import { Observable } from 'rxjs';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { AngularFireFunctions } from '@angular/fire/functions';

@Component({
  selector: 'app-questions',
  templateUrl: './questions.component.html',
  styleUrls: ['./questions.component.css']
})
export class QuestionsComponent implements OnInit {

  // using this user id WON"T work because students have different uid!!!! Have to filter by class code? or reorder DB?
  answers: Observable<any>;
  loggedIn = this.afAuth.auth.onAuthStateChanged;
  data;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private fns: AngularFireFunctions,
    public afAuth: AngularFireAuth,
    db: AngularFireDatabase) {
    // this.answers = db.list<any>('answers/' + afAuth.auth.currentUser.uid + '/astronomy/modules/').valueChanges();
  }

  ngOnInit() {
    // this.hero$ = this.route.paramMap.pipe(
    //   switchMap((params: ParamMap) =>
    //     this.service.getHero(params.get('id')))
    // );
  }

}
