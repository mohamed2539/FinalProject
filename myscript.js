function addNewRow() {
    var newRow = document.createElement("tr");
    var cell1 = document.createElement("td");
    var labelText = document.createTextNode("2");
    cell1.appendChild(labelText);
    newRow.appendChild(cell1);
    var cell2 = document.createElement("td");
    var inputField = document.createElement("input");
    inputField.type = "text";
    cell2.appendChild(inputField);
    newRow.appendChild(cell2);
    var cell3 = document.createElement("td");
    var labelText3 = document.createTextNode("3");
    cell3.appendChild(labelText3);
    newRow.appendChild(cell3);
    var cell4 = document.createElement("td");
    var inputField4 = document.createElement("input");
    inputField4.type = "text";
    cell4.appendChild(inputField4);
    newRow.appendChild(cell4);
    var cell5 = document.createElement("td");
    var inputField5 = document.createElement("input");
    inputField5.type = "text";
    cell5.appendChild(inputField5);
    newRow.appendChild(cell5);

    var table = document.getElementById("permissionData");
    table.appendChild(newRow);
}