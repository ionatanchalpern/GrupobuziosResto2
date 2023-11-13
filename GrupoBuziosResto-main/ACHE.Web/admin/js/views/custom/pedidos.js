function exportar() {
    $("#divErrores").hide();
    $("#lnkDownload").hide();
    $("#imgLoading").show();
    $("#btnExportar").attr("disabled", true);

    $.ajax({
        type: "POST",
        url: "pedidos.aspx/Exportar",
        data: "{ nombreContacto: '" + $("#txtContacto").val().toUpperCase()
                + "', operador: '" + $("#txtOperador").val().toUpperCase()
                + "', fechaInDesde: '" + $("#txtEstadiaInDesde").val()
                + "', fechaInHasta: '" + $("#txtEstadiaInHasta").val()
                + "', fechaOutDesde: '" + $("#txtEstadiaOutDesde").val()
                + "', fechaOutHasta: '" + $("#txtEstadiaOutHasta").val()
                + "', altaDesde: '" + $("#txtAltaDesde").val()
                + "', altaHasta: '" + $("#txtAltaHasta").val()
                + "', pasajero: '" + $("#txtPasajero").val().toUpperCase()
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
    $("#txtOperador, #txtContacto, #txtAltaDesde, #txtAltaHasta, #txtEstadiaInDesde, #txtEstadiaInHasta, #txtEstadiaOutDesde, #txtEstadiaOutHasta, #txtPasajero").keypress(function (event) {
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
                        IDUsuario: { type: "integer" },
                        FechaAlta: { type: "date" },
                        EstadiaDesde: { type: "date" },
                        EstadiaHasta: { type: "date" },
                        Pasajero: { type: "string" },
                        NombreContacto: { type: "string" },
                        PagoPor: { type: "string" },
                        Empresa: { type: "string" },
                        Total: { type: "decimal" },
                        Cantidad: { type: "integer" },
                        Validados: { type: "integer" },
                    }
                }
            },
            pageSize: 50,
            batch: true,
            transport: {
                read: {
                    url: "pedidos.aspx/Get", //specify the URL which data should return the records. This is the Read method of the Products.asmx service.
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
                            , fechaInDesde: $("#txtEstadiaInDesde").val(), fechaInHasta: $("#txtEstadiaInHasta").val()
                            , fechaOutDesde: $("#txtEstadiaOutDesde").val(), fechaOutHasta: $("#txtEstadiaOutHasta").val()
                            , altaDesde: $("#txtAltaDesde").val(), altaHasta: $("#txtAltaHasta").val() }, data);

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
            { field: "Empresa", title: "Operador", width: "100px" },
            { field: "NombreContacto", title: "Contacto", width: "100px" },
            { field: "FechaAlta", title: "Fecha Compra", format: "{0:dd/MM/yyyy}", width: "80px" },
            { field: "EstadiaDesde", title: "Desde", format: "{0:dd/MM/yyyy}", width: "80px" },
            { field: "EstadiaHasta", title: "Hasta", format: "{0:dd/MM/yyyy}", width: "80px" },
            { field: "Pasajero", title: "Pasajero", width: "100px" },
            { field: "Cantidad", title: "Cantidad", width: "70px" },
            { field: "Total", title: "Total Neto Op", width: "70px" },
            { field: "Validados", title: "Validados", width: "70px" },
            { field: "PagoPor", title: "Pago Por", width: "70px" },
            //{ field: "Total", title: "Total", width: "70px" },            
            { command: { text: "", template: "<div align='center'><img src='../../images/grid/gridDetails.gif' style='cursor:pointer' title='Decidir' class='editColumn'/></div>" }, title: "Ver", width: "50px" },
            { command: { text: "", template: "<div align='center'><img src='../../images/grid/gridDelete.gif' style='cursor:pointer' title='Rechazar' class='deleteColumn'/></div>" }, title: "Elim", width: "50px" }
        ]
    });

    $("#grid").delegate(".deleteColumn", "click", function (e) {
        var grid = $("#grid").data("kendoGrid");
        var dataItem = grid.dataItem($(e.currentTarget).closest("tr"));
        if (confirm("¿Esta seguro que desea eliminar el item seleccionado?")) {
            $.ajax({
                type: "POST",
                url: "pedidos.aspx/Delete",
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

        $.ajax({
            type: "POST",
            url: "pedidos.aspx/GetInfo",
            data: "{ idPedido: " + dataItem.IDPedido + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            //data: "id=" + dataItem.IDTutorial,
            success: function (data, text) {
                if (data.d != null) {
                    $("#divTabla").html(data.d);
                }

                $('#modalPedido').modal('show');
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status + ":" + thrownError);
            }
        });

    });
}

function contestarSolicitud(estado)
{
    if (estado == "A") {
        $.ajax({
            type: "POST",
            url: "usuariospendientes.aspx/Aprobar",
            data: "{ id: " + parseInt($("#hdnIDSolicitud").val()) + ", obs: '" + $("#txtObservaciones").val() + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            //data: "id=" + dataItem.IDTutorial,
            success: function (data, text) {
                $('#modalUsuario').modal('hide');
                filter();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status + ":" + thrownError);
            }
        });
    }
    else {
        $.ajax({
            type: "POST",
            url: "usuariospendientes.aspx/Rechazar",
            data: "{ id: " + parseInt($("#hdnIDSolicitud").val()) + ", motivo: '" + $("#txtMotivoRechazo").val() + "', obs: '" + $("#txtObservaciones").val() + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            //data: "id=" + dataItem.IDTutorial,
            success: function (data, text) {
                $('#modalUsuario').modal('hide');
                filter();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status + ":" + thrownError);
            }
        });
    }
}

function cerrarModal() {
    $('#modalPedido').modal('hide');
}

function verTodos() {
    $("#txtContacto, #txtOperador, #txtPasajero, #txtEstadiaInHasta, #txtEstadiaInDesde, #txtEstadiaOutHasta, #txtEstadiaOutDesde").val("");
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
    if (operador != "") {
        $filter.push({ field: "Empresa", operator: "contains", value: operador });
    }

    var contacto = $("#txtContacto").val();
    if (contacto != "") {
        $filter.push({ field: "NombreContacto", operator: "contains", value: contacto });
    }

    var pasajero = $("#txtPasajero").val();
    if (pasajero != "") {
        $filter.push({ field: "Pasajero", operator: "contains", value: pasajero });
    }

    grid.dataSource.filter($filter);
}

$(document).ready(function () {
    configControls()
    $("#txtEstadiaInDesde,#txtEstadiaOutDesde").kendoDatePicker({ format: 'dd/MM/yyyy' });
    $("#txtEstadiaInHasta,#txtEstadiaOutHasta").kendoDatePicker({ format: 'dd/MM/yyyy' });

    $("#txtAltaDesde").kendoDatePicker({ format: 'dd/MM/yyyy' });
    $("#txtAltaHasta").kendoDatePicker({ format: 'dd/MM/yyyy' });
});