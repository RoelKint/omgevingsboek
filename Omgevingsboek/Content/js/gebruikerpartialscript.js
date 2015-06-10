function defer(method) {
    if (window.jQuery) {
        if (jQuery.ui)
            method();
    } else
        setTimeout(function () { defer(method) }, 50);
}

$().ready(function () {
    $('button[type=submit]').on('click', function () {
        if ($('#addPoi').valid()) {
            var $btn = $(this).button('loading')
        }
    })

    $(".sidebar input:file").change(function () {
        var fileName = $(this).val();
        console.log(fileName);
        $(".sidebar .fileUpload").toggleClass("upload-ok");
        //$(".filename").html(fileName);
    });

    Array.prototype.getUnique = function () {
        var u = {}, a = [];
        for (var i = 0, l = this.length; i < l; ++i) {
            if (u.hasOwnProperty(this[i])) {
                continue;
            }
            a.push(this[i]);
            u[this[i]] = 1;
        }
        return a;
    }

  

    $("#amount").val("" + $("#slider-range").slider("values", 0) +
            " - " + $("#slider-range").slider("values", 1));

    $("#minAge").text("5");
    $("#maxAge").text("10");
    $("#minAgeHidden").val("5");
    $("#maxAgeHidden").val("10");

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
                $('#straatHidden').val(addressComponents.streetName);
                $('#gemeenteHidden').val(addressComponents.city);
                $('#postcodeHidden').val(addressComponents.postalCode);
                $('#nummerHidden').val(addressComponents.streetNumber);
                $('#latitudeHidden').val((currentLocation.latitude));
                $('#latitudeHidden').val($('#latitudeHidden').val().replace(".", ","));
                $('#longitudeHidden').val(currentLocation.longitude);
                $('#longitudeHidden').val($('#longitudeHidden').val().replace(".", ","));

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
        $(".sidebar > form").toggleClass("hidden");
        $(".sidebar .poi").toggleClass("hidden");
    });

    $("#viewPoi").click(function () {
        $(".sidebar > form").toggleClass("hidden")
        $(".sidebar .poi").toggleClass("hidden");
    });

});