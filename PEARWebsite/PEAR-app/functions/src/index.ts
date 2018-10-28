import * as functions from 'firebase-functions';
import * as json2csv from '../../node_modules/json2csv';
import * as admin from 'firebase-admin';

// // Start writing Firebase Functions
// // https://firebase.google.com/docs/functions/typescript
//
// export const helloWorld = functions.https.onRequest((request, response) => {
//  response.send("Hello from Firebase!");
// });

// Example code to delete later!!
export const bigben = functions.https.onRequest((req, res) => {
    const hours = (new Date().getHours() % 12) + 1 // London is UTC + 1hr;
    res.status(200).send(`<!doctype html>
    <head>
      <title>Time</title>
    </head>
    <body>
      ${'BONG '.repeat(hours)}
    </body>
  </html>`);
});

// Admin Firebase SDK
const serviceAccount = require("../serviceAccountKey.json");

admin.initializeApp({
	credential: admin.credential.cert(serviceAccount),
	databaseURL: "https://pear-f60a2.firebaseio.com"
});

// json2csv
export const csvJsonReport = functions.https.onRequest((request, response) => {

	// You should you how to prepare an object
	// It could be anything that you like from your collections for example.
	const report = { 'a': 0, 'b': 1 };

	// Return JSON to screen
	response.status(200).json(report);

	// If you want to download a CSV file, you need to convert the object to CSV format
	// Please read this for other usages of json2csv - https://github.com/zemirco/json2csv
	const csv = json2csv(report);
	response.setHeader(
		'Content-disposition',
		'attachment; filename=report.csv'
	);
	response.set('Content-Type', 'text/csv');
	response.status(200).send(csv);
});
