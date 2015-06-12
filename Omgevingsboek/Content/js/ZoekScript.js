$().ready(function () {

    $('#ZoekenGebruikers').keyup(function () {

        delay(function () {
            var value = $('#ZoekenGebruikers').val();
            $.ajax({
                type: "GET",
                url: "Gebruikers?search=" + value + "&mode=1&desc="+desc,
                success: function (data) {
                    $("#formA tbody tr").remove();
                    $.each($.parseJSON(data), function () {
                        var item = $(this)[0];
                        console.log(item);
                        var listelement = '<tr> <td><input type="checkbox" name="checkbox" value="' + item.User.Id + '"></td><td>' + item.User.Naam + ' ' + item.User.Voornaam + '</td><td>' + item.User.UserName + '</td><td>';
                        if (item.Activiteiten !== undefined) {
                            $.each(item.Activiteiten, function () {
                                var a = $(this)[0];
                                if (a == item.Activiteiten[item.Activiteiten.length - 1]) {
                                    listelement = listelement + '<a href="#">' + a.Naam + '</a>';
                                }
                                else {
                                    listelement = listelement + '<a href="#">' + a.Naam + '</a>,';
                                }
                            });
                        }
                        listelement = listelement + '</td><td><div class="displayInlineButtons"><button><span class="glyphicon glyphicon-remove"></span></button><button><span class="glyphicon glyphicon-pencil"></span></button></div></td></tr>';
                        $("#formA tbody").append(listelement);
                    });
                    
                }
            });
        }, 300);
    });

    var delay = (function () {
        var timer = 0;
        return function (callback, ms) {
            clearTimeout(timer);
            timer = setTimeout(callback, ms);
        };
    })();
});