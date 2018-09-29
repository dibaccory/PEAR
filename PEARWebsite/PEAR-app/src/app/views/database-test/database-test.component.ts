import { Component, OnInit } from '@angular/core';
import { AngularFireDatabase, AngularFireList } from '@angular/fire/database';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-database-test',
//   template: `
//   <ul>
//   <li **ngFor="let item of items | async">
//     {{ item | json }}
//   </li>
// </ul>
// `,
  templateUrl: './database-test.component.html',
  styleUrls: ['./database-test.component.css']
})
export class DatabaseTestComponent implements OnInit {
  items: Observable<any>;

  constructor(db: AngularFireDatabase) {
    // this.items = db.list('answers').valueChanges();
    db.list<any>('users').valueChanges().subscribe(console.log);
  }

  ngOnInit() {
  }

}
