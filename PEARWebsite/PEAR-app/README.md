# PEAR-app

## Angular
1. Make sure you have at least Node.js version 8.x or greater and npm version 5.x or greater
2. Install with `npm install -g @angular/cli`
3. Navigate to `/PEARWebsite/PEAR-app`

## Angularfire2
1. Do Step 3 and Step 4 from [Official setup](https://github.com/angular/angularfire2/blob/master/docs/install-and-setup.md) to configure it for Firebase console. 

`environment.ts` and `environment.prod.ts` isn't included in the repository because it contains information specific to our console

## Firebase for web
1. Install with `npm install -g firebase-tools `
2. Log in to firebase console with `firebase login`
3. Initialize with `firebase init`  
  a. Firebase services to choose: Firebase hosting and Firebase Realtime Database  
  b. When asked for directory for public root, write `dist/PEAR-app` so it can access Angular-created index  
  c. Don't overwrite 

## To run on localhost
You need two terminals.
1. On first terminal run `ng build --watch`
2. On second terminal run `firebase serve` to open on `http://localhost:5000`

## To deploy
1. Build the angular project for production `ng build --prod`
2. Then run `firebase deploy` and navigate to [PEAR Website](http://pear-f60a2.firebaseapp.com)

--------------
## Development server
This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 6.1.5.  
Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The app will automatically reload if you change any of the source files.

## Code scaffolding

Run `ng generate component component-name` to generate a new component. You can also use `ng generate directive|pipe|service|class|guard|interface|enum|module`.

## Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory. Use the `--prod` flag for a production build.

## Running unit tests

Run `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io).

## Running end-to-end tests

Run `ng e2e` to execute the end-to-end tests via [Protractor](http://www.protractortest.org/).

## Further help

To get more help on the Angular CLI use `ng help` or go check out the [Angular CLI README](https://github.com/angular/angular-cli/blob/master/README.md).
