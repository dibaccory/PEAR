import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthAF2Service {

  constructor() { }

  printToConsole(arg) {
    console.log(arg);
  }
}
