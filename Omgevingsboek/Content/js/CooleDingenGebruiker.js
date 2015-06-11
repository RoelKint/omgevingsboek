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

    function progressHandlingFunction(e) {
        if (e.lengthComputable) {
            $('progress').attr({ value: e.loaded, max: e.total });
        }
    } 
    function uploadNew(file) {
        console.log("hey");

        var formData = new FormData();
        
        formData.append("Afbeelding", file);
        $.ajax({
            type: "POST",
            url: "../Home/UpdateAfbeelding",
            data: formData,
            dataType: 'json',//change to your own, else read my note above on enabling the JsonValueProviderFactory in MVC
            contentType: false,
            processData: false,
            success: function (data) {
                //BTW, data is one of the worst names you can make for a variable
                //handleSuccessFunctionHERE(data);
                console.log(':t');
            },
            error: function (jqXHR, textStatus, errorThrown) {
                //do your own thing
                console.log("fail");
            }
        });
       // $.ajax({
       //     url: '../Home/UpdateAfbeelding',  //Server script to process data
       //     type: 'POST',
       //   // xhr: function () {  // Custom XMLHttpRequest
       //   //     var myXhr = $.ajaxSettings.xhr();
       //   //     if (myXhr.upload) { // Check if upload property exists
       //   //         myXhr.upload.addEventListener('progress', progressHandlingFunction, false); // For handling the progress of the upload
       //   //     }
       //   //     return myXhr;
       //   // },
       //     //Ajax events
       //     success: completeHandler,
       //     // Form data
       //     data: formData,
       //     //Options to tell jQuery not to process data or worry about content-type.
       //     cache: false,
       //     contentType: false,
       //     processData: false
       // });
                //$('progress').attr({ value: e.loaded, max: e.total });

        } 
    function completeHandler() {

    }
    $("input:file").change(function () {
        var file = this.files[0];
        var name = file.name;
        var size = file.size;
        var type = file.type;

        console.log(file);
        console.log(name);
        console.log(size);
        //var fileName = $(this).val();
        //console.log(fileName);
        //$.post
        console.log("jup");
        uploadNew(file);
        //$(".filename").html(fileName);
    });

    $('.NewAct').click(function () {
        console.log("aoi");
        $('actlist')
    });
    $('.NewBoek').click(function () {
        console.log($('.booklist'));
        var lijst = $('.booklist');
        console.log(lijst);
        var i = 0;
        lijst.children('div').each(function () {
            i += 1;
            var j = (i % 4);
            console.log(j);
            //this.css("position", "relative");
            console.log($(this).children('a').offset());

            $(this).css("position", "relative");
            console.log($(this).children('a').position());
            $(this).css("visibility", "visible");
            $(this).children('a').css("position", "relative");
            $('.NewItems').remove();
            console.log($(this).css("position", "relative"));

        });
    });
    function sleep(milisec) {
        var e = new Date().getTime() + (milisec * 10);
        while (new Date().getTime() <= e) { }
    }
    console.log(parInfo);
    console.log(bewInfo);
    console.log(document.getElementsByName("tel"));

});


