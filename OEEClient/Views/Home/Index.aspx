<%@ Page Title="Rockwell Automation" Language="C#" MasterPageFile="~/Views/Shared/LoginForm.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <script>
        var loginAction = "../../Home/doLoginRequest";
    </script>

    <script type="text/javascript" src="../../Scripts/Home/Index.js"></script>
    <div class="login">
        <asp:Table runat="server" ID="Table2">
            <asp:TableRow>
                <asp:TableCell ColumnSpan="3">
                    <center><img src="../../Content/logo.png" width="50%" height="50%"></img></center>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>User ID</asp:TableCell>
                <asp:TableCell>:</asp:TableCell>
                <asp:TableCell><input type="text" id="userId" name="userId" /></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>Password</asp:TableCell>
                <asp:TableCell>:</asp:TableCell>
                <asp:TableCell><input type="password" id="password" name="password" /></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell><input type="submit" value="Login" id="submit" /></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="3">
                    <center><%=Session[OEEComponent.maintenance.AppConstants.SESSION_LOGIN_NOTE] %></center>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
