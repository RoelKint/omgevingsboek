$().ready(function () {

    $('#ZoekenGebruikers').keyup(function () {

        delay(function () {
            $.ajax({
                type: "GET",
                url: "ZoekGebruiker?q=" + $('#ZoekenGebruikers').val()
            });



        }, 1000);
    });

    var delay = (function () {
        var timer = 0;
        return function (callback, ms) {
            clearTimeout(timer);
            timer = setTimeout(callback, ms);
        };
    })();
});