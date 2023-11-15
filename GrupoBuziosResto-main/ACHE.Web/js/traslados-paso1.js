function configControls() {
    //$("#cmbServicioEspecial").val('');
    //$("#cmbServicio").val('');

    $("#cmbProveedor").change(function () {
        getServicios();
        getServiciosEspeciales();
    });

    $("#cmbServicio").change(function () {
        $("#cmbServicioEspecial").val('');
    });

    $("#cmbServicioEspecial").change(function () {
        $("#cmbServicio").val('');
    });


  /*  $("#cmbServicio").change(function () {
        getPrecios();
    });*/
}

function getServicios() {
    $("#cmbServicioEspecial").val('');
    var idProveedor = $("#cmbProveedor").val();
    if (idProveedor != "") {
        idProveedor = parseInt(idProveedor);
        $.ajax({
            type: "POST",
            url: "/traslados-paso1.aspx/GetServicios",
            data: "{ idProveedor: " + idProveedor + " }",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, text) {
                if (data.d != null && data.d.length > 0) {
                    $("#divForm").hide();
                    $("#cmbServicio").empty();
                    $("#cmbServicio").append("<option value=''>Seleccione un servicio</option>");
                    for (var i = 0; i < data.d.length; i++)
                        $("#cmbServicio").append("<option value='" + data.d[i].ID + "'>" + data.d[i].Nombre + "</option>");

                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                var err = eval("(" + xhr.responseText + ")");
                alert(err.Message);
            }
        });
    }
    else {
        $("#cmbServicio").empty();
        $("#cmbServicio").append("<option value=''>Seleccione un servicio</option>");
        $("#divForm").hide();
        $("#divPrecios, #divMsjAdicional").hide();

        $("#hdnTipoServicio").val("");
        $("#hdnTipo").val("");
        $("#divRegular, #divPrivado, #divRegularNR, #divPrivadoNR").hide();
    }
}

function getServiciosEspeciales() {
    $("#cmbServicio").val('');
    var idProveedor = $("#cmbProveedor").val();
    if (idProveedor != "") {
        idProveedor = parseInt(idProveedor);
        $.ajax({
            type: "POST",
            url: "/traslados-paso1.aspx/GetServiciosEspeciales",
            data: "{ idProveedor: " + idProveedor + " }",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, text) {
                if (data.d != null && data.d.length > 0) {
                    $("#divForm").hide();
                    $("#cmbServicioEspecial").empty();
                    $("#cmbServicioEspecial").append("<option value=''>Seleccione un servicio</option>");
                    for (var i = 0; i < data.d.length; i++)
                        $("#cmbServicioEspecial").append("<option value='" + data.d[i].ID + "'>" + data.d[i].Nombre + "</option>");

                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                var err = eval("(" + xhr.responseText + ")");
                alert(err.Message);
            }
        });
    }
    else {
        $("#cmbServicioEspecial").empty();
        $("#cmbServicioEspecial").append("<option value=''>Seleccione un servicio</option>");
        $("#divForm").hide();
        $("#divPrecios, #divMsjAdicional").hide();

        $("#hdnTipoServicio").val("");
        $("#hdnTipo").val("");
        $("#divRegular, #divPrivado, #divRegularNR, #divPrivadoNR").hide();
    }
}

function guardarId() {
    var IDServicio = parseInt($("#cmbServicio").val());
    var info = "{ IDServicio: " + IDServicio + " }";
    var cantMax;

    $.ajax({
        type: "POST",
        url: "/traslados-paso1.aspx/GetMaxPasajeros",
        data: info,
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, text) {
            cantMax = data.d;

            procesarCantMax();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            var err = eval("(" + xhr.responseText + ")");
            alert(err.Message);
        }
    });

    function procesarCantMax() {
        console.log(cantMax);

        if (cantMax > 0) {
            var dropdown = document.getElementById("cmbCantidadAdultos");

            for (var i = 0; i < dropdown.options.length; i++) {
                var optionValue = parseInt(dropdown.options[i].value);

                if (optionValue > cantMax && optionValue > 4) {
                    dropdown.options[i].style.display = "none";
                } else {
                    dropdown.options[i].style.display = "block";
                }
            }
        }
    }
}

