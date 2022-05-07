// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function AddDeleteFavorites(actionUrl)
{
    $(document).ready(function() {
        $.ajax({
            type: "POST",
            url: actionUrl,
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            success: function(data) {
                $('#modalText').text(data.msg);
            },
            error: function() {
                alert("Ошибка")
            }
        });
    });
}