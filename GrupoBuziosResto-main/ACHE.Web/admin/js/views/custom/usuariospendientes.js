function exportar() {
    $("#divErrores").hide();
    $("#lnkDownload").hide();
    $("#imgLoading").show();
    $("#btnExportar").attr("disabled", true);


    $.ajax({
        type: "POST",
        url: "usuarios.aspx/Exportar",
        data: "{ nombre: '" + $("#txtNombre").val()
                + "', email: '" + $("#txtEmail").val() + "'}",
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
    $("#txtEmpresa, #txtEmail").keypress(function (event) {
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
                        IDUsuario: { type: "integer" },
                        TipoUsuario: { type: "string" },
                        Empresa: { type: "string" },
                        Email: { type: "string" },
                        NombreContacto: { type: "string" },
                        Activo: { type: "string" },
                        Estado: { type: "string" },                        
                        FechaAlta: { type: "date" },
                    }
                }
            },
            pageSize: 50,
            batch: true,
            transport: {
                read: {
                    url: "usuariospendientes.aspx/Get", //specify the URL which data should return the records. This is the Read method of the Products.asmx service.
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
            { field: "IDUsuario", title: "ID", width: "50px" },
            { field: "TipoUsuario", title: "Tipo de usuario", width: "80px" },
            { field: "Empresa", title: "Empresa", width: "120px" },
            { field: "Email", title: "Email", width: "170px" },
            { field: "NombreContacto", title: "Nombre de contacto", width: "180px" },
            { field: "Activo", title: "Activo", width: "80px" },
            { field: "Estado", title: "Estado", width: "80px" },
            { field: "FechaAlta", title: "Fecha de alta", format: "{0:dd/MM/yyyy}", width: "80px" },
            { command: { text: "", template: "<div align='center'><img src='../../images/grid/gridDetails.gif' style='cursor:pointer' title='Decidir' class='editColumn'/></div>" }, title: "Decidir", width: "50px" }
            //{ command: { text: "", template: "<div align='center'><img src='../../images/grid/gridDelete.gif' style='cursor:pointer' title='Rechazar' class='deleteColumn'/></div>" }, title: "Rechazar", width: "50px" }
        ]
    });

    $("#grid").delegate(".editColumn", "click", function (e) {
        var grid = $("#grid").data("kendoGrid");
        var dataItem = grid.dataItem($(e.currentTarget).closest("tr"));

        $.ajax({
            type: "POST",
            url: "usuariospendientes.aspx/GetInfo",
            data: "{ id: " + dataItem.IDUsuario + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            //data: "id=" + dataItem.IDTutorial,
            success: function (data, text) {
                if (data.d != null) {
                    $("#lblEmpresa").html(data.d.Empresa);
                    $("#lblContacto").html(data.d.NombreContacto);
                    $("#lblEmail").html(data.d.Email);
                    $("#lblDireccion").html(data.d.Direccion);
                    $("#lblTelefono").html(data.d.Telefono);
                    $("#lblActivo").html(data.d.Activo);
                    $("#lblFechaAlta").html(data.d.FechaAltaString);
                    $("#lblEstado").html(data.d.Estado);
                    $("#txtMotivoRechazo").val("");
                    $("#txtObservaciones").val(data.d.Observaciones);
                    $("#hdnIDSolicitud").val(dataItem.IDUsuario);
                }

                $('#modalUsuario').modal('show');
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status + ":" + thrownError);
            }
        });

    });

    /*$("#grid").delegate(".deleteColumn", "click", function (e) {
        var grid = $("#grid").data("kendoGrid");
        var dataItem = grid.dataItem($(e.currentTarget).closest("tr"));
        if (confirm("¿Esta seguro que desea rechazar la solicitud seleccionada?")) {
            $.ajax({
                type: "POST",
                url: "usuariospendientes.aspx/Delete",
                data: "{ id: " + dataItem.IDUsuario + "}",
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

    $("#grid").delegate(".editColumn", "click", function (e) {
        var grid = $("#grid").data("kendoGrid");
        var dataItem = grid.dataItem($(e.currentTarget).closest("tr"));
        if (confirm("¿Esta seguro que desea aprobar la solicitud seleccionada?")) {
            $.ajax({
                type: "POST",
                url: "usuariospendientes.aspx/Aprobar",
                data: "{ id: " + dataItem.IDUsuario + "}",
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
    });*/
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

function verTodos() {
    $("#txtEmpresa").val("");
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

    /*var apellido = escape($("#txtApellido").val());
    if (apellido != "") {
        $filter.push({ field: "Apellido", operator: "contains", value: apellido });
    }*/

    var empresa = $("#txtEmpresa").val();
    if (empresa != "") {
        $filter.push({ field: "Empresa", operator: "contains", value: empresa });
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