
function AgregarProducto() {
    var intID = parseInt($("#hdnId").val());
    $.ajax({
        type: "POST",
        url: "Cart.aspx/AgregarProducto",
        data: '{id: ' + intID + ', cantidad: ' + $("#txtCantidad").val() + '}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function () {
            alert("Agrego un producto");
        },
        error: function () {
            alert("Error");
        }
    });
}

function ModificarProducto() {
    $.ajax({
        type: "POST",
        url: "Cart.aspx/ModificarProducto",
        data: '{id: ' + intID + ', cantidad: ' + $("#txtCantidad").val() + '}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function () {
            alert("Modifico un producto");
        },
        error: function () {
            alert("Error");
        }
    });
}

function EliminarProducto() {
    $.ajax({
        type: "POST",
        url: "Cart.aspx/EliminarProducto",
        data: '{id: ' + intID + '}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function () {
            alert("Elimino un producto");
        },
        error: function () {
            alert("Error");
        }
    });
}
