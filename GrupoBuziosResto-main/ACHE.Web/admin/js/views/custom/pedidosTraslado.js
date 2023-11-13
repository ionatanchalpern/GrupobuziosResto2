var oTable = null;

function exportar() {
    $("#divErrores").hide();
    $("#lnkDownload").hide();
    $("#imgLoading").show();
    $("#btnExportar").attr("disabled", true);

    $.ajax({
        type: "POST",
        url: "pedidosTraslado.aspx/Exportar",
        data: "{ nombreContacto: '" + $("#txtContacto").val().toUpperCase()
                + "', operador: '" + $("#txtOperador").val().toUpperCase()
                + "', fechaIdaDesde: '" + $("#txtIdaDesde").val()
                + "', fechaIdaHasta: '" + $("#txtIdaHasta").val()
                + "', fechaVueltaDesde: '" + $("#txtVueltaDesde").val()
                + "', fechaVueltaHasta: '" + $("#txtVueltaHasta").val()
                + "', altaDesde: '" + $("#txtAltaDesde").val()
                + "', altaHasta: '" + $("#txtAltaHasta").val()
                + "', pasajero: '" + $("#txtPasajero").val()
                + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, text) {
            if (data.d != "") {
                $("#divErrores").hide();
                $("#imgLoading").hide();
                $("#lnkDownload").show();
                $("#lnkDownload").attr("href", data.d);
                $("#lnkDownload").attr("download", data.d);
                $("#btnExportar").attr("disabled", false);
            }
        },
        error: function (response) {
            var r = jQuery.parseJSON(response.responseText);
            $("#divErrores").html(r.Message);
            $("#divError").show();
            $('html, body').animate({ scrollTop: 0 }, 'slow');
            $("#imgLoading").hide();
            $("#lnkDownload").hide();
            $("#btnExportar").attr("disabled", false);
        }
    });
}

