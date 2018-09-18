import { Injectable } from '@angular/core';
import { Routes } from '@angular/router';

import { AngularFireAuth } from 'angularfire2/auth';
import * as firebase from 'firebase/app';
import { Observable } from 'rxjs';

@Injectable()
export class AuthService {
  private user: Observable<firebase.User>;
  private userDetails: firebase.User = null;
  
  constructor(private _firebaseAuth: AngularFireAuth, private router: Routes) {
    this.user = _firebaseAuth.authState;
  }

  signInRegular(email, password) {
    const credential = firebase.auth.EmailAuthProvider.credential( email, password );

    return this._firebaseAuth.auth.signInWithEmailAndPassword(email, password)
  }
}