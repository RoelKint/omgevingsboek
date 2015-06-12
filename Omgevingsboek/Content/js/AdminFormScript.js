console.log("hhey" + vanaf);
var table;
var TopRow;
var els;
if(filter != "") {
   var sortOp = $('[name="filter"]').val();
} else {
    var sortOp = "Naam";
}
var hoe = 0;
$().ready(function () {

    table = $('.AdminTable');
    TopRow = table.children('thead').children('tr').first();
    
    console.log(TopRow);
    console.log(TopRow.children('th').length);
    TopRow.children('th').each(function (i) {
        console.log(this);
        if (i == 0 || i == TopRow.children('th').length - 1) {
        } else {
            $(this).append('<span style="float:right" class="glyphicon glyphicon-menu-down"><span/>');
            
            $(this).children('.glyphicon').click(function (e) {
                getNewData(e);
            });
        }

        //console.log(table.children('tr').first());


    });



    function getNewData(e) {
        var pressed = e.target;
        var par = e.currentTarget.parentElement;
        var row = $(par.children[0]).html();
        //var arr = par.html().split('<');
        console.log(sortOp);
        console.log(row);

       

        //kijken of de tabel geselecteerd is
        if (sortOp == row) {
            
            //kijken of asc of desc
            if (hoe == 0) {
                hoe = 1;
                console.log(e.target);
                $(pressed).removeClass('glyphicon-menu-down');
                $(pressed).addClass('glyphicon-menu-up');
            } else if (hoe == 1) {
                hoe = 0;
                $(pressed).removeClass('glyphicon-menu-up');
                $(pressed).addClass('glyphicon-menu-down');
            }
        } else {

            sortOp = row;
            //kijken of asc of desc
            if ($(this).hasClass('glyphicon-menu-down')) {
                hoe = 0;
            } else {
                hoe = 1;
            }
        }
        console.log(row + "<<<---- row");
        if (row == "Naam") {
            row = 1;
        } else if (row == "Eigenaar") {
            row = 2;
        } else if (row == "Poi") {
            row = 3;
        }
        console.log(pagina);

        //DIT IS WAAR IK MIJN JSON GA HALEN. EN DIT GEEFT EEN ERROR TERUG
        $.getJSON("../Admin/" + pagina + "?vanaf=" + vanaf + "&desc=" + hoe + "&filter=" + row + "&mode=1", function (data) {

            //console.log(data);
            els = jQuery.parseJSON(data);
            switchTable();
            // console.log(arr);
        });
    }


    function switchTable() {
        var body = table.children('tbody');
        body.children('tr').remove();
        console.log(els);
        
        for (i = 0; i < els.length; i++) {
            console.log(els[i]);
            
            if (pagina == "Activities") {

            
                body.append("<tr><td><input form='formA' id='DylanToch' name='ActiviteitenToDelete' value='" + els[i]["Id"] + "' type='checkbox' /></td>" + "<td>" + els[i]["Naam"] + "</td><td>" + els[i]["Eigenaar"]["UserName"] + "</td>" + "<td>" + els[i]["Poi"]["Naam"] + "</td>" + "</tr>");
            } 
        }
    }
});