function configControls() {
    $("#txtOperador, #txtContacto, #txtAltaDesde, #txtAltaHasta, #txtIdaDesde, #txtIdaHasta, #txtVueltaDesde, #txtVueltaHasta,#txtPasajero").keypress(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {
            filter();
            return false;
        }
    });

    $("#grid").kendoGrid({
        dataSource: {
            serverSorting: true,
            serverPaging: true,
            serverFiltering: true,
            schema: {
                data: "d.Data",
                total: "d.Total",
                model: {
                    fields: {
                        IDPedido: { type: "integer" },
                        Estado: { type: "string" },
                        FechaAlta: { type: "date" },
                        FechaIda: { type: "date" },
                        FechaVuelta: { type: "date" },
                        NombreContacto: { type: "string" },
                        Tipo: { type: "string" },
                        Servicio: { type: "string" },
                        Proovedor:{type:"string"},
                        Operador: { type: "string" },
                        Pasajero: { type: "string" },
                        Total: { type: "decimal" },
                        CantPasajeros: { type: "integer" },
                        NroFile: { type: "string" },
                        PagoPor: { type: "string" }
                    }
                }
            },
            pageSize: 50,
            batch: true,
            transport: {
                read: {
                    url: "pedidosTraslado.aspx/Get", //specify the URL which data should return the records. This is the Read method of the Products.asmx service.
                    contentType: "application/json; charset=utf-8", // tells the web service to serialize JSON
                    type: "POST" //use HTTP POST request as the default GET is not allowed for ASMX
                },
                parameterMap: function (data, operation) {
                    if (operation != "read") {
                        // web service method parameters need to be send as JSON. The Create, Update and Destroy methods have a "products" parameter.
                        return JSON.stringify({ products: data.models })
                    } else {
                        // web services need default values for every parameter
                        data = $.extend({
                            sort: null, filter: null
                            , fechaIdaDesde: $("#txtIdaDesde").val(), fechaIdaHasta: $("#txtIdaHasta").val()
                            , fechaVueltaDesde: $("#txtVueltaDesde").val(), fechaVueltaHasta: $("#txtVueltaHasta").val()
                            , altaDesde: $("#txtAltaDesde").val(), altaHasta: $("#txtAltaHasta").val()
                        }, data);

                        return JSON.stringify(data);
                    }
                }
            }
        },
        height: 500,
        //scrollable: true,
        sortable: true,
        //filterable: true,
        pageable: { input: false, numeric: true },
        columns: [
            { field: "IDPedido", title: "ID", width: "50px" },
            { field: "FechaAlta", title: "Fecha", format: "{0:dd/MM/yyyy}", width: "80px" },
            { field: "Estado", title: "Estado", width: "80px" },
            { field: "Tipo", title: "Tipo", width: "70px" },
            { field: "Servicio", title: "Servicio", width: "100px" },
            { field: "Proovedor", title: "Proovedor", width: "100px" },
            { field: "Operador", title: "Operador", width: "100px" },
            { field: "NroFile", title: "NroFile", width: "100px" },
            { field: "IDPedido", title: "ID", width: "50px", hidden: true },
            { field: "NombreContacto", title: "Contacto", width: "100px" },
          //  { field: "Origen", title: "Origen", width: "70px" },
         //   { field: "Destino", title: "Destino", width: "70px" },
            { field: "FechaIda", title: "Ida", format: "{0:dd/MM/yyyy}", width: "80px" },
            { field: "FechaVuelta", title: "Vuelta", format: "{0:dd/MM/yyyy}", width: "80px" },
            { field: "Pasajero", title: "Pasajero", width: "100px" },
         //   { field: "CantMenores", title: "Menores", width: "70px" },
            { field: "CantPasajeros", title: "Adultos", width: "70px" },
            { field: "Total", title: "Total", width: "70px" },
            { field: "PagoPor", title: "Pago Por", width: "70px" },
            //{ field: "Total", title: "Total", width: "70px" },            
            { command: { text: "", template: "<div align='center'><img src='../../images/grid/gridDetails.gif' style='cursor:pointer' title='Ver pasajeros' class='editColumn'/></div>" }, title: "Ver pasajeros", width: "80px" },
            { command: { text: "", template: "<div align='center'><img src='../../images/grid/gridDelete.gif' style='cursor:pointer' title='Rechazar' class='deleteColumn'/></div>" }, title: "Eliminar", width: "50px" }
        ]
    });

    $("#grid").delegate(".deleteColumn", "click", function (e) {
        var grid = $("#grid").data("kendoGrid");
        var dataItem = grid.dataItem($(e.currentTarget).closest("tr"));
        if (confirm("¿Esta seguro que desea eliminar el item seleccionado?")) {
            $.ajax({
                type: "POST",
                url: "pedidosTraslado.aspx/Delete",
                data: "{ id: " + dataItem.IDPedido + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                //data: "id=" + dataItem.IDTutorial,
                success: function (data, text) {
                    filter();
                },
                error: function (response) {
                    var r = jQuery.parseJSON(response.responseText);
                    alert(r.Message);
                }
            });
        }
    });

    $("#grid").delegate(".editColumn", "click", function (e) {
        var grid = $("#grid").data("kendoGrid");
        var dataItem = grid.dataItem($(e.currentTarget).closest("tr"));
        $("#titPasajeros").html("Pasajeros del pedido " + dataItem.IDPedido);// + ", " + dataItem.Nombre);
        $.ajax({
            type: "POST",
            url: "pedidosTraslado.aspx/getPasajeros",
            data: "{ id: " + dataItem.IDPedido + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, text) {
                $("#bodyPasajeros").html("");

                if (data != null && data.d.length > 0) {

                    if (oTable != null) {

                        oTable.fnClearTable();
                        oTable.fnDestroy();
                    }

                    oTable = $('#tablePasajeros').dataTable({
                        "sDom": "<'row'<'col-sm-6'l><'col-sm-6'f>r>t<'row'<'col-sm-5'i><'col-sm-7'p>>",
                        "paging": true,
                        "bLengthChange": false,
						"bFilter": false,
                        "iDisplayLength": 10,
                        "ordering": false,
                        "bSort": false,
                        "info": false,
                        //"bDestroy": true,
                        "searching": false,
                        "sPaginationType": "bootstrap",
                        "oLanguage": {
                            "sProcessing": "Procesando...",
                            "sLengthMenu": "Mostrar _MENU_ registros",
                            "sZeroRecords": "No se encontraron resultados",
                            "sEmptyTable": "Ningún dato disponible en esta tabla",
                            "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
                            "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
                            "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
                            "sInfoPostFix": "",
                            "sSearch": "Buscar:",
                            "sUrl": "",
                            "sInfoThousands": ",",
                            "sLoadingRecords": "Cargando...",
                            "oPaginate": {
                                "sFirst": "Primero",
                                "sLast": "Último",
                                "sNext": "Siguiente",
                                "sPrevious": "Anterior"
                            },
                            "oAria": {
                                "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                                "sSortDescending": ": Activar para ordenar la columna de manera descendente"
                            }
                        },
                        "fnDrawCallback": function () {
                            var pageCount = Math.ceil((this.fnSettings().fnRecordsDisplay()) / this.fnSettings()._iDisplayLength);
                            if (pageCount == 1) {
                                $('.dataTables_paginate').first().hide();
                            } else {
                                $('.dataTables_paginate').first().show();
                            }
                        }
                    });

                    for (var i = 0; i < data.d.length; i++) {
                        oTable.fnAddData([
                            data.d[i].Nombre,
                            data.d[i].DNI,
                          ]);
                    }


                    $("#tablePasajeros_info").parent().remove();
                    $("#tablePasajeros").css("width", "100%");
                    $(".dataTables_paginate").first().parent().removeClass("col-sm-7");
                    $(".dataTables_paginate").first().parent().addClass("col-sm-12");
                }
                else
                    $("#bodyPasajeros").html("<tr><td colspan='2'>No hay pasajeros</td></tr>");

                $('#modalPasajeros').modal('show');
            },
            error: function (response) {
                var r = jQuery.parseJSON(response.responseText);
                alert(r.Message);
            }
        });
    });





}

function cerrarModal() {
    $('#modalPedido').modal('hide');
}

function verTodos() {
    $("#txtOperador, #txtContacto, #txtAltaDesde, #txtAltaHasta, #txtIdaDesde, #txtIdaHasta, #txtVueltaDesde, #txtVueltaHasta,#txtPasajero").val("");
    filter();
}

function filter() {
    $("#divError").hide();
    $("#imgLoading").hide();
    $("#lnkDownload").hide();
    $("#btnExportar").attr("disabled", false);

    var grid = $("#grid").data("kendoGrid");
    var $filter = new Array();

    var operador = $("#txtOperador").val();
    if (operador != "")
        $filter.push({ field: "Operador", operator: "contains", value: operador });

    var contacto = $("#txtContacto").val();
    if (contacto != "")
        $filter.push({ field: "NombreContacto", operator: "contains", value: contacto });
    
    var pasajero = $("#txtPasajero").val();
    if (pasajero != "")
        $filter.push({ field: "Pasajero", operator: "contains", value: pasajero });
    

    grid.dataSource.filter($filter);
}

$(document).ready(function () {
    configControls();
    $("#txtIdaDesde, #txtVueltaDesde, #txtIdaHasta, #txtVueltaHasta, #txtAltaDesde, #txtAltaHasta").kendoDatePicker({ format: 'dd/MM/yyyy' });
});