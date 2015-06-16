
$().ready(function () {
    var ageSlider;
    var timeSlider;

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
        console.log("hey");
    }
    function fillActivtyFields(e) {
        console.log("ho");
        $('#name').val("lolletjes");
        $('.fileUpload').children('span').text('Upload nieuwe foto')
        $('.fileUpload').css('background-color', '#216075');
        $('[name=Prijs]').val(12);
        $('[name=DitactischeToelichting]').val("hallowkie wowkie");
        $('[name=Uitleg]').val("De uitleg van de eeuw.");

        initSliders();
        getvalues();
    }
    function getvalues() {
       var jsonString = "../Home/" + pagina + "?vanaf=" + vanaf + "&desc=" + desc + "&filter=" + currentRow + "&search=" + search + "&mode=1";

        $.getJSON(jsonString, function (data) {
            els = jQuery.parseJSON(data);
            console.log(els);
            switchTable();
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
                   } else if ($('#routes').hasClass("active") == true) {
                     
                       var attr = $("#waypointsList option:first-child").attr('disabled');
                       //check if list is empty or
                       if (typeof attr !== typeof undefined && attr !== false) {
                           // lijst leegmaken
                           $("#waypointsList").children().remove();
                       }
                       $("#waypointsList").append($('<option>', {
                           value: /*hier moet de lat & lng inkomen voor de berekening van de route*/ 0,
                           text: tagSel.poi.Naam
                       }));

                       marker = new google.maps.Marker({
                           position: /*TODO: Use actual location here*/new google.maps.LatLng(50, 3.01),
                           map: map
                       });
                       map.setCenter(new google.maps.LatLng(50, 3.01));
                       map.setZoom(9);
                       markerArray.push(marker);


                   }
               });

         });

        $("#activityForm input:file").change(function () {
            var fileName = $(this).val();
            $("#activityForm .fileUpload").toggleClass("upload-ok");
            //$(".filename").html(fileName);
        });
    }

    function setListItemListenersActivity() {
        //hier moeten de clicklisteners gezet worden
        console.log("foo");
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