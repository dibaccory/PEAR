import { Component, OnInit } from '@angular/core';
import { Teacher } from '../teacher';
import { Admin } from '../user';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  teachers: Teacher[] = [];
  login: Admin[] = [];

  constructor() { }

  ngOnInit() {
  }

  // getHeroes(): void {
    // this.teacherService.getHeroes()
      // .subscribe(teachers => this.teachers = teachers.slice(1, 5));
  // }
}
