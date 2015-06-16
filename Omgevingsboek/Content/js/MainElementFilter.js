loadedChildrens = [];
nonElementChildrens = [];

$(".books").each(function (el) {
    loadedChildrens.push($(this).children("div.element"));
    nonElementChildrens.push($(this).children(":not(div.element)"));
});

$('#searchMainElements').bind('input', function () {
    var query = $(this).val();
    $(".books").each(function (el) {
        filterElements(
            query,
            this,
            loadedChildrens[el],
            nonElementChildrens[el]
        );
    });
    isFiltered = true;
});

function filterElements(substring, container, loadedChildren, nonElementChildren) {
    var filtered = loadedChildren;

    if (substring.trim() != "") {
        filtered = loadedChildren.filter(function (el) {
            title = $(this).find(".caption")[0].textContent.toLowerCase();
            if (title.indexOf(substring) > -1) return true;
        })
        isFiltered = true;
    } else {
        isFiltered = false;
    }

    container.innerHTML = '';
    filtered.get()
		.concat(nonElementChildren.get())
		.forEach(
			function (el) { console.log(this); container.appendChild(el) }
		);
}