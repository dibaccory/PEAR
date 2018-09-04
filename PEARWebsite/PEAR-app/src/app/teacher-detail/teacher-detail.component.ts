// The teacher property must be an Input property, annotated with the @Input() 
// decorator, because the external TeachersComponent will bind to it
import { Component, OnInit, Input } from '@angular/core';
// The TeacherDetailComponent template binds to the component's teacher property
// which is of type Teacher.
import { Teacher } from '../teacher';

@Component({
  selector: 'app-teacher-detail',
  templateUrl: './teacher-detail.component.html',
  styleUrls: ['./teacher-detail.component.css']
})
export class TeacherDetailComponent implements OnInit {
  @Input() teacher: Teacher;

  constructor() { }
  ngOnInit() { }
}

// This component simply receives a teacher object through its teacher property and displays it
