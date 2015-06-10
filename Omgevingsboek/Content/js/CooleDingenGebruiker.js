$().ready(function () {
    
    var bew;
    var bewInfo;
    var par;
    var parInfo;
    var btn;
    if ('.bewProf') { 
        par = $('#vlakEnIcoon').parent();
        bew = par.children().eq(1);
        bew.click(false, function () { uploadNew });
    }
    if ('.bewInfo') {
        parInfo = $('.infoProf');
        bewInfo = $('.bewInfo');
        bewInfo.css("cursor", "pointer")

        bewInfo.click(false, function () { ChangeToInput() });

    }
   
    function uploadNew() {

    }
   
    


    $("input:file").change(function () {
        var fileName = $(this).val();
        console.log(fileName);
        
        //$(".filename").html(fileName);
    });



    console.log(parInfo);
    console.log(bewInfo);
    console.log(document.getElementsByName("tel"));

});


