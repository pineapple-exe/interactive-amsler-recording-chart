import { getData } from './utils.js';

let sessions;
let total;
const currentUrlParams = new URLSearchParams(document.location.search);
const pageValue = currentUrlParams.get('page');
let currentPage = pageValue === null ? 1 : pageValue;
let name = null;
const size = 50;
const propertyAliases = ["Session ID", "Name", "Date", "Visual field progression"];

const tableHeads = (propertyAliases) => {
    let heads = "";

    for (let i = 0; i < propertyAliases.length; i++) {
        heads += `<th>${propertyAliases[i]}</th>`;
    }

    return heads;
}

export const searchByName = () => {
    name = document.getElementById('filter-input').value;
    renderRecords();
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

const filledTableBody = (sessions) => {
    let tableRows = "";
    sessions.forEach(r => tableRows += 
        `<tr class="record">
            <td class="id"><a class="id" href="/Record?id=${r.id}">${r.id}</a></td>
            <td>${r.name}</td>
            <td>${r.dateTime}</td>
            <td>${progressionFormat(r.visualFieldProgression.improvement, r.visualFieldProgression.regression)}</td>
        </tr>`
    );

    return `<tbody>${tableRows}</tbody>`;
}

const pagination = (size, total) => {
    let pageItems = '';

    for (let i = 1; i <= Math.ceil(total / size); i++) {
        pageItems += `<li class="page-item"> <a href="/Records?page=${i}">${i}</a> </li>`;
    }

    return pageItems;
}

export async function renderRecords() {
    let parameters = name === null ? { pageIndex: currentPage - 1, size: size } : { pageIndex: currentPage - 1, size: size, name: name };

    const sessionsPage = await getData('sessions', new URLSearchParams(parameters));

    sessions = sessionsPage.sessions;
    total = sessionsPage.total;

    const tableOrgans = `${godHead(tableHeads(propertyAliases))} ${filledTableBody(sessions)}`;

    document.querySelector("table#records").innerHTML = tableOrgans;
    document.querySelector("ul#pages").innerHTML = pagination(size, total);
}