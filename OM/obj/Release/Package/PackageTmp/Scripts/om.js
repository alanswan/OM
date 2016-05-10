(function (mcp, $, undefined) {

    om.openModal = function (e) {
        $('#backOddsInput').val($(e.parentElement.parentElement.childNodes[4]).text())
        $('#layOddsInput').val($(e.parentElement.parentElement.childNodes[5]).text())
        $('#calcModal').modal('show')
    }

})(window.om = window.om || {}, jQuery);