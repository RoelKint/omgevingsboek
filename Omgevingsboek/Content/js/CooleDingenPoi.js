var dummy = '<input class="inputf" type="text" title="AddTag" size=2 />';
var plusje = '<span class="addTag glyphicons glyphicons-plus">&#x002b</span>';
var parent;
var input;
var ja;

$().ready(function () {
    
    $('.addTag').click(false, function () { allesRondInput() });
                
            $('.row').bind('click', function () {
                if(ja == 1) {
                    ja = 0;
                } else {
                   // if (input) {
                   //     if (input.val().length > 0) {
                   //         $.post("../home/test.html", function (data) {
                   //             input.val();
                   //         });
                   //     }
                    // }
                    refreshTags();
                }
            });
        });
        
        
    
function allesRondInput() {
    parent = $('.addTag').parent();
    console.log(parent);
    $('.addTag').fadeOut(100, function () {
        parent.append(dummy);
        input = $('.inputf');

        input.bind('click', function () {
            ja = 1;
        });

        input.bind('keydown', function () {
            this.size = this.value.length * 1 + 2;
        });
    });
}
        
function refreshTags() {
    var par = input.parent()
    par.html('lolletjes' + plusje);
    $('.addTag').click(false, function () { allesRondInput() });
    
}