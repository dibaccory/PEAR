import { Component, OnInit } from '@angular/core';
import { AngularFireAuth } from '@angular/fire/auth';
import { AngularFireDatabase } from '@angular/fire/database';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-questions',
  templateUrl: './questions.component.html',
  styleUrls: ['./questions.component.css']
})
export class QuestionsComponent implements OnInit {

  module;
  key;
  tester;
  tester2;

  planetBuildQ = {
    earth: '',
    jupiter: '',
    mars: '',
    mercury: '',
    neptune: '',
    saturn: '',
    sun: '',
    uranus: '',
    venus: '',
  };

  planetCollectQ = {
    earth: '',
    jupiter: '',
    mars: '',
    mercury: '',
    neptune: '',
    saturn: '',
    sun: '',
    uranus: '',
    venus: '',
  };

  constructor(public afAuth: AngularFireAuth, db: AngularFireDatabase) {

    this.module = db.database.ref('classrooms/astronomy/modules/solar system/').orderByKey();

    this.module.once('value')
      .then((snapshot) => {
        snapshot.forEach((childSnapshot) => {
          this.key = childSnapshot.key; // planet

          // const collect = childSnapshot.child('collect').val();
          const build = childSnapshot.child('build/question1/question').val();

          this.planetBuildQ[this.key] = build;
        });
      });
  }

  ngOnInit() {
  }

}
