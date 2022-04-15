<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterForm.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src="../../Scripts/Dashboard/Index.js"></script>
    <h2>Dashboard - <%=ViewData["param1"]%> <%=ViewData["param2"]%></h2>
    <div id="radioset">
	    <input type="radio" id="radio1" name="radio"><label for="radio1">Current Shift</label>
	    <input type="radio" id="radio2" name="radio"><label for="radio2">Yesterday</label>
	    <input type="radio" id="radio3" name="radio"><label for="radio3">Weekly</label>
        <input type="radio" id="radio4" name="radio"><label for="radio4">Monthly</label>
        <input type="radio" id="radio5" name="radio"><label for="radio5">Yearly</label>
        <input type="radio" id="radio6" name="radio"><label for="radio6">Now</label>
	</div>
    <br />
    <div id="panel">
        <div class="column">
            <div class="portlet">
                <div class="portlet-header">Availability</div>
                <div class="portlet-content">
                    <iframe name="WidgetAvailable" height="100%" width="100%"></iframe>
                </div>
            </div>
        </div>

        <div class="column">
            <div class="portlet">
                <div class="portlet-header">Throughput</div>
                <div class="portlet-content">
                    <iframe name="WidgetPerformance" height="100%" width="100%"></iframe>
                </div>
            </div>
        </div>
 
        <div class="column">
            <div class="portlet">
                <div class="portlet-header">Quality</div>
                <div class="portlet-content">
                    <iframe name="WidgetQuality" height="100%" width="100%"></iframe>
                </div>
            </div>

            <div class="portlet">
                <div class="portlet-header">OEE</div>
                <div class="portlet-content">
                    <iframe name="WidgetOee" height="100%" width="100%"></iframe>
                </div>
            </div>

        </div>    
    </div>
    

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
