var listOfElements = [];
var i = 0;
var maxHeight;
var idn = 1;


$(function () {

    $(document).ready(function () {
        if ($('#result').val()) {
            var elements = $('#result').val().trim().split('#');
            $.each(elements, function (key, value) {
                if (value.length > 0) {
                    var fields = value.trim().split(';');
                    //var element = '<div id="'+idn+'" attr-type="'+$(this).attr("id")+'" class="elem-'+idn+' draggable ui-widget-content"';
                    var style = '';
                    var type = '';
                    $.each(fields, function (keyfields, valuefields) {

                        if (valuefields.length > 0) {
                            var field = valuefields.trim().split(':');
                            if (field[0] == "left" || field[0] == "top" || field[0] == "width" || field[0] == "height") {
                                style += field[0] + ": " + field[1] + "px;";
                            } else if (field[0] == "type") {
                                type = field[1];
                            }

                        }
                    });
                    //alert( style );					
                    $(".droppable").append('<div id="' + idn + '" attr-type="' + type + '" class="elem-' + idn + ' draggable ui-widget-content"' +
					'style="' + style + '">' +
					'<img src="' + type + '.png"/><span class="eclose">x</span></div>');
                    setElementFunctions(idn);
                    idn++;
                }
            });
        }
    });


    $(".droppable").droppable({
        drop: function (event, ui) {
            ui.draggable.css('max-height', $('#wrapper').height() - ui.draggable.position().top);
            ui.draggable.css('max-width', $('#wrapper').width() - ui.draggable.position().left);

            var value0 = '';
            for (var i = 0; i < listOfElements.length; i++) {
                if (listOfElements[i]['id'] == ui.draggable.attr("id")) {
                    listOfElements[i]['width'] = ui.draggable.width();
                    listOfElements[i]['height'] = ui.draggable.height();
                    listOfElements[i]['left'] = ui.draggable.position().left;
                    listOfElements[i]['top'] = ui.draggable.position().top;
                }

                value0 += 'id:' + listOfElements[i]['id'] + ';type:' + $('.elem-' + listOfElements[i]['id']).attr("attr-type") + ';left:' + (Math.floor($('.elem-' + listOfElements[i]['id']).position().left)) + ';top:' +
                    ($('.elem-' + listOfElements[i]['id']).position().top) +
                    ';width:' + $('.elem-' + listOfElements[i]['id']).width() + ';height:' + $('.elem-' + listOfElements[i]['id']).height() + '#';
            }
            $("#result").val(value0);
        }
    });

    $('#sidebar .element').click(function () {
        //$(".droppable" ).addClass( "ui-state-highlight" ).find( "p" ).html( "Dropped!" );

        $(".droppable").append('<div id="' + idn + '" attr-type="' + $(this).attr("id") + '" class="elem-' + idn + ' draggable ui-widget-content"' +
        'style="top:0px;left:0px;max-height:' + $('#wrapper').height() + 'px;max-width:' + $('#wrapper').width() + 'px;">' +
        '<img src="/WebApp/plugins/Layouts/' + $(this).attr("id") + '.png"/><span class="eclose">x</span></div>');

        setElementFunctions(idn);
        idn++;
    });

    function setElementFunctions(id0) {
        var element = new Array(6)
        element['id'] = id0;
        element['type'] = $('.elem-' + id0).attr("attr-type");
        element['width'] = $('.elem-' + id0).width();
        element['height'] = $('.elem-' + id0).height();
        element['left'] = $('.elem-' + id0).position().left;
        element['top'] = $('.elem-' + id0).position().top;

        listOfElements.push(element);
        var value0 = $("#result").val();
        value0 += 'id:' + id0 + ';type:' + $('.elem-' + id0).attr("attr-type") + ';left:' + (Math.floor($('.elem-' + id0).position().left)) + ';top:' +
    			    ($('.elem-' + id0).position().top) +
    	            ';width:' + $('.elem-' + id0).width() + ';height:' + $('.elem-' + id0).height() + '#';
        $("#result").val(value0);

        $('.elem-' + id0).find("span.eclose").click(function () {

            for (var i = 0; i < listOfElements.length; i++) {
                if (listOfElements[i]['id'] == $(this).parent().attr("id")) {
                    listOfElements.splice(i, 1);
                }
            }

            var value0 = '';
            for (var i = 0; i < listOfElements.length; i++) {
                value0 += 'id:' + listOfElements[i]['id'] + ';type:' + $('.elem-' + listOfElements[i]['id']).attr("attr-type") + ';left:' + (Math.floor($('.elem-' + listOfElements[i]['id']).position().left)) + ';top:' +
                       ($('.elem-' + listOfElements[i]['id']).position().top) +
                       ';width:' + $('.elem-' + listOfElements[i]['id']).width() + ';height:' + $('.elem-' + listOfElements[i]['id']).height() + '#';
            }
            $("#result").val(value0);
            $(this).parent().remove();
        });

        $('.elem-' + id0).draggable({
            containment: '#wrapper',
            cursor: 'move',
            snap: '#wrapper'
        });

        $('.elem-' + id0).resizable({});

        $('.elem-' + id0).resize(function () {

            var value0 = '';
            for (var i = 0; i < listOfElements.length; i++) {
                if (listOfElements[i]['id'] == $(this).attr("id")) {
                    listOfElements[i]['width'] = $(this).width();
                    listOfElements[i]['height'] = $(this).height();
                    listOfElements[i]['left'] = $(this).position().left;
                    listOfElements[i]['top'] = $(this).position().top;
                }

                value0 += 'id:' + listOfElements[i]['id'] + ';type:' + $('.elem-' + listOfElements[i]['id']).attr("attr-type") + ';left:' +
				Math.floor($('.elem-' + listOfElements[i]['id']).position().left) + ';top:' + $('.elem-' + listOfElements[i]['id']).position().top +
    	        ';width:' + $('.elem-' + listOfElements[i]['id']).width() + ';height:' + $('.elem-' + listOfElements[i]['id']).height() + '#';
            }

            $("#result").val(value0);
        });
    }
});