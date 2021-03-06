﻿var listLoaded;

json.forEach(function (poi) {
    //console.log(poi.poi.Tags);
    joinedtags = poi.poi["Tags"].map(function (tag) {
        return tag.Tag.Naam;
    });
    poi.poi["OrigTags"] = poi.poi["Tags"];
    poi.poi["Tags"] = joinedtags;
});

//console.log(json);

$('#searchPoi').bind('input', function () {
    filter($(this).val());
    listLoaded = true;
});

function distanceFrom(points) {
    var lat1 = points.lat1;
    var radianLat1 = lat1 * (Math.PI / 180);
    var lng1 = points.lng1;
    var radianLng1 = lng1 * (Math.PI / 180);
    var lat2 = points.lat2;
    var radianLat2 = lat2 * (Math.PI / 180);
    var lng2 = points.lng2;
    var radianLng2 = lng2 * (Math.PI / 180);
    var earth_radius = 6371;
    var diffLat = (radianLat1 - radianLat2);
    var diffLng = (radianLng1 - radianLng2);
    var sinLat = Math.sin(diffLat / 2);
    var sinLng = Math.sin(diffLng / 2);
    var a = Math.pow(sinLat, 2.0) + Math.cos(radianLat1) * Math.cos(radianLat2) * Math.pow(sinLng, 2.0);
    var distance = earth_radius * 2 * Math.asin(Math.min(1, Math.sqrt(a)));
    return distance.toFixed(3);
}

function filter(query) {
    var filtered = json;

    query = query.trim();
    query = query.replace(/ +(?= )/g,'');

    var matcher = /(\S+:\S+)/gi;

    var options = {
        caseSensitive: true,
        includeScore: false,
        shouldSort: true,
        threshold: 0.5,
        location: 0,
        distance: 100,
        maxPatternLength: 32,
        keys: [["poi"]["Naam"]]
    };

    filters = [];
    match = matcher.exec(query);
    while (match != null) {
        if (match != null) { filters.push(match[1]); }
        match = matcher.exec(query);
    }

    //Vind property matchers, e.g. tag:zwemmen
    filters.forEach(function (filter) {
        prop = filter.split(":")[0].charAt(0).toUpperCase() + filter.split(":")[0].slice(1);
        val = filter.split(":")[1];

        //console.log(val);
        //console.log(prop);
        console.log("\n");
        console.log(filtered.length);
        filtered = filtered.filter(function (el) {
            var matched = false;
            console.log("\n");
                if (el.poi[prop] == null) { matched = false; }
                if (typeof el.poi[prop] == "object") {
                    return el.poi[prop].some(function (propEl) {
                        var fuse = new Fuse([propEl], options);
                        var res = fuse.search(val)
                        if (res[0] != null) {
                            return true;
                        } else {
                            return false;
                        }
                    });
                } else {
                    var fuse = new Fuse([el.poi[prop]], options);
                    var res = fuse.search(val);
                    if (res[0] != null) {
                        matched = true;
                    }
                    matched = false;
                }
            return matched;
        });

    })
    console.log(filtered);
    //Zoek op texts die geen property matchers zijn
    if (query.replace(matcher, "").replace(" ", "").trim() != "") {
        filtered = filtered.filter(function (el) {
            var fuse = new Fuse([el.poi.Naam], options);
            var res = fuse.search(query.replace(matcher, "").replace(" ", "").trim());
            if (res[0] != null) {
                return true;
            }
            return false;
        });
    }
    console.log(filtered);
    //Verwijder huidige nodes en vervang door nieuwe items
    while (document.getElementById("list").firstChild) {
        document.getElementById("list").removeChild(document.getElementById("list").firstChild);
    }

    filtered.forEach(function (home) {
        var poiSource = $("#poi-template").html();
        var poiTemplate = Handlebars.compile(poiSource);

        var tagSource = $("#poi-template").html();
        var tagTemplate = Handlebars.compile(tagSource);


        var distance = parseInt(distanceFrom({
            // School locatie
            'lat1': 51.20944,
            'lng1': 3.22528,
            // Poi
            'lat2': 50 + Math.random(),
            'lng2': 3 + Math.random()
        }));

        var context = {
            title: home.poi.Naam,
            tags: home.poi.OrigTags.map(function(el){return "<span>" + el.Tag.Naam + "</span>"}).join(" "),
            image: home.poi.Afbeelding,
            distance: distance,
            city: home.poi.Gemeente,
            id: home.poi.ID,
            activities: home.Activiteiten.map(function(act){ return "<p class='activity'>" + "<a href='/Activiteiten/Details/" + act.Id + "'>" + act.Naam + "</a></p>"}).join(""),
            numActivities: home.Activiteiten.length,
        };
        var html = poiTemplate(context);
        $(html).appendTo("#list");
    });

    $(function () {
        $('.tag[data-toggle="popover"]').popover()
    });

    $(".poiTags span").click(function (tag) {
        document.getElementById("searchPoi").value = document.getElementById("searchPoi").value + " Tags:" + $(this)[0].textContent;
        filter($("#searchPoi").val());
        listLoaded = true;
        $("#searchPoi").trigger("input");
    })

    listLoaded = true;
}

filter("");