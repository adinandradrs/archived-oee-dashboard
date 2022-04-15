<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterForm.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--
    datasource
    -->

    <script>
        var addAction = "../../Line/doAddRequest";
        var updateAction = "../../Line/doUpdateRequest";
        var deleteAction = "../../Line/doDeleteRequest";
        var fetchAction = "../../Line/doFetchRequest";
    </script>
    
    <script type="text/javascript" src="../../Scripts/Line/Index.js"></script>
    <div><h2>Master Data Line</h2></div>
    <div id="radioset">
	    <input type="radio" id="radio1" name="radio"><label for="radio1">Add</label>
	    <input type="radio" id="radio2" name="radio"><label for="radio2">Edit</label>
	    <input type="radio" id="radio3" name="radio"><label for="radio3">Delete</label>
	</div>
    
    <div id="tabs">
	    <ul>
		    <li class="form"><a href="#form">Form</a></li>
		    <li><a href="#data">Data</a></li>
	    </ul>
        <div class="viewport">
            <div class="form" id="form">
                <asp:Table ID="tableForm" runat="server" ClientIDMode="Static">
                    <asp:TableRow>
                        <asp:TableCell>
                            Description
                        </asp:TableCell>
                        <asp:TableCell>
                            :
                        </asp:TableCell>
                        <asp:TableCell>
                            <input type="text" name="description" maxlength="50" id="description" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            Available
                        </asp:TableCell>
                        <asp:TableCell>
                            :
                        </asp:TableCell>
                        <asp:TableCell>
                            <input type="text" name="cAvailable" maxlength="50" id="cAvailable" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            Performance
                        </asp:TableCell>
                        <asp:TableCell>
                            :
                        </asp:TableCell>
                        <asp:TableCell>
                            <input type="text" name="cPerformance" maxlength="50" id="cPerformance" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            Quality
                        </asp:TableCell>
                        <asp:TableCell>
                            :
                        </asp:TableCell>
                        <asp:TableCell>
                            <input type="text" name="cQuality" maxlength="50" id="cQuality" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            Available
                        </asp:TableCell>
                        <asp:TableCell>
                            :
                        </asp:TableCell>
                        <asp:TableCell>
                            <input type="text" name="wAvailable" maxlength="50" id="wAvailable" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            Performance
                        </asp:TableCell>
                        <asp:TableCell>
                            :
                        </asp:TableCell>
                        <asp:TableCell>
                            <input type="text" name="wPerformance" maxlength="50" id="wPerformance" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            Quality
                        </asp:TableCell>
                        <asp:TableCell>
                            :
                        </asp:TableCell>
                        <asp:TableCell>
                            <input type="text" name="wQuality" maxlength="50" id="wQuality" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            Active?
                        </asp:TableCell>
                        <asp:TableCell>
                            :
                        </asp:TableCell>
                        <asp:TableCell>
                            <input type="checkbox" name="isActive" id="isActive" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                        </asp:TableCell>
                        <asp:TableCell>
                        </asp:TableCell>
                        <asp:TableCell>
                            <input type="submit" value="Submit" id="submit" onclick="" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </div>
	        <div id="data">
                <table id="list">
                    <tr>
                        <td></td>
                    </tr>
                </table>
                <div id="pager"></div>
            </div>
	    </div>
    </div> 

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
