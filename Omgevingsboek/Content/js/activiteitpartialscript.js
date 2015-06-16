$().ready(function () {
    $('#poipartial').hide();



    $("#expandactivity").click(function () {
        $(".sidebar > form").toggleClass("form-exp");
        $(".sidebar .activity").toggleClass("form-exp");
        $(".expandable").toggleClass("expandable-exp");
        $(".expandable > #expand").toggleClass("expand-exp");
        $(".content").toggleClass("content-exp");
    });
});

