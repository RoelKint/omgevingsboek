$().ready(function () {

    var bew;
    var par;
    if ('.bewProf') { 
    par = $('#vlakEnIcoon').parent();
    bew = par.children().eq(1);
    bew.click(false, function () { uploadNew });
    }
    function uploadNew() {

    }



    $("input:file").change(function () {
        var fileName = $(this).val();
        console.log(fileName);
        $(".fileUpload").toggleClass("upload-ok");
        //$(".filename").html(fileName);
    });



    console.log(par);
    console.log(bew);

});


