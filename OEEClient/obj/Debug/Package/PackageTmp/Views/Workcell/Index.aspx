<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterForm.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--
    datasource
    -->
    <asp:ObjectDataSource ID="linesDataSource" runat="server" 
        SelectMethod="getLines" TypeName="OEEClient.Controllers.WorkcellController"></asp:ObjectDataSource>
    
    <script>
        var addAction = "../../Workcell/doAddRequest";
        var updateAction = "../../Workcell/doUpdateRequest";
        var deleteAction = "../../Workcell/doDeleteRequest";
        var fetchAction = "../../Workcell/doFetchRequest";

        var addDetailAction = "../../TagMap/doAddRequest";
        var updateDetailAction = "../../TagMap/doUpdateRequest";
        var deleteDetailAction = "../../TagMap/doDeleteRequest";
        
        var fetchDetailAction = "../../TagMap/doFetchDetailRequest";
        var vMachineId = null;
    </script>

    <script type="text/javascript" src="../../Scripts/Workcell/Index.js"></script>
    <script type="text/javascript" src="../../Scripts/Workcell/IndexDetail.js"></script>
    <div><h2>Master Data Workcell</h2></div>
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
                            Machine ID
                        </asp:TableCell>
                        <asp:TableCell>
                            :
                        </asp:TableCell>
                        <asp:TableCell>
                            <input type="text" name="machineId" maxlength="30" id="machineId" />
                        </asp:TableCell>
                    </asp:TableRow>
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
                            Line
                        </asp:TableCell>
                        <asp:TableCell>
                            :
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:DropDownList ID="lineId" runat="server"
                                DataSourceID="linesDataSource" DataTextField="description" 
                                DataValueField="lineId" ClientIDMode="Static" AutoPostBack="false">
                            </asp:DropDownList>
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
                            Low Available
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
                            Low Performance
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
                            Low Quality
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
                
                <br />
                <div id="radioset-detail">
	                <input type="radio" id="radio4" name="radio"><label for="radio4">Add</label>
	                <input type="radio" id="radio5" name="radio"><label for="radio5">Edit</label>
	                <input type="radio" id="radio6" name="radio"><label for="radio6">Delete</label>
	            </div>
                <!--detail-->
                <div id="tabs-detail">
	                <ul>
		                <li class="form-detail"><a href="#form-detail">Form</a></li>
		                <li><a href="#data-detail">Data</a></li>
	                </ul>
                    <div style="width:500px;">
                        <div class="form-detail" id="form-detail">
                            <asp:Table ID="tableFormDetail" runat="server" ClientIDMode="Static">
                                <asp:TableRow>
                                    <asp:TableCell>
                                        PLC Tag
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        :
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <input type="text" name="plcTag" maxlength="50" id="plcTag" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        Tag Type
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        :
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <select id="tagType" name="tagType">
                                            <option value="MTBF">MTBF</option>
                                            <option value="Available">Available</option>
                                            <option value="Quality">Quality</option>
                                            <option value="Performance">Performance</option>
                                            <option value="MTTR">MTTR</option>
                                        </select>
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
                                        <input type="checkbox" name="isActive" id="isActive2" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <input type="submit" value="Submit" id="submit2" onclick="javascript:onSubmitClick();" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </div>
                    </div>
                    <div id="data-detail">
                        <table id="list-detail">
                            <tr>
                                <td></td>
                            </tr>
                        </table>
                        <div id="pager-detail"></div>
                    </div>
                </div>
            </div>
	    </div>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
