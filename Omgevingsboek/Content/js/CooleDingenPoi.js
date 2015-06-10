var dummy = '<input class="inputf" type="text" title="AddTag" size=2 />';
var plusje = '<span class="addTag glyphicons glyphicons-plus">&#x002b</span>';
var parent;
var input;
var ja;
var tags;


$().ready(function () {
    $(document).keyup(function(e) {

        if (e.keyCode == 27 || e.keyCode == 35) { refreshTags(); }   // escape key maps to keycode `27` en de andere is break (die knop naast home)
    });
    $('.addTag').click(false, function () { allesRondInput() });
                
            $('.row').bind('click', function () {
                if(ja == 0) {
                    
                } else {
                   if (input) {
                       if (input.val().length > 0) {
                           $.post("../home/AddTagToPoi?poiId=" + $('#poiId').val() + "&tag=" + input.val(), function (data) {
                               //nikske ;)
                               refreshTags();
                           });
                       }
                    }
                    
                }
            });
        });
        
        
    
function allesRondInput() {
    parent = $('.addTag').parent();
    console.log(parent);
    $('.addTag').fadeOut(100, function () {
        parent.append(dummy);
        ja = 1;
        input = $('.inputf');
        input.focus();
        input.bind('click', function () {
            ja = 1;
        });
        input.bind('keydown', function () {
            this.size = this.value.length * 1 + 2;
        });
    });
}


        
function refreshTags() {
    var tagsTekst = ''
    $.post("../home/GetTagsByPoi?poiId=" + $('#poiId').val(), function (data) {
        console.log(data);
        tags = jQuery.parseJSON(data);
        console.log(tags); 
        for (i = 0; i < tags.length; i++) {
            tagsTekst = tagsTekst + '<span>' + tags[i] + '</span>'
        }

        var par = input.parent();


        console.log(tagsTekst + "<<<-----");
        par.html(tagsTekst + plusje);
        ja = 0;
        $('.addTag').click(false, function () { allesRondInput() });
    });
    
    }