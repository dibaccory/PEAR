import { Component, OnInit } from '@angular/core';
import { AngularFireDatabase, AngularFireList } from '@angular/fire/database';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  title = 'PEAR';
  className = 'Astronomy';

  questions: Observable<any>;

  constructor(db: AngularFireDatabase) {
    // this.questions = db.list<any>('classrooms/astronomy/modules/solarsystem').valueChanges();
    // db.list<any>('users').valueChanges().subscribe(console.log);
  }

  ngOnInit() {
  }
}
