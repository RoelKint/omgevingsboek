/// <reference path="StreetviewSequence.js" />
var map;
var markerArray = [];
var geocoder;
var directionsDisplay;
var directionsService = new google.maps.DirectionsService();
var autocomplete;
var maploaded;
var routeSequence;

Array.prototype.move = function (old_index, new_index) {
    while (old_index < 0) {
        old_index += this.length;
    }
    while (new_index < 0) {
        new_index += this.length;
    }
    if (new_index >= this.length) {
        var k = new_index - this.length;
        while ((k--) + 1) {
            this.push(undefined);
        }
    }
    this.splice(new_index, 0, this.splice(old_index, 1)[0]);
    return this; // for testing purposes
};

function initialize() {
    directionsDisplay = new google.maps.DirectionsRenderer();
    geocoder = new google.maps.Geocoder();
    var mapOptions = {
        center: { lat: 50, lng: 3 },
        zoom: 14,
        panControl: false,
        zoomControl: true,
        mapTypeControl: true,
        scaleControl: true,
        streetViewControl: true,
        overviewMapControl: true
    };
    var location = new google.maps.LatLng(50, 3);
    var panoramaOptions = {
        position: location,
        pov: {
            heading: 0,
            pitch: 0
        }
    }
    var marker = new google.maps.Marker({
        map: map,
        anchorPoint: new google.maps.Point(0, 0)
    });
    map = new google.maps.Map(document.getElementById('map-canvas'),
        mapOptions);


    google.maps.event.addListenerOnce(map, 'idle', function () {
        //console.log("map fully loaded");
        maploaded = true;
    });

    
    //var input = document.getElementById('pac-input');
    //map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);
    //autocomplete = new google.maps.places.Autocomplete(input);
    //autocomplete.bindTo('bounds', map);

    //google.maps.event.addListener(autocomplete, 'place_changed', function () {
    //    marker.setVisible(false);
    //    var place = autocomplete.getPlace();
    //    if (!place.geometry) {
    //        window.alert("Geen resultaten gevonden");
    //        return;
    //    }
    //    // If the place has a geometry, then present it on a map.
    //    map.setCenter(place.geometry.location);
    //    map.setZoom(15);
    //    $('#pac-input').val('');           // Why 17? Because it looks good.
    //    searchPlace();
    //});

    //controleren als er geolocatie op de browser zit.
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            var pos = new google.maps.LatLng(position.coords.latitude,
                                             position.coords.longitude);
            map.setCenter(pos);
        }, function () {
            handleNoGeolocation(true);
        });
    } else {
        // Browser doesn't support Geolocation
        handleNoGeolocation(false);
    }
}
function searchPlace() {
    var locatieinput = autocomplete.getPlace();
    if (locatieinput) {
        geocoder.geocode({ 'latLng': locatieinput.geometry.location }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                if (results[1]) {
                    map.setZoom(11);
                    marker = new google.maps.Marker({
                        position: locatieinput.geometry.location,
                        map: map
                    });
                    markerArray.push(marker);
                    var list = document.getElementById("waypointsList");
                    var listelement = document.createElement("option");
                    listelement.text = results[1].formatted_address;
                    listelement.value = locatieinput;
                    list.add(listelement);

                    setUpPanorama(locatieinput.geometry.location);
                    calculateRoute();
                } else {
                    alert('No results found');
                }
            } else {
                alert('Geocoder failed due to: ' + status);
            }
        });

        //geocoder.geocode({ 'address': locatieinput }, function (results, status) {
        //    if (status == google.maps.GeocoderStatus.OK) {
        //        map.setCenter(results[0].geometry.location);
        //        var marker = new google.maps.Marker({
        //            map: map,
        //            position: results[0].geometry.location
        //        });

        //        setUpPanorama(results[0].geometry.location);

        //        markerArray.push(marker);
        //        var list = document.getElementById("waypointsList");
        //        var listelement = document.createElement("option");
        //        listelement.text = locatieinput;
        //        listelement.value = locatieinput;
        //        list.add(listelement);
        //        //inputbox leegmaken
        //        document.getElementById("locatieStringInput").value = "";
        //    } else {
        //        alert('Geocode was not succesful for the following reason: ' + status);
        //    }
        //})
    }
}

function setUpPanorama(location) {
    var panoramaOptions = {
        position: location,
        pov: {
            heading: 0,
            pitch: 0
        }
    }
    $('#pano').children().remove();
    var panorama = new google.maps.StreetViewPanorama(document.getElementById('pano'), panoramaOptions);
}

