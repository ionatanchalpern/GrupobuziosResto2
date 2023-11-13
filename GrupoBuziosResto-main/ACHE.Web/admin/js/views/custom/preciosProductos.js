function configControls() {
    $("#cmbTipos, #txtDesde, #txtHasta").change(function () {
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
                        IDPrecioProducto: { type: "integer" },
                        Tipo: { type: "string" },
                        Precio: { type: "integer" },
                        PrecioOperador: { type: "integer" },
                        FechaDesde: { type: "date" },
                        FechaHasta: { type: "date" },
                   //     IDDestino: { type: "integer" },
                       // Destino: { type: "string" },

                    }
                }
            },
            pageSize: 50,
            batch: true,
            transport: {
                read: {
                    url: "preciosProductos.aspx/Get", //specify the URL which data should return the records. This is the Read method of the Products.asmx service.
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
            { field: "IDPrecioProducto", title: "ID", width: "50px" },
            { field: "Tipo", title: "Tipo", width: "100px" },
           //        { field: "Destino", title: "Destino", width: "100px" },

            { field: "FechaDesde", title: "Fecha desde", format: "{0:dd/MM/yyyy}", width: "80px" },
                        { field: "FechaHasta", title: "Fecha hasta", format: "{0:dd/MM/yyyy}", width: "80px" },
     { field: "Precio", title: "Neto Restó", width: "80px", format: "{0:c}" },
            { field: "PrecioOperador", title: "Neto Op.", width: "80px", format: "{0:c}" },
            //{ field: "Activo", title: "Activo", width: "50px" },
            { command: { text: "", template: "<div align='center'><img src='../../images/grid/gridEdit.gif' style='cursor:pointer' title='Editar' class='editColumn'/></div>" }, title: "Editar", width: "50px" },
            //{ command: { text: "", template: "<div align='center'><img src='../../images/grid/gridDelete.gif' style='cursor:pointer' title='Eliminar' class='deleteColumn'/></div>" }, title: "Eliminar", width: "50px" }
        ]
    });

    $("#grid").delegate(".editColumn", "click", function (e) {
        var grid = $("#grid").data("kendoGrid");
        var dataItem = grid.dataItem($(e.currentTarget).closest("tr"));
        window.location.href = "preciosProductose.aspx?Mode=E&Id=" + dataItem.IDPrecioProducto;
    });

    $("#grid").delegate(".deleteColumn", "click", function (e) {
        var grid = $("#grid").data("kendoGrid");
        var dataItem = grid.dataItem($(e.currentTarget).closest("tr"));
        if (confirm("¿Esta seguro que desea eliminar el item seleccionado?")) {
            $.ajax({
                type: "POST",
                url: "preciosProductos.aspx/Delete",
                data: "{ id: " + dataItem.IDPrecioProducto + "}",
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

function Nuevo() {
    window.location.href = "preciosProductose.aspx?Mode=A";
}

function verTodos() {
    $("#cmbTipos").val("");
    $("#txtDesde, #txtHasta").val("");
    filter();
}

function filter() {
    var grid = $("#grid").data("kendoGrid");
    var $filter = new Array();

    var tipo = $("#cmbTipos").val();
    if (tipo != "") {
        $filter.push({ field: "Tipo", operator: "contains", value: tipo });
    }
    /*
    var destino = $("#cmbDestino").val();
    if (destino != "") {
        $filter.push({ field: "IDDestino", operator: "equal", value: parseInt(destino) });
    }
    */
    grid.dataSource.filter($filter);
}

$(document).ready(function () {
    configControls();
    $("#txtDesde").kendoDatePicker({ format: 'dd/MM/yyyy' });
    $("#txtHasta").kendoDatePicker({ format: 'dd/MM/yyyy' });
});