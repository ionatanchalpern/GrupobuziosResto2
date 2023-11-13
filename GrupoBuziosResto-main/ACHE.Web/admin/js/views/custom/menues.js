function configControls() {
    /*$("#txtNombre, #txtCodigo").keypress(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {
            filter();
            return false;
        }
    });*/

    //validarUsuario();

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
                        IDMenu: { type: "integer" },
                        IDRestaurant: { type: "integer" },
                        TipoMenu: { type: "string" },
                        Restaurant: { type: "string" },
                    }
                }
            },
            pageSize: 50,
            batch: true,
            transport: {
                read: {
                    url: "menues.aspx/Get", //specify the URL which data should return the records. This is the Read method of the Products.asmx service.
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
            { field: "IDMenu", title: "ID", width: "50px" },
            { field: "TipoMenu", title: "Tipo", width: "80px" },
            { field: "Restaurant", title: "Restaurant", width: "100px" },
            { command: { text: "", template: "<div align='center'><img src='../../images/grid/gridEdit.gif' style='cursor:pointer' title='Editar' class='editColumn'/></div>" }, title: "Editar", width: "50px" },
            { command: { text: "", template: "<div align='center'><img src='../../images/grid/gridDelete.gif' style='cursor:pointer' title='Eliminar' class='deleteColumn'/></div>" }, title: "Eliminar", width: "50px" }
        ]
    });

    $("#grid").delegate(".editColumn", "click", function (e) {
        var grid = $("#grid").data("kendoGrid");
        var dataItem = grid.dataItem($(e.currentTarget).closest("tr"));
        window.location.href = "menuese.aspx?Mode=E&Id=" + dataItem.IDMenu;
    });

    $("#grid").delegate(".deleteColumn", "click", function (e) {
        var grid = $("#grid").data("kendoGrid");
        var dataItem = grid.dataItem($(e.currentTarget).closest("tr"));
        if (confirm("¿Esta seguro que desea eliminar el item seleccionado?")) {
            $.ajax({
                type: "POST",
                url: "menues.aspx/Delete",
                data: "{ id: " + dataItem.IDMenu + "}",
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
    window.location.href = "menuese.aspx?Mode=A";
}

function verTodos() {
    $("#cmbTipos").val("");
    $("#cmbRestos").val("");
    filter();
}

function filter() {

    var grid = $("#grid").data("kendoGrid");
    var $filter = new Array();

    var tipo = $("#cmbTipos").val();
    if (tipo != "") {
        $filter.push({ field: "TipoMenu", operator: "contains", value: tipo });
    }

    var resto = parseInt($("#cmbRestos").val());
    if (resto != 0) {
        $filter.push({ field: "IDRestaurant", operator: "equal", value: resto });
    }

    grid.dataSource.filter($filter);
}

//function validarUsuario() {
//    $.ajax({
//        type: "POST",
//        url: "menues.aspx/ValidateUser",
//        data: "{ }",
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (data, text) {
//            filter();
//        },
//        error: function (response) {
//            var r = jQuery.parseJSON(response.responseText);
//            alert(r.Message);
//        }
//    });
//}

$(document).ready(function () {
    configControls()
});