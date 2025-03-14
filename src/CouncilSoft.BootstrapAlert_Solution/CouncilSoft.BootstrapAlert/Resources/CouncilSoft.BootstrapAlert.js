function addAlertToMessageArea(severity, alertMessage, autoDismissInMilliseconds, showDismissButton) {
    console.log(`Queueing new alert: '${alertMessage}'`);
// ReSharper disable once AssignToImplicitGlobalInFunctionScope
    $messageArea = $("#messageArea");
    var alertLevel = "alert-info";
    var glyph = "glyphicon-info-sign";
    var html;
    var name = "";

    if (severity === -2) {
        alertLevel = "alert-danger";
        glyph = "glyphicon-remove-sign";
        name = "Error";
    } else if (severity === -1) {
        alertLevel = "alert-warning";
        glyph = "glyphicon-exclamation-sign";
        name = "Warning";
    } else if (severity === 0) {
        alertLevel = "alert-info";
        glyph = "glyphicon-info-sign";
        name = "Info";
    } else if (severity === 1) {
        alertLevel = "alert-success";
        glyph = "glyphicon-ok-sign";
        name = "Success";
    }

    const randomNumber = Math.round(Math.random() * 999999);
    var newId = `message_${randomNumber}`;

    html = `<div class='alert ${alertLevel} alert-dismissible' role='alert' id='${newId}'>`;
    if (showDismissButton) {
        html += "<button type='button' class='close' data-dismiss='alert'><span aria-hidden='true'>&times;</span>";
        html += "<span class='sr-only'>Close</span></button>";
    }
    html += `<i class='glyphicon ${glyph}'></i>&nbsp;`;
    html += `<strong>${name}:</strong>&nbsp;`;
    html += alertMessage;
    html += "</div>";

    $messageArea.append(html);

    console.log(`Auto-dismiss in ${autoDismissInMilliseconds}`);
    if (autoDismissInMilliseconds > 0) {
        console.log("Dismissing message...");
        window.setTimeout(function() {
                $(`#${newId}`).fadeTo(500, 0).slideUp(500,
                    function() {
                        $(this).remove();
                    });
            },
            autoDismissInMilliseconds);
    }
}