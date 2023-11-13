function configControls() {
    $.ajax({
        type: "POST",
        url: "/traslados-paso2.aspx/generarTabla",
        data: "{ idPedido: " + parseInt($("#hdnIDPedido").val()) + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                $("#bodyPasajeros").html(data.d);
            }
        }
    });
}

$(document).ready(function () {
    configControls();
});

function validate(evt) {
    var theEvent = evt || window.event;
    var key = theEvent.keyCode || theEvent.which;
    key = String.fromCharCode(key);
    var regex = /[0-9]|\./;
    if (!regex.test(key)) {
        theEvent.returnValue = false;
        if (theEvent.preventDefault) theEvent.preventDefault();
    }
}

function irPaso1() {
    var idPedido = parseInt($("#hdnIDPedido").val());
    if (idPedido > 0)
        window.location.href = "traslados-paso1.aspx?Id=" + idPedido;
    else
        window.location.href = "traslados-paso1.aspx";
    return false;
}

function continuar() {
    var isValid = true;
    var msg;
    var info = "{ info: '";
    $(".selectPasajeros").each(function () {
        if (this.value == "") {
            isValid = false;
            msg = "Debe completar todos los campos";
        }
        info += "-" + $(this).attr("id") + "#" +this.value;

    });

    var pagoPor = "";
    if ($("#txtPagoPor").val() != null && $("#txtPagoPor").val() != "" )
        pagoPor = $("#txtPagoPor").val() 

        info += "', nroFile: '" + $("#txtNroFile").val() + "', pagoPor:'"+ pagoPor+"'}";
   
    if (isValid) {
        $.ajax({
            type: "POST",
            url: "/traslados-paso2.aspx/guardar",
            data: info,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if ($("#hdnTipoServicio").val() == "R") {
                    var idPedido = parseInt($("#hdnIDPedido").val());
                    if (idPedido > 0)
                        window.location.href = "traslados-paso3.aspx?Id=" + idPedido;
                    else
                        window.location.href = "traslados-paso3.aspx";
                } else {
                    var idPedido = parseInt($("#hdnIDPedido").val());
                    if (idPedido > 0)
                        window.location.href = "traslados-paso3-oneWay.aspx?Id=" + idPedido;
                    else
                        window.location.href = "traslados-paso3-oneWay.aspx";
                }
            },
            error: function (response) {
                var r = jQuery.parseJSON(response.responseText);
                $("#divError").html(r.Message);
                $("#divError").hide();
                $("#divOk").hide();
            }
        });
        return false;
    } else {
        $("#divError").html(msg);
        $("#divError").show();
        $("#divOk").hide();
        return false;
    }
}