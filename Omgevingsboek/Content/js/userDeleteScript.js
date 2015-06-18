$('.delItem').click(function () {
    if (paginaa == "Boek") {
        $('.alertt').dialog({
            dialogClass: "dlg-no-title",
            resizable: false,
            height: 200,
            modal: true,
            buttons: {
                "verwijderen": function () {
                    location.href = '../home/DeleteBoek?Id=' + id
                    $(this).dialog("close");
                },
                Cancel: function () {
                    $(this).dialog("close");
                }
            },
            closeText: "hide"
        });
    } else if (paginaa == "Activiteit") {
        $('.alertt').dialog({
            dialogClass: "dlg-no-title",
            resizable: false,
            height: 200,
            modal: true,
            buttons: {
                "verwijderen": function () {
                    location.href = '../../home/DeleteActiviteit?Id=' + id + '&boekId=' + idB
                    $(this).dialog("close");
                },
                Cancel: function () {
                    $(this).dialog("close");
                }
            },
            closeText: "hide"
        });
    } else if (paginaa == "Route") {
        $('.alertt').dialog({
            dialogClass: "dlg-no-title",
            resizable: false,
            height: 200,
            modal: true,
            buttons: {
                "verwijderen": function () {
                    location.href = '../home/DeleteActiviteit?Id=' + id + '&boekId=' + idB
                    $(this).dialog("close");
                },
                Cancel: function () {
                    $(this).dialog("close");
                }
            },
            closeText: "hide"
        });
    }
    
});

//compleet verwijderen via glyphicons (via jsonlistup)


//../home/DeleteBoek?Id=@Model.Id