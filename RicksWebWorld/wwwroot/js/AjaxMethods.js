$(document).ready(function () {

    //See if the user is on the correct page
    var loc = window.location.pathname;
    if (loc === "/Role/Overview") {
        retrieveRoles();
    }
    else if (loc === "/Role/OverviewEdit") {
        retrieveRoles();
    }

    
    //Retrieve partialview roles
    function retrieveRoles(data, status) {
        $.ajax({
            type: "GET",
            url: "/Role/RetrieveRoles",
            dataType: "html",
            success: successFunc,
            error: errorFunc
        });
    }

    //Basic success and error functions
    function successFunc(data, status) {
        $("#partial").html(data);
        $("divLoader").hide();
    }

    //If something goes wrong use this method
    function errorFunc(jQXHR, textStatus, errorThrown) {
        alert("An error occurred while trying to contact the server: " + jQXHR.status + " " + textStatus + " " + errorThrown);
    }
});