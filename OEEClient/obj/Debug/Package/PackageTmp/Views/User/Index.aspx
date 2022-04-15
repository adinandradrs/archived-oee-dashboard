<%@ Page Title="User Management" Language="C#" MasterPageFile="~/Views/Shared/MasterForm.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--
    datasource
    -->
    <asp:ObjectDataSource ID="groupsDataSource" runat="server" 
        SelectMethod="getGroups" TypeName="OEEClient.Controllers.UserController"></asp:ObjectDataSource>
    
    <script>
        var addAction = "../../User/doAddRequest";
        var updateAction = "../../User/doUpdateRequest";
        var deleteAction = "../../User/doDeleteRequest";
        var fetchAction = "../../User/doFetchRequest";
    </script>

    <script type="text/javascript" src="../../Scripts/User/Index.js"></script>
    <div><h2>Master Data User</h2></div>
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
                            User ID
                        </asp:TableCell>
                        <asp:TableCell>
                            :
                        </asp:TableCell>
                        <asp:TableCell>
                            <input type="text" name="userId" maxlength="30" id="userId" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            Password
                        </asp:TableCell>
                        <asp:TableCell>
                            :
                        </asp:TableCell>
                        <asp:TableCell>
                            <input type="password" name="password" maxlength="120" id="password" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            Group
                        </asp:TableCell>
                        <asp:TableCell>
                            :
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:DropDownList ID="groupId" runat="server" 
                                DataSourceID="groupsDataSource" DataTextField="description" 
                                DataValueField="groupId" ClientIDMode="Static" AutoPostBack="false">
                            </asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            First Name
                        </asp:TableCell>
                        <asp:TableCell>
                            :
                        </asp:TableCell>
                        <asp:TableCell>
                            <input type="text" name="firstName" id="firstName" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            Last Name
                        </asp:TableCell>
                        <asp:TableCell>
                            :
                        </asp:TableCell>
                        <asp:TableCell>
                            <input type="text" name="lastName" id="lastName" />
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

    <asp:HiddenField ID="userUid" runat="Server" ClientIDMode="Static" />
    <asp:HiddenField ID="userGroupId" runat="Server" ClientIDMode="Static" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
