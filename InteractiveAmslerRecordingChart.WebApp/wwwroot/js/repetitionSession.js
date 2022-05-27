import * as grid from './grid.js';
import * as sessions from './sessions.js';

let oldCoordinates;
let coordinatesQueue;

async function getOldCoordinates() {
    const endpoint = 'oldCoordinates';
    let resp = await fetch(`api/InteractiveAmslerRecordingChart/${endpoint}?name=${sessions.name}`);

    if (resp.status == 204)
        return null;
    else
        return await resp.json();
}

export async function goRepetitionSession() {
    oldCoordinates = await getOldCoordinates();
    const motherButton = document.getElementById("repetition-session-catalyst");

    if (!oldCoordinates) {
        motherButton.disabled = true;
    } else {
        motherButton.disabled = false;
        coordinatesQueue = [...oldCoordinates];

        document.getElementById("improvement").innerHTML = '';
        document.getElementById("regression").innerHTML = '';

        sessions.flipElementsState(true, false);
        grid.drawDot(coordinatesQueue[0].x, coordinatesQueue[0].y);
    }
}

const mapProgression = () => {
    let improvementCount = 0;
    let regressionCount = 0;

    console.log(oldCoordinates);
    console.log(sessions.checkedCoordinates);

    for (let i = 0; i < sessions.checkedCoordinates.length; i++) {
        const previousStatus = oldCoordinates[i].visualFieldStatus;
        const updatedStatus = sessions.checkedCoordinates[i].visualFieldStatus;

        if (previousStatus == 1 && updatedStatus == 0) {
            improvementCount++;
        } else if (previousStatus == 0 && updatedStatus == 1) {
            regressionCount++;
        }
    }
    document.getElementById("improvement").innerHTML = `Improved spots: ${improvementCount}`;
    document.getElementById("regression").innerHTML = `Regressed spots: ${regressionCount}`;
}

const checkNext = () => {
    grid.freshAmslerChart();
    grid.drawDot(coordinatesQueue[0].x, coordinatesQueue[0].y);
}

const proceedRepetitionSession = (visualFieldStatus) => {
    sessions.checkedCoordinates.push({
        x: coordinatesQueue[0].x,
        y: coordinatesQueue[0].y,
        visualFieldStatus: visualFieldStatus
    });
    coordinatesQueue.shift();
    mapProgression();

    if (coordinatesQueue.length > 0) {
        checkNext();
    } else {
        document.getElementById("active-repetition-session-container").classList.add("hidden");
        grid.freshAmslerChart();
        grid.toneItDown();
    }
}

export const visibilityIsAYes = () => {
    proceedRepetitionSession(0);
}

export const visibilityIsANo = () => {
    proceedRepetitionSession(1);
}
