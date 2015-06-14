var table;
var TopRow;
var els;
console.log("hoi");

$().ready(function () {

    table = $('.AdminTable');
    console.log(table);

    TopRow = table.children('thead').children('tr').first();
    console.log(TopRow.children('th').length);

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

        console.log(par);

       

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
        var jsonString = "../Admin/" + pagina + "?vanaf=" + vanaf + "&desc=" + desc + "&filter=" + row + "&mode=1";
        console.log(jsonString);
        $.getJSON(jsonString, function (data) {
            els = jQuery.parseJSON(data);
            console.log(els);
            switchTable();
        });
    }


    function switchTable() {
        var body = table.children('tbody');
        console.log(body);
        body.children('tr').remove();
        var string = "";
        for (i = 0; i < els.length; i++) {
            if (pagina == "Activities") {
                string = "<tr><td><input form='formA' id='DylanToch' name='ActiviteitenToDelete' value='" + els[i]["Id"] + "' type='checkbox' /></td>" + "<td>" + els[i]["Naam"] + "</td><td>" + els[i]["Eigenaar"]["UserName"] + "</td>" + "<td>" + els[i]["Poi"]["Naam"] + "</td>" + "<td><div class='displayInlineButtons'><button><span class='glyphicon glyphicon-remove'></span></button></div></td></tr>";
            } else if (pagina == "Boeken") {

                string = "<tr><td><input form='formA' name='BoekenToDelete' value='" + els[i]["Id"] + "' type='checkbox' /></td><td>" + els[i]["Naam"] + "</td><td>" + els[i]["Eigenaar"]["UserName"] + "</td>" + "<td>";
                
                for(j = 0 ;j <els[i]["Activiteiten"].length ;j++) {
                    string += els[i]["Activiteiten"][j];
                }
                string += "</td><td><div class='displayInlineButtons'><button><span class='glyphicon glyphicon-remove'></span></button></div></td></tr>"

            } else if (pagina == "Pois") {
                string = "<tr><td><input form='formA name='PoisToDelete' value='" + els[i]["Id"] + "'type='checkbox' /> </td><td>" + els[i]["Naam"] + "</td><td>" + els[i]["Eigenaar"]["UserName"] + "</td><td>" + els[i]["Straat"] + " " + els[i]["Nummer"] + " " + els[i]["PostCode"] + " " + els[i]["Gemeente"] + "</td><td>"+els[i]["Telefoon"]+"</td><td>";

                for (j = 0 ; j < els[i]["Tags"].length ; j++) {
                    string += els[i]["Tags"][j];
                }
                string += "</td><td><div class='displayInlineButtons'><button><span class='glyphicon glyphicon-remove'></span></button></div></td></tr>";


            } else if (pagina == "Gebruikers") {
                string = "<tr><td><input form='formA name='GebruikersToDelete' value='" + els[i]["Id"] + "'type='checkbox' /> </td><td>" + els[i]["VoorNaam"] + els[i]["Naam"] + "</td><td>" + els[i]["UserName"] + "</td><td>" + els[i]["Straat"] + " " + els[i]["Nummer"] + " " + els[i]["PostCode"] + " " + els[i]["Gemeente"] + "</td><td>";

            }
        body.append(string);
        }
    }
});