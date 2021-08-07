$(document).ready(function () {
    $("#newDevSubmit").click(function (e) {
        var firstName = $('#inputFirstName').val();
        var lastName = $('#inputLastName').val();
        var eMail = $('#inputEmail').val();
        var phone = $('#inputPhone').val();

        var data = {
            FirstName: firstName,
            LastName: lastName,
            Email: eMail,
            Phone: phone,
        }
        var jsonData = JSON.stringify(data);

        $.ajax({
            type: "POST",
            url: 'AjaxDevPost',
            dataType: 'json',
            contentType: 'application/json',
            data: jsonData,
            success: function () {
                location.reload();
            },
            error: function (xhr, status) {
                var parsedJsonResponse = JSON.parse(xhr.responseText);

            }
        });
    });
    $("#editDevSubmit").click(function (e) {
        var firstName = $('#editFirstName').val();
        var lastName = $('#editLastName').val();
        var eMail = $('#editEmail').val();
        var phone = $('#editPhone').val();
        var id = document.getElementById('editFirstName').getAttribute('data-edit-id');

        var data = {
            Id: id,
            FirstName: firstName,
            LastName: lastName,
            Email: eMail,
            Phone: phone,
        }
        var jsonData = JSON.stringify(data);
        $.ajax({
            type: "PUT",
            url: 'AjaxDevEdit',
            dataType: 'json',
            contentType: 'application/json',
            data: jsonData,
            success: function () {
                location.reload();
            }
        });
    });
    $('[id="editDeveloper"]').click(function (e) {
        Id = this.getAttribute('data-developer');
        var devFName = "devFName-" + Id;
        var devLName = "devLName-" + Id;
        var devEMail = "devEMail-" + Id;
        var devPhone = "devPhone-" + Id;
        document.getElementById("editFirstName").value = document.getElementById(devFName).innerHTML;
        document.getElementById("editLastName").value = document.getElementById(devLName).innerHTML;
        document.getElementById("editEmail").value = document.getElementById(devEMail).innerHTML;
        document.getElementById("editPhone").value = document.getElementById(devPhone).innerHTML;
        $('#editFirstName').attr('data-edit-id', Id);
    });
    $('[id="deleteDeveloper"]').click(function (e) {
        Id = this.getAttribute('data-developer');
        document.getElementById("fullNameConfirm").innerHTML = document.getElementById("devFName-" + Id).innerHTML + " " + document.getElementById("devLName-" + Id).innerHTML;
    });
    $("#confirmDelete").click(function (e) {
        var link = 'AjaxDeleteDev';
        link = link + "?id=" + Id;
        $.ajax({
            url: link,
            dataType: "json",
            type: "DELETE",
            contentType: 'application/json'
        });
        setTimeout(location.reload.bind(location), 300);
    });
});
var Id;