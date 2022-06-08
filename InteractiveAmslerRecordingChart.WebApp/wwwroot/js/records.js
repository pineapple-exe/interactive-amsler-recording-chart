import { getData } from './utils.js';
let records;
const propertyAliases = ["Session ID", "Name", "Date", "Visual field progression"];

const tableHeads = (propertyAliases) => {
    let heads = "";

    for (let i = 0; i < propertyAliases.length; i++) {
        heads += `<th>${propertyAliases[i]}</th>`;
    }

    return heads;
}

const godHead = (heads) => {
    return `<thead><tr>${heads}</tr></thead>`;
}

export const progressionFormat = (improvement, regression) => {
    if (improvement + regression === 0) {
        return "Neutral";
    } else {
        return `Improvement: ${improvement}, Regression: ${regression}`;
    }
}

const filledTableBody = (records) => {
    let tableRows = "";
    records.forEach(r => tableRows += 
        `<tr class="record">
            <td class="id"><a class="id" href="/Record?id=${r.id}">${r.id}</a></td>
            <td>${r.name}</td>
            <td>${r.dateTime}</td>
            <td>${progressionFormat(r.visualFieldProgression.improvement, r.visualFieldProgression.regression)}</td>
        </tr>`
    );

    return `<tbody>${tableRows}</tbody>`;
}

export async function renderRecords() {
    records = await getData('records', new URLSearchParams({
        /*        pageIndex: currentPageIndex,*/
        /*        size: recordsPerPage*/
    }));

    const tableOrgans = `${godHead(tableHeads(propertyAliases))} ${filledTableBody(records)}`;

    document.querySelector("table#records").innerHTML = tableOrgans;
}