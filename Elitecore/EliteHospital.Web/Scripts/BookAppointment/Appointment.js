
function filterAppointments(status) {
    $(".appointments .apomnts").css("display", "none");
    var filteredAppointments = $(".appointments .apomnts").filter(function () {
        return $(this).attr("status") == status;
    });
    $(filteredAppointments).each(function (index) {
        $(this).css("display", "block");
    });
}

function filterAppointmentClick(id) {
    if (id == "all-tab") {
        $(".appointments .apomnts").css("display", "block");
    }
    else if (id == "upcoming-tab") {
        filterAppointments("Upcoming");
    }
    else if (id == "cancelled-tab") {
        filterAppointments("Cancelled");
    }
}