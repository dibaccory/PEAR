import { Component, OnInit } from '@angular/core';
import { AngularFireDatabase } from '@angular/fire/database';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-default',
  templateUrl: './default.component.html',
  styleUrls: ['./default.component.css']
})
export class DefaultComponent implements OnInit {

  classClode: Observable<any[]>;

  constructor(db: AngularFireDatabase) {
    this.classClode = db.list<any>('classrooms').valueChanges();
  }

  ngOnInit() {
  }

}
