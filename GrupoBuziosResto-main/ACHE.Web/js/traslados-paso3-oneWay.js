
$(document).ready(function () {

    configControls();
    if ($("#hdnIda").val() == "true") {
        $("#divIda").show();
        $("#soloida").attr('checked', 'checked');
        if ($("#hdnLugar1").val() == "Aeropuerto") {
            $("#divAeropuertoIda").show();
            $("#divHotelIda").show();
            $("#spnHotelIda").html("Al Hotel");

        } else {
            $("#divHotelIda").show();
            if ($("#hdnLugar2").val() == "Aeropuerto") {
                $("#divAeropuertoIda").show();
                $("#spnHotelIda").html("Al Hotel");
            } else {
                $("#divHotelIda2").show();
                $("#spnHotelIda").html("Desde Hotel");
                $("#spnHotelIda2").html("Al Hotel");

            }
        }
    } else {
        $("#solovuelta").attr('checked', 'checked');
        $("#divVuelta").show();
        $("#divIda").hide();
        if ($("#hdnLugar1").val() == "Aeropuerto") {
            $("#divAeropuertoVuelta").show();
            llenarComboVuelta();
            $("#divHotelVuelta").show();
            $("#spnHotelVuelta").html("Desde Hotel");
        } else {
            $("#divHotelVuelta").show();
            $("#divHotelVuelta").show();
            if ($("#hdnLugar2").val() == "Aeropuerto") {
                $("#divAeropuertoVuelta").show();
                llenarComboVuelta();
                $("#spnHotelVuelta").html("Desde Hotel");
            } else {
                $("#divHotelVuelta2").show();
                $("#spnHotelVuelta").html("Desde Hotel");
                $("#spnHotelVuelta2").html("Al Hotel");
            }
        }
    }
    getPrecios();
    $('#txtFechaIda, #txtFechaVuelta').change(function () {
        getPrecios();
    });
});

