function configControls() {
    $("#txtNombre").keypress(function (event) {
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
                        IDRestaurant: { type: "integer" },
                        Nombre: { type: "string" },
                        Direccion: { type: "string" },
                        HorarioAtencion: { type: "integer" },
                        FechaAlta: { type: "date" },
                        Activo: { type: "string" },
                    }
                }
            },
            pageSize: 50,
            batch: true,
            transport: {
                read: {
                    url: "restaurantes.aspx/Get", //specify the URL which data should return the records. This is the Read method of the Products.asmx service.
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
            { field: "IDRestaurant", title: "ID", width: "50px", hidden: "hidden" },
            { field: "Nombre", title: "Nombre", width: "100px" },
            { field: "Direccion", title: "Direccion", width: "100px" },
            { field: "HorarioAtencion", title: "Horario Atencion", width: "80px", format: "{0:c}" },
            { field: "FechaAlta", title: "Fecha de alta", format: "{0:dd/MM/yyyy}", width: "80px" },
            { field: "Activo", title: "Activo", width: "50px" },
            { command: { text: "", template: "<div align='center'><img src='../../images/grid/gridEdit.gif' style='cursor:pointer' title='Editar' class='editColumn'/></div>" }, title: "Editar", width: "50px" },
            { command: { text: "", template: "<div align='center'><img src='../../images/grid/gridDelete.gif' style='cursor:pointer' title='Eliminar' class='deleteColumn'/></div>" }, title: "Eliminar", width: "50px" }
        ]
    });

    $("#grid").delegate(".editColumn", "click", function (e) {
        var grid = $("#grid").data("kendoGrid");
        var dataItem = grid.dataItem($(e.currentTarget).closest("tr"));
        window.location.href = "restaurantese.aspx?Mode=E&Id=" + dataItem.IDRestaurant;
    });

    $("#grid").delegate(".deleteColumn", "click", function (e) {
        var grid = $("#grid").data("kendoGrid");
        var dataItem = grid.dataItem($(e.currentTarget).closest("tr"));
        if (confirm("¿Esta seguro que desea eliminar el item seleccionado?")) {
            $.ajax({
                type: "POST",
                url: "restaurantes.aspx/Delete",
                data: "{ id: " + dataItem.IDRestaurant + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                //data: "id=" + dataItem.IDTutorial,
                success: function (data, text) {
                    filter();
                },
                error: function (response) {
                    var r = jQuery.parseJSON(response.responseText);
                    alert(r.Message);

                    //$("#divErrores").html(r.Message);
                    //$("#divErrores").show();
                    //$('html, body').animate({ scrollTop: 0 }, 'slow');
                }
                //error: function (xhr, ajaxOptions, thrownError) {
                //    alert(xhr.status + ":" + thrownError);
                //}
            });
        }
    });
}

function Nuevo() {
    window.location.href = "restaurantese.aspx?Mode=A";
}

function verTodos() {
    filter();
}

function filter() {
    var grid = $("#grid").data("kendoGrid");
    var $filter = new Array();

    var nombre = $("#txtNombre").val();
    if (nombre != "") {
        $filter.push({ field: "Nombre", operator: "contains", value: nombre });
    }

    grid.dataSource.filter($filter);
}

$(document).ready(function () {
    configControls()
});