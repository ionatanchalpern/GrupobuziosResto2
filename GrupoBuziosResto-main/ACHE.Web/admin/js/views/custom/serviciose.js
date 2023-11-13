$(document).ready(function () {
 //   configControls();
});
function configControls() {
    if ($("#hdnIDServicio").val() == "0")
         cargarSubtipos();

    $("#ddlTipo").change(function () {
        cargarSubtipos();
    });

}
function cargarSubtipos() {
    $.ajax({
        type: "POST",
        url: "serviciose.aspx/GetSubTipos",
        data: "{ tipo: '" + $("#ddlTipo").val() + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, text) {
            if (data.d != null && data.d.length > 0) {
                $("#ddlSubTipos").empty();
                //  $("#cmbAeropuertoVuelta").append("<option value=''>Seleccione un servicio</option>");
                for (var i = 0; i < data.d.length; i++)
                    $("#ddlSubTipos").append("<option value='" + data.d[i].ID + "'>" + data.d[i].Nombre + "</option>");
                    // $("#ddlSubTipos").append("<option value='" + data.d[i].ID + "'>" + data.d[i].Nombre + "</option>");

            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            var err = eval("(" + xhr.responseText + ")");
            alert(err.Message);
        }
    });
}