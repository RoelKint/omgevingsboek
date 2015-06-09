$().ready(function () {
    $('#profPic').hover(function () {
        $(this).fadeTo("slow", 0.5, function () {
            // Animation complete.
        });
    });
});