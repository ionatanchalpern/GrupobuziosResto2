    function exportar() {
    $("#divErrores").hide();
    $("#lnkDownload").hide();
    $("#imgLoading").show();
    $("#btnExportar").attr("disabled", true);

    $.ajax({
        type: "POST",
        url: "usuarios.aspx/Exportar",
        data: "{ empresa: '" + $("#txtEmpresa").val()
                + "', nombreContacto: '" + $("#txtContacto").val()
                + "', email: '" + $("#txtEmail").val()
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
    $("#txtEmpresa, #txtContacto, #txtEmail").keypress(function (event) {
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
                        IDOperador: { type: "integer" },
                        Empresa: { type: "string" },
                        NombreContacto: { type: "string" },
                        Email: { type: "string" },
                        FechaAlta: { type: "date" },
                        Activo: { type: "string" },
                    }
                }
            },
            pageSize: 50,
            batch: true,
            transport: {
                read: {
                    url: "usuarios.aspx/Get", //specify the URL which data should return the records. This is the Read method of the Products.asmx service.
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
            { field: "IDOperador", title: "ID", width: "50px" },
            { field: "Empresa", title: "Empresa", width: "120px" },
            { field: "NombreContacto", title: "Nombre de contacto", width: "180px" },
            { field: "Email", title: "Email", width: "170px" },
            { field: "FechaAlta", title: "Fecha de alta", format: "{0:dd/MM/yyyy}", width: "80px" },
            { field: "Activo", title: "Activo", width: "80px" },
            { command: { text: "", template: "<div align='center'><img src='../../images/grid/gridDetails.gif' style='cursor:pointer' title='Info' class='editColumn'/></div>" }, title: "Info", width: "50px" },
            { command: { text: "", template: "<div align='center'><img src='../../images/grid/gridDelete.gif' style='cursor:pointer' title='Eliminar' class='deleteColumn'/></div>" }, title: "Eliminar", width: "50px" }
        ]
    });

    $("#grid").delegate(".editColumn", "click", function (e) {
        var grid = $("#grid").data("kendoGrid");
        var dataItem = grid.dataItem($(e.currentTarget).closest("tr"));

        $.ajax({
            type: "POST",
            url: "usuarios.aspx/GetInfo",
            data: "{ id: " + dataItem.IDOperador + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, text) {
                if (data.d != null) {
                    $("#lblEmpresa").html(data.d.Empresa);
                    $("#lblContacto").html(data.d.NombreContacto);
                    $("#lblEmail").html(data.d.Email);
                    $("#lblPwd").html(data.d.Pwd);
                    $("#lblTel").html(data.d.Telefono);
                    $("#lblDir").html(data.d.Direccion);
                    $("#lblActivo").html(data.d.Activo);
                    $("#lblFechaAlta").html(data.d.FechaAltaString);
                    $("#lblObservaciones").html(data.d.Observaciones);
                    $("#lblObservaciones").html(data.d.Observaciones);
                    if (data.d.ServiciosEspeciales)
                        $("#chkServiciosEsp").attr("checked", true);
                    else
                        $("#chkServiciosEsp").attr("checked", false);

                    $("#hdnIDUsuario").val(dataItem.IDOperador);
                }

                $('#modalUsuario').modal('show');
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status + ":" + thrownError);
            }
        });

    });

    $("#grid").delegate(".deleteColumn", "click", function (e) {
        var grid = $("#grid").data("kendoGrid");
        var dataItem = grid.dataItem($(e.currentTarget).closest("tr"));
        if (confirm("¿Esta seguro que desea eliminar el item seleccionado?")) {
            $.ajax({
                type: "POST",
                url: "usuarios.aspx/Delete",
                data: "{ id: " + dataItem.IDOperador + "}",
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

function guardarUsuario() {
    $.ajax({
        type: "POST",
        url: "usuarios.aspx/Save",
        data: "{id:  "+$('#hdnIDUsuario').val()+", chkServiciosEsp: " + $('#chkServiciosEsp').is(':checked') + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, text) {
            $('#modalUsuario').modal('hide');
        },
        error: function (xhr, ajaxOptions, thrownError) {
            var err = eval("(" + xhr.responseText + ")");
            alert(err.Message);
        }
    });
}

function verTodos() {
    $("#txtEmpresa").val("");
    $("#txtContacto").val("");
    $("#txtEmail").val("");
    filter();
}

function filter() {
    $("#divError").hide();
    $("#imgLoading").hide();
    $("#lnkDownload").hide();
    $("#btnExportar").attr("disabled", false);

    var grid = $("#grid").data("kendoGrid");
    var $filter = new Array();

    var empresa = $("#txtEmpresa").val();
    if (empresa != "") {
        $filter.push({ field: "Empresa", operator: "contains", value: empresa });
    }

    var contacto = escape($("#txtContacto").val());
    if (contacto != "") {
        $filter.push({ field: "NombreContacto", operator: "contains", value: contacto });
    }

    var email = $("#txtEmail").val();
    if (email != "") {
        $filter.push({ field: "Email", operator: "contains", value: email });
    }

    grid.dataSource.filter($filter);
}

$(document).ready(function () {
    configControls()
});