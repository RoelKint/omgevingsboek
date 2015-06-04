json.forEach(function (poi) {
    //console.log(poi.poi.Tags);
    joinedtags = poi.poi["Tags"].map(function (tag) {
        return tag.Naam;
    }).join(" ");
    poi.poi["Tags"] = joinedtags;
});

$('#searchPoi').bind('input', function () {
    filter($(this).val());
});

function filter(query) {
    var filtered = json;

    var matcher = /(\S+:\S+)/gi;

    var options = {
        caseSensitive: true,
        includeScore: false,
        shouldSort: false,
        threshold: 0.6,
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
        prop = filter.split(":")[0];
        val = filter.split(":")[1];

        filtered = filtered.filter(function (el) {

            if (el.poi[prop] == null) { return false; }
            console.log(el.poi.Tags);
            var fuse = new Fuse([el.poi[prop]], options);
            var res = fuse.search(val);
            if (res[0] != null) {
                return true;
            }
            return false;
        });
    })

    //Zoek op texts die geen property matchers zijn
    if (query.replace(matcher, "").replace(" ", "") != "") {
        filtered = filtered.filter(function (el) {
            var fuse = new Fuse([el.poi.Naam], options);
            var res = fuse.search(query.replace(matcher, ""));
            console.log(res);
            if (res[0] != null) {
                return true;
            }
            return false;
        });
    }

    //Verwijder huidige nodes en vervang door nieuwe items
    while (document.getElementById("list").firstChild) {
        document.getElementById("list").removeChild(document.getElementById("list").firstChild);
    }

    filtered.forEach(function (home) {
        var poiSource = $("#poi-template").html();
        var poiTemplate = Handlebars.compile(poiSource);

        var tagSource = $("#poi-template").html();
        var tagTemplate = Handlebars.compile(tagSource);

        var context = {
            title: home.poi.Naam,
            tags: home.poi.Tags.split(" ").map(function (el) { return '<span>' + el + '</span>' }).join(" "),
            image: home.Afbeelding,
            distance: "?",
            city: home.poi.Gemeente,
            numActivities: home.Activiteiten.length,
        };
        var html = poiTemplate(context);
        $(html).appendTo("#list");
    });
}

filter("");