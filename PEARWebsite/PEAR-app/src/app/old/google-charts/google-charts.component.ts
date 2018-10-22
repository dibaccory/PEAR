import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';

@Component({
  selector: 'app-google-charts',
  templateUrl: './google-charts.component.html',
  styleUrls: ['./google-charts.component.css']
})
export class GoogleChartsComponent implements OnInit {

  chart = {
    title: 'Test Chart',
    type: 'ColumnChart',
    data: [
      ['Copper', 8.94],
      ['Silver', 10.49],
      ['Gold', 19.30],
      ['Platinum', 21.45],
    ],
    columnNames: ['Element', 'Density'],
    options: {
      animation: {
        duration: 250,
        easing: 'ease-in-out',
        startup: true
      }
    }
  };

  constructor(private location: Location) {
  }

  ngOnInit() {
  }
}
