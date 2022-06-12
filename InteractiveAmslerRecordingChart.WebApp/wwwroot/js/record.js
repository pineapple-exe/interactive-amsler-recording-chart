import * as grid from './grid.js';
import { progressionFormat } from './records.js';
import { getData } from './utils.js';

const comparisonLink = (comparisonId) => {
    if (comparisonId !== null) {
        return `<a class="id" href="/Record?id=${comparisonId}">${comparisonId}</a>`;
    } else return 'None';
}

async function renderRecord(session, comparablePast, comparableFuture) {
    document.getElementById("record-container").innerHTML =
    `<ul>
        <li><span class="property-name"> Session ID: </span> <span class="property-value"> ${session.id} </span></li>
        <li><span class="property-name"> Name: </span> <span class="property-value"> ${session.name} </span></li>
        <li><span class="property-name"> Date: </span> <span class="property-value"> ${session.dateTime} </span></li>
        <li><span class="property-name"> Progression: </span> <span class="property-value">
            ${progressionFormat(session.visualFieldProgression.improvement, session.visualFieldProgression.regression)} </span></li>
        <li><span class="property-name"> Recent past: </span> <span class="property-value"> ${comparisonLink(comparablePast)} </span></li>
        <li><span class="property-name"> Next: </span> <span class="property-value"> ${comparisonLink(comparableFuture)} </span></li>
     </ul>`;
}

const drawOldDot = (coo) => {
    if (coo.visualFieldStatus == 0) {
        grid.drawDot(coo.x, coo.y, '#4448A5');
    } else {
        grid.drawDot(coo.x, coo.y);
    }
}

export async function renderPage() {
    const currentUrlParams = new URLSearchParams(document.location.search);

    const session = await getData('session', new URLSearchParams({ id: currentUrlParams.get('id') }));
    const comparablePast = await getData('comparisonId', new URLSearchParams({ currentId: session.id, timeTravel: 0 }));
    const comparableFuture = await getData('comparisonId', new URLSearchParams({ currentId: session.id, timeTravel: 1 }));

    renderRecord(session, comparablePast, comparableFuture);
    grid.freshAmslerChart();
    session.coordinatesWithStatus.forEach(c => drawOldDot(c));
}