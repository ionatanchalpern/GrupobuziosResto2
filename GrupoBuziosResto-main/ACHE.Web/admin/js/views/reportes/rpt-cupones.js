function exportar() {
    $("#divErrores").hide();
    $("#lnkDownload").hide();
    $("#imgLoading").show();
    $("#btnExportar").attr("disabled", true);

    $.ajax({
        type: "POST",
        url: "rpt-cupones.aspx/Exportar",
        data: "{ restaurant: '" + $("#txtRestaurant").val().toUpperCase()
                + "', operador: '" + $("#txtOperador").val().toUpperCase()
                + "', fechaDesde: '" + $("#txtEstadiaDesde").val()
                + "', fechaHasta: '" + $("#txtEstadiaHasta").val()
                + "', codigo: '" + $("#txtCodigo").val()
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
    $("#txtRestaurant, #txtEstadiaDesde, #txtEstadiaHasta, #txtCodigo, #txtOperador").keypress(function (event) {
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
                        Codigo: { type: "string" },
                        FechaValidacion: { type: "date" },
                        EstadiaDesde: { type: "date" },
                        EstadiaHasta: { type: "date" },
                        Pasajero: { type: "string" },
                        Restaurant: { type: "string" },
                        Operador: { type: "string" },
                        Precio: { type: "decimal" },
                        Validado: { type: "string" },
                        Tipo: { type: "string" },
                        PagoPor: { type: "string" }
                    }
                }
            },
            pageSize: 50,
            batch: true,
            transport: {
                read: {
                    url: "rpt-cupones.aspx/Get", //specify the URL which data should return the records. This is the Read method of the Products.asmx service.
                    contentType: "application/json; charset=utf-8", // tells the web service to serialize JSON
                    type: "POST" //use HTTP POST request as the default GET is not allowed for ASMX
                },
                parameterMap: function (data, operation) {
                    if (operation != "read") {
                        // web service method parameters need to be send as JSON. The Create, Update and Destroy methods have a "products" parameter.
                        return JSON.stringify({ products: data.models })
                    } else {
                        // web services need default values for every parameter
                        data = $.extend({ sort: null, filter: null, fechaDesde: $("#txtEstadiaDesde").val(), fechaHasta: $("#txtEstadiaHasta").val() }, data);

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
            { field: "Codigo", title: "Codigo", width: "100px" },
            { field: "FechaValidacion", title: "Fecha Val", format: "{0:dd/MM/yyyy}", width: "80px" },
            { field: "Tipo", title: "Tipo", width: "100px" },
            { field: "Operador", title: "Operador", width: "100px" },
            { field: "PagoPor", title: "Pago Por", width: "100px" },
            { field: "Restaurant", title: "Restaurant", width: "100px" },
            { field: "Precio", title: "Neto Resto", width: "70px" },
            //{ field: "Validado", title: "Validado", width: "70px" }
        ]
    });
}

function verTodos() {
    $("#txtCodigo, #txtOperador, #txtRestaurant, #txtEstadiaDesde, #txtEstadiaHasta").val("");
    filter();
}

function filter() {
    $("#divError").hide();
    $("#imgLoading").hide();
    $("#lnkDownload").hide();
    $("#btnExportar").attr("disabled", false);

    var grid = $("#grid").data("kendoGrid");
    var $filter = new Array();

    var codigo = escape($("#txtCodigo").val());
    if (codigo != "") {
        $filter.push({ field: "Codigo", operator: "contains", value: codigo });
    }

    var operador = $("#txtOperador").val();
    if (operador != "") {
        $filter.push({ field: "Operador", operator: "contains", value: operador.toUpperCase() });
    }

    var resto = $("#txtRestaurant").val();
    if (resto != "") {
        $filter.push({ field: "Restaurant", operator: "contains", value: resto.toUpperCase() });
    }

    grid.dataSource.filter($filter);
}

$(document).ready(function () {
    configControls()
    $("#txtEstadiaDesde").kendoDatePicker({ format: 'dd/MM/yyyy' });
    $("#txtEstadiaHasta").kendoDatePicker({ format: 'dd/MM/yyyy' });
});