function IsValidURL(url) {
    url = url.toLowerCase();

    var regExp = /(\w+:{0,1}\w*@)?(\S+)(:[0-9]+)?(\/|\/([\w#!:.?+=&%@!\-\/]))?/

    if (url.indexOf(".com") < 1)
        return false;
    else {
        if (regExp.test(url)) {
            return true;
        } else {
            return false;
        }
    }
}

function IsValidEmail(email) {
    email = email.toLowerCase();
    var regExp = /(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|"(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])/
    if (regExp.test(email)) {
        return true;
    } else {
        return false;
    }
}

function IsNumeric(value) {
    if (value != "")
        return !isNaN(parseFloat(value)) && isFinite(value);
    else return true;
}

function EvitarEspaciosEnBlanco(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    return tecla != 32;
}

function DatesAreValid(fechaDesde, fechaHasta) {
    var str1 = fechaDesde;
    var str2 = fechaHasta;

    var dt1 = parseInt(str1.substring(0, 2), 10);
    var mon1 = parseInt(str1.substring(3, 5), 10);
    var yr1 = parseInt(str1.substring(6, 10), 10);

    var dt2 = parseInt(str2.substring(0, 2), 10);
    var mon2 = parseInt(str2.substring(3, 5), 10);
    var yr2 = parseInt(str2.substring(6, 10), 10);

    var date1 = new Date(yr1, mon1, dt1);
    var date2 = new Date(yr2, mon2, dt2);

    if (date2 < date1) {
        return false;
    }
    else {
        return true;
    }
}

function ValidarExtension(file) {
    var extArray = new Array(".gif", ".jpg", ".png", ".jpeg");

    allowSubmit = false;

    if (!file) return;
    while (file.indexOf("\\") != -1)
        file = file.slice(file.indexOf("\\") + 1);
    ext = file.slice(file.indexOf(".")).toLowerCase();
    for (var i = 0; i < extArray.length; i++) {
        if (extArray[i] == ext) { allowSubmit = true; break; }
    }

    return allowSubmit;
}

function removeSpaces(s) {
    return s.split(' ').join('');
}

function CuitEsValido(cuit) {

    if (typeof (cuit) == 'undefined')
        return true;

    cuit = cuit.toString().replace(/[-_]/g, "");
    if (cuit == '')
        return true; //No estamos validando si el campo esta vacio, eso queda para el "required"

    if (cuit.length != 11)
        return false;
    else {

        var mult = [5, 4, 3, 2, 7, 6, 5, 4, 3, 2];
        var total = 0;

        for (var i = 0; i < mult.length; i++) {
            total += parseInt(cuit.charAt(i)) * mult[i];
        }

        var mod = total % 11;
        var digito = mod == 0 ? 0 : mod == 1 ? 9 : 11 - mod;
    }

    return digito == parseInt(cuit.charAt(10));
}
