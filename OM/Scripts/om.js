(function (mcp, $, undefined) {

    om.startLoading = function(e) {
        $.blockUI(
                { message: '<img src="../Images/loading-mcp.gif" /><p class="loading-text">Odds matching in progress...</p>',
                centerY: false,  
                css: { top: '100px' } 
                });
    }

    om.startLoadingMobile = function (e) {
        $.blockUI(
                {
                    message: '<p class="loading-text">Odds matching in progress...</p>',
                    centerY: false,
                    css: { top: '100px' }
                });
    }

    om.openModal = function (e) {
        var eStake = $('#eStake');
        eStake.text("");
        var liability = $('#liability');
        liability.text("");
        var ewinH = $('#ewin');
        ewinH.text("");
        var bwinH = $('#bwin');
        bwinH.text("");
        $('#backOddsInput').val($(e.parentElement.parentElement.childNodes[7]).text())
        $('#layOddsInput').val($(e.parentElement.parentElement.childNodes[8]).text())
        $('#backStakeInput').val('')
        $('#comInput').val('')
        $('#calcModal').modal('show')
    }

    om.openModalMobile = function (e) {
        
        var top = $(e).offset().top;

        $('#calcModal').css('top', top + 'px');
        $('#calcModal').css('position', 'absolute');
        $('#calcModal').css('overflow', 'visible');
        $('#calcModal').css('outline', 'none');

        var eStake = $('#eStake');
        eStake.text("");
        var liability = $('#liability');
        liability.text("");
        var ewinH = $('#ewin');
        ewinH.text("");
        var bwinH = $('#bwin');
        bwinH.text("");
        $('#backOddsInput').val($('#bookmaker-odds').text())
        $('#layOddsInput').val($('#exchange-odds').text())
        $('#backStakeInput').val('')
        $('#comInput').val('')
        $('#calcModal').modal('show')
    }

    om.changeExchange = function (exchange) {
        om.startLoading();
        $("#grid").data("kendoGrid").dataSource.transport.options.read.url = "/Odds/GetOddsData?bookmaker=" + $("#dropdownlistBookmaker").data('kendoDropDownList').text() + "&exchange=" + exchange
        $('#grid').data('kendoGrid').dataSource.read();
    }

    om.changeExchangeMobile = function (exchange) {
        om.startLoadingMobile();
        $("#listView").data("kendoListView").dataSource.transport.options.read.url = "/Odds/GetOddsData?bookmaker=" + $("#dropdownlistBookmaker").data('kendoDropDownList').text() + "&exchange=" + exchange
        $('#listView').data('kendoListView').dataSource.read();
    }
    
    om.changeBookmaker = function (bookmaker) {
        om.startLoading();
        $("#grid").data("kendoGrid").dataSource.transport.options.read.url = "/Odds/GetOddsData?bookmaker=" + bookmaker + "&exchange=" + $("#dropdownlistExchange").data('kendoDropDownList').text()
        $('#grid').data('kendoGrid').dataSource.read();
    }

    om.changeBookmakerMobile = function (bookmaker) {
        om.startLoadingMobile();
        $("#listView").data("kendoListView").dataSource.transport.options.read.url = "/Odds/GetOddsData?bookmaker=" + bookmaker + "&exchange=" + $("#dropdownlistExchange").data('kendoDropDownList').text()
        $('#listView').data('kendoListView').dataSource.read();
    }

    om.refreshOdds = function () {
        om.startLoading();
        $("#grid").data("kendoGrid").dataSource.transport.options.read.url = "/Odds/GetOddsData?bookmaker=" + $("#dropdownlistBookmaker").data('kendoDropDownList').text() + "&exchange=" + $("#dropdownlistExchange").data('kendoDropDownList').text()
        $('#grid').data('kendoGrid').dataSource.read();
    }

    om.refreshOddsMobile = function () {
        om.startLoadingMobile();
        $("#listView").data("kendoListView").dataSource.transport.options.read.url = "/Odds/GetOddsData?bookmaker=" + $("#dropdownlistBookmaker").data('kendoDropDownList').text() + "&exchange=" + $("#dropdownlistExchange").data('kendoDropDownList').text()
        $('#listView').data('kendoListView').dataSource.read();
    }

    om.ddlChange = function () {
        var stNumber = parseFloat($('#backStakeInput').val());
        var ddl = $('#betTypeDDL').data('kendoDropDownList')
        if (stNumber != "" && ddl != undefined)
        {
            om.calculate();
        }
    }

    om.calculate = function () {

                var eStake = $('#eStake');
                eStake.text("");

                var liability = $('#liability');
                liability.text("");

                var ewinH = $('#ewin');
                ewinH.text("");

                var bwinH = $('#bwin');
                bwinH.text("");

                $('#stake-warning-message').hide();
                $('#bookmaker-odds-warning-message').hide();
                $('#exchange-odds-warning-message').hide();

            var ddl = $('#betTypeDDL').data('kendoDropDownList').text()
            var stNumber = parseFloat($('#backStakeInput').val());
            var boNumber = parseFloat($('#backOddsInput').val());
            var eoNumber = parseFloat($('#layOddsInput').val());
            var commisionBox = parseFloat($('#comInput').val()) / 100
            var cond = true;
            var x = 0.00;
            var ewin = 0;
            var bwin = 0;
            var e = 0;
            var calculateBool = true

            if (isNaN(eoNumber)) {
                $('#exchange-odds-warning-message').show();
                calculateBool = false;
            }
            if (isNaN(boNumber)) {
                $('#bookmaker-odds-warning-message').show();
                calculateBool = false;
            }
            if (isNaN(commisionBox)) {
                commisionBox = 0;
            }

            if(isNaN(stNumber))
            {
                $('#stake-warning-message').show();
                calculateBool = false;
            }
            if(calculateBool)
            {
                while (cond) {
                    x = x + 0.01;

                    var b = (stNumber * boNumber) - stNumber;
                    if(ddl == "Free Bet (Stake Not Returned)")
                    {
                        b = b - stNumber
                    }
                    e = (x * eoNumber) - x;
                    var comm = x * commisionBox;

                    bwin = b - e;
                    ewin = x - stNumber - comm

                    if ((ewin - bwin) > 0) {
                        cond = false;
                    }
                }
            
                if(ddl != "Normal")
                {
                    ewin = ewin + stNumber;
                    bwin = bwin + stNumber;
                }

                eStake.text("You could lay " + (Math.round(x * 100) / 100).toFixed(2));

                liability.text("Your liability would be " + (Math.round(e * 100) / 100).toFixed(2));

                ewinH.text("If you won in the exchange, your profit/loss would be " + (Math.round(ewin * 100) / 100).toFixed(2));

                bwinH.text("If you won in the bookmaker, your profit/loss would be " + (Math.round(bwin * 100) / 100).toFixed(2));
            }
        }

})(window.om = window.om || {}, jQuery);