var table;
var TopRow;
var els;
var currentPar;
var currentRow;
$().ready(function () {

    
    resetVanaf();
    jsonItUp("../Admin/" + pagina + "?vanaf=" + vanaf + "&desc=" + desc + "&filter=" + currentRow + "&search=" + search + "&mode=1");
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
            if (pagina == "Pois" && i == 5 || i==7) {
                $(this).children(".glyphicon").remove();
            }
            if (pagina == "Gebruikers" && i == 3 ) {

                $(this).children(".glyphicon").remove();
            }
            $(this).children('.glyphicon').click(function (e) {
                getNewData(e);
            });
        }
    });
    //De kliklisteners
    $('.oneDel').click(function (e) {
        console.log('ola');
        delOne(e);
    });
    $('.delList').css('display', 'inline');
    $('.superDelList').click(function () {
        var formData = new FormData();
        var Delurl = "";
        var lijst = $('[name=listId]');
        console.log(lijst);
        var array = [];
        lijst.each(function (iets) {
            if ($(lijst[iets]).is(":checked")) {
                console.log("Aantal");
                var value = parseInt($(lijst[iets]).attr("value"));

                if (pagina == "Activities") {
                    formData.append("ActiviteitenToDelete", value);
                    Delurl = "../Admin/HardDeleteActiviteit";
                } else if (pagina == "Boeken") {
                    formData.append("BoekenToDelete", value);
                    Delurl = "../Admin/HardDeleteBoeken";
                } else if (pagina == "Pois") {
                    formData.append("PoisToDelete", value);
                    Delurl = "../Admin/HardDeletePoi";
                } else if (pagina = "Gebruikers") {
                    formData.append("DeleteUsersSoft", value);
                    Delurl = "../Admin/DeleteUsersHard";
                }
            }
        })
        jsonListUp(formData, Delurl);
    });
    $('.delList').click(function () {
        console.log("klik");
        $('.alertt').dialog({
            dialogClass: "dlg-no-title",
            resizable: false,
            height: 200,
            modal: true,
            buttons: {
                "verwijderen": function () {
                    deleteList();
                    $(this).dialog("close");
                },
                Cancel: function () {
                    $(this).dialog("close");
                }
            },
            closeText: "hide"
        });
        
    });
    $('.compDel').click(function () {
        console.log("klak");
        $('.alerth').dialog({
            dialogClass: "dlg-no-title",
            resizable: false,
            height: 200,
            modal: true,
            buttons: {
                "verwijderen": function () {
                    hardDeleteList();
                    $(this).dialog("close");
                },
                Cancel: function () {
                    $(this).dialog("close");
                }
            },
            closeText: "hide"
        });
    });
    $('.changeRecht').click(function () {
        $('.alertr').dialog({
            dialogClass: "dlg-no-title",
            resizable: false,
            height: 200,
            modal: true,
            buttons: {
                "verwijderen": function () {
                    changeRights();
                    $(this).dialog("close");
                },
                Cancel: function () {
                    $(this).dialog("close");
                }
            },
            closeText: "hide"
        });

    })
    $('.remUit').click(function (e) {
        DelOneuit(e);
       // deleteuitnodiging
    
    });
    $('.resUit').click(function (e) {
        ResOneUit(e);
    })
    function DelOneuit(e) {
        var roel;
        var value;
        var formData = new FormData();
        if ($(e.target).children('span').length == 0) {
            roel = $(e.target).parent().parent();
            value = $(e.target).parent().attr("value");
        } else {
            value = $(e.target).attr("value");
            roel = $(e.target).parent();
        }
        formData.append("deleteIds", value);
        url = "../Admin/deleteuitnodiging";
        $.ajax({
            type: "POST",
            url: url,
            data: formData,
            dataType: 'text',//change to your own, else read my note above on enabling the JsonValueProviderFactory in MVC
            contentType: false,
            processData: false,
            success: function (data) {
                console.log(data);
                roel.remove();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert('Hiet is niet gelukt sorry.');
                
            }
        });
    }
    function ResOneUit(e) {
        console.log(e);
        var value;
        var formData = new FormData();
        if ($(e.target).children('span').length == 0) {
            value = $(e.target).parent().attr("value");
        } else {
            value = $(e.target).attr("value");
        }
        formData.append("Id", value);
        url = "../Admin/HerzendUitnodiging";
        $.ajax({
            type: "POST",
            url: url,
            data: formData,
            dataType: 'text',//change to your own, else read my note above on enabling the JsonValueProviderFactory in MVC
            contentType: false,
            processData: false,
            success: function (data) {
                console.log(data);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert('Hiet is niet gelukt sorry.');
                console.log(data);
                console.log(value);
            }
        });
    }
    //compleet verwijderen via glyphicons
    function DelOne(e) {
        var value;
        var formData = new FormData();
        if ($(e.target).children('span').length == 0) {
            value = $(e.target).parent().attr("value");
        } else {
            value = $(e.target).attr("value");
        }
        if (pagina == "Activities") {
            formData.append("ActiviteitenToDelete", value);
            Delurl = "../Admin/DeleteActiviteit";
        } else if (pagina == "Boeken") {
            formData.append("BoekenToDelete", value);
            Delurl = "../Admin/DeleteBoeken";
        } else if (pagina == "Pois") {
            formData.append("PoisToDelete", value);
            Delurl = "../Admin/DeletePoi";
        } else if (pagina = "Gebruikers") {
            formData.append("DeleteUsersSoft", value);
            Delurl = "../Admin/UsersToDelete";
        }
        jsonListUp(formData, Delurl);

    }
    //soft verwijderen via button
    function deleteList() {

        var formData = new FormData();
        var Delurl = "";
        var lijst = $('[name=listId]');
        console.log(lijst);
        console.log("hu?");
        var array = [];
        lijst.each(function (iets) {
            if ($(lijst[iets]).is(":checked")) {
                console.log("Aantal");
                var value = parseInt($(lijst[iets]).attr("value"));

                if (pagina == "Activities") {
                    formData.append("ActiviteitenToDelete", value);
                    Delurl = "../Admin/DeleteActiviteit";
                } else if (pagina == "Boeken") {
                    console.log("hallo?");
                    formData.append("BoekenToDelete", value);
                    Delurl = "../Admin/DeleteBoeken";
                } else if (pagina == "Pois") {
                    console.log("hoi");
                    formData.append("PoisToDelete", value);
                    Delurl = "../Admin/DeletePoi";
                } else if (pagina = "Gebruikers") {
                    formData.append("DeleteUsersSoft", value);
                    Delurl = "../Admin/UsersToDelete";
                }
            }
        })
        jsonListUp(formData, Delurl);
    }
    //compleet verwijderen via button
    function hardDeleteList() {
        var formData = new FormData();
        var Delurl = "";
        var lijst = $('[name=listId]');
        console.log(lijst);
        console.log("hu?");
        var array = [];
        lijst.each(function (iets) {
            if ($(lijst[iets]).is(":checked")) {
                console.log("Aantal");
                var value = parseInt($(lijst[iets]).attr("value"));
                if (pagina == "Activities") {
                    formData.append("ActiviteitenToDelete", value);
                    Delurl = "../Admin/HardDeleteActiviteit";
                } else if (pagina == "Boeken") {
                    formData.append("BoekenToDelete", value);
                    Delurl = "../Admin/HardDeleteBoeken";
                } else if (pagina == "Pois") {
                    formData.append("PoisToDelete", value);
                    Delurl = "../Admin/HardDeletePoi";
                } else if (pagina = "Gebruikers") {
                    formData.append("UsersToDelete", value);
                    Delurl = "../Admin/DeleteUsersHard";
                }
            }
        })
        jsonListUp(formData, Delurl);
    };
    //veranderen rechten bij gebruikers
    function changeRights() {
        var formData = new FormData();
        var Delurl = "";
        var lijst = $('[name=listId]');
        console.log('lijst');
        console.log(lijst);
        lijst.each(function (iets) {
            console.log('hallo');
            if ($(lijst[iets]).is(":checked")) {
                var value = $(lijst[iets]).attr("naam");
                formData.append("UsersNames", value);
                console.log(value);
            }
            Delurl = "../Admin/ToggeRole";
        });
        Delurl = "../Admin/ToggeRole";
        jsonListUp(formData, Delurl);
    }
    //Verwerken van aangekregen data
    function switchTable() {
        var body = table.children('tbody');
        body.children('tr').remove();
        var string = "";
        for (i = 0; i < els.length; i++) {
            if (pagina == "Activities") {
                string = "<tr><td><input  name='listId' value='" + els[i]["Id"] + "' type='checkbox' /></td>" + "<td>" + els[i]["Naam"] + "</td><td>" + els[i]["Eigenaar"]["UserName"] + "</td>" + "<td>" + els[i]["Poi"]["Naam"] + "</td>"
                if (rol == true) {
                    var o;
                    if (els[i]["IsDeleted"]) {
                        string += "<td>ja</td>"
                    } else {
                        string += "<td>nee</td>"
                    }
                    string += "<td><div class='displayInlineButtons'><button class='oneDel' value=" + els[i]["Id"] + "><span class='glyphicon glyphicon-remove'></span></button></div></td></tr>";
                } else {
                    string += "<td><div class='displayInlineButtons'><button class='oneDel' value=" + els[i]["Id"] + "><span class='glyphicon glyphicon-remove'></span></button></div></td></tr>";
                }
                //IsDeleted
            } else if (pagina == "Boeken") {

                string = "<tr><td><input  name='listId' value='" + els[i]["Id"] + "' type='checkbox' /></td><td>" + els[i]["Naam"] + "</td><td>" + els[i]["Eigenaar"]["UserName"] + "</td>" + "<td>";
                for (j = 0 ; j < els[i]["Activiteiten"].length ; j++) {
                    if (j == els[i]["Activiteiten"].length - 1) {
                        string += "<a href='../Activiteiten/Details/" + els[i]["Activiteiten"][j]["Id"] + "'>" + els[i]["Activiteiten"][j]["Naam"] + "</a>"
                    } else {
                        string += "<a href='../Activiteiten/Details/" + els[i]["Activiteiten"][j]["Id"] + "'>" + els[i]["Activiteiten"][j]["Naam"] + "</a> ,"
                    }
                }
                if (rol == true) {
                    var o;
                    if (els[i]["IsDeleted"]) {
                        string += "<td>ja</td>"
                    } else {
                        string += "<td>nee</td>"
                    }
                    string += "<td><div class='displayInlineButtons'><button class='oneDel' value=" + els[i]["Id"] + "><span class='glyphicon glyphicon-remove'></span></button></div></td></tr>";
                } else {
                    string += "</td><td><div class='displayInlineButtons'><button class='oneDel' value=" + els[i]["Id"] + "><span class='glyphicon glyphicon-remove'></span></button></div></td></tr>"
                }
            } else if (pagina == "Pois") {
                string = "<tr><td><input  name='listId' value='" + els[i]["ID"] + "'type='checkbox' /> </td><td>" + els[i]["Naam"] + "</td><td>" + els[i]["Eigenaar"]["UserName"] + "</td><td>" + els[i]["Straat"] + " " + els[i]["Nummer"] + " " + els[i]["Postcode"] + " " + els[i]["Gemeente"] + "</td>";
                if (els[i]["Telefoon"] != null) {
                    string += "<td>"+ els[i]["Telefoon"] + "</td><td>";
                } else {
                    string += "<td></td><td>";
                }

                for (j = 0 ; j < els[i]["Tags"].length ; j++) {
                    string += '<span class="tag">' + els[i]["Tags"][j]["Tag"]["Naam"] + "</span>";
                }
                if (rol == true) {
                    var o;
                    if (els[i]["IsDeleted"]) {
                        string += "</td><td>ja</td>"
                    } else {
                        string += "</td><td>nee</td>"
                    }
                }
                string += "<td><div class='displayInlineButtons'><button class='oneDel' value=" + els[i]["ID"] + "><span class='glyphicon glyphicon-remove'></span></button></div></td></tr>";


            } else if (pagina == "Gebruikers") {
                string = "<tr><td><input naam='" + els[i]["User"]["UserName"] + "'  name='listId' value='" + els[i]["User"]["Id"] + "'type='checkbox' /> </td><td>" + els[i]["User"]["Voornaam"] + " " + els[i]["User"]["Naam"] + "</td><td>" + els[i]["User"]["UserName"] + "</td><td>";
                for (j = 0 ; j < els[i]["Activiteiten"].length ; j++) {
                    if (j == els[i]["Activiteiten"].length-1) {
                        string += "<a href='../Activiteiten/Details/" + els[i]["Activiteiten"][j]["Id"] + "'>" + els[i]["Activiteiten"][j]["Naam"] + "</a>";
                        console.log("ik gebeur zenne");
                    } else {
                        string += "<a href='../Activiteiten/Details/" + els[i]["Activiteiten"][j]["Id"] + "'>" + els[i]["Activiteiten"][j]["Naam"] + "</a> ,";
                    }
                    
                }
                if (rol == true) {
                    var o;
                    if (els[i]["User"]["Deleted"]) { o = "nee" } else { o = "ja" }
                    string += "</td><td>" + o + "</td><td>" + els[i]["Role"] + "</td><td><div class='displayInlineButtons'><button class='oneDel' value=" + els[i]["Id"] + "><span class='glyphicon glyphicon-remove'></span></button></div></td></tr>";

                } else {
                string += "</td><td><div class='displayInlineButtons'><button class='oneDel' value=" + els[i]["Id"] + "><span class='glyphicon glyphicon-remove'></span></button></div></td></tr>";
                }
            }
            body.append(string);

        }
        $(".oneDel").click(function (e) {
            DelOne(e);
        });
        resetVanaf();
    }
    //Het script voor de volgende of vorige 30
    function resetVanaf() {
        if (vanaf == 0) {
            $('.SelVor').attr('disabled', 'disabled');
            $('.SelVor').css('opacity', 0.5);
            $('.SelVor').off();
            $('.SelVol').off();
            $('.SelVol').click(function () {
                vanaf = vanaf + 30;
                var jsonString = "../Admin/" + pagina + "?vanaf=" + vanaf + "&desc=" + desc + "&filter=" + currentRow + "&search=" + search + "&mode=1";
                jsonItUp(jsonString);
            });
        } else {
            $('.SelVor').removeAttr('disabled');
            $('.SelVor').css('opacity', 1);
            $('.SelVor').off();
            $('.SelVor').click(function () {
                vanaf = vanaf - 30;
                var jsonString = "../Admin/" + pagina + "?vanaf=" + vanaf + "&desc=" + desc + "&filter=" + currentRow + "&search=" + search + "&mode=1";
                jsonItUp(jsonString);
            });
            $('.SelVol').off();
            $('.SelVol').click(function () {
                vanaf = vanaf + 30;
                var jsonString = "../Admin/" + pagina + "?vanaf=" + vanaf + "&desc=" + desc + "&filter=" + filter + "&search=" + search + "&mode=1";
                jsonItUp(jsonString);

            });
        }
        $('.voorbVanaf').html("" + vanaf + " - " + (vanaf + 30));
    }
    //oproepen json it up & zetten van vinkjes voor sortering
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
            filter = row;
            //kijken of asc of desc
            if ($(this).hasClass('glyphicon-menu-up')) {
                $(pressed).removeClass('glyphicon-menu-up');
                $(pressed).addClass('glyphicon-menu-down');
                desc = 0;
            } else {
                $(pressed).removeClass('glyphicon-menu-down');
                $(pressed).addClass('glyphicon-menu-up');
                desc = 1;
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


    //versturen van lijsten voor het veranderen van acties
    function jsonListUp(formData, url) {
        formData.append("vanaf", vanaf);
        formData.append("desc", desc);
        formData.append("filter", filter);
        $.ajax({
            type: "POST",
            url: url,
            data: formData,
            dataType: 'text',//change to your own, else read my note above on enabling the JsonValueProviderFactory in MVC
            contentType: false,
            processData: false,
            success: function (data) {
                console.log(data);
                //BTW, data is one of the worst names you can make for a variable
                //handleSuccessFunctionHERE(data);
                var jsonString = "../Admin/" + pagina + "?vanaf=" + vanaf + "&desc=" + desc + "&filter=" + currentRow + "&search=" + search + "&mode=1";
                jsonItUp(jsonString);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                //do your own thing
            }
        });
    }
    //ophalen van lijsten voor het laten zien van data
    function jsonItUp(jsonString) {
        //console.log(jsonString);
        $.getJSON(jsonString, function (data) {
            els = jQuery.parseJSON(data);
            console.log(els);
            switchTable();
        });
    }


});