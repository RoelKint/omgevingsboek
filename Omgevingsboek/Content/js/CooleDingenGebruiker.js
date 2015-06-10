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

  //function progressHandlingFunction(e) {
  //    if (e.lengthComputable) {
  //        $('progress').attr({ value: e.loaded, max: e.total });
  //    }
  //}
  //
  //function uploadNew() {
  //    var formData = new FormData($('form')[0]);
  //    $.ajax({
  //        url: 'home/UpdateAfbeelding',  //Server script to process data
  //        type: 'POST',
  //        xhr: function () {  // Custom XMLHttpRequest
  //            var myXhr = $.ajaxSettings.xhr();
  //            if (myXhr.upload) { // Check if upload property exists
  //                myXhr.upload.addEventListener('progress', progressHandlingFunction, false); // For handling the progress of the upload
  //            }
  //            return myXhr;
  //        },
  //        //Ajax events
  //        beforeSend: beforeSendHandler,
  //        success: completeHandler,
  //        error: errorHandler,
  //        // Form data
  //        data: formData,
  //        //Options to tell jQuery not to process data or worry about content-type.
  //        cache: false,
  //        contentType: false,
  //        processData: false
  //    });
  //            $('progress').attr({ value: e.loaded, max: e.total });
  //
  //    }
  //
  //
  //
  //
  //
  //$("input:file").change(function () {
  //    var file = this.files[0];
  //    var name = file.name;
  //    var size = file.size;
  //    var type = file.type;
  //
  //    console.log(file);
  //    console.log(name);
  //    console.log(size);
  //    //var fileName = $(this).val();
  //    //console.log(fileName);
  //    //$.post
  //    console.log("jup");
  //    uploadNew();
  //    //$(".filename").html(fileName);
  //});
  //


    console.log(parInfo);
    console.log(bewInfo);
    console.log(document.getElementsByName("tel"));

});