function hideCantPasajeros() {
    var dropdown = document.getElementById("cmbCantidadAdultos");
    for (var i = 0; i < dropdown.options.length; i++) {
        var optionValue = parseInt(dropdown.options[i].value);

        if (optionValue > 4) {
            dropdown.options[i].style.display = "none";
        }
    }
};
    function continuar() {
        var isValid = true;
        if (!validate('cmbProveedor', 'spnProveedor'))
            isValid = false;
        if (!validate('cmbServicio', 'spnServicio') && !validate('cmbServicioEspecial', 'spnServicio'))
            isValid = false;
        if (!validate('cmbCantidadAdultos', 'spnCantidadAdultos'))
            isValid = false;
        if (!validate('cmbCantidadMenores', 'spnCantidadMenores'))
            isValid = false;
        if (!validate('cmbCantidadMenores2', 'spnCantidadMenores2'))
            isValid = false;

        if (isValid) {
            var idUsuario = parseInt($("#hdnIDUsuario").val());
            var IDServicio = parseInt($("#cmbServicio").val());
            if ($("#cmbServicioEspecial").val() != null && $("#cmbServicioEspecial").val() != "")
                IDServicio = parseInt($("#cmbServicioEspecial").val());

            var cantAdultos = parseInt($("#cmbCantidadAdultos").val());
            if (cantAdultos > 0) {
                var cantMenores = parseInt($("#cmbCantidadMenores").val());
                var cantMenores2 = parseInt($("#cmbCantidadMenores2").val());
                var info = "{ idUsuario: " + idUsuario
                    + ", IDServicio: " + IDServicio
                    + ", cantAdultos: " + cantAdultos
                    + ", cantMenores: " + cantMenores
                    + ", cantMenores2: " + cantMenores2
                    + " }";

                $.ajax({
                    type: "POST",
                    url: "/traslados-paso1.aspx/AddTraslado",
                    data: info,
                    async: true,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, text) {
                        var idPedido = parseInt($("#hdnIDPedido").val());
                        if (idPedido > 0)
                            window.location.href = "traslados-paso2.aspx?Id=" + idPedido;
                        else
                            window.location.href = "traslados-paso2.aspx";
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        var err = eval("(" + xhr.responseText + ")");
                        alert(err.Message);
                    }
                });
                return false;
            } else {
                $("#spnCantidadAdultos").html("la cantidad debe ser mayor a 0");
                return false;
            }
        }
        else
            return false;
    }

    function validate(control, span) {
        var campo = $("#" + control).val();
        var error = $("#" + span).html();
        if (campo == "" || campo == null) {
            $("#" + span).html("obligatorio");
            return false;
        }
        else {
            $("#" + span).html("");
            return true;
        }
    }

    function getPrecios() {
        var idServicio = $("#cmbServicio").val();
        if ($("#cmbServicioEspecial").val() != null && $("#cmbServicioEspecial").val() != "")
            idServicio = $("#cmbServicioEspecial").val();

        if (idServicio != "") {
            idServicio = parseInt(idServicio);
            $.ajax({
                type: "POST",
                url: "/traslados-paso1.aspx/GetPrecios",
                data: "{ idServicio: " + idServicio + " }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data, text) {
                    if (data.d != null && data.d.length > 0) {
                        $("#spnRegular").html("USD " + data.d[0] + ".-");
                        $("#spnPrivado").html("USD " + data.d[1] + ".-");
                        $("#spnRegularNR").html("USD " + data.d[2] + ".-");
                        $("#spnPrivadoNR").html("USD " + data.d[3] + ".-");

                        $("#obsRegular,#obsRegular2").html(data.d[4]);
                        $("#obsPrivado,#obsPrivado2").html(data.d[5]);

                        $("#rdbRegular").prop('checked', false);
                        $("#rdbRegularNR").prop('checked', false);
                        $("#rdbPrivado").prop('checked', false);
                        $("#rdbPrivadoNR").prop('checked', false);

                        if (data.d[0] != "0") {
                            $("#divRegular").show();
                            $("#rdbRegular").prop('checked', true);
                        }
                        else
                            $("#divRegular").hide();

                        if (data.d[1] != "0") {
                            $("#divPrivado").show();
                            $("#rdbPrivado").prop('checked', true);
                        }
                        else
                            $("#divPrivado").hide();

                        if (data.d[2] != "0")
                            $("#divRegularNR").show();
                        else
                            $("#divRegularNR").hide();

                        if (data.d[3] != "0")
                            $("#divPrivadoNR").show();
                        else
                            $("#divPrivadoNR").hide();

                        $("#divPrecios, #divMsjAdicional").show();

                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    var err = eval("(" + xhr.responseText + ")");
                    alert(err.Message);
                }
            });
        }
        else {
            $("#spnRegular, #spnPrivado, #spnRegularNR, #spnPrivadoNR,#obsRegular,#obsRegular2,#obsPrivado,#obsPrivado2").html("");
            $("#divPrecios, #divMsjAdicional").hide();
            //     $("#divForm").hide();
            $("#hdnTipoServicio").val("");
            $("#hdnTipo").val("");
            $("#divRegular, #divPrivado, #divRegularNR, #divPrivadoNR").hide();
        }
    }

    $(document).ready(function () {
        hideCantPasajeros();
        configControls();
    });
