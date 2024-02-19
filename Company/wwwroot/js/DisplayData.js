function DisplayProjects() {
    let selectedValueemp = document.getElementById("empsel").value;
    let projects = document.getElementById("projs");
    if (selectedValueemp != 0) {

        $.ajax({
            url: `/WorksFor/GetProjects/${selectedValueemp}`,
            success: function (data) {
                projects.innerHTML = data;
                console.log(data);
            }
        })
    }
}

function DisplayHours() {
    event.preventDefault();
    let selectedValuepr = document.getElementById("project").value;
    let selectedValueemp = document.getElementById("empsel").value;
    let hours = document.getElementById("hours");
    hours.innerHTML = "";
    $.ajax({
        url: `/WorksFor/GetHours?projId=${selectedValuepr}&empId=${selectedValueemp}`,
        success: function (data) {
            hours.innerHTML = data;
            console.log(data);
        }

    })

}