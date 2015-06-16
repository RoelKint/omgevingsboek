var listLoadedActivity;

//json2.forEach(function (activity) {
//    //console.log(poi.poi.Tags);
//    //joinedtags = activity.poi["Tags"].map(function (tag) {
//        //return tag.Tag.Naam;
//    });
//    //poi.poi["OrigTags"] = poi.poi["Tags"];
//    //poi.poi["Tags"] = joinedtags;
//});

//console.log(json);

$('#searchActivity').bind('input', function () {
    filter($(this).val());
    listLoadedActivity = true;
});

function filter(query) {
    var filtered = json2;

    query = query.trim();
    query = query.replace(/ +(?= )/g, '');

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
    while (document.getElementById("listActivity").firstChild) {
        document.getElementById("listActivity").removeChild(document.getElementById("listActivity").firstChild);
    }

    filtered.forEach(function (home) {
        var activitySource = $("#activity-template").html();
        var activityTemplate = Handlebars.compile(activitySource);

        var tagSource = $("#activity-template").html();
        var tagTemplate = Handlebars.compile(tagSource);

        var context = {
            title: home.act.Naam,
            //tags: home.poi.OrigTags.map(function (el) { return "<span>" + el.Tag.Naam + "</span>" }).join(" "),
            image: home.act.AfbeeldingNaam,
            //distance: distance,
            city: home.act.Poi.Gemeente,
            id: home.act.Id,
            //activities: home.Activiteiten.map(function (act) { return "<p class='activity'>" + "<a href='/Home/Activiteit/" + act.Id + "'>" + act.Naam + "</a></p>" }).join(""),
            //numActivities: home.Activiteiten.length,
        };
        var html = activityTemplate(context);
        $(html).appendTo("#listActivity");
    });

    $(function () {
        $('[data-toggle="popover"]').popover()
    });

    $(".poiTags span").click(function (tag) {
        document.getElementById("searchPoi").value = document.getElementById("searchPoi").value + " Tags:" + $(this)[0].innerText;
        filter($("#searchPoi").val());
        listLoadedActivity = true;
        $("#searchPoi").trigger("input");
    })

    listLoadedActivity = true;
}

filter("");