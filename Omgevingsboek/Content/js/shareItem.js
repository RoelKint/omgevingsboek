//#share_item_wrapper
$(function () {
    $(".shareItem").popover({
        html: true,
        content: function () {
            return $('#share_item_wrapper').html();
        }
    });
    $('.shareItem').on('shown.bs.popover', function () {
        console.log("shown.bs.popover");
        $.get("/home/sharelist?Id=1&Type=boek", function (data) {
            var people = $.parseJSON(data);

            $("#share_item_wrapper .people")[0].innerHTML = "";
            var container = $("#share_item_wrapper .people")[0];
            people.forEach(function (person) {
                var s = '<li>' + person.Naam + '</li>';
                var el = document.createElement('div');
                el.innerHTML = s;
                container.appendChild(el)
            })
        });
    });
})