function getPrecios() {
    if ($("#divIda").is(":visible")) {
        $('#txtFechaVuelta').val("");

    }
    if ($("#divVuelta").is(":visible")) {
        $('#txtFechaIda').val("");

    }
    if ($('#txtFechaIda').val() != "" || $('#txtFechaVuelta').val() != "") {
       

        $.ajax({
            type: "POST",
            url: "/traslados-paso3.aspx/GetPrecios",
            data: "{ fechaIda: '" + $('#txtFechaIda').val() + "', fechaVuelta: '" + $('#txtFechaVuelta').val() + "', idPedido: " + $("#hdnIDPedido").val() + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, text) {
                if (data.d != null && data.d.length > 0) {
                    $("#spnRegular").html("USD " + data.d[0] + ".-");
                    $("#spnPrivado").html("USD " + data.d[1] + ".-");
                  

                    $("#obsRegular").html(data.d[2]);
                    $("#obsPrivado").html(data.d[3]);
                    $("#titPrecio").show();
                    $("#rdbRegular").prop('checked', false);
                    $("#rdbPrivado").prop('checked', false);

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
        $("#titPrecio").hide();

        $("#spnRegular, #spnPrivado, #spnRegularNR, #spnPrivadoNR,#obsRegular,#obsRegular2,#obsPrivado,#obsPrivado2").html("");
        $("#divPrecios, #divMsjAdicional").hide();
        //     $("#divForm").hide();
        $("#hdnTipoServicio").val("");
        $("#hdnTipo").val("");
        $("#divRegular, #divPrivado, #divRegularNR, #divPrivadoNR").hide();
    }
}
function configControls() {
    $('#txtFechaIda, #txtFechaVuelta').datepicker({
        format: 'dd/mm/yyyy',
        autoclose: true,
        language: "es",
        todayHighlight: true
    });
    var hora = parseInt($("#cmbHoraIda").val());
    if ((hora >= 21 || hora <= 4))
        $("#spnPrecioAdicional").show();
    else
        $("#spnPrecioAdicional").hide();

    $("#cmbHoraIda").change(function () {
        var hora = parseInt($("#cmbHoraIda").val());
        if (hora == 0 || (hora >= 21 || hora <= 4))
            $("#spnPrecioAdicional").show();
        else
            $("#spnPrecioAdicional").hide();
    });

 

    $('#soloida').click(function () {
        if ($(this).is(':checked')) {
            $("#divIda").show();
            $("#divVuelta").hide();
            if ($("#hdnLugar1").val() == "Aeropuerto") {
                $("#divAeropuertoIda").show();
                $("#divHotelIda").show();
                $("#spnHotelIda").html("Al Hotel");
            } else {
                $("#divHotelIda").show();
                if ($("#hdnLugar2").val() == "Aeropuerto") {
                    $("#divAeropuertoIda").show();
                    $("#spnHotelIda").html("Al Hotel");
                } else {
                    $("#divHotelIda2").show();
                    $("#spnHotelIda").html("Desde Hotel");
                    $("#spnHotelIda2").html("Al Hotel");
                }
            }
            getPrecios();

        }
    });

    $('#solovuelta').click(function () {
        if ($(this).is(':checked')) {
          
            $("#divVuelta").show();
            $("#divIda").hide();
            if ($("#hdnLugar1").val() == "Aeropuerto") {
                $("#divAeropuertoVuelta").show();
                llenarComboVuelta();
                $("#divHotelVuelta").show();
                $("#spnHotelVuelta").html("Desde Hotel");
            } else {
                $("#divHotelVuelta").show();
                $("#divHotelVuelta").show();
                if ($("#hdnLugar2").val() == "Aeropuerto") {
                    $("#divAeropuertoVuelta").show();
                    llenarComboVuelta();
                    $("#spnHotelVuelta").html("Desde Hotel");
                } else {
                    $("#divHotelVuelta2").show();
                    $("#spnHotelVuelta").html("Desde Hotel");
                    $("#spnHotelVuelta2").html("Al Hotel");

                }
            }
            getPrecios();

        }

    });

}

function grabarDatos() {
    var isValid = true;
    var fechaIda = "";
    if ($("#divIda").is(":visible")) {
        fechaIda = $("#txtFechaIda").val();
        if (!validate('txtFechaIda', 'spnFechaIda'))
            isValid = false;
        else
            if (!validateFecha('txtFechaIda', 'spnFechaIda'))
                isValid = false;
        
    }

    var idAeropuertoIda = 0;  
    var companiaIda = "";  
    var nroVuelIda = "";
    var horaArriboIda = "";   
    var MinutosArriboIda = "";
    if ($("#divAeropuertoIda").is(":visible")) {
            idAeropuertoIda = parseInt($("#cmbAeropuertoIda").val());
            if (!validate('cmbAeropuertoIda', 'spnAeropuertoIda'))
                isValid = false;
            companiaIda = $("#txtCompaniaAerea").val();
            if (!validate('txtCompaniaAerea', 'spnCompaniaAerea'))
                isValid = false;
            nroVuelIda = $("#txtNroVuelo").val();
            if (!validate('txtNroVuelo', 'spnNroVuelo'))
                isValid = false;
            horaArriboIda = $("#cmbHoraIda").val();
            MinutosArriboIda = $("#cmbMinutosIda").val();
    }

    var nombreHotel1 = "";
    var direccionHotel1 = "";
    if ($("#divHotelIda").is(":visible")) {

        nombreHotel1 = $("#txtNombreHotelIda").val();
        if (!validate('txtNombreHotelIda', 'spnNombreHotelIda'))
            isValid = false;
        direccionHotel1 = $("#txtDireccionHotelIda").val();
      //  if (!validate('txtDireccionHotelIda', 'spnDireccionHotelIda'))
       //     isValid = false;
    }


    var nombreHotel2 = "";
    var direccionHotel2 = "";
    if ($("#divHotelIda2").is(":visible")) {
        nombreHotel2 = $("#txtNombreHotelIda2").val();
        if (!validate('txtNombreHotelIda2', 'spnNombreHotelIda2'))
            isValid = false;
        direccionHotel2 = $("#txtDireccionHotelIda2").val();
      //  if (!validate('txtDireccionHotelIda2', 'spnDireccionHotelIda2'))
      //      isValid = false;
    }

    var fechaVuelta = "";
    if ($("#divVuelta").is(":visible")) {
        fechaVuelta = $("#txtFechaVuelta").val();
        if (!validate('txtFechaVuelta', 'spnFechaVuelta'))
            isValid = false;
    }
    var idAeropuertoVuelta = 0;
    var companiaVuelta = "";
    var nroVueloVuelta = "";
    var minutosArriboVuelta = "";
    var horaArriboVuelta = "";
    if ($("#divAeropuertoVuelta").is(":visible")) {
        idAeropuertoVuelta = parseInt($("#cmbAeropuertoVuelta").val());
        if (!validate('cmbAeropuertoVuelta', 'spnAeropuertoVuelta'))
            isValid = false;
        companiaVuelta = $("#txtCompaniaVuelta").val();
        if (!validate('txtCompaniaVuelta', 'spnCompaniaVuelta'))
            isValid = false;
        nroVueloVuelta = $("#txtNroVueloVuelta").val();
        if (!validate('txtNroVueloVuelta', 'spnNroVueloVuelta'))
            isValid = false;
        horaArriboVuelta = $("#cmbHoraVuelta").val();
        minutosArriboVuelta = $("#cmbMinutosVuelta").val();
    }

    var nombreHotel3 = "";
    var direccionHotel3 = "";

    if ($("#divHotelVuelta").is(":visible")) {
        nombreHotel3 = $("#txtNombreHotelVuelta").val();
        if (!validate('txtNombreHotelVuelta', 'spnNombreHotelVuelta'))
            isValid = false;

        direccionHotel3 = $("#txtDireccionHotelVuelta").val();
        if (!validate('txtDireccionHotelVuelta', 'spnDireccionHotelVuelta'))
            isValid = false;
    }

    var nombreHotel4 = "";
    var direccionHotel4 = "";
    if ($("#divHotelVuelta2").is(":visible")) {
        nombreHotel4 = $("#txtNombreHotelVuelta2").val();
        if (!validate('txtNombreHotelVuelta2', 'spnNombreHotelVuelta2'))
            isValid = false;
        direccionHotel4 = $("#txtDireccionHotelVuelta2").val();
     //   if (!validate('txtDireccionHotelVuelta2', 'spnDireccionHotelVuelta2'))
          //  isValid = false;
    }


    var observaciones = $("#txtObservaciones").val();
    if (isValid) {
        var info = "{ idPedido: " + $("#hdnIDPedido").val()
               + ", fechaIda: '" + fechaIda
               + "', idAeropuertoIda: " + idAeropuertoIda
               + ", companiaIda: '" + companiaIda
               + "', nroVueloIda:' " + nroVuelIda
               + "', horaArriboIda: '" + horaArriboIda + ":" + MinutosArriboIda
               + "', nombreHotel1: '" + nombreHotel1
               + "', direccionHotel1: '" + direccionHotel1
               + "', nombreHotel2: '" + nombreHotel2
               + "', direccionHotel2: '" + direccionHotel2
               + "', fechaVuelta: '" + fechaVuelta
               + "', idAeropuertoVuelta: " + idAeropuertoVuelta
               + ", companiaVuelta: '" + companiaVuelta
               + "', nroVueloVuelta: '" + nroVueloVuelta
               + "', horaArriboVuelta: '" + horaArriboVuelta + ":" + minutosArriboVuelta
               + "', nombreHotel3: '" + nombreHotel3
               + "', direccionHotel3: '" + direccionHotel3
               + "', nombreHotel4: '" + nombreHotel4
               + "', direccionHotel4: '" + direccionHotel4
               + "', observaciones: '" + observaciones
               + "'}";
        $.ajax({
            type: "POST",
            url: "/traslados-paso3.aspx/CrearPedido",
            data: info,
            async: true,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, text) {
                window.location.href = "carrito-conf-traslados.aspx";
            },
            error: function (xhr, ajaxOptions, thrownError) {
                var err = eval("(" + xhr.responseText + ")");
                alert(err.Message);
                return false;
            }
        });
    } else {
        return false;
    }



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

function irPaso2() {
    var idPedido = parseInt($("#hdnIDPedido").val());
    if (idPedido > 0)
        window.location.href = "traslados-paso2.aspx?Id=" + idPedido;
    else
        window.location.href = "traslados-paso2.aspx";
    return false;
}

function validateFecha(fecha, span) {
    var fecha = $("#" + fecha).val();
    var today = new Date();
    var desde = new Date(today);
    desde.setDate(today.getDate() + 3);
    var dd = desde.getDate();
    var mm = desde.getMonth() + 1;
    var yyyy = desde.getFullYear();
    if (dd < 10)
        dd = '0' + dd
    if (mm < 10)
        mm = '0' + mm
    var fechaDesde = dd + '/' + mm + '/' + yyyy;
    if (fecha != "" && fechaDesde != "") {
        var fechaArr = fecha.split("/");
        var desdeArr = fechaDesde.split("/");
        if (Date.parse(fechaArr[1] + "-" + fechaArr[0] + "-" + fechaArr[2]) < Date.parse(desdeArr[1] + "-" + desdeArr[0] + "-" + desdeArr[2])) {
            $("#" + span).html("<br>La fecha de ida debe ser al menos para dentro de tres días.");
            return false;
        }
        else return true;
    }
}

function llenarComboVuelta() {
    $.ajax({
        type: "POST",
        url: "/traslados-paso3-oneWay.aspx/GetAeropuertosVuelta",
        data: "{ idPedido: " + $("#hdnIDPedido").val() + " }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, text) {
            if (data.d != null && data.d.length > 0) {
                $("#cmbAeropuertoVuelta").empty();
              //  $("#cmbAeropuertoVuelta").append("<option value=''>Seleccione un servicio</option>");
                for (var i = 0; i < data.d.length; i++)
                    $("#cmbAeropuertoVuelta").append("<option value='" + data.d[i].ID + "'>" + data.d[i].Nombre + "</option>");

            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            var err = eval("(" + xhr.responseText + ")");
            alert(err.Message);
        }
    });
}