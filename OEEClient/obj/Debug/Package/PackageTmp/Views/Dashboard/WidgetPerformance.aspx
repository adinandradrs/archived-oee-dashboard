<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <script type="text/javascript" src="../../Content/jquery-ui/js/jquery-1.11.0.min.js"></script>
    <link rel="stylesheet" href="../../Content/jqwidgets/styles/jqx.base.css" type="text/css" />
    <script type="text/javascript" src="../../Content/jqwidgets/jqxcore.js"></script>
    <script type="text/javascript" src="../../Content/jqwidgets/jqxdraw.js"></script>
    <script type="text/javascript" src="../../Content/jqwidgets/jqxgauge.js"></script>
</head>
<body>
    <style type="text/css">
        #gaugeValue {
	        background-image: -webkit-gradient(linear, 50% 0%, 50% 100%, color-stop(0%, #fafafa), color-stop(100%, #f3f3f3));
	        background-image: -webkit-linear-gradient(#fafafa, #f3f3f3);
	        background-image: -moz-linear-gradient(#fafafa, #f3f3f3);
	        background-image: -o-linear-gradient(#fafafa, #f3f3f3);
	        background-image: -ms-linear-gradient(#fafafa, #f3f3f3);
	        background-image: linear-gradient(#fafafa, #f3f3f3);
	        -webkit-border-radius: 3px;
	        -moz-border-radius: 3px;
	        -ms-border-radius: 3px;
	        -o-border-radius: 3px;
	        border-radius: 3px;
	        -webkit-box-shadow: 0 0 50px rgba(0, 0, 0, 0.2);
	        -moz-box-shadow: 0 0 50px rgba(0, 0, 0, 0.2);
	        box-shadow: 0 0 50px rgba(0, 0, 0, 0.2);
	        padding: 10px;
	    }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            var gauge = null;

            function getParameterByName(name) {
                name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
                var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
                            results = regex.exec(location.search);
                return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
            }

            $.ajax({
                type: 'POST',
                url: '../../Dashboard/Gauge',
                data: {
                    type : getParameterByName("type"),
                    machineUid: getParameterByName("machineUid"),
                    lineId: getParameterByName("lineId"),
                    time: getParameterByName("time"),
                    gaugeType: "performance"

                },
                success: function (data) {
                    gauge = data;
                    $('#gaugeContainer').jqxGauge(
                        gauge
                    );
                }
            });

        });
    </script>
    <div id="demoWidget" style="position: relative;">
	    <div style="float: left;" id="gaugeContainer"></div>
    </div>
</body>
</html>
