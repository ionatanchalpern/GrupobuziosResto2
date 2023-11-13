function AddItem(idProducto, cantCenas, cantPax) {
    if (parseInt(cantCenas) == 0 || parseInt(cantPax) == 0)
        alert("La cantidad ingresada debe ser mayor a 0");
    else {
        $.ajax({
            type: "POST",
            url: "/cart.aspx/AddItem",
            data: "{ id: " + parseInt(idProducto) + ", cantCenas: " + parseInt(cantCenas) + ", cantPax: " + parseInt(cantPax) + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, text) {
                //alert("El producto se ha añadido con exito");
                $("#lnkConfirm").click();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status + ":" + thrownError);
            }
        });
    }
}

function AddItemCarrito(idProducto, cantCenas, cantPax) {
    if (parseInt(cantCenas) == 0 || parseInt(cantPax) == 0)
        alert("La cantidad ingresada debe ser mayor a 0");
    else {
        $.ajax({
            type: "POST",
            url: "/cart.aspx/AddItem",
            data: "{ id: " + parseInt(idProducto) + ", cantCenas: " + parseInt(cantCenas) + ", cantPax: " + parseInt(cantPax) + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, text) {
                GetTotal();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status + ":" + thrownError);
            }
        });
    }
}

function ActualizarItemCarrito(idProducto, cantCenasOriginal, cantPaxOriginal) {
    var cantCenas = $.trim($("#txtCantCenas_" + idProducto).val());
    var cantPax = $.trim($("#txtCantPax_" + idProducto).val());
    if (cantCenas != "" && cantPax != "") {
        if (parseInt(cantCenas) == 0 || parseInt(cantPax) == 0) {
            alert("La cantidad ingresada debe ser mayor a 0");
            $("#txtCantCenas_" + idProducto).val(cantCenasOriginal);
            $("#txtCantPax_" + idProducto).val(cantPaxOriginal);
            return;
        }
        else {
            AddItemCarrito(idProducto, cantCenas, cantPax);
            ActualizarSubtotal(idProducto);

            var precioUnitario = parseInt($("#tdPrecioUnitario_" + idProducto).html());
            var subtotal = cantCenas * cantPax * precioUnitario;
            //GetTotal();
            //var string = numeral(subtotal).format('0[.]0[,]00 $');
            $("#tdSubtotal_" + idProducto).html(subtotal);

            //format: "{0:c}"
        }
    }
    else {
        alert("Ingrese la cantidad");
        $("#txtCantCenas_" + idProducto).val(cantCenasOriginal);
        $("#txtCantPax_" + idProducto).val(cantPaxOriginal);
    }
}

function ActualizarSubtotal(id) {
    $.ajax({
        type: "POST",
        url: "/cart.aspx/ActualizarSubtotal",
        data: "{ id: " + parseInt(id) + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, text) {
            $("#tdSubtotal_" + id).html(data.d);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('error');
            alert(xhr.status + ":" + thrownError);
        }
    });
}

function CrearPedido() {
    $("#errorPedido").hide();
    var desde = $("#spnDesde").html();
    var hasta = $("#spnHasta").html();

    var arrDesde = desde.split("/");
    var arrHasta = hasta.split("/");

    var auxDesde = arrDesde[1] + "/" + arrDesde[0] + "/" + arrDesde[2];
    var auxHasta = arrHasta[1] + "/" + arrHasta[0] + "/" + arrHasta[2];

    var info = "{ id: " + parseInt($("#hdnIDCurrentUser").val())
            + ", pasajero: '" + $("#txtPasajero").val()
            + "', dni: '" + $("#txtNroDocumento").val()
            + "', estadiaDesde: '" + auxDesde
            + "', estadiaHasta: '" + auxHasta
            + "', pagoPor: '" + $("#txtPagoPor").val()
            + "'}";

    $.ajax({
        type: "POST",
        url: "carrito.aspx/CrearPedido",
        data: info,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, text) {
            if (data.d != 0)
                window.location.href = "carrito-fin.aspx?IdPedido=" + data.d;
        },
        error: function (response) {
            var r = jQuery.parseJSON(response.responseText);
            $("#errorPedido").html(r.Message);
            $("#errorPedido").show();
        }
    });

    return false;
}

function eliminarPedido(idPedido) {
    if (confirm("¿Está seguro que desea eliminar el pedido seleccionado?")) {
        $.ajax({
            type: "POST",
            url: "/historial.aspx/Eliminar",
            data: "{ id: " + parseInt(idPedido) + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, text) {
                $("#fila_" + idPedido).remove();
            },
            error: function (response) {
                var r = jQuery.parseJSON(response.responseText);
            }
        });
    }
}

function modificarPedido(idPedido) {
    if (confirm("¿Está seguro que desea modificar el pedido seleccionado?")) {
        $.ajax({
            type: "POST",
            url: "/historial.aspx/Modificar",
            data: "{ id: " + parseInt(idPedido) + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, text) {
                window.location.href = "/carrito.aspx";
            },
            error: function (response) {
                var r = jQuery.parseJSON(response.responseText);
            }
        });
    }
}

