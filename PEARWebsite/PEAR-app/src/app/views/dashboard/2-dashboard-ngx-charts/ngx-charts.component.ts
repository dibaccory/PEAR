import { Component, OnInit } from '@angular/core';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { AngularFireAuth } from '@angular/fire/auth';
import { AngularFireDatabase, AngularFireAction } from '@angular/fire/database';
import { Observable, Subscription, BehaviorSubject } from 'rxjs';
import { switchMap } from 'rxjs/operators';

// import { single } from './sample-data';

@Component({
  selector: 'app-ngx-charts',
  templateUrl: './ngx-charts.component.html',
  styleUrls: ['./ngx-charts.component.css']
})
export class NgxChartsComponent implements OnInit {
  // single: any[];
  // users: any[];
  planets: Observable<any[]>;
  urlRef;
  user;
  cellNum;
  totalAttempts;

  // modules$: Observable<AngularFireAction<firebase.database.DataSnapshot>[]>;
  // totalAttempts$: BehaviorSubject<string | null>;

  // Options
  // showXAxis = true;
  // showYAxis = true;
  // gradient = false;
  // showLegend = true;
  // showXAxisLabel = true;
  // xAxisLabel = 'Student';
  // showYAxisLabel = true;
  // yAxisLabel = '# of Attempts';

  // colorScheme = {
  //   domain: ['#5AA454', '#A10A28', '#C7B42C', '#AAAAAA']
  // };

  // have to change from current user to array of all student users and cycle through for each...somehow
  constructor(public afAuth: AngularFireAuth, db: AngularFireDatabase) {

    this.user = afAuth.auth.currentUser.uid;
    this.urlRef = db.database.ref('answers/' + this.user + '/astronomy/modules/solar system/');
    this.planets = db.list<any>(this.urlRef).valueChanges();

    // this.totalAttempts$ = new BehaviorSubject(null);

    // this.planets = this.totalAttempts$.pipe(
    //   switchMap(sfsd =>
    //     db.list(this.urlRef, ref =>
    //       sfsd ? ref.orderByChild('sdsf').equalTo(sfsd) : ref
    //     ).snapshotChanges()
    //     )
    // );

    // db.list(this.urlRef, ref => ref.orderByChild('totalAttempts'));

    // this.urlRef.once('value', function(snapshot) {
    //   snapshot.forEach(function(child) {
    //     console.log(child.key + ': ' + child.val());
    //   });
    // });

  // this.answers$ = db.list('answers/' + afAuth.auth.currentUser.uid + '/astronomy/modules/',
  //   ref => ref.orderByChild('solar system').equalTo('time spent')).snapshotChanges();

    // Object.assign(this, { answers$ });
  }

  // onSelect(event) {
  //   console.log(event);
  // }

  ngOnInit() {
  }

}
