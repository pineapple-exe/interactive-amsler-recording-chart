import * as grid from './grid.js';

export let name;
export let checkedCoordinates = [];

//______________Manual Session______________

export const goManualSession = () => {
    flipElementsState(true, true);
}

const handleMouseMove = (e) => {
    grid.freshAmslerChart();
    grid.drawDot(e.offsetX, e.offsetY);
}

const handleManualSpotting = (e) => {
    checkedCoordinates.push({ x: e.offsetX, y: e.offsetY, visualFieldStatus: 1 });
}
//__________________________________________

export const submitName = () => {
    name = document.querySelector('input').value;

    const formContainer = document.getElementById('form-container');
    formContainer.classList.remove("cover");
    formContainer.classList.add("hidden");

    grid.freshAmslerChart();
}

export const flipElementsState = (thisIsStart, thisIsManual) => {
    const manualSessionElement = document.getElementById("manual-session-container");
    const repetitionSessionElement = document.getElementById("repetition-session-container");
    const passiveSessionElement = document.getElementById("passive-session-container");

    if (thisIsManual == null) thisIsManual = !manualSessionElement.classList.contains("hidden");

    if (thisIsStart) {
        passiveSessionElement.classList.add("hidden");

        if (thisIsManual) {
            manualSessionElement.classList.remove("hidden");

            grid.canvas.classList.add("invisible-cursor");
            grid.canvas.addEventListener('mousemove', handleMouseMove);
            grid.canvas.addEventListener('click', handleManualSpotting);
        } else {
            repetitionSessionElement.classList.remove("hidden");
        }
    } else {
        passiveSessionElement.classList.remove("hidden");

        if (thisIsManual) {
            manualSessionElement.classList.add("hidden");

            grid.canvas.classList.remove("invisible-cursor");
            grid.canvas.removeEventListener('mousemove', handleMouseMove);
            grid.canvas.removeEventListener('click', handleManualSpotting);
        } else {
            document.getElementById("active-repetition-session-container").classList.remove("hidden");
            repetitionSessionElement.classList.add("hidden");
        }
    }
}

export const quitSession = () => {
    if (checkedCoordinates.length > 0) {
        const endpoint = "addSession";
        const session = {
            name: name,
            coordinatesWithStatus: checkedCoordinates,
            dateTime: new Date(Date.now()).toJSON()
        };

        fetch(`api/InteractiveAmslerRecordingChart/${endpoint}`, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(session)
        });
    }
    document.getElementById("repetition-session-catalyst").disabled = false;
    flipElementsState(false, null);
    grid.freshAmslerChart();
    checkedCoordinates = [];
}