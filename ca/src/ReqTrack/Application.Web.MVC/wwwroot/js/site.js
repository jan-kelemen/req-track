$(function () {
    $("#stepTable").delegate("button[name='add']", "click", function (e) {
        e.preventDefault();
        var row =
            '<tr>' +
                '<td><input name="Steps[]" class="form-control"></td>' +
                '<td><button class="btn btn-default tableButton" name="up">Up</button></td>' +
                '<td><button class="btn btn-default tableButton" name="down">Down</button></td>' +
                '<td><button class="btn btn-default tableButton" name="del" onclick="remove_row(this)">Delete</button></td>' +
            '</tr>';

        $('#stepTable tr:last').after(row);
    });

    //https://stackoverflow.com/a/12594240
    $("#stepTable").delegate("button[name='up']", "click", function (e) {
        e.preventDefault();
        var $element = this;
        var row = $($element).parents("tr:first");

        if (!row.prev().is($('#stepTableHeader'))) {
            row.insertBefore(row.prev());
        }
    });
    $("#stepTable").delegate("button[name='down']", "click", function (e) {
        e.preventDefault();
        var $element = this;
        var row = $($element).parents("tr:first");
        row.insertAfter(row.next());
    });
});

function remove_row(btn) {
    var parent = btn.parentNode.parentNode;
    parent.remove();
}