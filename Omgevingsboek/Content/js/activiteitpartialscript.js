$().ready(function () {
    
    $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
        console.log(e.target); // newly activated tab
        if ($(e.target).attr('href') == "#routes") {
            console.log("op routes geklikt");
            google.maps.event.trigger(map, 'resize');
            $("#activiteitSidebar").show(0);
            $("#poipartial").hide(0);

        } else if ($(e.target).attr('href') == "#activiteiten") {
            console.log("op activiteiten geklikt");
            $("#activiteitSidebar").hide(0);
            $("#poipartial").show(0);
            
        }
    })

    function resizeMap() {
        setTimeout(resizeMapActual, 500);
    }
    function resizeMapActual() {
        google.maps.event.trigger(map, 'resize');
    }



    $("#expandactivity").click(function () {
        $(".sidebar > form").toggleClass("form-exp");
        $(".sidebar .activity").toggleClass("form-exp");
        $(".expandable").toggleClass("expandable-exp");
        $(".expandable > #expand").toggleClass("expand-exp");
        $(".content").toggleClass("content-exp");
    });
});