function RemoveItem(idProducto) {
    $.ajax({
        type: "POST",
        url: "/cart.aspx/RemoveItem",
        data: "{ id: " + parseInt(idProducto) + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, text) {
            $("#fila_" + idProducto).remove();
            GetTotal();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status + ":" + thrownError);
        }
    });
}

function GetTotal() {
    $.ajax({
        type: "POST",
        url: "/carrito.aspx/ObtenerTotal",
        data: "",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, text) {
            $("#lblTotal").html(data.d);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status + ":" + thrownError);
        }
    });
}

function validarFechas() {
    $("#lblError, #lblOk").html("");
    var isValid = true;
    if (!validarFechasCarrito()) {
        var desde = $('#txtEstadiaDesde').val();
        var hasta = $('#txtEstadiaHasta').val();
        if (validarFechaMenor(desde, hasta)) {
            if (validateFechaDesde(desde)) {
                var desdeArr = desde.split("/");
                var hastaArr = hasta.split("/");

             //   var auxDesde = desdeArr[1] + "/" + desdeArr[0] + "/" + desdeArr[2];
           //     var auxHasta = hastaArr[1] + "/" + hastaArr[0] + "/" + hastaArr[2];
                var auxDesde = desdeArr[2] + "-" + desdeArr[1] + "-" + desdeArr[0];
                var auxHasta =  hastaArr[2] + "-"+hastaArr[1] + "-"+ +hastaArr[0] ;
                $.ajax({
                    type: "POST",
                    url: "/cart.aspx/SetFechas",
                    data: "{ desde: '" + auxDesde + "', hasta: '" + auxHasta + "' }",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, text) {
                        $("#txtEstadiaDesde, #txtEstadiaHasta").val("");
                        //$("#lblOk").html("Fechas guardadas correctamente!<br />");
                        validarItemsCarrito();
                    },
                    error: function (response) {
                        var r = jQuery.parseJSON(response.responseText);
                        alert(r.Message);
                    }
                });
            }
            else
                isValid = false;
        }
        else
            isValid = false;
    }

    if (isValid) {
        if (!validarItemsCarrito()) {
            alert('Debe agregar al menos un producto al carrito');
            isValid = false;
        }
    }

    if (isValid)
        window.location.href = "/carrito.aspx";
}

function validateFechaDesde(fecha) {
    var today = new Date();
    var desde = new Date(today);
    desde.setDate(today.getDate());
    var dd = desde.getDate();
    var mm = desde.getMonth() + 1;
    var yyyy = desde.getFullYear();
    if (dd < 10)
        dd = '0' + dd
    if (mm < 10)
        mm = '0' + mm
    var fechaDesde = dd + '/' + mm + '/' + yyyy;
    if (fecha != "" && fechaDesde != "") {
        var fechaArr = fecha.split("/");
        var desdeArr = fechaDesde.split("/");
        if (Date.parse(fechaArr[1] + "-" + fechaArr[0] + "-" + fechaArr[2]) < Date.parse(desdeArr[1] + "-" + desdeArr[0] + "-" + desdeArr[2])) {
            $("#lblError").html("La fecha desde no puede ser menor a la fecha de hoy<br />");
            $("html, body").animate({ scrollTop: 0 }, "slow");
            return false;
        }
        else return true;
    }
}

function validarFechaMenor(desde, hasta) {
    if (desde != "" && hasta != "") {
        var desdeArr = desde.split("/");
        var hastaArr = hasta.split("/");
        if (Date.parse(desdeArr[1] + "-" + desdeArr[0] + "-" + desdeArr[2]) >= Date.parse(hastaArr[1] + "-" + hastaArr[0] + "-" + hastaArr[2])) {
            $("#lblError").html("La fecha desde debe ser menor a la fecha hasta<br />");
            $("html, body").animate({ scrollTop: 0 }, "slow");
            return false;
        }
        else {
            $("#lblError").html("");
            return true;
        }
    }
    else {
        $("#lblError").html("Debe ingresar las fechas<br />");
        $("html, body").animate({ scrollTop: 0 }, "slow");
        return false;
    }
}

function validarItemsCarrito() {
    var hayItems = false;
    $.ajax({
        type: "POST",
        url: "/cart.aspx/ValidarItems",
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, text) {
            if (data != null)
                hayItems = data.d;
        },
        error: function (response) {
            //var r = jQuery.parseJSON(response.responseText);
            alert("Hubo un error, por favor intente nuevamente");
        }
    });
    return hayItems;
}

function validarFechasCarrito() {
    var hayFechas = false;
    $.ajax({
        type: "POST",
        url: "/cart.aspx/ValidarFechas",
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, text) {
            if (data != null) {
                hayFechas = data.d;
            }
        },
        error: function (response) {
            //var r = jQuery.parseJSON(response.responseText);
            alert("Hubo un error, por favor intente nuevamente");
        }
    });
    return hayFechas;
}

$(document).ready(function () {
    GetTotal();
    //$('.numeric').numeric();
    //$("#txtEstadiaDesde").kendoDatePicker({ format: 'dd/MM/yyyy' });
    //$("#txtEstadiaHasta").kendoDatePicker({ format: 'dd/MM/yyyy' });
});