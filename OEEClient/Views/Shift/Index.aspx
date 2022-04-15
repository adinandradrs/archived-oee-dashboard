<%@ Page Title="Shift Management" Language="C#" MasterPageFile="~/Views/Shared/MasterForm.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--
    datasource
    -->

    <script>
        var addAction = "../../Shift/doAddRequest";
        var updateAction = "../../Shift/doUpdateRequest";
        var deleteAction = "../../Shift/doDeleteRequest";
        var fetchAction = "../../Shift/doFetchRequest";
    </script>
    
    <script type="text/javascript" src="../../Scripts/Shift/Index.js"></script>
    <div><h2>Master Data Shift</h2></div>
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
                            Started
                        </asp:TableCell>
                        <asp:TableCell>
                            :
                        </asp:TableCell>
                        <asp:TableCell>
                            <input type="text" name="started" maxlength="50" id="started" formatter="timepicker" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            Ended
                        </asp:TableCell>
                        <asp:TableCell>
                            :
                        </asp:TableCell>
                        <asp:TableCell>
                            <input type="text" name="ended" maxlength="50" id="ended" formatter="timepicker" />
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
