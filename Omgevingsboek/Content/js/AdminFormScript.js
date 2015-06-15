var table;
var TopRow;
var els;
var currentPar;
var currentRow;
console.log("hoi");
$().ready(function () {
    resetVanaf();
    table = $('.AdminTable');

    TopRow = table.children('thead').children('tr').first();
    
    TopRow.children('th').each(function (i) {
        if (i == 0 || i == TopRow.children('th').length - 1) {
        } else {
            $(this).append('<span style="float:right" class="glyphicon glyphicon-menu-down"><span/>');
            if (pagina == "Boeken" && i == 3) {
                //console.log($(this).children(".glyphicon"));
                $(this).children(".glyphicon").remove();
            }
            if (pagina == "Pois" && i == 5 || i==4) {
                $(this).children(".glyphicon").remove();
            }
            if (pagina == "Gebruikers" && i == 3) {
                $(this).children(".glyphicon").remove();
            }
            $(this).children('.glyphicon').click(function (e) {
                getNewData(e);
            });
        }
    });

    $('.delList').click(function () {
        
        var formData = new FormData();

        var lijst = document.getElementsByName('listId');
        formData.append("ActiviteitenToDelete", lijst);
        formData.append("vanaf", vanaf);
        formData.append("desc", desc);
        formData.append("filter", filter);
        jsonListUp(formData);


        
    });

    function getNewData(e) {
        var pressed = e.target;
        var par = e.currentTarget.parentElement;
        row = $(par.children[0]).html();

        //kijken of de tabel geselecteerd is
        if (filter == row) {
            
            //kijken of asc of desc
            if (desc == 0) {
                desc = 1;
                $(pressed).removeClass('glyphicon-menu-down');
                $(pressed).addClass('glyphicon-menu-up');
            } else if (desc == 1) {
                desc = 0;
                $(pressed).removeClass('glyphicon-menu-up');
                $(pressed).addClass('glyphicon-menu-down');
            }
        } else {
            console.log('oi');
            filter = row;
            //kijken of asc of desc
            if ($(this).hasClass('glyphicon-menu-up')) {
                $(pressed).removeClass('glyphicon-menu-up');
                $(pressed).addClass('glyphicon-menu-down');
                desc = 0;
                console.log('hey');
            } else {
                $(pressed).removeClass('glyphicon-menu-down');
                $(pressed).addClass('glyphicon-menu-up');
                desc = 1;
                console.log('ney');
            }
        }
        if (pagina == "Activities") {
        if (row == "Naam") {
            row = 1;
        } else if (row == "Eigenaar") {
            row = 2;
        } else if (row == "Poi") {
            row = 3;
        }
        } else if (pagina == "Boeken") {
            if (row == "Naam") {
                row = 1;
            } else if (row == "Eigenaar") {
                row = 2;
            } 
        } else if (pagina == "Pois") {
            if (row == "Naam") {
                row = 1;
            } else if (row == "Mail") {
                row = 2;
            } else if (row == "Adres") {
                row = 3;
            } 
        } else if (pagina = "Gebruikers") {
            if (row == "Naam") {
                row = 1;
            } else if (row == "Email") {
                row = 2;
            }
        }
        currentRow = row;
        currentPar = par
        //DIT IS WAAR IK MIJN JSON GA HALEN. EN DIT GEEFT EEN ERROR TERUG
        var jsonString = "../Admin/" + pagina + "?vanaf=" + vanaf + "&desc=" + desc + "&filter=" + currentRow + "&search=" + search + "&mode=1";
        jsonItUp(jsonString);
        
        
        
    }
    function jsonListUp(formData) {
        $.ajax({
            type: "POST",
            url: "../Admin/DeleteActiviteit",
            data: formData,
            dataType: 'json',//change to your own, else read my note above on enabling the JsonValueProviderFactory in MVC
            contentType: false,
            processData: false,
            success: function (data) {
                //BTW, data is one of the worst names you can make for a variable
                //handleSuccessFunctionHERE(data);
                console.log(':t');
            },
            error: function (jqXHR, textStatus, errorThrown) {
                //do your own thing
                console.log("fail");
            }
        });
    }
    function jsonItUp(jsonString) {
        //console.log(jsonString);
        $.getJSON(jsonString, function (data) {
            els = jQuery.parseJSON(data);
            console.log(els);
            switchTable();
        });
    }

function switchTable() {
        var body = table.children('tbody');
        body.children('tr').remove();
        var string = "";
        for (i = 0; i < els.length; i++) {
            if (pagina == "Activities") {
                string = "<tr><td><input form='formA' id='DylanToch' name='ActiviteitenToDelete' value='" + els[i]["Id"] + "' type='checkbox' /></td>" + "<td>" + els[i]["Naam"] + "</td><td>" + els[i]["Eigenaar"]["UserName"] + "</td>" + "<td>" + els[i]["Poi"]["Naam"] + "</td>" + "<td><div class='displayInlineButtons'><button><span class='glyphicon glyphicon-remove'></span></button></div></td></tr>";
            } else if (pagina == "Boeken") {

                string = "<tr><td><input form='formA' name='BoekenToDelete' value='" + els[i]["Id"] + "' type='checkbox' /></td><td>" + els[i]["Naam"] + "</td><td>" + els[i]["Eigenaar"]["UserName"] + "</td>" + "<td>";
                
                for(j = 0 ;j <els[i]["Activiteiten"].length ;j++) {
                    string +=  "<a href='#'>"+  els[i]["Activiteiten"][j]["Naam"] + "</a>"
                }
                string += "</td><td><div class='displayInlineButtons'><button><span class='glyphicon glyphicon-remove'></span></button></div></td></tr>"

            } else if (pagina == "Pois") {
                string = "<tr><td><input form='formA name='PoisToDelete' value='" + els[i]["Id"] + "'type='checkbox' /> </td><td>" + els[i]["Naam"] + "</td><td>" + els[i]["Eigenaar"]["UserName"] + "</td><td>" + els[i]["Straat"] + " " + els[i]["Nummer"] + " " + els[i]["Postcode"] + " " + els[i]["Gemeente"] + "</td><td>"+els[i]["Telefoon"]+"</td><td>";

                for (j = 0 ; j < els[i]["Tags"].length ; j++) {
                    string += '<span class="tag">' + els[i]["Tags"][j]["Tag"]["Naam"] + "</span>";
                }
                string += "</td><td><div class='displayInlineButtons'><button><span class='glyphicon glyphicon-remove'></span></button></div></td></tr>";


            } else if (pagina == "Gebruikers") {
                string = "<tr><td><input form='formA name='GebruikersToDelete' value='" + els[i]["User"]["Id"] + "'type='checkbox' /> </td><td>" + els[i]["User"]["Voornaam"] + " " + els[i]["User"]["Naam"] + "</td><td>" + els[i]["User"]["UserName"] + "</td><td>";
                for (j = 0 ; j < els[i]["Activiteiten"].length ; j++) {
                        string += "<a href='#'>" + els[i]["Activiteiten"][j]["Naam"] + "</a>";
                    }
                string += "</td><td><div class='displayInlineButtons'><button><span class='glyphicon glyphicon-remove'></span></button></div></td></tr>";

            }
            body.append(string);
            
        }
        resetVanaf();
    }
function resetVanaf() {
    if (vanaf == 0) {
        $('.SelVor').attr('disabled', 'disabled');
        $('.SelVor').css('opacity', 0.5);
        $('.SelVor').off();
        $('.SelVol').off();
        $('.SelVol').click(function () {
            vanaf += 30;
            var jsonString = "../Admin/" + pagina + "?vanaf=" + vanaf + "&desc=" + desc + "&filter=" + currentRow + "&search=" + search + "&mode=1";
            jsonItUp(jsonString);
        });
    } else {
        $('.SelVor').removeAttr('disabled');
        $('.SelVor').css('opacity', 1);
        $('.SelVor').off();
            $('.SelVor').click(function () {
                vanaf -= 30;
                var jsonString = "../Admin/" + pagina + "?vanaf=" + vanaf + "&desc=" + desc + "&filter=" + currentRow + "&search=" + search + "&mode=1";
                jsonItUp(jsonString);
            });
            $('.SelVol').off();
            $('.SelVol').click(function () {
                
                var jsonString = "../Admin/" + pagina + "?vanaf=" + vanaf + "&desc=" + desc + "&filter=" + filter + "&search=" + search + "&mode=1";
                jsonItUp(jsonString);
                
            });
    }
    $('.voorbVanaf').html("" + vanaf + " - " + (vanaf + 30));
}
});