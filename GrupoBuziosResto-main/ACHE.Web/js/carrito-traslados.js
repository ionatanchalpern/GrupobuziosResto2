function eliminarPedido(idPedido) {
    if (confirm("¿Está seguro que desea eliminar el pedido seleccionado?")) {
        $.ajax({
            type: "POST",
            url: "/historial-traslados.aspx/Eliminar",
            data: "{ id: " + parseInt(idPedido) + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, text) {
                $("#fila_" + idPedido).remove();
            },
            error: function (response) {
                var r = jQuery.parseJSON(response.responseText);
                alert(r.Message);
            }
        });
    }
}

function modificarPedido(idPedido) {
    if (confirm("¿Está seguro que desea modificar el pedido seleccionado?")) {
        window.location.href = "traslados-paso1.aspx?Id=" + idPedido;
    }
}