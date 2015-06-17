//#share_item_wrapper
console.log("Init shareItem");
$(function () {
    $(".shareItem")
            .popover({
                trigger: 'manual',
                html: true,
                content: function () {
                    return $('#share_item_wrapper').html();
                }
            }).click(function (e) {
                if ($(this).data('bs.popover').tip().hasClass('in')) {
                    //Hack om manueel te testen of popover zichtbaar is
                $(this).popover('hide');
            } else {
                e.preventDefault();
                console.log("Current Id: " + this.dataset.id);
                console.log("Current Type: " + this.dataset.type);
                var itemId = this.dataset.id;
                var type = this.dataset.type;
                var clicked = this;

                $.get("/home/sharelist?Id=" + itemId + "&Type=" + type, function (data) {
                    var containers = $("#share_item_wrapper .people");
                    try {
                        var people = $.parseJSON(data);
                    } catch (err) {
                        console.log(err);
                        alert("Onze excuses, er liep iets fout tijdens het opladen van de deellijst. Toon dit aan een van onze onbetaalde programmeurs en hoop dat ze in een goede bui zijn: " + err);
                    }
                    console.log("Sharelist for" + type + " " + itemId + " loaded succesfully");
                    reloadDataSet(people, containers, itemId, type);
                    $(clicked).popover('show');
                    //Anders laad de popover voor de async .get call done is
                    $('input#us2-address.form-control').on('input', function () {
                        var substring = $(this).val();
                        var filtered = people.filter(function (el) {
                            if (el.Naam.toLowerCase().indexOf(substring.toLowerCase() ) > -1) return true;
                        })
                        $(".popover-content .people")[0].innerHTML = "";
                        reloadDataSet(filtered, containers, itemId, type).forEach(function(el){
                            $(".popover-content .people")[0].appendChild(el);
                        });
                    });
                })
            }
            }).mouseleave(function (e) {

            });

    $('.shareItem').on('shown.bs.popover', function () {
        console.log("shown.bs.popover");
        console.log($("#share_item_wrapper #us2-address"));
    });

});

function reloadDataSet(people, containers, itemId, type) {
    var newList = [];
    console.log("reload");
    $(containers).each(function (i) {
        var container = this;
        console.log(this);
        container.innerHTML = "";
        people.forEach(function (person) {
            console.log(person.Naam);
            //var s = '<option>' + person.Naam + '</option>';
            var label = document.createElement('label');

            var div = document.createElement("div");
            div.className = "checkbox";

            var el = document.createElement('input');

            el.setAttribute("type", "checkbox");
            el.setAttribute("name", "Username");

            el.setAttribute("value", person.Username);
            el.dataset.Id = itemId;
            el.dataset.Type = type;
            el.dataset.IsGedeeld = person.IsGedeeld;

            if (person.IsGedeeld) {
                el.setAttribute("checked", "checked");
            }

            el.setAttribute("onclick", 'handleClick(this);');

            label.appendChild(el);
            label.innerHTML = label.innerHTML + person.Naam;

            div.appendChild(label);

            container.appendChild(div);
            newList.push(div);
        })
    })
    return newList;
}

function handleClick(cb) {
    //EditShare(string Username, int Id, string Type, bool IsGedeeld)

    if (cb.checked != (eval(cb.dataset.IsGedeeld))) {
        var jqxhr = $.post("/home/editshare", {
            Username: cb.value,
            Id: cb.dataset.Id,
            Type: cb.dataset.Type,
            IsGedeeld: cb.checked
        }, function () {
                console.log("success");
        })
            .done(function () {
                console.log("second success");
                console.log("should reload dataset here");
                //reloadDataSet();
            })
            .fail(function () {
                cb.setAttribute("checked", "checked")
                console.log("error");
                alert("Onze excuses, er liep iets fout tijdens het opladen van de deellijst.");
            })
            .always(function () {
                console.log("finished");
        });
    }
}