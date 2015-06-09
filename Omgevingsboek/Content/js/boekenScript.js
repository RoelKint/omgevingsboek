$().ready(function () {
    $("#toevoegenBoek").click(function () {
        $('.toevoegen').hide();
        $('.toevoegenClick').show();
        $(this)
               .animate({ top: "+=50px", left: "0" })
               .animate({ marginLeft:"" })
               .animate({ top: "-=50px" }, function () { $(this).off()});
    });
});