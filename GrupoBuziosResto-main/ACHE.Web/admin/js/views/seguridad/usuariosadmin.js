function configControls() {
    $("#txtNombre, #txtEmail").keypress(function (event) {
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
                        ID: { type: "integer" },
                        Nombre: { type: "string" },
                        Email: { type: "string" },
                        Tipo: { type: "string" },
                        Activo: { type: "string" }
                    }
                }
            },
            pageSize: 50,
            batch: true,
            transport: {
                read: {
                    url: "usuariosadmin.aspx/Get", //specify the URL which data should return the records. This is the Read method of the Products.asmx service.
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
        //scrollable: true,
        sortable: true,
        //filterable: true,
        pageable: { input: false, numeric: true },
        columns: [
            { field: "ID", title: "ID", width: "50px" },
            { field: "Nombre", title: "Nombre", width: "200px" },
            { field: "Email", title: "Email", width: "200px" },
            { field: "Tipo", title: "Tipo", width: "100px" },
            { field: "Activo", title: "Activo", width: "100px" },
            { command: { text: "", template: "<div align='center'><img src='../../images/grid/gridEdit.gif' style='cursor:pointer' title='Editar' class='editColumn'/></div>" }, title: "Editar", width: "50px" },
            { command: { text: "", template: "<div align='center'><img src='../../images/grid/gridDelete.gif' style='cursor:pointer' title='Eliminar' class='deleteColumn'/></div>" }, title: "Eliminar", width: "50px" }
        ]
    });
	
	$("#grid").delegate(".editColumn", "click", function (e) {
        var grid = $("#grid").data("kendoGrid");
        var dataItem = grid.dataItem($(e.currentTarget).closest("tr"));
        window.location.href = "usuariosadmine.aspx?Mode=E&Id=" + dataItem.ID;
    });

    $("#grid").delegate(".deleteColumn", "click", function (e) {
        var grid = $("#grid").data("kendoGrid");
        var dataItem = grid.dataItem($(e.currentTarget).closest("tr"));
        if (confirm("¿Esta seguro que desea eliminar el item seleccionado?")) {
            $.ajax({
                type: "POST",
                url: "usuariosadmin.aspx/Delete",
                data: "{ id: " + dataItem.ID + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                //data: "id=" + dataItem.IDTutorial,
                success: function (data, text) {
                    filter();
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.status + ":" + thrownError);
                }
            });
        }
    });
}

function Nuevo() {
    window.location.href = "usuariosadmine.aspx?Mode=A";
}

function verTodos() {
    $("#txtNombre, #txtEmail").val("");
    filter();
}

function filter() {
    var grid = $("#grid").data("kendoGrid");
    var $filter = new Array();
    
    var nombre = escape($("#txtNombre").val());
    if (nombre != "") {
        $filter.push({ field: "Nombre", operator: "contains", value: nombre });
    }

    var email = escape($("#txtEmail").val());
    if (email != "") {
        $filter.push({ field: "Email", operator: "contains", value: email });
    }

    grid.dataSource.filter($filter);
}

$(document).ready(function () {
    configControls()
});