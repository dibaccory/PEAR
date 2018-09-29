import { Component, OnInit } from '@angular/core';
import { AngularFireDatabase, AngularFireList } from '@angular/fire/database';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-database-test',
  templateUrl: './database-test.component.html',
  styleUrls: ['./database-test.component.css']
})
export class DatabaseTestComponent implements OnInit {
  users: Observable<any>;
  answers: Observable<any>;
  classrooms: Observable<any>;

  constructor(db: AngularFireDatabase) {
    this.users = db.list<any>('users').valueChanges();
    this.answers = db.list<any>('answers').valueChanges();
    this.classrooms = db.list<any>('classrooms').valueChanges();
    // db.list<any>('users').valueChanges().subscribe(console.log);
  }

  ngOnInit() {
  }

}