function calculateRoute() {
    directionsDisplay.setMap(map);
    console.log("calculateRoute called");
    if (markerArray.length >= 2) {
        var request;


        var travelMode = selectTravelMode();
        var unitSystem = google.maps.UnitSystem.METRIC;
        if (markerArray.length == 2) {
            //route zonder waypoints
            var origin = markerArray[0].getPosition();
            //setUpPanorama(origin);
            var destination = markerArray[1].getPosition();
            var optimizeWaypoints = true;

            //add data to request object
            request = {
                origin: origin,
                destination: destination,
                travelMode: travelMode,
                unitSystem: unitSystem
            };



        } else {
            markerArray[0].setMap(null);
            markerArray[markerArray.length - 1].setMap(null);
            var origin = markerArray[0].getPosition();
            setUpPanorama(origin);
            var destination = markerArray[markerArray.length - 1].getPosition();
            //nieuwe array maken om waypoints in op te slaan
            var waypoints = [];
            for (var i = 1; i < markerArray.length - 1; i++) {
                //items uit markerArray halen en toevoegen aan waypoints array
                waypoints.push({ location: markerArray[i].getPosition(), stopover: true })
            }
            //controleren of route moet geoptimaliseerd worden
            var optimizeWaypoints = document.getElementById("optimalRoute").checked;
            //alert(optimizeWaypoints);

            //add data to request object
            request = {
                origin: origin,
                destination: destination,
                travelMode: travelMode,
                unitSystem: unitSystem,
                waypoints: waypoints,
                optimizeWaypoints: optimizeWaypoints
            };
        }

        directionsService.route(request, function (result, status) {
            if (status == google.maps.DirectionsStatus.OK) {
                $("#routeSubmit").prop("disabled", false).popover('hide');
                directionsDisplay.setDirections(result);
                directionsDisplay.setMap(map);
                directionsDisplay.setPanel(document.getElementById('directions-panel'));
                //hier de markers verwijderen van de kaart, omdat die gerendered worden door de directionsDisplay
                removeAllMarkersFromMap();
                var duration = 30;
                var totalFrames = duration *24;
                var canvasWidth = $("#route").width();
                var canvasHeight = 300;
                routeSequence = StreetviewSequence('#route', {
                    route: result,
                    key: "AIzaSyCjq-aM_jzPvZ52dZHXcljkggQraeltQrM",
                    duration: duration*1000,
                    loop: true,
                    width: canvasWidth,
                    height: canvasHeight,
                    totalFrames: totalFrames
                });

                var $routeProgressContainer = $("#route-progress-container");
                var $routeProgressBar = $routeProgressContainer.find('.progress-bar');
                routeSequence.progress(function (p) {
                    $routeProgressBar.css({ width: (p * 100) + '%' });
                });
                routeSequence.done(function (player) {
                    $routeProgressContainer.hide();
                    player.play();
                });
            } else {
                console.log(status);
            }
        });
    } else {
        //minder dan 2 punten op route -> geen routeberekening
        //alert("Kan geen route maken indien er minder dan 2 punten zijn");
        directionsDisplay.setMap(null);
    }
}


//Deze functie is voor de button op de form, om alle markers te verwijderen.
function removeAllMarkers() {
    removeAllMarkersFromMap();
    removeAllMarkersFromArray();
}
function removeAllMarkersFromMap() {
    for (var i = 0; i < markerArray.length; i++) {
        markerArray[i].setMap(null);
    }
    //markerArray = [];
}

function removeAllMarkersFromArray() {
    markerArray = [];
    $('option').remove();

}
function removeDirections() {
    directionsDisplay.setMap(null);
}

function removeWaypointFromList() {
    var list = document.getElementById("waypointsList");
    //var Waypoint = list.options[list.selectedIndex].value;
    if (list.selectedIndex != -1) {
        removeMarker(list.selectedIndex);
        list.remove(list.selectedIndex);
    }
    //calculateRoute();
}

function removeMarker(index) {
    markerArray[index].setMap(null);
    markerArray.splice(index, 1);
}

$(document).ready(function () {
    $('#moveWaypointUp,#moveWaypointDown').click(function () {
        var $op = $('#waypointsList option:selected'),
            $this = $(this);
        if ($op.length) {
            if ($this.attr("id") == "moveWaypointUp") {
                var oldindex = $("#waypointsList")[0].selectedIndex;
                markerArray.move(oldindex, oldindex - 1);
                $op.first().prev().before($op);
                //calculateRoute();
            } else {
                var oldindex = $("#waypointsList")[0].selectedIndex;
                markerArray.move(oldindex, oldindex + 1);
                $op.last().next().after($op);
                //calculateRoute();
            }
        }
    });

    $('#travel0,#travel1,#travel2,#travel3').change(function () { calculateRoute(); });
});

function handleNoGeolocation(errorFlag) {
    if (errorFlag) {
        var content = 'Error: The Geolocation service failed.';
    } else {
        var content = 'Error: Your browser doesn\'t support geolocation.';
    }

    var options = {
        map: map,
        position: new google.maps.LatLng(51, 3),
        content: content
    };

    var infowindow = new google.maps.InfoWindow(options);
    map.setCenter(options.position);
}

function selectTravelMode() {
    var selectedVal = "";
    var selected = $("input[name=travelmode]:checked");
    if (selected.length > 0) {
        selectedVal = selected.val();
    }
    switch (selectedVal) {
        case "DRIVING":
            return google.maps.TravelMode.DRIVING;
        case "BICYCLING":
            return google.maps.TravelMode.BICYCLING;
        case "TRANSIT":
            return google.maps.TravelMode.TRANSIT;
        case "WALKING":
            return google.maps.TravelMode.WALKING;
        default:
            return google.maps.TravelMode.WALKING;
    }
}

google.maps.event.addDomListener(window, 'load', initialize);