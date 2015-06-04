var json = [
   {
       "poi": {
           "Eigenaar": null,
           "Tags": [
             {
                 "Activiteiten": null,
                 "Pois": [],
                 "ID": 1,
                 "Naam": "Museum"
             },
             {
                 "Activiteiten": null,
                 "Pois": [],
                 "ID": 2,
                 "Naam": "geschiedenis"
             },
             {
                 "Activiteiten": null,
                 "Pois": [],
                 "ID": 3,
                 "Naam": "Brugge"
             },
             {
                 "Activiteiten": null,
                 "Pois": [],
                 "ID": 4,
                 "Naam": "panorama"
             }
           ],
           "ID": 1,
           "Naam": "Gentpoort",
           "EigenaarId": "13d768d6-fb4c-46b9-bb0a-73c8e7014652",
           "Afbeelding": "18259403910",
           "GeoLocatie": null,
           "Email": "musea@brugge.be",
           "Telefoon": "+32 50 44 87 43",
           "Straat": "Gentpoortstraat",
           "Nummer": "48",
           "Gemeente": "Brugge",
           "Postcode": 8000,
           "MinLeeftijd": 9,
           "MaxLeeftijd": 12,
           "Prijs": 0.0
       },
       "Afbeelding": "https://farm9.staticflickr.com/8840/18259403910_6138f0a862.jpg",
       "Activiteiten": [
         {
             "Benodigdheden": null,
             "Boeken": null,
             "DeelLijst": null,
             "Eigenaar": null,
             "Fotoboeken": null,
             "Poi": null,
             "Routes": null,
             "Tags": null,
             "Videos": null,
             "Id": 1,
             "EigenaarId": "13d768d6-fb4c-46b9-bb0a-73c8e7014652",
             "Naam": "Museum in een van de stadspoorten",
             "PoiId": 1,
             "MinLeeftijd": 9,
             "MaxLeeftijd": 10,
             "MinDuur": 50,
             "MaxDuur": 50,
             "Prijs": 5.0,
             "AfbeeldingNaam": null,
             "DitactischeToelichting": "Project: Maak een miniatuurmolen.",
             "Uitleg": "Overbrengingen: tandwielen en riemoverbrenging Binnenin de molen zijn zowel tandwieloverbrengingen, als een riemoverbrenging te zien.U kunt de leerlingen laten experimenteren met verschillende overbrengingen. Hiervoor kunt u gebruik maken van verschillende LEGO® sets rond overbrengingen."
         },
         {
             "Benodigdheden": null,
             "Boeken": null,
             "DeelLijst": null,
             "Eigenaar": null,
             "Fotoboeken": null,
             "Poi": null,
             "Routes": null,
             "Tags": null,
             "Videos": null,
             "Id": 2,
             "EigenaarId": "13d768d6-fb4c-46b9-bb0a-73c8e7014652",
             "Naam": "Museum in een van de stadspoorten",
             "PoiId": 1,
             "MinLeeftijd": 11,
             "MaxLeeftijd": 12,
             "MinDuur": 120,
             "MaxDuur": 180,
             "Prijs": 0.0,
             "AfbeeldingNaam": null,
             "DitactischeToelichting": "Geschiedenis van Brugge (eigen streek)",
             "Uitleg": "Voor een groep/klas doet het museum zijn deuren open. Wel eerst eens bellen."
         }
       ]
   },
{
    "poi": {
        "Eigenaar": null,
        "Tags": [
          {
              "Activiteiten": null,
              "Pois": [],
              "ID": 5,
              "Naam": "Molen"
          }
        ],
        "ID": 2,
        "Naam": "Sint-Janshuismolen",
        "EigenaarId": "13d768d6-fb4c-46b9-bb0a-73c8e7014652",
        "Afbeelding": "18420708676",
        "GeoLocatie": null,
        "Email": "musea@brugge.be",
        "Telefoon": "+32 50 44 87 43",
        "Straat": "Kruisvest",
        "Nummer": "",
        "Gemeente": "Brugge",
        "Postcode": 8000,
        "MinLeeftijd": 11,
        "MaxLeeftijd": 11,
        "Prijs": 0.0
    },
    "Afbeelding": "https://farm9.staticflickr.com/8888/18420708676_0fda078f15.jpg",
    "Activiteiten": []
}
]

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