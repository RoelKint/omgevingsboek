$().ready(function () {
    var ageSlider;
    var timeSlider;


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
    });
    $(".toggleActivityForm").click(function (e) {
        e.preventDefault();
        emptyActivityFields();
        $("#activityForm").slideToggle(400, function () {
        });
    });
    $(".toggleActivityEdit").click(function (e) {
        e.preventDefault();
        fillActivtyFields(e)
        $("#activityForm").slideToggle(400, function () {
        });
    });
    function emptyActivityFields() {
        $('#name').val('');
        $('[name=DitactischeToelichting]').val('');
        $('[name=Uitleg]').val("");
        $('[name=Prijs]').val(0);
        console.log("hey");
    }


    $("#routeSubmit").click(function () {
        var waypointList = "";
        $("#waypointsList option").each(function () {
            var optionlistitem = $(this).attr("value");
            waypointList += optionlistitem + ",";
        })
        
        waypointList = waypointList.slice(0, -1);
        $("#activiteitenIds").val(waypointList);
        console.log($("#activiteitenIds").val());
        console.log(waypointList);

        console.log("nu zou submit event moeten firen");
        $("#addRouteForm").submit();

    });
    $(".toggleRouteForm").click(function (e) {
        e.preventDefault();
        $("#routeForm").slideToggle(400, function () {



            google.maps.event.trigger(map, 'resize');
            //$("#routeSubmit").prop("disabled", true);
            //    $("#routeSubmitContainer").popover({
            //    trigger:"hover",
            //    content: "Je moet eerste de route berekenen voor je ze kan opslaan",
            //    placement: "bottom"
            //})
        });
    })

    function fillActivtyFields(e) {
        console.log("ho");
        getvalues(1);
    }
    function getvalues(Id) {
        var jsonString = "../Home/" + 'GetActiviteit' + "?Id=" + Id;

        $.getJSON(jsonString, function (data) {
            els = jQuery.parseJSON(data);
            console.log(els);
            console.log(els['Naam']);
            $('#name').val(els['Naam']);
            
            if (els['Prijs'] != null) 
                $('[name=Prijs]').val(els['Prijs']);
            if (els['AfbeeldingNaam'] != null) {
                $('.fileUpload').children('span').html('Upload nieuwe </br> foto')
                $('.fileUpload').css('background-color', '#216075');
                $('[name=DitactischeToelichting]').val(els['DitactischeToelichting']);
                $('[name=Uitleg]').val(els['Uitleg']);
            }


        });
    };
    function initSliders() {
        setSliderInputValues(
            $("#minTimeBook"),
            $("#maxTimeBook"),
            $("#minTimeHiddenBook"),
            $("#maxTimeHiddenBook"),
            0,
            24
        );

        setSliderInputValues(
            $("#minAgeBoek"),
            $("#maxAgeBoek"),
            $("#minAgeHiddenBoek"),
            $("#maxAgeHiddenBoek"),
            0,
            18
        );

        ageSlider = setSliderValues(
            $("#slider-boek-age"),
            $("#minAgeBoek"),
            $("#maxAgeBoek"),
            $("#minAgeHiddenBoek"),
            $("#maxAgeHiddenBoek"),
            0,
            18
        );

        timeSlider = setSliderValues(
            $("#slider-boek-time"),
            $("#minTimeBook"),
            $("#maxTimeBook"),
            $("#minTimeHiddenBook"),
            $("#maxTimeHiddenBook"),
            0,
            24
        );
    }

    function setSliderValues(sliderEl, minShow, maxShow, minHidden, maxHidden, minVal, maxVal) {
        return sliderEl.slider({
            range: true,
            min: minVal,
            max: maxVal,
            values: [minVal, maxVal],
            slide: function (event, ui) {
                setSliderInputValues(minShow, maxShow, minHidden, maxHidden, ui.values[0], ui.values[1]);
                
            }
        });
    }

    function moveSliders(sliderEl, minVal, maxVal) {
        sliderEl.slider("values", [minVal, maxVal]);
    }

    function setSliderInputValues(minShow, maxShow, minHidden, maxHidden, minVal, maxVal) {
        minShow.text(minVal);
        maxShow.text(maxVal);
        minHidden.val(minVal);
        maxHidden.val(maxVal);
    }

    function setListItemListeners() {
        $("#list .row.poi").each(
         function (element) {
             var poi = this;


             $(this).children(".preview").click(
               function (element) {
                   tagSel = json.filter(function (el) { return el.poi.ID == $(poi).attr("data-id") })[0];
                   if ($('#activityForm').css("display") !== 'none') {

                       $("#poi").val(tagSel.poi.ID);
                       $("#poiName").val(tagSel.poi.Naam);
                       moveSliders($("#slider-boek-age"), tagSel.poi.MinLeeftijd, tagSel.poi.MaxLeeftijd);
                       setSliderInputValues(
                            $("#minAgeBoek"),
                            $("#maxAgeBoek"),
                            $("#minAgeHiddenBoek"),
                            $("#maxAgeHiddenBoek"),
                            tagSel.poi.MinLeeftijd,
                            tagSel.poi.MaxLeeftijd
                        );

                       $("#prijsLeerling").val(tagSel.poi.Prijs);
                   };
               });

         });

        $("#activityForm input:file").change(function () {
            var fileName = $(this).val();
            if(fileName.trim() != ""){
                $(this).parent().toggleClass("upload-ok");
            }
            //$(".filename").html(fileName);
        });
    }

    function setListItemListenersActivity() {
        //hier moeten de clicklisteners gezet worden
        //hier moeten de clicklisteners gezet worden
        console.log("foo");
        console.log("item listeners activity");
        $("#listActivity .row.activity").each(function (element) {
            var act = this;

            $(this).children('.preview').click(
                function (element) {
                    console.log(json2.filter(function (el) { return el.act.Id == $(act).attr("data-id") }));
                    tagSel = json2.filter(function (el) { return el.act.Id == $(act).attr("data-id") })[0];
                    console.log(tagSel.act.Poi.Latitude);
                    console.log(tagSel.act.Poi.Longitude);

                    console.log("dit is een test");
                    marker = new google.maps.Marker({
                        position: /*TODO: Use actual location here*/new google.maps.LatLng(tagSel.act.Poi.Latitude, tagSel.act.Poi.Longitude),
                        map: map
                    });
                    var attr = $("#waypointsList option:first-child").attr('disabled');
                    //check if list is empty or
                    if (typeof attr !== typeof undefined && attr !== false) {
                        // lijst leegmaken
                        console.log("lijst leegmaken");
                        $("#waypointsList").children().remove();
                    }
                    $("#waypointsList").append($('<option>', {
                        value: tagSel.act.Id,
                        text: tagSel.act.Naam
                    }));
                    map.setCenter(new google.maps.LatLng(tagSel.act.Poi.Latitude, tagSel.act.Poi.Longitude));
                    map.setZoom(9);
                    markerArray.push(marker);
                }
            );
        });
    }


    function deferListener(method) {
        if (typeof listLoaded !== 'undefined' && listLoaded == true) {
            method();
            listLoaded = false;
            $('#searchPoi').bind('input', function () {
                method();
            });
        } else
            setTimeout(function () { deferListener(method) }, 50);
    }

    function deferListener2(method) {
        if (typeof listLoadedActivity !== 'undefined' && listLoadedActivity == true) {
            method();
            listLoadedActivity = false;
            $("#searchActivity").bind('input', function () {
                method();
            });
        } else {
            setTimeout(function () {
                deferListener2(method)
            }, 50);
        }
    }

    deferListener(setListItemListeners);
    deferListener2(setListItemListenersActivity);
    initSliders();

});