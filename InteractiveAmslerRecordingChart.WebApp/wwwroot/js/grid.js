export const canvas = document.getElementById("amsler-grid");
const context = canvas.getContext("2d");

const cellsPerRow = 20;
const gridAreaFactor = canvas.offsetWidth;
const cellAreaFactor = gridAreaFactor / cellsPerRow;

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

export const freshAmslerChart = () => {
    context.clearRect(0, 0, canvas.width, canvas.height);
    drawGrid(cellAreaFactor);
    drawCenterDot(gridAreaFactor);
}

export const drawDot = (x, y, fillStyle = 'indianred') => {
    context.beginPath();
    context.fillStyle = fillStyle;
    context.arc(x, y, 5, 0, 2 * Math.PI);
    context.fill();
    context.fillStyle = 'black';
}

export const toneItDown = () => {
    context.fillStyle = 'rgba(255, 255, 255, 0.5)';
    context.fillRect(0, 0, canvas.width, canvas.height);
    context.fillStyle = 'black';
}