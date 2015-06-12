var table;
var TopRow;
var els;


$().ready(function () {

    table = $('.AdminTable');
    TopRow = table.children('thead').children('tr').first();
    TopRow.children('th').each(function (i) {
        if (i == 0 || i == TopRow.children('th').length - 1) {
        } else {
            $(this).append('<span style="float:right" class="glyphicon glyphicon-menu-down"><span/>');
            
            $(this).children('.glyphicon').click(function (e) {
                getNewData(e);
            });
        }



    });



    function getNewData(e) {
        var pressed = e.target;
        var par = e.currentTarget.parentElement;
        var row = $(par.children[0]).html();

       

        //kijken of de tabel geselecteerd is
        if (filter == row) {
            
            //kijken of asc of desc
            if (desc == 0) {
                desc = 1;
                console.log(e.target);
                $(pressed).removeClass('glyphicon-menu-down');
                $(pressed).addClass('glyphicon-menu-up');
            } else if (desc == 1) {
                desc = 0;
                $(pressed).removeClass('glyphicon-menu-up');
                $(pressed).addClass('glyphicon-menu-down');
            }
        } else {

            filter = row;
            //kijken of asc of desc
            if ($(this).hasClass('glyphicon-menu-down')) {
                desc = 0;
            } else {
                desc = 1;
            }
        }
        if (row == "Naam") {
            row = 1;
        } else if (row == "Eigenaar") {
            row = 2;
        } else if (row == "Poi") {
            row = 3;
        }

        //DIT IS WAAR IK MIJN JSON GA HALEN. EN DIT GEEFT EEN ERROR TERUG
        $.getJSON("../Admin/" + pagina + "?vanaf=" + vanaf + "&desc=" + desc + "&filter=" + row + "&mode=1", function (data) {

           
            els = jQuery.parseJSON(data);
            switchTable();
        });
    }


    function switchTable() {
        var body = table.children('tbody');
        body.children('tr').remove();
        for (i = 0; i < els.length; i++) {
            if (pagina == "Activities") {
                body.append("<tr><td><input form='formA' id='DylanToch' name='ActiviteitenToDelete' value='" + els[i]["Id"] + "' type='checkbox' /></td>" + "<td>" + els[i]["Naam"] + "</td><td>" + els[i]["Eigenaar"]["UserName"] + "</td>" + "<td>" + els[i]["Poi"]["Naam"] + "</td>" + "</tr>");
            } 
        }
    }
});