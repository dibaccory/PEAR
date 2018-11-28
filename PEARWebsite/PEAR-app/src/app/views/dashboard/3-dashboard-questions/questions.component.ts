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

  registeredClassCode;
  module;
  key;
  questions = [];
  keys: any[] = [];

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

          // console.log('key: ' + this.key);
          this.setQandA(childSnapshot);

          this.planetQ[this.key] = this.questions;
        });
      });

    config.closeOthers = false;
  }

  setQandA(childSnapshot) {
    this.questions = [
      {
        question: childSnapshot.child('/collect/question1/question').val(),
        answer: childSnapshot.child('/collect/question1/answers/A1').val()
      },
      {
        question: childSnapshot.child('/collect/question2/question').val(),
        answer: childSnapshot.child('/collect/question2/answers/A1').val()
      },
      {
        question: childSnapshot.child('/collect/question3/question').val(),
        answer: childSnapshot.child('/collect/question3/answers/A1').val()
      },
      {
        question: childSnapshot.child('/collect/question4/question').val(),
        answer: childSnapshot.child('/collect/question4/answers/A1').val()
      },
      {
        question: childSnapshot.child('/collect/question5/question').val(),
        answer: childSnapshot.child('/collect/question5/answers/A1').val()
      }
    ];
    // console.log(this.questions);
  }

  ngOnInit() {
  }

}
