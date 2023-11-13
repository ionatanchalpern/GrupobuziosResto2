/*function pnlRequestStarted(ajaxManager, eventArgs) {
    eventArgs.EnableAjax = (eventArgs.EventTarget.indexOf("imgEdit") != -1 || eventArgs.EventTarget.indexOf("imgDelete") != -1) ? false : true;
}
*/
function setEditorDescripcion()
{
    $("#txtDescripcion").jqte({
        color: false, format: true, fsize: false, indent: false, rule: false,
        remove: false, strike: false, source: false, outdent: false,
        ol: false, sub: false, sup: false, title: false,
        size: true,
        formats: [
            ["p", "Normal"],
            ["h2", "Destacado 1"],
            ["h3", "Destacado 2"]
        ],
        blur: function () {
            document.getElementById('hdDescripcion').value = $("#txtDescripcion").val();
        }
    });
}

function setEditorDescripcionBreve() {
    $("#txtDescripcionBreve").jqte({
        color: false, format: true, fsize: false, indent: false, rule: false,
        remove: false, strike: false, source: false, outdent: false,
        ol: false, sub: false, sup: false, title: false,
        size: true,
        formats: [
            ["p", "Normal"],
            ["h2", "Destacado 1"],
            ["h3", "Destacado 2"]
        ],
        blur: function () {
            document.getElementById('hdDescripcionBreve').value = $("#txtDescripcionBreve").val();
        }
    });
}


