$().ready(function () {
    var dummyInfo = '<li><b>Gegevens:</b></li><li><input type="tel" value="' + document.getElementsByName("tel").item(0).value + '" name="phonenumber" /></li><li><input type="email" value="' + document.getElementsByName("ema").item(0).value + '" name="email" /></li>'
    var button = '<li><button id="BtnEditGegevens" type="button">Click Me!</button></li>';
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
    function ChangeToInput() {
        console.log("sexy Roel");
        $('.infoProf').empty();
        $('.infoProf').html(dummyInfo + button);
        $('#BtnEditGegevens')
        
        btn = $('#BtnEditGegevens');
        btn.click(false, function () { submitNewGegevens() });
    }
    function submitNewGegevens() {
        console.log(btn);
    }
    function uploadNew() {

    }
    function setDetails() {
        $('.infoProf').empty();
        $.post("../home/GetTagsByPoi?poiId=" + $('#poiId').val(), function (data) {
            console.log(data);
        }
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


