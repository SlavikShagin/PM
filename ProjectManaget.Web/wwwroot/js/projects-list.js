$(document).ready(function () {
    $("#newPrjSubmit").click(function (e) {
        var prjName = $('#inputProjectName').val();
        var prjDesc = $('#inputProjectDescription').val();

        var data = {
            Name: prjName,
            ProjectSubject: prjDesc,
        }
        var jsonData = JSON.stringify(data);

        $.ajax({
            type: "POST",
            url: "AjaxPrjPost",
            dataType: 'json',
            contentType: 'application/json',
            data: jsonData,
            success: function () {
                location.reload();
            }
        });
    });
    $("#editPrjSubmit").click(function (e) {
        var link = "../projects/AjaxPrjEdit";
        var pName = document.getElementById("editProjectName").value;
        var pDesc = document.getElementById("editProjectDescription").value;
        var data = {
            Id: Number(Id),
            Name: pName,
            ProjectSubject: pDesc,
        }
        var jsonData = JSON.stringify(data);
        $.ajax({
            type: "PUT",
            url: link,
            dataType: 'json',
            contentType: 'application/json',
            data: jsonData,
            success: function () {
                location.reload();
            }
        });
    });
    $('[id="editProject"]').click(function (e) {
        Id = this.getAttribute('data-project');
        var pnId = "prjName-" + Id;
        var pdId = "prjDesc-" + Id;
        document.getElementById("editProjectName").value = document.getElementById(pnId).innerHTML;
        document.getElementById("editProjectDescription").innerHTML = document.getElementById(pdId).innerHTML;
    });
    $('[id="deleteProject"]').click(function (e) {
        Id = this.getAttribute('data-project');
        document.getElementById("pNameConfirm").innerHTML = document.getElementById("prjName-" + Id).innerHTML;
    });
    $('#confirmDelete').click(function (e) {
        var link = "AjaxDeletePrj";
        link = link + "?id=" + Id;
        $.ajax({
            url: link,
            dataType: "json",
            type: "DELETE",
            contentType: 'application/json'
        });
        setTimeout(location.reload.bind(location), 300);
    });
    $('[id="listOfDevs"]').on("focus", function () {
        var id = this.getAttribute('data-project');
        var listDiv = document.getElementById("devListOptions-" + id);
        listDiv.innerHTML = "";
        var link = '../developers/AjaxDevelopersList';
        var content = "";
        $.ajax({
            url: link,
            type: "GET",
            dataType: "json",
            contentType: 'application/json',
            success: function (data) {
                for (var x = 0; x < data.length; x++) {
                    content += '<p class="dropdown-item">';

                    if (data[x].projects.some(item => item.id == id)) {
                        content += '<input type="checkbox" class="form-check-input" checked data-developer="' + data[x].id + ' " data-project="' + id + '"  form-check-primary form-check-glow" name="customCheck" id="developerChecker">';
                    }
                    else {
                        content += '<input type="checkbox" class="form-check-input" data-developer="' + data[x].id + '" data-project="' + id + '" form-check-primary form-check-glow" name="customCheck" id="developerChecker">';
                    }

                    content += data[x].firstName + " " + data[x].lastName;
                    content += '</p>';
                }
                $(content).appendTo(listDiv);
                $('[id="developerChecker"]').click(function () {
                    if (this.checked) {
                        var idDeveloper = this.getAttribute('data-developer');
                        var idProject = this.getAttribute('data-project');
                        var link = "AjaxAddLink";
                        var data = {
                            developerId: Number(idDeveloper),
                            projectId: Number(idProject)
                        }
                        var jsonData = JSON.stringify(data);
                        $.ajax({
                            type: "PUT",
                            url: link,
                            dataType: 'json',
                            contentType: 'application/json',
                            data: jsonData,
                            success: function () {
                                console.log("Assigned")
                            }
                        });
                    }
                    else if (!this.checked) {
                        var idDeveloper = this.getAttribute('data-developer');
                        var idProject = this.getAttribute('data-project');
                        var link = "AjaxRemoveLink";
                        var data = {
                            developerId: Number(idDeveloper),
                            projectId: Number(idProject)
                        }
                        var jsonData = JSON.stringify(data);
                        $.ajax({
                            type: "PUT",
                            url: link,
                            dataType: 'json',
                            contentType: 'application/json',
                            data: jsonData,
                            success: function () {
                                console.log("Unassigned")
                            }
                        });
                    }
                });
            }, error: function (xhr) { },
        });
    });

});

var Id;