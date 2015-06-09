var dummy = '<input type="text" title="AddTag"placeholder="___" />';
var parent;
var input;
var ja;

$().ready(function () {
    
    $('.addTag').click(function () {
        parent = $(this).parent();
        console.log(parent);
        $(this).fadeOut(100, function () {
            parent.append('<input class="inputf" type="text" title="AddTag" size=2 />');
            input = $('.inputf');
            
            input.bind('click', function () {
                ja = 1;
            });

            input.bind('keydown', function() {
               
                console.log(this.size);
                console.log(this.value.length);
                this.size = this.value.length * 1+2;
            })
                
            
            


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
        
        
    });
});

function refreshTags() {
    var par = input.parent()
    par.html('lolletjes');
    console.log(input.parent());
}