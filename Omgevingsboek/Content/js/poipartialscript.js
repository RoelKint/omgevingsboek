function defer(method) {
    if (window.jQuery) {
        if (jQuery.ui)
            method();
    } else
        setTimeout(function () { defer(method) }, 50);
}

$().ready(function () {

    console.log("Jquery loaded");
    var collapsed = true;
    $("#slider-range").slider({
        range: true,
        min: 0,
        max: 18,
        values: [5, 10],
        slide: function (event, ui) {
            $("#amount").val("" + ui.values[0] + " - " + ui.values[1]);

            $("#minAge").text("" + ui.values[0]);
            $("#maxAge").text("" + ui.values[1]);
        }
    });

    $("#amount").val("" + $("#slider-range").slider("values", 0) +
            " - " + $("#slider-range").slider("values", 1));

    $("#minAge").text($("#slider-range").slider("values", 0));
    $("#maxAge").text($("#slider-range").slider("values", 1));

    $('#locationPicker').popover({
        html: true,
        content: function () {
            return $('#location_picker_wrapper').html();
        }
    });
    $('#locationPicker').on('shown.bs.popover', function () {
        // set what happens when user clicks on the button
        console.log("shown.bs.popover");
        //$('.popover #us2').locationpicker('autosize');
        $('.popover #us2').locationpicker({
            location: { latitude: 50.8028051, longitude: 3.279785 },
            radius: 0,
            inputBinding: {
                latitudeInput: $('.popover #us2-lat'),
                longitudeInput: $('.popover #us2-lon'),
                locationNameInput: $('.popover #us2-address')
            },
            enableAutocomplete: true,
            onchanged: function (currentLocation, radius, isMarkerDropped) {
                var addressComponents = $(this).locationpicker('map').location.addressComponents;
                $('#inputAdres').val(addressComponents.addressLine1 + ", " + addressComponents.postalCode + " " + addressComponents.city + ", " + addressComponents.country);
            }
        });
    });

    $("#expand").click(function () {
        $(".sidebar > form").toggleClass("form-exp");
        $(".sidebar .poi").toggleClass("form-exp");
        $(".expandable").toggleClass("expandable-exp");
        $(".expandable > #expand").toggleClass("expand-exp");
        $(".content").toggleClass("content-exp");
    });

    $("#addPoi").click(function () {
        $(".sidebar > form").removeAttr("hidden")
        $(".sidebar .poi").attr("hidden","hidden");
    });

    $("#viewPoi").click(function () {
        $(".sidebar > form").attr("hidden","hidden")
        $(".sidebar .poi").removeAttr("hidden");
    });

});