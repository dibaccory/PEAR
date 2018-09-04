import { Component, OnInit } from '@angular/core';
import { Teacher } from '../teacher';
import {TEACHERS} from '../mock-teachers';

@Component({
  selector: 'app-teachers',
  templateUrl: './teachers.component.html',
  styleUrls: ['./teachers.component.css']
})
export class TeachersComponent implements OnInit {
  teachers = TEACHERS;
  selectedTeacher: Teacher;

  constructor() { }
  ngOnInit() { }

  onSelect(teacher: Teacher): void {
    this.selectedTeacher = teacher;
  }
}
