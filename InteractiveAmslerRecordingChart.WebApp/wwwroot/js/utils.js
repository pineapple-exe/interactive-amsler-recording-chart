export async function getData(endpoint, urlSearchParams) {
    let resp = await fetch(`api/InteractiveAmslerRecordingChart/${endpoint}?` + urlSearchParams);

    if (resp.status === 204)
        return null;
    else
        return await resp.json();
}