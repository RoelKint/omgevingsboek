$('.delItem').click(function () {
    console.log('de clickers');
})




//compleet verwijderen via glyphicons (via jsonlistup)
function DelOne(e) {
    var value;
    var formData = new FormData();
    if ($(e.target).children('span').length == 0) {
        value = $(e.target).parent().attr("value");
    } else {
        value = $(e.target).attr("value");
    }
    
        formData.append("ActiviteitenToDelete", value);
        Delurl = "../Home/DeleteActiviteit";
    
    jsonListUp(formData, Delurl);

}

//versturen van lijsten voor het veranderen van acties
function jsonListUp(formData, url) {
    $.ajax({
        type: "POST",
        url: url,
        data: formData,
        dataType: 'text',//change to your own, else read my note above on enabling the JsonValueProviderFactory in MVC
        contentType: false,
        processData: false,
        success: function (data) {
            console.log(data);
            var jsonString = "../Admin/" + pagina + "?vanaf=" + vanaf + "&desc=" + desc + "&filter=" + currentRow + "&search=" + search + "&mode=1";
            jsonItUp(jsonString);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            //do your own thing
        }
    });
}