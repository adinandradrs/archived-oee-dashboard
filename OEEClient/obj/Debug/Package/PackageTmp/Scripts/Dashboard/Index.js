$(document).ready(function () {

        document.getElementsByName("WidgetAvailable")[0].src = "../../Dashboard/WidgetAvailable?machineUid=" + getParameterByName("machineUid") + "&lineId=" + getParameterByName("lineId");
        document.getElementsByName("WidgetPerformance")[0].src = "../../Dashboard/WidgetPerformance?machineUid=" + getParameterByName("machineUid") + "&lineId=" + getParameterByName("lineId");
        document.getElementsByName("WidgetQuality")[0].src = "../../Dashboard/WidgetQuality?machineUid=" + getParameterByName("machineUid") + "&lineId=" + getParameterByName("lineId");
        document.getElementsByName("WidgetOee")[0].src = "../../Dashboard/WidgetOee?machineUid=" + getParameterByName("machineUid") + "&lineId=" + getParameterByName("lineId");

    $("#radio1").click(function () {
        src = "machineUid=" + getParameterByName("machineUid") + "&lineId=" + getParameterByName("lineId") + "&time=shift";
        execute(src);

                document.getElementsByName("WidgetAvailable")[0].src = "../../Dashboard/WidgetAvailable?" + src;
                document.getElementsByName("WidgetPerformance")[0].src = "../../Dashboard/WidgetPerformance?" + src;
                document.getElementsByName("WidgetQuality")[0].src = "../../Dashboard/WidgetQuality?" + src;
                document.getElementsByName("WidgetOee")[0].src = "../../Dashboard/WidgetOee?" + src;
    });

    $("#radio2").click(function () {
        src = "machineUid=" + getParameterByName("machineUid") + "&lineId=" + getParameterByName("lineId") + "&time=yesterday";
//        execute(src);
                document.getElementsByName("WidgetAvailable")[0].src = "../../Dashboard/WidgetAvailable?" + src;
                document.getElementsByName("WidgetPerformance")[0].src = "../../Dashboard/WidgetPerformance?" + src;
                document.getElementsByName("WidgetQuality")[0].src = "../../Dashboard/WidgetQuality?" + src;
                document.getElementsByName("WidgetOee")[0].src = "../../Dashboard/WidgetOee?" + src;
    });

    $("#radio3").click(function () {
        src = "machineUid=" + getParameterByName("machineUid") + "&lineId=" + getParameterByName("lineId") + "&time=lastweek";
        execute(src);
                document.getElementsByName("WidgetAvailable")[0].src = "../../Dashboard/WidgetAvailable?" + src;
                document.getElementsByName("WidgetPerformance")[0].src = "../../Dashboard/WidgetPerformance?" + src;
                document.getElementsByName("WidgetQuality")[0].src = "../../Dashboard/WidgetQuality?" + src;
                document.getElementsByName("WidgetOee")[0].src = "../../Dashboard/WidgetOee?" + src;
    });

    $("#radio4").click(function () {
        src = "machineUid=" + getParameterByName("machineUid") + "&lineId=" + getParameterByName("lineId") + "&time=lastmonth";
        //execute(src);
                document.getElementsByName("WidgetAvailable")[0].src = "../../Dashboard/WidgetAvailable?" + src;
                document.getElementsByName("WidgetPerformance")[0].src = "../../Dashboard/WidgetPerformance?" + src;
                document.getElementsByName("WidgetQuality")[0].src = "../../Dashboard/WidgetQuality?" + src;
                document.getElementsByName("WidgetOee")[0].src = "../../Dashboard/WidgetOee?" + src;
    });

    $("#radio5").click(function () {
        src = "machineUid=" + getParameterByName("machineUid") + "&lineId=" + getParameterByName("lineId") + "&time=lastyear";
        //execute(src);
                document.getElementsByName("WidgetAvailable")[0].src = "../../Dashboard/WidgetAvailable?" + src;
                document.getElementsByName("WidgetPerformance")[0].src = "../../Dashboard/WidgetPerformance?" + src;
                document.getElementsByName("WidgetQuality")[0].src = "../../Dashboard/WidgetQuality?" + src;
                document.getElementsByName("WidgetOee")[0].src = "../../Dashboard/WidgetOee?" + src;
    });

    $("#radio6").click(function () {
        src = "machineUid=" + getParameterByName("machineUid") + "&lineId=" + getParameterByName("lineId") + "&time=latest";
        //execute(src);
                document.getElementsByName("WidgetAvailable")[0].src = "../../Dashboard/WidgetAvailable?" + src;
                document.getElementsByName("WidgetPerformance")[0].src = "../../Dashboard/WidgetPerformance?" + src;
                document.getElementsByName("WidgetQuality")[0].src = "../../Dashboard/WidgetQuality?" + src;
                document.getElementsByName("WidgetOee")[0].src = "../../Dashboard/WidgetOee?" + src;
    });

    var gauge = null;

    function ajaxAvailable() {
        $.ajax({
            type: 'POST',
            url: '../../Dashboard/Gauge',
            data: {
                type: getParameterByName("type"),
                machineUid: getParameterByName("machineUid"),
                lineId: getParameterByName("lineId"),
                time: getParameterByName("time"),
                gaugeType: "available"

            },
            success: function (data) {
                gauge = data;
                $('#WidgetAvailable').jqxGauge(
                gauge
            );
            }
        });
    }

    ajaxAvailable();

    function ajaxOee() {
        $.ajax({
            type: 'POST',
            url: '../../Dashboard/Gauge',
            data: {
                type: getParameterByName("type"),
                machineUid: getParameterByName("machineUid"),
                lineId: getParameterByName("lineId"),
                time: getParameterByName("time"),
                gaugeType: "oee"

            },
            success: function (data) {
                gauge = data;
                $('#WidgetOee').jqxGauge(
                gauge
            );
            }
        });
    }

    ajaxOee();

    function ajaxQuality() {
        $.ajax({
            type: 'POST',
            url: '../../Dashboard/Gauge',
            data: {
                type: getParameterByName("type"),
                machineUid: getParameterByName("machineUid"),
                lineId: getParameterByName("lineId"),
                time: getParameterByName("time"),
                gaugeType: "quality"

            },
            success: function (data) {
                gauge = data;
                $('#WidgetQuality').jqxGauge(
                gauge
            );
            }
        });
    }

    ajaxQuality();

    function ajaxThroughput() {
        $.ajax({
            type: 'POST',
            url: '../../Dashboard/Gauge',
            data: {
                type: getParameterByName("type"),
                machineUid: getParameterByName("machineUid"),
                lineId: getParameterByName("lineId"),
                time: getParameterByName("time"),
                gaugeType: "performance"

            },
            success: function (data) {
                gauge = data;
                $('#WidgetThroughput').jqxGauge(
                gauge
            );
            }
        });
    }

    ajaxThroughput();

    function ajaxByTime(type, param, target) {
        $.ajax({
            type: 'POST',
            url: '../../Dashboard/Gauge?gaugeType=' + type + '&' + param,
            success: function (data) {
                gauge = data;
                $('#' + target).html('');
                $('#' + target).jqxGauge(
                    gauge
                );
            }
        });
    }

    function execute(src) {
        ajaxByTime("available", src, "WidgetAvailable");
        ajaxByTime("performance", src, "WidgetThroughput");
        ajaxByTime("quality", src, "WidgetQuality");
        ajaxByTime("oee", src, "WidgetOee");
    }

});