$(document).ready(function () {

    var crudMode = "";
    var localWorkcell = null;

    function Workcell(machineUid, machineId, description, lineId, isActive, cAvailable, cPeformance, cQuality, wAvailable, wPeformance, wQuality) {
        this.machineUid = machineUid;
        this.machineId = machineId;
        this.description = description;
        this.lineId = lineId;
        this.isActive = isActive;
        this.cAvailable = cAvailable;
        this.cPerformance = cPeformance;
        this.cQuality = cQuality;
        this.wAvailable = wAvailable;
        this.wPerformance = wPeformance;
        this.wQuality = wQuality;
    }

    $("#radio1").click(function () {
        $("#machineId").attr("readonly", false);
        $("#tabs").tabs("option", "active", 0);
        $(".form").show();
        crudMode = "add";
        clearSelection();
        clearForm();
        $("#submit").val("Submit");
        localWorkcell = null;
    });

    $("#radio2").click(function () {
        $("#machineId").attr("readonly", true);
        if (localWorkcell == null) {
            alert("Please select workcell/machine to update");
        }
        else {
            crudMode = "update";
            $("#tabs").tabs("option", "active", 0);
            $(".form").show();
            fillForm(localWorkcell);
            $("#submit").val("Update");
        }
        clearSelection();
    });

    $("#radio3").click(function () {
        crudMode = "delete";
        if (localWorkcell != null) {
            doPost(localWorkcell, deleteAction);
        }
        else {
            alert("Please select workcell/machine to delete");
        }
        localWorkcell = null;
        clearSelection();
    });

    function doFetch() {
        var machineId = $("#machineId").val();
        var description = $("#description").val();
        var isActive = $("#isActive").is(":checked");
        var lineId = $("#lineId").val();
        var cAvailable = $("#cAvailable").val();
        var cPerformance = $("#cPerformance").val();
        var cQuality = $("#cQuality").val();
        var wAvailable = $("#wAvailable").val();
        var wPerformance = $("#wPerformance").val();
        var wQuality = $("#wQuality").val();
        if (crudMode == "add") {
            var workcell = new Workcell(null, machineId, description, lineId, isActive, cAvailable, cPerformance, cQuality, wAvailable, wPerformance, wQuality);
        }
        else if (crudMode == "update") {
            var workcell = new Workcell(localWorkcell.machineUid, machineId, description, lineId, isActive, cAvailable, cPerformance, cQuality, wAvailable, wPerformance, wQuality);
        }
        return workcell;
    }

    function doFetchByRow(row) {
        var machineUid = row["machineUid"];
        var machineId = row["machineId"];
        var description = row["description"];
        var lineId = row["lineId.lineId"];
        var isActive = row["isActive"];
        var cAvailable = row["cAvailable"];
        var cPerformance = row["cPerformance"];
        var cQuality = row["cQuality"];
        var wAvailable = row["wAvailable"];
        var wPerformance = row["wPerformance"];
        var wQuality = row["wQuality"];
        localWorkcell = new Workcell(machineUid, machineId, description, lineId, isActive, cAvailable, cPerformance, cQuality, wAvailable, wPerformance, wQuality);
        return localWorkcell;
    }

    function doPost(workcell, action) {
        $.ajax({
            type: 'POST',
            url: action,
            data: {
                'machineUid': workcell.machineUid,
                'machineId': workcell.machineId,
                'description': workcell.description,
                'isActive': workcell.isActive,
                'lineId': workcell.lineId,
                'cAvailable': workcell.cAvailable,
                'cQuality': workcell.cQuality,
                'cPerformance': workcell.cPerformance,
                'wAvailable': workcell.wAvailable,
                'wQuality': workcell.wQuality,
                'wPerformance': workcell.wPerformance
            },
            success: function (data) {
                alert(data.result + " " + data.msg);
                $("#list").jqGrid().setGridParam(
                    {
                        datatype: 'json',
                        page: 1,
                        url: fetchAction
                    }
                ).trigger("reloadGrid");
                
                //$("#list").trigger("reloadGrid");
            }
        });
    }

    $("#form1").validate({
        rules: {
            machineId: {
                required: true
            },
            description: {
                required: true
            },
            lineId: {
                required: true
            }
        },
        messages: {
            machineId: {
                required: "Please provide a Machine ID"
            },
            description: {
                required: "Please provide a description"
            },
            lineId: {
                required: "Please provide a line"
            }
        },
        submitHandler: function () {
            if (crudMode == "add") {
                doPost(doFetch(), addAction);
            }
            else if (crudMode == "update") {
                doPost(doFetch(), updateAction);
            }
        }
    });


    function fillForm(workcell) {
        $("#machineId").val(workcell.machineId);
        $("#description").val(workcell.description);
        $("#lineId").val(workcell.lineId);
        $("#cAvailable").val(workcell.cAvailable);
        $("#cPerformance").val(workcell.cPerformance);
        $("#cQuality").val(workcell.cQuality);
        $("#wAvailable").val(workcell.wAvailable);
        $("#wPerformance").val(workcell.wPerformance);
        $("#wQuality").val(workcell.wQuality);
        if (workcell.isActive == "Yes") {
            $("#isActive").attr("checked", true);
        }
    }

    $("#list").jqGrid({
        url: fetchAction,
        datatype: "json",
        mtype: "POST",
        colNames: ["Machine UID", "Machine ID", "Description", "Available", "Performance", "Quality", "Low Available", "Low Performance", "Low Quality", "Created By", "Modified By", "Is Active", "Line ID", "Line Description"],
        colModel: [
            { name: "machineUid", sortable: false, hidden: true },
            { name: "machineId", sortable: false },
            { name: "description", sortable: false },
            { name: "cAvailable", sortable: false },
            { name: "cPerformance", sortable: false },
            { name: "cQuality", sortable: false },
            { name: "wAvailable", sortable: false },
            { name: "wPerformance", sortable: false },
            { name: "wQuality", sortable: false },
            { name: "createdBy.userId", sortable: false, hidden: true },
            { name: "modifiedBy.userId", sortable: false, hidden: true },
            { name: "isActive", sortable: false, formatter: "checkbox", align: 'center' },
            { name: "lineId.lineId", sortable: false, hidden: true },
            { name: "lineId.description", sortable: false }
        ],
        pager: "#pager",
        rowNum: 5,
        rownumbers: true,
        rowList: [5, 10, 15],
        height: 'auto',
        width: '590',
        grouping: true,
        groupingView: {
            groupField: ['lineId.description'],
            groupColumnShow: false,
            groupText: ['<center><b>{0} - {1} Workcell(s)</b></center>']
        },
        caption: "Workcell Data",
        onSelectRow: function (id) {
            console.log(id + " " + $(this).getRowData(id));
            var row = $(this).getRowData(id);
            if (crudMode != null || crudMode != "add") {
                localWorkcell = doFetchByRow(row);
            }
            vMachineId = row["machineId"];

            var postedUrl = fetchDetailAction + "?machineId=" + row["machineId"];
            $("#list-detail").setGridParam({ url: postedUrl, page: 1 });
            $("#list-detail").trigger("reloadGrid");
        }
    });



});