import { IPublication } from "./IPublication";

async function GetPublications() {
    const response = await fetch("https://localhost:5001/api/publication/index=0",{
        method: "Get",
        headers: { "Accept": "application/json" }
    });

    if(response.ok === true){
        const publications = await response.json();
        let rows = document.querySelector("tbody");
        publications.forEach(publication => {
            rows.append(setRow(publication));
        });
    }
}

function setRow(publication: IPublication): HTMLTableRowElement{
    const tr = document.createElement("tr");

    const authorNameTd = document.createElement("td");
    authorNameTd.append(publication.author.name);
    tr.append(authorNameTd);

    const textTd = document.createElement("td");
    textTd.append(publication.text);
    tr.append(textTd);

    return tr;
}

export default GetPublications();