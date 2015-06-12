$().ready(function () {

    $('#ZoekenGebruikers').keyup(function () {

        var value = $('#ZoekenGebruikers').val();
        console.log(value);
        delay(function () {
            $.ajax({
                type: "GET",
                
                url: "ZoekGebruiker?q=" + value,
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
        }, 500);
    });

    var delay = (function () {
        var timer = 0;
        return function (callback, ms) {
            clearTimeout(timer);
            timer = setTimeout(callback, ms);
        };
    })();
});