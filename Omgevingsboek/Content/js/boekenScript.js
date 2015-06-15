$().ready(function () {
    var fixed = '.static'; //class which will be locked


    $(".grid").sortable({
        cancel: fixed,
        tolerance: 'pointer',
        revert: 'invalid',
        helper : 'clone',
        start: function () {
            $(fixed, this).each(function () {
                var $this = $(this);
                $this.data('pos', $this.index());
            });
        },
        
        stop: function (event, ui) {
            var order = $('.grid').sortable('toArray').toString();
            $.ajax({
                type: "GET",
                url: "/Home/SaveBoekenSort?IsGedeeldLijst=false&volgorde=" + order
            });
        }
    });
    $(".grid2").sortable({
        cancel: fixed,
        tolerance: 'pointer',
        revert: 'invalid',
        helper: 'clone',
        start: function () {
            $(fixed, this).each(function () {
                var $this = $(this);
                $this.data('pos', $this.index());
            });
        },
        stop: function (event, ui) {
            var order = $('.grid2').sortable('toArray').toString();
            $.ajax({
                type: "GET",
                url: "/Home/SaveBoekenSort?IsGedeeldLijst=true&volgorde=" + order
            });
        }
    });



    /*
    $("#toevoegenBoek").click(function () {
        $('.toevoegen').hide();
        $('.toevoegenClick').show();
        $(this).addClass("col-md-6 col-sm-12").removeClass("col-md-3 col-sm-6", 500);
    });

    $('#btnAnnulerenAddBoek').click(function () {
        alert("annuleren geklikt");
        $('.toevoegen').show();
        $('.toevoegenClick').hide();
        $(this).removeClass("col-md-6 col-sm-12").removeClass("col-md-3 col-sm-6", 500);
    })
    */

});