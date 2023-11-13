function configControls() {
    $("#txtNombre, #txtDesde, #txtHasta").keypress(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {
            filter();
            return false;
        }
    });

    $("#cmbProveedor").change(function () {
        filter();
        return false;
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
                        IDPrecioServicio: { type: "integer" },
                        IDProveedor: { type: "integer" },
                        Operador: { type: "string" },
                        Proveedor: { type: "string" },
                        Servicio: { type: "string" },
                        //ImporteNeto: { type: "integer" },
                        PrecioPrivado: { type: "integer" },
                        PrecioRegular: { type: "integer" },
                        FechaDesde: { type: "date" },
                        FechaHasta: { type: "date" }
                    }
                }
            },
            pageSize: 50,
            batch: true,
            transport: {
                read: {
                    url: "preciosServiciosOperadores.aspx/Get", //specify the URL which data should return the records. This is the Read method of the Products.asmx service.
                    contentType: "application/json; charset=utf-8", // tells the web service to serialize JSON
                    type: "POST" //use HTTP POST request as the default GET is not allowed for ASMX
                },
                parameterMap: function (data, operation) {
                    if (operation != "read") {
                        // web service method parameters need to be send as JSON. The Create, Update and Destroy methods have a "products" parameter.
                        return JSON.stringify({ products: data.models })
                    } else {
                        // web services need default values for every parameter
                        data = $.extend({ sort: null, filter: null, fechaDesde: $("#txtDesde").val(), fechaHasta: $("#txtHasta").val() }, data);

                        return JSON.stringify(data);
                    }
                }
            }
        },
        height: 500,
        scrollable: true,
        sortable: true,
        //filterable: true,
        pageable: {
            refresh: true,
            pageSizes: false,
            input: false,
            numeric: true
        },
        columns: [
            { field: "Operador", title: "Operador", width: "100px" },
            { field: "Proveedor", title: "Proveedor", width: "100px" },
            { field: "Servicio", title: "Servicio", width: "100px" },
            { field: "FechaDesde", title: "Fecha desde", format: "{0:dd/MM/yyyy}", width: "80px" },
                        { field: "FechaHasta", title: "Fecha hasta", format: "{0:dd/MM/yyyy}", width: "80px" },
            //{ field: "ImporteNeto", title: "Neto", width: "80px" },
            { field: "PrecioRegular", title: "Regular", width: "80px" },
            { field: "PrecioPrivado", title: "Privado", width: "80px" },
            { command: { text: "", template: "<div align='center'><img src='../../images/grid/gridEdit.gif' style='cursor:pointer' title='Editar' class='editColumn'/></div>" }, title: "Editar", width: "50px" },
            { command: { text: "", template: "<div align='center'><img src='../../images/grid/gridDelete.gif' style='cursor:pointer' title='Eliminar' class='deleteColumn'/></div>" }, title: "Eliminar", width: "50px" }
        ]
    });

    $("#grid").delegate(".editColumn", "click", function (e) {
        var grid = $("#grid").data("kendoGrid");
        var dataItem = grid.dataItem($(e.currentTarget).closest("tr"));
        window.location.href = "preciosServiciosOperadorese.aspx?Mode=E&Id=" + dataItem.IDPrecioServicio;
    });

    $("#grid").delegate(".deleteColumn", "click", function (e) {
        var grid = $("#grid").data("kendoGrid");
        var dataItem = grid.dataItem($(e.currentTarget).closest("tr"));
        if (confirm("¿Esta seguro que desea eliminar el item seleccionado?")) {
            $.ajax({
                type: "POST",
                url: "preciosServicios.aspx/Delete",
                data: "{ id: " + dataItem.IDPrecioServicio + "}",
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
}

function nuevo() {
    window.location.href = "preciosServiciosOperadorese.aspx?Mode=A";
}

function verTodos() {
    $("#txtNombre").val("");
    $("#cmbOperador").val("0");
    $("#cmbProveedor").val("0");
    $("#txtDesde, #txtHasta").val("");
    filter();
}

function filter() {

    var grid = $("#grid").data("kendoGrid");
    var $filter = new Array();

    var nombre = $("#txtNombre").val();
    if (nombre != "") {
        $filter.push({ field: "Servicio", operator: "contains", value: nombre });
    }

    var proveedor = $("#cmbProveedor").val();
    if (proveedor != "" && proveedor != "0") {
        $filter.push({ field: "IDProveedor", operator: "equal", value: parseInt(proveedor) });
    }

    var operador = $("#cmbOperador").val();
    if (operador != "" && operador != "0") {
        $filter.push({ field: "IDUsuario", operator: "equal", value: parseInt(operador) });
    }
    grid.dataSource.filter($filter);
}

$(document).ready(function () {
    configControls();
    $("#txtDesde").kendoDatePicker({ format: 'dd/MM/yyyy' });
    $("#txtHasta").kendoDatePicker({ format: 'dd/MM/yyyy' });
});