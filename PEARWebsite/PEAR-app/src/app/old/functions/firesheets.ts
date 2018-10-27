// Copyright 2017 Google LLC.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// https://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

import * as functions from 'firebase-functions';
import * as _ from 'lodash';
import { google } from 'googleapis';
const sheets = google.sheets('v4');

const spreadsheetId = '18oWpNlrYK1JeBvKsXHkt7qxekVhyTkhsRpYW0nYnzOM';

const serviceAccount = require('serviceAccount.json');

const jwtClient = new google.auth.JWT({
    email: serviceAccount.client_email,
    key: serviceAccount.private_key,
    scopes: ['https://www.googleapis.com/auth/spreadsheets'],  // read and write sheets
});
const jwtAuthPromise = jwtClient.authorize();

interface Scores { string: number; }

export const copyScoresToSheet = functions.database.ref('/scores').onUpdate(async change => {
    const data: Scores = change.after.val();

    // Sort the scores.  scores is an array of arrays each containing name and score.
    const scores = _.map<Scores, [string, number]>(data, (value, key) => [key, value]);
    scores.sort((a, b) =>  b[1] - a[1]);

    await jwtAuthPromise;
    await sheets.spreadsheets.values.update({
        auth: jwtClient,
        spreadsheetId: spreadsheetId,
        range: 'Scores!A2:B7',  // update this range of cells
        valueInputOption: 'RAW',
        requestBody: { values: scores }
    }, {});
});
