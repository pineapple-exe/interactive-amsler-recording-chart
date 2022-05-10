const canvas = document.getElementById("amsler-grid");
const context = canvas.getContext("2d");

const cellsPerRow = 20;
const gridAreaFactor = canvas.offsetWidth;
const cellAreaFactor = gridAreaFactor / cellsPerRow;

let latestGuidingX;
let latestGuidingY;
let problemAreas = [];
let visibleAreas = [];

const drawGrid = (cellAreaFactor) => {
    context.beginPath();
    context.lineWidth = 1;
    let x = 0;
    let y = 0;

    // Horizontal lines
    for (let i = 0; i < cellsPerRow + 1; i++) {
        context.moveTo(0, y);
        context.lineTo(gridAreaFactor, y);
        context.stroke();

        y += cellAreaFactor;
    }

    //Vertical lines
    for (let i = 0; i < cellsPerRow + 1; i++) {
        context.moveTo(x, 0);
        context.lineTo(x, gridAreaFactor);
        context.stroke();

        x += cellAreaFactor;
    }
}

const drawCenterDot = (gridAreaFactor) => {
    const middleCoordinate = gridAreaFactor / 2;

    context.beginPath();
    context.arc(middleCoordinate, middleCoordinate, 9, 0, 2 * Math.PI);
    context.fill();
    context.stroke();
}

const handleMouseMove = (e) => {
    freshAmslerChart();
    drawTestDot(e.offsetX, e.offsetY);
}

const handleManualSpotting = (e) => {
    problemAreas.push({ x: e.offsetX, y: e.offsetY });
}

const flipElementsState = (passiveSessionElement, activeSessionElement, thisIsStart, altSessionElement = null) => {
    if (thisIsStart) {
        passiveSessionElement.classList.add("hidden");
        activeSessionElement.classList.remove("hidden");

        if (altSessionElement != null) {
            altSessionElement.classList.remove("hidden");
        } else {
            canvas.classList.add("invisible-cursor");
            canvas.addEventListener('mousemove', handleMouseMove);
            canvas.addEventListener('click', handleManualSpotting);
        }
    } else {
        passiveSessionElement.classList.remove("hidden");
        activeSessionElement.classList.add("hidden");

        if (altSessionElement != null) {
            altSessionElement.classList.add("hidden");
        } else {
            canvas.classList.remove("invisible-cursor");
            canvas.removeEventListener('mousemove', handleMouseMove);
            canvas.removeEventListener('click', handleManualSpotting);
        }
    }
}

const freshAmslerChart = () => {
    context.clearRect(0, 0, canvas.width, canvas.height);
    drawGrid(cellAreaFactor);
    drawCenterDot(gridAreaFactor);
}

const quitSession = () => {
    //Save session in database.

    problemAreas = [];
    visibleAreas = [];

    const altSessionElement = document.getElementById("alt-session-container");

    flipElementsState(document.getElementById("passive-session-container"),
                      document.getElementById("active-session-container"),
                      false,
                      !altSessionElement.classList.contains("hidden") ? altSessionElement : null);

    freshAmslerChart();
}

const drawTestDot = (x, y) => {
    context.beginPath();
    context.fillStyle = 'indianred';
    context.arc(x, y, 5, 0, 2 * Math.PI);
    context.fill();
    context.fillStyle = 'black';
}

const runSession = (altSessionElement = null) => {
    const passiveSessionElement = document.getElementById("passive-session-container");
    const activeSessionElement = document.getElementById("active-session-container");

    flipElementsState(passiveSessionElement, activeSessionElement, true, altSessionElement);
}

const visibilityIsAYes = () => {
    visibleAreas.push({ x: latestGuidingX, y: latestGuidingY });

    runGuidance(visibleAreas, problemAreas);
}

const visibilityIsANo = () => {
    problemAreas.push({ x: latestGuidingX, y: latestGuidingY });

    runGuidance(visibleAreas, problemAreas);
}

const runGuidance = (visibleAreas, problemAreas) => {
    if (visibleAreas.length + problemAreas.length === 0) {
        //Fetch random spot.
        console.log('Fetch random spot.');
    } else {
        //Fetch spot based on visibleAreas and problemAreas.
        console.log('Fetch spot based on visibleAreas and problemAreas.');
    }
}

const runAltSession = () => {
    const altSessionElement = document.getElementById("alt-session-container");

    runSession(altSessionElement);
    runGuidance(visibleAreas, problemAreas);
}

freshAmslerChart();