import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-splashpage',
  templateUrl: './splashpage.component.html',
  styleUrls: ['./splashpage.component.css']
})
export class SplashpageComponent implements OnInit {
  title = 'PEAR';

  constructor() { }

  ngOnInit() {
  }

}
