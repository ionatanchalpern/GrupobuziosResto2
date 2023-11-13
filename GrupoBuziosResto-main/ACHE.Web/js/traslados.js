var traslados = {
    configControls: function () {
        $('#txtDesde, #txtHasta, #txtFecha').datepicker({
            format: 'dd/mm/yyyy',
            autoclose: true,
            language: "es",
            todayHighlight: true
        });

        /*$("#txtDesde, #txtHasta, #txtPasajero").keypress(function (event) {
            var keycode = (event.keyCode ? event.keyCode : event.which);
            if (keycode == '13') {
                filter();
                return false;
            }
        });*/

        
        $("#cmbHora").change(function () {
            var hora = parseInt($("#cmbHora").val());
            if (hora!=0 && (hora >= 21 || hora <= 4))
                $("#spnPrecioAdicional").show();
            else
                $("#spnPrecioAdicional").hide();
        });

        $("#cmbIdaHora").change(function () {
            var hora = parseInt($("#cmbIdaHora").val());
            if (hora != 0 && (hora >= 21 || hora <= 4))
                $("#spnPrecioAdicional1").show();
            else
                $("#spnPrecioAdicional1").hide();
        });

        $("#cmbProveedor").change(function () {
            traslados.getServicios();
            traslados.getOrigenes();
            traslados.getDestinos();
        });

        $("#cmbServicio").change(function () {
            traslados.getPrecios();
        });

        /*if ($("#rdbIdaVuelta").is(":checked"))
            $("#divVuelta").show();

        $('#rdbIdaVuelta').click(function () {
            if ($(this).is(':checked'))
                $("#divVuelta").show();
        });

        $('#rdbIda').click(function () {
            if ($(this).is(':checked'))
                $("#divVuelta").hide();
        });*/
    },
    getServicios: function () {
        var idProveedor = $("#cmbProveedor").val();
        if (idProveedor != "") {
            idProveedor = parseInt(idProveedor);
            $.ajax({
                type: "POST",
                url: "/default-traslados.aspx/GetServicios",
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
    },
    getPrecios: function () {
        var idServicio = $("#cmbServicio").val();
        if (idServicio != "") {
            idServicio = parseInt(idServicio);
            $.ajax({
                type: "POST",
                url: "/default-traslados.aspx/GetPrecios",
                data: "{ idServicio: " + idServicio + " }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data, text) {
                    if (data.d != null && data.d.length > 0) {
                        $("#spnRegular").html(data.d[0]);
                        $("#spnPrivado").html(data.d[1]);
                        $("#spnRegularNR").html(data.d[2]);
                        $("#spnPrivadoNR").html(data.d[3]);

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

                        if (data.d[1] != "0"){
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

                        $("#hdnTipoServicio").val(data.d[6]);
                        
                        if (data.d[6] == "R") {//RoundTrip: muestro ida y vuelta
                            $("#divRdbIdayVuelta, #divFechaIdayVuelta").show();
                            $("#divRdbIda, #divRdbVuelta, #divFechaIndividual").hide();
                            $("#rdbIdaVuelta").prop("checked", true);
                            $("#rdbIda, #rdbVuelta").prop("checked", false);

                            traslados.changeRdb('');
                        }
                        else if (data.d[6] == "H"){// de hotel a hotel
                            $("#divRdbIdayVuelta, #divFechaIdayVuelta").hide();
                            $("#divRdbIda, #divRdbVuelta").hide();
                            $("#divFechaIndividual").show();

                            $("#rdbIdaVuelta,#rdbVuelta, #rdbIda").prop("checked", false);
                            traslados.changeRdb('H');
                        }
                        else {//Solo Ida o Solo vuelta

                            if ($("#hdnIDPedido").val() == "" || $("#hdnIDPedido").val() == "0") {

                                $("#divRdbIdayVuelta, #divFechaIdayVuelta").hide();
                                $("#divRdbIda, #divRdbVuelta, #divFechaIndividual").show();

                                $("#rdbIdaVuelta,#rdbVuelta").prop("checked", false);
                                $("#rdbIda").prop("checked", true);

                                traslados.changeRdb('I');
                            }
                            else {//si es edicion

                                $("#divRdbIdayVuelta, #divFechaIdayVuelta").hide();
                                $("#divRdbIda, #divRdbVuelta, #divFechaIndividual").show();

                                if ($("#hdnTipo").val() == "I") {
                                    $("#rdbIdaVuelta,#rdbVuelta").prop("checked", false);
                                    $("#rdbIda").prop("checked", true);
                                }
                                else {
                                    $("#rdbIdaVuelta,#rdbIda").prop("checked", false);
                                    $("#rdbVuelta").prop("checked", true);
                                }

                                traslados.changeRdb($("#hdnTipo").val());
                            }
                        }
                        
                        $("#divForm").show();
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
            $("#divForm").hide();
            $("#hdnTipoServicio").val("");
            $("#hdnTipo").val("");
            $("#divRegular, #divPrivado, #divRegularNR, #divPrivadoNR").hide();
        }
    },
    getOrigenes: function () {
        var idProveedor = $("#cmbProveedor").val();
        if (idProveedor != "") {
            idProveedor = parseInt(idProveedor);
            $.ajax({
                type: "POST",
                url: "/default-traslados.aspx/GetOrigenes",
                data: "{ idProveedor: " + idProveedor + " }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data, text) {
                    if (data.d != null && data.d.length > 0) {
                        $("#cmbOrigen, #ddlIdaOrigen, #ddlVueltaDestino, #ddlInternoOrigen").empty();
                        $("#cmbOrigen, #ddlIdaOrigen, #ddlVueltaDestino, #ddlInternoOrigen").append("<option value=''>Seleccione una opcion</option>");
                        for (var i = 0; i < data.d.length; i++)
                            $("#cmbOrigen, #ddlIdaOrigen, #ddlVueltaDestino, #ddlInternoOrigen").append("<option value='" + data.d[i].ID + "'>" + data.d[i].Nombre + "</option>");
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    var err = eval("(" + xhr.responseText + ")");
                    alert(err.Message);
                }
            });
        }
        else {
            $("#cmbOrigen, #ddlIdaOrigen, #ddlVueltaDestino, #ddlInternoOrigen").empty();
            $("#cmbOrigen, #ddlIdaOrigen, #ddlVueltaDestino, #ddlInternoOrigen").append("<option value=''>Seleccione una opcion</option>");
        }
    },
    getDestinos: function () {
        var idProveedor = $("#cmbProveedor").val();
        if (idProveedor != "") {
            idProveedor = parseInt(idProveedor);
            $.ajax({
                type: "POST",
                url: "/default-traslados.aspx/GetDestinos",
                data: "{ idProveedor: " + idProveedor + " }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data, text) {
                    if (data.d != null && data.d.length > 0) {
                        $("#cmbDestino, #ddlVueltaOrigen, #ddlIdaDestino, #ddlInternoDestino").empty();
                        $("#cmbDestino, #ddlVueltaOrigen, #ddlInternoDestino").append("<option value=''>Seleccione una opcion</option>");
                        for (var i = 0; i < data.d.length; i++)
                            $("#cmbDestino, #ddlVueltaOrigen, #ddlIdaDestino, #ddlInternoDestino").append("<option value='" + data.d[i].ID + "'>" + data.d[i].Nombre + "</option>");
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    var err = eval("(" + xhr.responseText + ")");
                    alert(err.Message);
                }
            });
        }
        else {
            $("#cmbDestino, #ddlVueltaOrigen, #ddlIdaDestino, #ddlInternoDestino").empty();
            $("#cmbDestino, #ddlVueltaOrigen, #ddlIdaDestino, #ddlInternoDestino").append("<option value=''>Seleccione una opcion</option>");
        }
    },
    validarPedidoIdayVuelta: function () {
        var isValid = true;

        if (!traslados.validate('cmbProveedor', 'spnProveedor'))
            isValid = false;
        if (!traslados.validate('cmbServicio', 'spnServicio'))
            isValid = false;
        if (!traslados.validate('txtDesde', 'spnDesde'))
            isValid = false;
        if (!traslados.validate('txtHasta', 'spnHasta'))
            isValid = false;
        if (traslados.validate('txtDesde', 'spnDesde') && traslados.validate('txtHasta', 'spnHasta')) {
            if (!traslados.validateFechaMenor('txtDesde', 'txtHasta', 'spnDesde'))
                isValid = false;
        }
        if (!traslados.validateFecha('txtDesde', 'spnDesde'))
            isValid = false;
        if (!traslados.validateFecha('txtHasta', 'spnHasta'))
            isValid = false;
        if (!traslados.validate('cmbCantidadAdultos', 'spnCantidadAdultos'))
            isValid = false;
        if (!traslados.validate('cmbCantidadMenores', 'spnCantidadMenores'))
            isValid = false;
        if (!traslados.validate('cmbCantidadMenores2', 'spnCantidadMenores2'))
            isValid = false;
        if (!traslados.validate('txtPasajero', 'spnPasajero'))
            isValid = false;
        if (!traslados.validate('txtDni', 'spnDni'))
            isValid = false;
        /*if (!traslados.validate('txtEmail', 'spnEmail'))
            isValid = false;
        else {
            if (!IsValidEmail($("#txtEmail").val())) {
                isValid = false;
                $("#spnEmail").html("Debe ingresar un email válido");
            }
        }*/
        if (!traslados.validate('cmbOrigen', 'spnOrigen'))
            isValid = false;
        if (!traslados.validate('txtAerolinea', 'spnAerolinea'))
            isValid = false;
        if (!traslados.validate('txtNumeroVuelo', 'spnNumeroVuelo'))
            isValid = false;
        //if (!traslados.validate('cmbHora', 'spnHora') || !traslados.validate('cmbMinutos', 'spnHora'))
        //    isValid = false;
        if (!traslados.validate('txtAerolinea2', 'spnAerolinea2'))
            isValid = false;
        if (!traslados.validate('txtNumeroVuelo2', 'spnNumeroVuelo2'))
            isValid = false;
        //if (!traslados.validate('cmbHora2', 'spnHora2') || !traslados.validate('cmbMinutos2', 'spnHora2'))
        //    isValid = false;
        if (!traslados.validate('cmbDestino', 'spnDestino'))
            isValid = false;
        if (!traslados.validate('txtHotel', 'spnHotel'))
            isValid = false;

        var tipo = "";
        if ($("#rdbRegular").is(":checked"))
            tipo = "R";
        if ($("#rdbRegularNR").is(":checked"))
            tipo = "RNR";
        if ($("#rdbPrivado").is(":checked"))
            tipo = "P";
        if ($("#rdbPrivadoNR").is(":checked"))
            tipo = "PNR";

        if (tipo == ""){
            isValid = false;
            $("#spnErrorServicio").html("Seleccione el tipo de servicio");
            $("#spnErrorServicio").show();
        }
        else
            $("#spnErrorServicio").hide();


        if (isValid) {
            traslados.crearPedidoIdayVuelta();
            return false;
        }
        else
            return false;
    },
    validarPedidoIda: function () {
        var isValid = true;

        if (!traslados.validate('cmbProveedor', 'spnProveedor'))
            isValid = false;
        if (!traslados.validate('cmbServicio', 'spnServicio'))
            isValid = false;
        //if (!traslados.validate('txtDesde', 'spnDesde'))
        //    isValid = false;
        //if ($("#rdbVuelta").is(":checked") || $("#rdbIda").is(":checked")) {
        if (!traslados.validate('txtFecha', 'spnFecha'))
            isValid = false;
        if (!traslados.validateFecha('txtFecha', 'spnFecha'))
            isValid = false;
        if (!traslados.validate('cmbCantidadAdultos', 'spnCantidadAdultos'))
            isValid = false;
        if (!traslados.validate('cmbCantidadMenores', 'spnCantidadMenores'))
            isValid = false;
        if (!traslados.validate('cmbCantidadMenores2', 'spnCantidadMenores2'))
            isValid = false;
        if (!traslados.validate('txtPasajero', 'spnPasajero'))
            isValid = false;
        if (!traslados.validate('txtDni', 'spnDni'))
            isValid = false;
        /*if (!traslados.validate('txtEmail', 'spnEmail'))
            isValid = false;
        else {
            if (!IsValidEmail($("#txtEmail").val())) {
                isValid = false;
                $("#spnEmail").html("Debe ingresar un email válido");
            }
        }*/
        if (!traslados.validate('ddlIdaOrigen', 'spnIdaOrigenError'))
            isValid = false;
        if (!traslados.validate('txtIdaAerolinea', 'spnIdaAerolineaError'))
            isValid = false;
        if (!traslados.validate('txtIdaNumeroVuelo', 'spnIdaNumeroVueloError'))
            isValid = false;
        if (!traslados.validate('ddlIdaDestino', 'spnIdaDestinoError'))
            isValid = false;
        if (!traslados.validate('txtIdaHotel', 'spnIdaHotelError'))
            isValid = false;

        var tipo = "";
        if ($("#rdbRegular").is(":checked"))
            tipo = "R";
        if ($("#rdbRegularNR").is(":checked"))
            tipo = "RNR";
        if ($("#rdbPrivado").is(":checked"))
            tipo = "P";
        if ($("#rdbPrivadoNR").is(":checked"))
            tipo = "PNR";

        if (tipo == "") {
            isValid = false;
            $("#spnErrorServicio").html("Seleccione el tipo de servicio");
            $("#spnErrorServicio").show();
        }
        else
            $("#spnErrorServicio").hide();


        if (isValid) {
            traslados.crearPedidoIda();
            return false;
        }
        else
            return false;
    },
    validarPedidoVuelta: function () {
        var isValid = true;

        if (!traslados.validate('cmbProveedor', 'spnProveedor'))
            isValid = false;
        if (!traslados.validate('cmbServicio', 'spnServicio'))
            isValid = false;
        //if (!traslados.validate('txtDesde', 'spnDesde'))
        //    isValid = false;
        /*if ($("#rdbIdaVuelta").is(":checked")) {
            if (!traslados.validate('txtHasta', 'spnHasta'))
                isValid = false;
            else if (traslados.validate('txtDesde', 'spnDesde') && traslados.validate('txtHasta', 'spnHasta')) {
                if (!traslados.validateFechaMenor('txtDesde', 'txtHasta', 'spnDesde'))
                    isValid = false;
            }
        }*/
        if (!traslados.validate('txtFecha', 'spnFecha'))
            isValid = false;
        if (!traslados.validateFecha('txtFecha', 'spnFecha'))
            isValid = false;
        if (!traslados.validate('cmbCantidadAdultos', 'spnCantidadAdultos'))
            isValid = false;
        if (!traslados.validate('cmbCantidadMenores', 'spnCantidadMenores'))
            isValid = false;
        if (!traslados.validate('cmbCantidadMenores2', 'spnCantidadMenores2'))
            isValid = false;
        if (!traslados.validate('txtPasajero', 'spnPasajero'))
            isValid = false;
        if (!traslados.validate('txtDni', 'spnDni'))
            isValid = false;
        /*if (!traslados.validate('txtEmail', 'spnEmail'))
            isValid = false;
        else {
            if (!IsValidEmail($("#txtEmail").val())) {
                isValid = false;
                $("#spnEmail").html("Debe ingresar un email válido");
            }
        }*/
        if (!traslados.validate('ddlVueltaOrigen', 'spnVueltaOrigen'))
            isValid = false;
        if (!traslados.validate('txtVueltaHotel', 'spnVueltaHotel'))
            isValid = false;
        if (!traslados.validate('ddlVueltaDestino', 'spnVueltaDestino'))
            isValid = false;
        //if (!traslados.validate('cmbHora', 'spnHora') || !traslados.validate('cmbMinutos', 'spnHora'))
        //    isValid = false;
        if (!traslados.validate('txtVueltaCompaniaAerea', 'spnVueltaCompaniaAerea'))
            isValid = false;
        //if (!traslados.validate('txtVueltaNroVuelo', 'spnNumeroVuelo2'))
        //    isValid = false;
        //if (!traslados.validate('cmbHora2', 'spnHora2') || !traslados.validate('cmbMinutos2', 'spnHora2'))
        //    isValid = false;
        //if (!traslados.validate('cmbDestino', 'spnDestino'))
        //    isValid = false;
        //if (!traslados.validate('txtHotel', 'spnHotel'))
        //    isValid = false;

        var tipo = "";
        if ($("#rdbRegular").is(":checked"))
            tipo = "R";
        if ($("#rdbRegularNR").is(":checked"))
            tipo = "RNR";
        if ($("#rdbPrivado").is(":checked"))
            tipo = "P";
        if ($("#rdbPrivadoNR").is(":checked"))
            tipo = "PNR";

        if (tipo == "") {
            isValid = false;
            $("#spnErrorServicio").html("Seleccione el tipo de servicio");
            $("#spnErrorServicio").show();
        }
        else
            $("#spnErrorServicio").hide();

        if (isValid) {
            traslados.crearPedidoVuelta();
            return false;
        }
        else
            return false;
    },
    validarPedidoInterno: function () {
        var isValid = true;

        if (!traslados.validate('cmbProveedor', 'spnProveedor'))
            isValid = false;
        if (!traslados.validate('cmbServicio', 'spnServicio'))
            isValid = false;
        //if (!traslados.validate('txtDesde', 'spnDesde'))
        //    isValid = false;
        //if ($("#rdbVuelta").is(":checked") || $("#rdbIda").is(":checked")) {
        if (!traslados.validate('txtFecha', 'spnFecha'))
            isValid = false;
        if (!traslados.validateFecha('txtFecha', 'spnFecha'))
            isValid = false;
        if (!traslados.validate('cmbCantidadAdultos', 'spnCantidadAdultos'))
            isValid = false;
        if (!traslados.validate('cmbCantidadMenores', 'spnCantidadMenores'))
            isValid = false;
        if (!traslados.validate('cmbCantidadMenores2', 'spnCantidadMenores2'))
            isValid = false;
        if (!traslados.validate('txtPasajero', 'spnPasajero'))
            isValid = false;
        if (!traslados.validate('txtDni', 'spnDni'))
            isValid = false;
        /*if (!traslados.validate('txtEmail', 'spnEmail'))
            isValid = false;
        else {
            if (!IsValidEmail($("#txtEmail").val())) {
                isValid = false;
                $("#spnEmail").html("Debe ingresar un email válido");
            }
        }*/
        if (!traslados.validate('ddlInternoOrigen', 'spnInternoOrigenError'))
            isValid = false;
        if (!traslados.validate('ddlInternoDestino', 'spnInternoDestinoError'))
            isValid = false;
        if (!traslados.validate('txtInternoOrigenHotel', 'spnInternoOrigenHotelError'))
            isValid = false;
        if (!traslados.validate('txtInternoDestinoHotel', 'spnInternoDestinoHotelError'))
            isValid = false;

        var tipo = "";
        if ($("#rdbRegular").is(":checked"))
            tipo = "R";
        if ($("#rdbRegularNR").is(":checked"))
            tipo = "RNR";
        if ($("#rdbPrivado").is(":checked"))
            tipo = "P";
        if ($("#rdbPrivadoNR").is(":checked"))
            tipo = "PNR";

        if (tipo == "") {
            isValid = false;
            $("#spnErrorServicio").html("Seleccione el tipo de servicio");
            $("#spnErrorServicio").show();
        }
        else
            $("#spnErrorServicio").hide();


        if (isValid) {
            traslados.crearPedidoInterno();
            return false;
        }
        else
            return false;
    },
    validate: function (control, span) {
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
    },
    validateCombo: function (control, span) {
        var campo = $("#" + control).val();
        var error = $("#" + span).html();
        if (campo == "0" || campo == null) {
            $("#" + span).html("obligatorio");
            return false;
        }
        else {
            $("#" + span).html("");
            return true;
        }
    },
    validateFecha: function (fecha, span) {
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
    },
    validateFechaMenor: function (fecha1, fecha2, span) {
        var desde = $("#" + fecha1).val();
        var hasta = $("#" + fecha2).val();
        if (desde != "" && hasta != "") {
            var desdeArr = desde.split("/");
            var hastaArr = hasta.split("/");
            if (Date.parse(desdeArr[1] + "-" + desdeArr[0] + "-" + desdeArr[2]) >= Date.parse(hastaArr[1] + "-" + hastaArr[0] + "-" + hastaArr[2])) {
                $("#" + span).html("<br>La fecha de ida debe ser menor a la fecha de vuelta");
                return false;
            }
            else return true;
        }
    },
    crearPedidoIdayVuelta: function () {
        var idProveedor = parseInt($("#cmbProveedor").val());
        var idServicio = parseInt($("#cmbServicio").val());
        var tipoServicio = "";

        var idOrigen = parseInt($("#cmbOrigen").val());
        var aerolinea = $("#txtAerolinea").val();
        var numero = $("#txtNumeroVuelo").val();
        var hora = $("#cmbHora").val();
        var minutos = $("#cmbMinutos").val();
        var aerolinea2 = $("#txtAerolinea2").val();
        var numero2 = $("#txtNumeroVuelo2").val();
        var hora2 = $("#cmbHora2").val();
        var minutos2 = $("#cmbMinutos2").val();
        var idDestino = parseInt($("#cmbDestino").val());
        var hotel = $("#txtHotel").val();
        var idUsuario = parseInt($("#hdnIDUsuario").val());
        var cantAdultos = parseInt($("#cmbCantidadAdultos").val());
        var cantMenores = parseInt($("#cmbCantidadMenores").val());
        var cantMenores2 = parseInt($("#cmbCantidadMenores2").val());
        var fechaIda = $("#txtDesde").val();
        var fechaVuelta = $("#txtHasta").val();
        var tipo = "";

        if ($("#rdbRegular").is(":checked"))
            tipo = "R";
        if ($("#rdbRegularNR").is(":checked"))
            tipo = "RNR";
        if ($("#rdbPrivado").is(":checked"))
            tipo = "P";
        if ($("#rdbPrivadoNR").is(":checked"))
            tipo = "PNR";

        var pasajero = $("#txtPasajero").val();
        var dni = $("#txtDni").val();
        var obs = $("#txtObservaciones").val();

        var info = "{ idProveedor: " + idProveedor
            + ", idServicio: " + idServicio
            + ", idOrigen: " + idOrigen
            + ", idDestino: " + idDestino
            + ", idUsuario: " + idUsuario
            + ", cantAdultos: " + cantAdultos
            + ", cantMenores: " + cantMenores
            + ", cantMenores2: " + cantMenores2
            + ", fechaIda: '" + fechaIda//auxIda
            + "', fechaVuelta: '" + fechaVuelta//auxVuelta
            + "', tipo: '" + tipo
            + "', pasajero: '" + pasajero
            + "', dni: '" + dni
            //+ "', email: '" + email
            + "', aerolineaArribo: '" + aerolinea
            + "', numeroVueloArribo: '" + numero
            + "', horaArribo: '" + hora + ":" + minutos
            + "', aerolineaPartida: '" + aerolinea2
            + "', numeroVueloPartida: '" + numero2
            + "', horaPartida: '" + hora2 + ":" + minutos2
            + "', hotel: '" + hotel
            + "', observaciones: '" + obs
            + "', hotel2: '"
            + "', observaciones2: '"
            + "', idPedido: " + parseInt($("#hdnIDPedido").val())
            + " }";

        $.ajax({
            type: "POST",
            url: "/default-traslados.aspx/CrearPedido",
            data: info,
            async: true,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, text) {
                if (data.d > 0) {
                    if (parseInt($("#hdnIDPedido").val()) > 0)
                        window.location.href = "carrito-conf-traslados.aspx?IdPedido=" + data.d + "&Mode=M";
                    else
                        window.location.href = "carrito-conf-traslados.aspx?IdPedido=" + data.d;
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                var err = eval("(" + xhr.responseText + ")");
                alert(err.Message);
            }
        });
    },
    crearPedidoIda: function () {
        var idProveedor = parseInt($("#cmbProveedor").val());
        var idServicio = parseInt($("#cmbServicio").val());
        var tipoServicio = "I";

        var idOrigen = parseInt($("#ddlIdaOrigen").val());
        var aerolinea = $("#txtIdaAerolinea").val();
        var numero = $("#txtIdaNumeroVuelo").val();
        var hora = $("#cmbIdaHora").val();
        var minutos = $("#cmbIdaMinutos").val();
        var aerolinea2 = "";
        var numero2 = "";
        var hora2 = "";
        var minutos2 = "";
        var idDestino = parseInt($("#ddlIdaDestino").val());
        var hotel = $("#txtIdaHotel").val();

        var idUsuario = parseInt($("#hdnIDUsuario").val());
        var cantAdultos = parseInt($("#cmbCantidadAdultos").val());
        var cantMenores = parseInt($("#cmbCantidadMenores").val());
        var cantMenores2 = parseInt($("#cmbCantidadMenores2").val());
        var fechaIda = $("#txtFecha").val();
        var fechaVuelta = "";//$("#txtHasta").val();
        var tipo = "";

        if ($("#rdbRegular").is(":checked"))
            tipo = "R";
        if ($("#rdbRegularNR").is(":checked"))
            tipo = "RNR";
        if ($("#rdbPrivado").is(":checked"))
            tipo = "P";
        if ($("#rdbPrivadoNR").is(":checked"))
            tipo = "PNR";

        var pasajero = $("#txtPasajero").val();
        var dni = $("#txtDni").val();
        //var email = $("#txtEmail").val();
        var obs = $("#txtIdaObservaciones").val();

        var info = "{ idProveedor: " + idProveedor
            + ", idServicio: " + idServicio
            + ", idOrigen: " + idOrigen
            + ", idDestino: " + idDestino
            + ", idUsuario: " + idUsuario
            + ", cantAdultos: " + cantAdultos
            + ", cantMenores: " + cantMenores
            + ", cantMenores2: " + cantMenores2
            + ", fechaIda: '" + fechaIda//auxIda
            + "', fechaVuelta: '" + fechaVuelta//auxVuelta
            + "', tipo: '" + tipo
            + "', pasajero: '" + pasajero
            + "', dni: '" + dni
            //+ "', email: '" + email
            + "', aerolineaArribo: '" + aerolinea
            + "', numeroVueloArribo: '" + numero
            + "', horaArribo: '" + hora + ":" + minutos
            + "', aerolineaPartida: '" + aerolinea2
            + "', numeroVueloPartida: '" + numero2
            + "', horaPartida: '" + hora2 + ":" + minutos2
            + "', hotel: '" + hotel
            + "', observaciones: '" + obs
            + "', hotel2: '"
            + "', observaciones2: '"
            + "', idPedido: " + parseInt($("#hdnIDPedido").val())
            + " }";

        $.ajax({
            type: "POST",
            url: "/default-traslados.aspx/CrearPedido",
            data: info,
            async: true,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, text) {
                if (data.d > 0) {
                    if (parseInt($("#hdnIDPedido").val()) > 0)
                        window.location.href = "carrito-conf-traslados.aspx?IdPedido=" + data.d + "&Mode=M";
                    else
                        window.location.href = "carrito-conf-traslados.aspx?IdPedido=" + data.d;
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                var err = eval("(" + xhr.responseText + ")");
                alert(err.Message);
            }
        });
    },
    crearPedidoVuelta: function () {
        var idProveedor = parseInt($("#cmbProveedor").val());
        var idServicio = parseInt($("#cmbServicio").val());
        var tipoServicio = "";

        var idOrigen = parseInt($("#ddlVueltaOrigen").val());
        var aerolinea = "";//$("#txtAerolinea").val();
        var numero = "";//$("#txtNumeroVuelo").val();
        var hora = "";//$("#cmbHora").val();
        var minutos = "";//$("#cmbMinutos").val();
        var aerolinea2 = $("#txtVueltaCompaniaAerea").val();
        var numero2 = $("#txtVueltaNroVuelo").val();
        var hora2 = $("#ddlVueltaHora").val();
        var minutos2 = $("#ddlVueltaMinutos").val();
        var idDestino = parseInt($("#ddlVueltaDestino").val());
        var hotel = $("#txtVueltaHotel").val();
        var idUsuario = parseInt($("#hdnIDUsuario").val());
        var cantAdultos = parseInt($("#cmbCantidadAdultos").val());
        var cantMenores = parseInt($("#cmbCantidadMenores").val());
        var cantMenores2 = parseInt($("#cmbCantidadMenores2").val());
        var fechaIda = "";
        var fechaVuelta = $("#txtFecha").val();
        var tipo = "";

        if ($("#rdbRegular").is(":checked"))
            tipo = "R";
        if ($("#rdbRegularNR").is(":checked"))
            tipo = "RNR";
        if ($("#rdbPrivado").is(":checked"))
            tipo = "P";
        if ($("#rdbPrivadoNR").is(":checked"))
            tipo = "PNR";

        var pasajero = $("#txtPasajero").val();
        var dni = $("#txtDni").val();
        //var email = $("#txtEmail").val();
        var obs = $("#txtVueltaObservaciones").val();

        var info = "{ idProveedor: " + idProveedor
            + ", idServicio: " + idServicio
            + ", idOrigen: " + idOrigen
            + ", idDestino: " + idDestino
            + ", idUsuario: " + idUsuario
            + ", cantAdultos: " + cantAdultos
            + ", cantMenores: " + cantMenores
            + ", cantMenores2: " + cantMenores2
            + ", fechaIda: '" + fechaIda//auxIda
            + "', fechaVuelta: '" + fechaVuelta//auxVuelta
            + "', tipo: '" + tipo
            + "', pasajero: '" + pasajero
            + "', dni: '" + dni
            //+ "', email: '" + email
            + "', aerolineaArribo: '" + aerolinea
            + "', numeroVueloArribo: '" + numero
            + "', horaArribo: '" + hora + ":" + minutos
            + "', aerolineaPartida: '" + aerolinea2
            + "', numeroVueloPartida: '" + numero2
            + "', horaPartida: '" + hora2 + ":" + minutos2
            + "', hotel: '" + hotel
            + "', observaciones: '" + obs
            + "', hotel2: '"
            + "', observaciones2: '"
            + "', idPedido: " + parseInt($("#hdnIDPedido").val())
            + " }";

        $.ajax({
            type: "POST",
            url: "/default-traslados.aspx/CrearPedido",
            data: info,
            async: true,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, text) {
                if (data.d > 0) {
                    if (parseInt($("#hdnIDPedido").val()) > 0)
                        window.location.href = "carrito-conf-traslados.aspx?IdPedido=" + data.d + "&Mode=M";
                    else
                        window.location.href = "carrito-conf-traslados.aspx?IdPedido=" + data.d;
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                var err = eval("(" + xhr.responseText + ")");
                alert(err.Message);
            }
        });
    },
    crearPedidoInterno: function () {
        var idProveedor = parseInt($("#cmbProveedor").val());
        var idServicio = parseInt($("#cmbServicio").val());
        var tipoServicio = "";

        var idOrigen = parseInt($("#ddlInternoOrigen").val());
        var idDestino = parseInt($("#ddlInternoDestino").val());
        var hotel = $("#txtInternoOrigenHotel").val();
        var hotel2 = $("#txtInternoDestinoHotel").val();
        var idUsuario = parseInt($("#hdnIDUsuario").val());
        var cantAdultos = parseInt($("#cmbCantidadAdultos").val());
        var cantMenores = parseInt($("#cmbCantidadMenores").val());
        var cantMenores2 = parseInt($("#cmbCantidadMenores2").val());
        var fechaIda = $("#txtFecha").val();
        var tipo = "";

        if ($("#rdbRegular").is(":checked"))
            tipo = "R";
        if ($("#rdbRegularNR").is(":checked"))
            tipo = "RNR";
        if ($("#rdbPrivado").is(":checked"))
            tipo = "P";
        if ($("#rdbPrivadoNR").is(":checked"))
            tipo = "PNR";

        var pasajero = $("#txtPasajero").val();
        var dni = $("#txtDni").val();
        //var email = $("#txtEmail").val();
        var obs = $("#txtInternoObsOrigen").val();
        var obs2 = $("#txtInternoObsDestino").val();

        var info = "{ idProveedor: " + idProveedor
            + ", idServicio: " + idServicio
            + ", idOrigen: " + idOrigen
            + ", idDestino: " + idDestino
            + ", idUsuario: " + idUsuario
            + ", cantAdultos: " + cantAdultos
            + ", cantMenores: " + cantMenores
            + ", cantMenores2: " + cantMenores2
            + ", fechaIda: '" + fechaIda//auxIda
            + "', fechaVuelta: '"
            + "', tipo: '" + tipo
            + "', pasajero: '" + pasajero
            + "', dni: '" + dni
            //+ "', email: '" + email
            + "', aerolineaArribo: '"
            + "', numeroVueloArribo: '"
            + "', horaArribo: '"
            + "', aerolineaPartida: '"
            + "', numeroVueloPartida: '"
            + "', horaPartida: '"
            + "', hotel: '" + hotel
            + "', observaciones: '" + obs
            + "', hotel2: '" + hotel2
            + "', observaciones2: '" + obs2
            + "', idPedido: " + parseInt($("#hdnIDPedido").val())
            + " }";

        $.ajax({
            type: "POST",
            url: "/default-traslados.aspx/CrearPedido",
            data: info,
            async: true,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, text) {
                if (data.d > 0) {
                    if (parseInt($("#hdnIDPedido").val()) > 0)
                        window.location.href = "carrito-conf-traslados.aspx?IdPedido=" + data.d + "&Mode=M";
                    else
                        window.location.href = "carrito-conf-traslados.aspx?IdPedido=" + data.d;
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                var err = eval("(" + xhr.responseText + ")");
                alert(err.Message);
            }
        });
    },
    changeRdb: function (tipo)
    {
        $(".errorRequired").html("");
        $("#divFormIdayVuelta, #divFormIda, #divFormVuelta, #divFormInterno").hide();

        if (tipo == "H") {
            //$("#divFormIdayVuelta, #divFormIda, #divFormVuelta").hide();
            $("#divFormInterno").show();
        }
        else if (tipo == "I")
        {
            //$("#divFormIdayVuelta, #divFormInterno").hide();
            $("#divFormIda").show();
            //$("#divFormVuelta").hide();
        }
        else if (tipo == "V") {
            //$("#divFormIdayVuelta, #divFormInterno").hide();
            //$("#divFormIda").hide();
            $("#divFormVuelta").show();
        }
        else {
            $("#divFormIdayVuelta").show();
            //$("#divFormIda, #divFormInterno").hide();
            //$("#divFormVuelta").hide();
        }

        $("#hdnTipo").val(tipo);
    }
}

$(document).ready(function () {
    traslados.configControls();

    if ($("#hdnIDPedido").val())
    {
        //traslados.changeRdb($("#hdnTipoServicio").val());
        //$("#divForm").show();
        //$("#divPrecios").show();
        traslados.getPrecios();
    }
});