function Exportar(tipo) {
    var isValid = true;
    var errores = "";

    if (isValid) {

        //$('#divErrores').hide();

        var params = "?tipo=" + tipo;

        var eform = document.getElementById('frmExportar');
        eform.action = baseURL + "Admin/Excel/Exportar" + params;
        eform.submit();
    }
    else {
        //$('#msgError').html(errores);
        //$('#divErrores').show();
        return false;
    }
}

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
    var regExp = /^((([a-z]|[0-9]|!|#|$|%|&|'|\*|\+|\-|\/|=|\?|\^|_|`|\{|\||\}|~)+(\.([a-z]|[0-9]|!|#|$|%|&|'|\*|\+|\-|\/|=|\?|\^|_|`|\{|\||\}|~)+)*)@((((([a-z]|[0-9])([a-z]|[0-9]|\-){0,61}([a-z]|[0-9])\.))*([a-z]|[0-9])([a-z]|[0-9]|\-){0,61}([a-z]|[0-9])\.)[\w]{2,4}|(((([0-9]){1,3}\.){3}([0-9]){1,3}))|(\[((([0-9]){1,3}\.){3}([0-9]){1,3})\])))$/
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




/*================================
Scroll Top
=================================*/
$(function () {
	$("#goTop").hide();
	$(window).scroll(function () {
		if ($(this).scrollTop() > 100) {
			$('#goTop').fadeIn();
		} else {
			$('#goTop').fadeOut();
		}
	});
	
	$('#goTop a').click(function () {
		$('body,html').animate({
			scrollTop: 0
		}, 500);
		return false;
	});
});
		
//You need an anonymous function to wrap around your function to avoid conflict
(function($){
	 
	//Attach this new method to jQuery
	$.fn.extend({
			 
		//This is where you write your plugin's name
		slideMnu: function() {
		  
			$('.right-toggle').click(function()
		{
			$('#panel-right').toggleClass('panel-close panel-open',500, 'easeOutExpo');
			});
			 
		}
	});
		 
})(jQuery);
		
	
$(document).ready(function(){

	/*===================
	TAB STYLE
	===================*/	  
	/*		  
	$(".tab-block").hide(); //Hide all content
	$(".mytabs li:first").addClass("active").show(); //Activate first tab
	$(".tab-block:first").show(); //Show first tab content
	
	//On Click Event
	$(".mytabs li").click(function() {
	
	    $(".mytabs li").removeClass("active"); //Remove any "active" class
	    $(this).addClass("active"); //Add "active" class to selected tab
	    $(".tab-block").hide(); //Hide all tab content
	
	    var activeTab = $(this).find("a").attr("href"); //Find the href attribute value to identify the active tab + content
	    $(activeTab).show(); //Fade in the active ID content
	    return false;
	});*/
	
	/*===================
	LIST-ACCORDION
	===================*/	  

	$('#list-accordion').accordion({
		header: ".title",
		autoheight: false
	});
	
	/*===================
	ACCORDION NAV
	===================*/
		
	$('#accordion-nav').accordion({
	    active: false,
	    header: '.head',
	    navigation: true,
	    event: 'click',
	    fillSpace: false
	});
	
	/*===================
	ADMINISTRATION MENU
	===================*/

	$('#usermenu').click(function () {
	    $('.admin-user').addClass('active');
	    $('.sub-menu').slideToggle('fast');
	});
	
	$(document).click(function(e) {
        var t = (e.target)
            if(t!= $(".sub-menu").get(0) && t!=$(".admin-user").get(0) ) {
                 $('.admin-user').removeClass('active');
				$(".sub-menu").hide();
            }
    });
	

	/*======================
	COLLAPSIBLE PANEL STYLE
	========================*/
	$.collapsible(".collapse-bar");
	
	/*======================
	ACCORDION MENU
	========================*/
	$('.menu').initMenu();

	/*======================
	RIGHT SLIDE BAR
	========================*/	
	//$('#panel-right').slideMnu();
			
	/*======================
	TOP SWITCHBOARD
	========================*/
	/*$('#shortcur-bar').sortable({
		items: 'li' ,
		placeholder:'drag-place'
	});
	$( '#shortcur-bar').disableSelection();*/

	/*======================
	DATE PICKER
	========================*/
	 /*--Datepicker--*/
	/*$(".datepicker").datepicker({
		showButtonPanel: true
	});*/
	
	/*======================
	SELECT BOX
	========================*/
//	$(".chzn-select").chosen();
//	$(".chzn-select-deselect").chosen({
//		allow_single_deselect: true
//	});
	
	/*======================
	INPUT UNIFROM
	========================*/
	/*--Input files style--*/
	
//	$(".input-uniform input[type=file],.input-uniform input[type=radio],.input-uniform input[type=checkbox], input[type=file]").uniform();

	/*======================
	UI COMBOBOX
	========================*/
	//$("#combobox" ).combobox();
		  
	
	/*======================
	TEXT EDITOR
	========================*/
	/*--LARGE--*/
//	$('textarea.tinymceS').tinymce({
//		// Location of TinyMCE script
//		script_url: 'js/tiny_mce/tiny_mce.js',
//	
//		// Example content CSS (should be your site CSS)
//		content_css: "css/editor-styles.css",
//	
//		// General options
//		theme: "advanced",
//		theme_advanced_toolbar_location: "top",
//		theme_advanced_toolbar_align: "left",
//		theme_advanced_statusbar_location: "bottom",
//		theme_advanced_resizing: false
//	});
	  
	/*--SIMPLE--*/
//	$('textarea.tinymce-simple').tinymce({
//		// Location of TinyMCE script
//		script_url: 'js/tiny_mce/tiny_mce.js',
//	
//		// General options
//		theme: "simple",
//		theme_advanced_resizing: false
//	});
	  
	/*--ADVANCED--*/
	  
//	$('textarea.tinymce-adv').tinymce({
//	    // Location of TinyMCE script
//	    script_url: 'js/tiny_mce/tiny_mce.js',
//	
//	    // Example content CSS (should be your site CSS)
//	    content_css: "css/editor-styles.css",
//	
//	    // General options
//	    theme: "advanced",
//	    plugins : "autolink,lists,pagebreak,style,layer,table,save,advhr,advimage,advlink,emotions,iespell,inlinepopups,insertdatetime,preview,media,searchreplace,print,contextmenu,paste,directionality,fullscreen,noneditable,visualchars,nonbreaking,xhtmlxtras,template,advlist",
//	
//	    // Theme options
//	    theme_advanced_buttons1 : "save,newdocument,|,bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,styleselect,formatselect,fontselect,fontsizeselect",
//	    theme_advanced_buttons2 : "cut,copy,paste,pastetext,pasteword,|,search,replace,|,bullist,numlist,|,outdent,indent,blockquote,|,undo,redo,|,link,unlink,anchor,image,cleanup,help,code,|,insertdate,inserttime,preview,|,forecolor,backcolor",
//	    theme_advanced_buttons3 : "tablecontrols,|,hr,removeformat,visualaid,|,sub,sup,|,charmap,emotions,iespell,media,advhr,|,print,|,ltr,rtl,|,fullscreen",
//	    theme_advanced_buttons4 : "insertlayer,moveforward,movebackward,absolute,|,styleprops,|,cite,abbr,acronym,del,ins,attribs,|,visualchars,nonbreaking,template,pagebreak",
//	    theme_advanced_toolbar_location : "top",
//	    theme_advanced_toolbar_align : "left",
//	    theme_advanced_statusbar_location : "bottom",
//	    theme_advanced_resizing : false,
//	    theme_advanced_toolbar_location: "top",
//	    theme_advanced_toolbar_align: "left",
//	    theme_advanced_statusbar_location: "bottom",
//	    theme_advanced_resizing: false
//	});
	  
	/*======================
	BREADCRUMB
	========================*/
//	$("#breadCrumb0").jBreadCrumb();
//	$("#breadCrumb1").jBreadCrumb();
//	$("#breadCrumb2").jBreadCrumb();
//	$("#breadCrumb3").jBreadCrumb();

	/*======================
	FILE EXPLORER
	========================*/

//	var f = $('#finder').elfinder({
//				url : 'connectors/php/connector.php',
//				lang : 'en',
//				docked : false

//				// dialog : {
//				// 	title : 'File manager',
//				// 	height : 500
//				// }

//				// Callback example
//				//editorCallback : function(url) {
//				//	if (window.console && window.console.log) {
//				//		window.console.log(url);
//				//	} else {
//				//		alert(url);
//				//	}
//				//},
//				//closeOnEditorCallback : true	
//	});
				
	/*======================
	CALENDAR
	========================*/
	/*				
	var date = new Date();
	var d = date.getDate();
	var m = date.getMonth();
	var y = date.getFullYear();
	
	$('#calendar').fullCalendar({
		header: {
			left: 'prev,next today',
			center: 'title',
			right: 'month,agendaWeek,agendaDay'
		},
		editable: true,
		events: [
			{
				title: 'All Day Event',
				start: new Date(y, m, 1)
			},
			{
				title: 'Long Event',
				start: new Date(y, m, d-5),
				end: new Date(y, m, d-2)
			},
			{
				id: 999,
				title: 'Repeating Event',
				start: new Date(y, m, d-3, 16, 0),
				allDay: false
			},
			{
				id: 999,
				title: 'Repeating Event',
				start: new Date(y, m, d+4, 16, 0),
				allDay: false
			},
			{
				title: 'Meeting',
				start: new Date(y, m, d, 10, 30),
				allDay: false
			},
			{
				title: 'Lunch',
				start: new Date(y, m, d, 12, 0),
				end: new Date(y, m, d, 14, 0),
				allDay: false
			},
			{
				title: 'Birthday Party',
				start: new Date(y, m, d+1, 19, 0),
				end: new Date(y, m, d+1, 22, 30),
				allDay: false
			},
			{
				title: 'Click for Google',
				start: new Date(y, m, 28),
				end: new Date(y, m, 29),
				url: 'http://google.com/'
			}
		]
	});*/

	//});
    
    /*======================
	jQuery MODAL
	========================*/
		
	/*jQuery(function ($) {
	// Load dialog on page load
	//$('#basic-modal-content').modal();

	// Load dialog on click
	$('#basic-modal .basic').click(function (e) {
	    $('#basic-modal-content').modal();
		    return false;
		    });
	});*/

	/*======================
	CONFIRM DIALOG
	========================*/

	/*jQuery(function ($) {
		$('#confirm-dialog input.confirm, #confirm-dialog a.confirm').click(function (e) {
			e.preventDefault();
	
			// example of calling the confirm function
			// you must use a callback function to perform the "yes" action
			confirm("Continue to the SimpleModal Project page?", function () {
				window.location.href = 'http://www.ericmmartin.com/projects/simplemodal/';
			});
		});
	});*/

	/*
	 * SimpleModal OSX Style Modal Dialog
	 * http://www.ericmmartin.com/projects/simplemodal/
	 * http://code.google.com/p/simplemodal/
	 *
	 * Copyright (c) 2010 Eric Martin - http://ericmmartin.com
	 *
	 * Licensed under the MIT license:
	 *   http://www.opensource.org/licenses/mit-license.php
	 *
	 * Revision: $Id: osx.js 238 2010-03-11 05:56:57Z emartin24 $
	 */
	/*
	jQuery(function ($) {
		var OSX = {
			container: null,
			init: function () {
				$("input.osx, a.osx").click(function (e) {
					e.preventDefault();	
	
					$("#osx-modal-content").modal({
						overlayId: 'osx-overlay',
						containerId: 'osx-container',
						closeHTML: null,
						minHeight: 80,
						opacity: 65, 
						position: ['0',],
						overlayClose: true,
						onOpen: OSX.open,
						onClose: OSX.close
					});
				});
			},
			open: function (d) {
				var self = this;
				self.container = d.container[0];
				d.overlay.fadeIn('slow', function () {
					$("#osx-modal-content", self.container).show();
					var title = $("#osx-modal-title", self.container);
					title.show();
					d.container.slideDown('slow', function () {
						setTimeout(function () {
							var h = $("#osx-modal-data", self.container).height()
								+ title.height()
								+ 20; // padding
							d.container.animate(
								{height: h}, 
								200,
								function () {
									$("div.close", self.container).show();
									$("#osx-modal-data", self.container).show();
								}
							);
						}, 300);
					});
				})
			},
			close: function (d) {
				var self = this; // this = SimpleModal object
				d.container.animate(
					{top:"-" + (d.container.height() + 20)},
					500,
					function () {
						self.close(); // or $.modal.close();
					}
				);
			}
		};
	
		OSX.init();
	
	});*/

});

function ConfirmarPopUp(message, callback) {
    $('#confirm').modal({
        closeHTML: "<a href='#' title='Close' class='modal-close'>x</a>",
        position: ["20%", ],
        overlayId: 'confirm-overlay',
        containerId: 'confirm-container',
        onShow: function (dialog) {
            var modal = this;

            $('.message', dialog.data[0]).append(message);

            // if the user clicks "yes"
            $('.yes', dialog.data[0]).click(function () {
                // call the callback
                if ($.isFunction(callback)) {
                    callback.apply();
                }
                // close the dialog
                modal.close(); // or $.modal.close();
            });
        }
    });
}

function MostrarPopUp(idDialog, height, width, callback) {
    $('#' + idDialog).modal({
        closeHTML: "<a href='#' title='Close' class='modal-close'>x</a>",
        position: ["20%", ],
        overlayId: 'confirm-overlay',
        containerId: 'edit-container',
        minHeight: height,
        minWidth: width,
        onShow: function (dialog) {
            var modal = this;

            // if the user clicks "yes"
            $('.yes', dialog.data[0]).click(function () {
                // call the callback
                if ($.isFunction(callback)) {
                    callback.apply();
                }
                // close the dialog
                //modal.close(); // or $.modal.close();
            });
        }
    });
}		




