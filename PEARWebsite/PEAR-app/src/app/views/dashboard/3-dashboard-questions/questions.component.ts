import { Component, OnInit } from '@angular/core';
import { AngularFireAuth } from '@angular/fire/auth';
import { AngularFireDatabase } from '@angular/fire/database';
import { NgbAccordionConfig } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-questions',
  templateUrl: './questions.component.html',
  styleUrls: ['./questions.component.css'],
  providers: [NgbAccordionConfig]
})
export class QuestionsComponent implements OnInit {

  module;
  key;
  questions: any[];

  planetQ = {
    earth: [],
    jupiter: [],
    mars: [],
    mercury: [],
    neptune: [],
    saturn: [],
    sun: [],
    uranus: [],
    venus: [],
  };

  constructor(public afAuth: AngularFireAuth, db: AngularFireDatabase, config: NgbAccordionConfig) {

    this.module = db.database.ref('classrooms/astronomy/modules/solar system/').orderByChild('question');

    this.module.once('value')
      .then((snapshot) => {
        snapshot.forEach((childSnapshot) => {
          this.key = childSnapshot.key; // planet - earth
          // const key2 = childSnapshot.child('collect').key; // collect

          console.log('key: ' + this.key);

          this.setQuestions(childSnapshot);

          this.planetQ[this.key] = this.questions;
        });
      });

    config.closeOthers = false;
    // config.type = '#ffffff';
  }

  setQuestions(childSnapshot) {
    this.questions = [
      childSnapshot.child('/collect/question1/question').val(),
      childSnapshot.child('/collect/question2/question').val(),
      childSnapshot.child('/collect/question3/question').val(),
      childSnapshot.child('/collect/question4/question').val(),
      childSnapshot.child('/collect/question5/question').val()
    ];
    console.log(this.questions);
  }

  ngOnInit() {
  }

}
