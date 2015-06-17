//#share_item_wrapper
$(function () {
    $.get("/home/sharelist?Id=1&Type=boek", function (data) {
        console.log("Sharelist loaded succesfully");
        var people = $.parseJSON(data);
        $(".shareItem").popover({
            html: true,
            content: function () {
                return $('#share_item_wrapper').html();
            }
        });

        $("#share_item_wrapper .people")[0].innerHTML = "";
        var container = $("#share_item_wrapper .people")[0];
        people.forEach(function (person) {
            console.log(person.Naam);
            //var s = '<option>' + person.Naam + '</option>';
            var label = document.createElement('label');
            label.innerHTML = person.Naam;
            label.setAttribute("for", person.Username);

            var el = document.createElement('input');
            el.setAttribute("type", "checkbox");
            el.setAttribute("name", "Username");
            el.setAttribute("value", person.Username);
            container.appendChild(label)
            container.appendChild(el)
        })

        $('.shareItem').on('shown.bs.popover', function () {
            console.log("shown.bs.popover");
        });
    });
});