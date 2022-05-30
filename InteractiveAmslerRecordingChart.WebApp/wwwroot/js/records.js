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

const filledTableBody = (records) => {
    let tableRows = "";
    records.forEach(r => tableRows += 
        `<tr class="record">
            <td class="id"><a class="id" href="/record/${r.id}">${r.id}</a></td>
            <td>${r.name}</td>
            <td>${r.dateTime}</td>
            <td>Improved: ${r.visualFieldProgression.improvement}, Regressed: ${r.visualFieldProgression.regression}</td>
        </tr>`
    );

    return `<tbody>${tableRows}</tbody>`;
}

async function getRecords() {
    const endpoint = 'records';
    let resp = await fetch(`api/InteractiveAmslerRecordingChart/${endpoint}`);

    if (resp.status === 204)
        return null;
    else
        return await resp.json();
}

async function renderRecords() {
    records = await getRecords();
    const tableOrgans = `${godHead(tableHeads(propertyAliases))} ${filledTableBody(records)}`;

    document.querySelector("table#records").innerHTML = tableOrgans;
}

renderRecords();