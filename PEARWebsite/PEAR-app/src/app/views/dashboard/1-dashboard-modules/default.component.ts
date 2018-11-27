import { Component, OnInit } from '@angular/core';
import { AngularFireDatabase } from '@angular/fire/database';
import { AngularFireAuth } from '@angular/fire/auth';
import { ClassCodesService } from '../../../services/class-code.service';
import { StudentsService } from '../../../services/students.service';

@Component({
  selector: 'app-default',
  templateUrl: './default.component.html',
  styleUrls: ['./default.component.css']
})
export class DefaultComponent implements OnInit {

  allStudents: string[] = [];
  classCodes: string[] = [];

  teacher: string;
  teacherClassCode;
  ref;
  registeredStudents: any[] = [];
  studentInfo;

  constructor(public afAuth: AngularFireAuth, public db: AngularFireDatabase, public students: StudentsService,
    public codes: ClassCodesService) {
    this.allStudents = students.studentUIDs;
    this.studentInfo = students.studentsInfo;
    this.classCodes = codes.classCodes;

    this.getRegisteredStudents();
  }

  getTeacherClassCode() {
    this.teacher = this.afAuth.auth.currentUser.uid;
    const ref = this.db.database.ref('teachers/' + this.teacher + '/classCode').orderByKey();

    ref.once('value')
      .then((snapshot) => {
        this.teacherClassCode = snapshot.val();
      });
    console.log(this.teacherClassCode);

    return this.teacherClassCode;
  }

  getRegisteredStudents() {
    // tslint:disable-next-line:prefer-const
    for (let uid of this.allStudents) {
      const ref = this.db.database.ref('answers/' + uid).orderByKey();

      ref.once('value').then((snapshot) => {
        const key = snapshot.key;

        if (key.toString === this.teacherClassCode) {
          this.registeredStudents.push(uid);
        }
      });
    }
  }

  ngOnInit() {
  }

}
