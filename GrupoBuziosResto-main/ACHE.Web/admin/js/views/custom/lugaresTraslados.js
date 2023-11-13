function configControls() {
    $("#txtNombre").keypress(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {
            filter();
            return false;
        }
    });

    $("#cmbProveedor, #cmbTipo").change(function () {
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
                        IDLugarTraslado: { type: "integer" },
                        IDProveedor: { type: "integer" },
                        Proveedor: { type: "string" },
                        Nombre: { type: "string" },
                        Tipo: { type: "string" },
                        Activo: { type: "string" },
                    }
                }
            },
            pageSize: 50,
            batch: true,
            transport: {
                read: {
                    url: "lugaresTraslados.aspx/Get", //specify the URL which data should return the records. This is the Read method of the Products.asmx service.
                    contentType: "application/json; charset=utf-8", // tells the web service to serialize JSON
                    type: "POST" //use HTTP POST request as the default GET is not allowed for ASMX
                },
                parameterMap: function (data, operation) {
                    if (operation != "read") {
                        // web service method parameters need to be send as JSON. The Create, Update and Destroy methods have a "products" parameter.
                        return JSON.stringify({ products: data.models })
                    } else {
                        // web services need default values for every parameter
                        data = $.extend({ sort: null, filter: null }, data);

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
            { field: "Proveedor", title: "Proveedor", width: "100px" },
            { field: "IDLugarTraslado", title: "ID", width: "50px", hidden: true },
            { field: "Nombre", title: "Nombre", width: "100px" },
            { field: "Tipo", title: "Tipo", width: "100px" },
            { field: "Activo", title: "Activo", width: "50px" },
            { command: { text: "", template: "<div align='center'><img src='../../images/grid/gridEdit.gif' style='cursor:pointer' title='Editar' class='editColumn'/></div>" }, title: "Editar", width: "50px" },
            { command: { text: "", template: "<div align='center'><img src='../../images/grid/gridDelete.gif' style='cursor:pointer' title='Eliminar' class='deleteColumn'/></div>" }, title: "Eliminar", width: "50px" }
        ]
    });

    $("#grid").delegate(".editColumn", "click", function (e) {
        var grid = $("#grid").data("kendoGrid");
        var dataItem = grid.dataItem($(e.currentTarget).closest("tr"));
        window.location.href = "lugaresTrasladose.aspx?Mode=E&Id=" + dataItem.IDLugarTraslado;
    });

    $("#grid").delegate(".deleteColumn", "click", function (e) {
        var grid = $("#grid").data("kendoGrid");
        var dataItem = grid.dataItem($(e.currentTarget).closest("tr"));
        if (confirm("¿Esta seguro que desea eliminar el item seleccionado?")) {
            $.ajax({
                type: "POST",
                url: "lugaresTraslados.aspx/Delete",
                data: "{ id: " + dataItem.IDLugarTraslado + "}",
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
    window.location.href = "lugaresTrasladose.aspx?Mode=A";
}

function verTodos() {
    $("#txtNombre, #cmbTipo").val("");
    $("#cmbProveedor").val("0");
    filter();
}

function filter() {

    var grid = $("#grid").data("kendoGrid");
    var $filter = new Array();

    var nombre = $("#txtNombre").val();
    if (nombre != "") {
        $filter.push({ field: "Nombre", operator: "contains", value: nombre });
    }

    var proveedor = $("#cmbProveedor").val();
    if (proveedor != "" && proveedor != "0") {
        $filter.push({ field: "IDProveedor", operator: "equal", value: parseInt(proveedor) });
    }

    var tipo = $("#cmbTipo").val();
    if (tipo != "") {
        $filter.push({ field: "Tipo", operator: "contains", value: tipo });
    }

    grid.dataSource.filter($filter);
}

$(document).ready(function () {
    configControls();
});