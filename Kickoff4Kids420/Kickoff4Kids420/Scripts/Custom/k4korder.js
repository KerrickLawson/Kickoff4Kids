$(document).ready(function () {

    $("input[type=checkbox]").change(function () {
        $("tr").toggleClass("danger", this.checked);
    }).change();
})