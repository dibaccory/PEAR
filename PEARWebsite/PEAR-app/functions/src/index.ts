import * as functions from 'firebase-functions';
import * as json2csv from 'json2csv';
import * as admin from 'firebase-admin';

admin.initializeApp({
	// credential: admin.credential.cert(serviceAccount),
	databaseURL: "https://pear-f60a2.firebaseio.com"
});

// Start writing Firebase Functions
// https://firebase.google.com/docs/functions/typescript
export const helloWorld = functions.https.onRequest((request, response) => {
 response.send("Hello from Firebase!");
});

// json2csv
export const csvJsonReport = functions.https.onRequest((request, response) => {

	// You should you how to prepare an object
	// It could be anything that you like from your collections for example.
	// const report = { 'a': 0, 'b': 1 };

	const report = admin.database().ref();
	// response.send(report);

	// Return JSON to screen
	response.status(200).json(report);

	// If you want to download a CSV file, you need to convert the object to CSV formatt
	// Please read this for other usages of json2csv - https://github.com/zemirco/json2csv
	// const csv = json2csv(report);
	// response.setHeader(
	// 	'Content-disposition',
	// 	'attachment; filename=report.csv'
	// );
	// response.set('Content-Type', 'text/csv');
	// response.status(200).send(csv);
});

// Add teachers to database when register account
export const addTeacherToDatabase = functions.auth.user().onCreate((user) => {

	console.log(user.email);
	console.log(user.displayName);
	
	const userObject = {
		email: user.email,
		displayName: user.displayName
		// classCode: user.classCode
	};
	admin.database().ref('teachers/' + user.uid).set(userObject);
});

// Delete teacher from database when delete account
export const deleteTeacherFromDatabase = functions.auth.user().onDelete((user) => {
	// ...
	admin.database().ref('teachers/' + user.uid).remove();
});