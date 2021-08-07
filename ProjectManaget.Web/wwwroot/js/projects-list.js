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
        var pName = document.getElementById("editProjectName").value;
        var pDesc = document.getElementById("editProjectDescription").value;
        var Devs = [11, 12];

        var data = {
            Id: Id,
            Name: pName,
            ProjectSubject: pDesc,
            Developers: Devs,
        }
        var jsonData = JSON.stringify(data);

        $.ajax({
            type: "PUT",
            url: "AjaxPrjEdit",
            dataType: 'json',
            contentType: 'application/json',
            data: jsonData,
            traditional: true,
            success: function () {
                location.reload();
            }
        });
        setTimeout(location.reload.bind(location), 300);
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
                    content += '<p class="dropdown-item"' + 'data-developer' + "=" + '"' + data[x].id + '"' + '>';
                    try {
                        if (id in data[x].projects) {
                            content += '<input type="checkbox" class="form-check-input" checked data-developer="' + data[x].id + ' " data-project="' + id + '"  form-check-primary form-check-glow" name="customCheck" id="developerChecker">';
                        }
                        else {
                            content += '<input type="checkbox" class="form-check-input" data-developer="' + data[x].id + '" data-project="' + id + '" form-check-primary form-check-glow" name="customCheck" id="developerChecker">';
                        }
                    }
                    catch (e) {}
                    finally {
                        content += '<input type="checkbox" class="form-check-input" data-developer="' + data[x].id + '" data-project="' + id + '" form-check-primary form-check-glow" name="customCheck" id="developerChecker">';
                    }
                    content += data[x].firstName + " " + data[x].lastName;
                    content += '</p>';
                }
                $(content).appendTo(listDiv);
            }
        });
    });
    $('[id="developerChecker"]').on('check', function () {
        if (this.checked) {
            var idDeveloper = this.getAttribute('data-developer');
            var idProject = this.getAttribute('data-project');
            var link = "AjaxAddLink";
            var data = {
                IdDeveloper: idDeveloper,
                IdProject: idProject
            }
            var jsonData = JSON.stringify(data);
            console.log(jsonData);
            $.ajax({
                type: "POST",
                url: link,
                dataType: 'json',
                contentType: 'application/json',
                data: jsonData,
            });
        }
        else if (!this.checked) {
            var idDeveloper = this.getAttribute('data-developer');
            var idProject = this.getAttribute('data-project');
            var link = "AjaxRemoveLink";
            var data = {
                IdDeveloper: idDeveloper,
                IdProject: idProject
            }
            var jsonData = JSON.stringify(data);
            console.log(jsonData);
            $.ajax({
                type: "DELETE",
                url: link,
                dataType: 'json',
                contentType: 'application/json',
                data: jsonData,
            });
        }
        console.log("done");
    });
});
var Id;