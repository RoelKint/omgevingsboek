$('.delItem').click(function () {
    if (pagina == "Boek") {
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
    } else if(pagina == "activiteit"){
        $('.alertt').dialog({
            dialogClass: "dlg-no-title",
            resizable: false,
            height: 200,
            modal: true,
            buttons: {
                "verwijderen": function () {
                    location.href = '../home/DeleteActiviteit?Id=' + id + '&IdB=' + idB
                    $(this).dialog("close");
                },
                Cancel: function () {
                    $(this).dialog("close");
                }
            },
            closeText: "hide"
        });
        delItem
    }
    
});

//compleet verwijderen via glyphicons (via jsonlistup)


//../home/DeleteBoek?Id=@Model.Id