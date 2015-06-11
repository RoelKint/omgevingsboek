
var pagina = $('[name="controllerNaam"]').val();
var vanaf = $('[name="vanaf"]').val();
var table;
var TopRow;
if($('[name="filter"]').val() != "") {
    sortOp = $('[name="filter"]').val();
} else {
    var sortOp = "Naam";
}
var hoe = 0;
$().ready(function () {

    table = $('.AdminTable');
    TopRow = table.children('tbody').children('tr').first();
    console.log(TopRow);
    console.log(TopRow.children('th').length);
    TopRow.children('th').each(function (i) {
        if (i == 0 || i == TopRow.children('th').length - 1) {
        } else {
            $(this).append('<span style="float:right" class="glyphicon glyphicon-menu-down"><span/>');
            $(this).children('.glyphicon').click(function (e) {
                var pressed = e.target;
                var par = e.currentTarget.parentElement;
                var row = $(par.children[0]).html();
                //var arr = par.html().split('<');



                //kijken of de tabel geselecteerd is
                if (sortOp == row) {
                    console.log("oi")

                    //kijken of asc of desc
                    if (hoe == 0) {
                        hoe = 1;
                        $(this).removeClass('glyphicon-menu-down');
                        $(this).addClass('glyphicon-menu-up');
                    } else if (hoe == 1) {
                        hoe = 0;
                        $(this).removeClass('glyphicon-menu-up');
                        $(this).addClass('glyphicon-menu-down');
                    }
                } else {
                    console.log("noi");
                    sortOp = row;
                    //kijken of asc of desc
                    if ($(this).hasClass('glyphicon-menu-down')) {
                        hoe = 0;
                    } else {
                        hoe = 1;
                    }
                }
                if (row == "Naam") {
                    row = 1;
                } else if (row == "Eigenaar") {
                    row = 2;
                } else if (row == "Poi") {
                    row = 3;
                } 
                console.log(pagina);

                //DIT IS WAAR IK MIJN JSON GA HALEN. EN DIT GEEFT EEN ERROR TERUG
                $.getJSON("../Admin/J" + pagina + "?vanaf=" + vanaf + "&desc=" + hoe + "&filter=" + row, function (data) {
                    
                    console.log(data);
                    var els = jQuery.parseJSON(data);
                    console.log(els);

                    // console.log(arr);


                });

            });
        }

        //console.log(table.children('tr').first());


    });
});