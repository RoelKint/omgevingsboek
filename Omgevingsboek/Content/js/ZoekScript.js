$().ready(function () {


    $('#Zoeken').keyup(function () {

        delay(function () {
            var value = $('#Zoeken').val();
            search = $('#Zoeken').val();
            var request = pagina + "?search=" + value + "&mode=1&desc=" + desc;
            if (typeof row !== "undefined") { request = request + "&filter=" + row }

            $.ajax({
                type: "GET",
                url: request,
                success: function (data) {
                    HandelAf(data);
                    
                }
            });
        }, 300);
    });

    function HandelAf(data){
        $(".AdminTable tbody tr").remove();
        $.each($.parseJSON(data), function () {
            var item = $(this)[0];

            if (pagina == "Gebruikers") {
                var listelement = '<tr> <td><input type="checkbox" name="checkbox" value="' + item.User.Id + '"></td><td>' + item.User.Naam + ' ' + item.User.Voornaam + '</td><td>' + item.User.UserName + '</td><td>';
                if (item.Activiteiten !== undefined) {
                    $.each(item.Activiteiten, function () {
                        var a = $(this)[0];
                        if (a == item.Activiteiten[item.Activiteiten.length - 1]) {
                            listelement = listelement + '<a href="#">' + a.Naam + '</a>';
                        }
                        else {
                            listelement = listelement + '<a href="#">' + a.Naam + '</a>, ';
                        }
                    });
                }
                listelement = listelement + '</td><td><div class="displayInlineButtons"><button><span class="glyphicon glyphicon-remove"></span></button><button><span class="glyphicon glyphicon-pencil"></span></button></div></td></tr>';
                $(".AdminTable tbody").append(listelement);
            } else if (pagina == "Boeken") {
                var listelement = '<tr> <td><input from="formA" type="checkbox" name="BoekenToDelete" value="' + item.Id + '"></td><td>' + item.Naam + '</td><td>' + item.Eigenaar.UserName + '</td><td>';
                if (item.Activiteiten !== undefined) {
                    $.each(item.Activiteiten, function () {
                        var a = $(this)[0];
                        if (a == item.Activiteiten[item.Activiteiten.length - 1]) {
                            listelement = listelement + '<a href="#">' + a.Naam + '</a>';
                        }
                        else {
                            listelement = listelement + '<a href="#">' + a.Naam + '</a>, ';
                        }
                    });
                }
                listelement = listelement + '</td><td><div class="displayInlineButtons"><button><span class="glyphicon glyphicon-remove"></span></button></div></td></tr>';
                $(".AdminTable tbody").append(listelement);
            } else if (pagina == "Activities") {
                var listelement = '<tr> <td><input from="formA" id="DylanToch" type="checkbox" name="ActiviteitenToDelete" value="' + item.Id + '"></td><td>' + item.Naam + '</td><td>' + item.Eigenaar.UserName + '</td><td>' + item.Poi.Naam;
                
                listelement = listelement + '</td><td><div class="displayInlineButtons"><button><span class="glyphicon glyphicon-remove"></span></button></div></td></tr>';
                $(".AdminTable tbody").append(listelement);
            } else if (pagina == "Pois") {
                var listelement = '<tr> <td><input from="formA" type="checkbox" name="PoisToDelete" value="' + item.Id + '"></td><td>' + item.Naam + '</td><td>' + item.Eigenaar.UserName + '</td><td>' + item.Straat + " " + item.Nummer + " " + item.Postcode + " " + item.Gemeente + '</td><td>' + item.Telefoon + '</td><td>';
                if (item.Tags !== undefined) {
                    $.each(item.Tags, function () {
                        var a = $(this)[0];
                        
                            listelement = listelement + '<span class="tag">' + a.Tag.Naam + '</span>';
                        
                    });
                }
                listelement = listelement + '</td><td><div class="displayInlineButtons"><button><span class="glyphicon glyphicon-remove"></span></button></div></td></tr>';
                $(".AdminTable tbody").append(listelement);
            }
        });
    }


    var delay = (function () {
        var timer = 0;
        return function (callback, ms) {
            clearTimeout(timer);
            timer = setTimeout(callback, ms);
        };
    })();
});