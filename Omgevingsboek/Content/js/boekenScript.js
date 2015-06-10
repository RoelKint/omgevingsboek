$().ready(function () {
    $("#toevoegenBoek").click(function () {
        $('.toevoegen').hide();
        $('.toevoegenClick').show();
        $(this).addClass("col-md-6 col-sm-12").removeClass("col-md-3 col-sm-6", 500);
    });


});