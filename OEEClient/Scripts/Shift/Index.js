$().ready(function () {

    var crudMode = "";
    var localShift;

    function Shift(shiftId, description, isActive, started, ended, startedString, endedString) {
        this.shiftId = shiftId;
        this.description = description;
        this.isActive = isActive;
        this.started = started;
        this.ended = ended;
        this.startedString = startedString;
        this.endedString = endedString;
    }

    $("#radio1").click(function () {
        $("#tabs").tabs("option", "active", 0);
        $(".form").show();
        crudMode = "add";
        clearSelection();
        clearForm();
        $("#submit").val("Submit");
        localShift = null;
    });

    $("#radio2").click(function () {
        $("#shiftId").attr("readonly", true);
        if (localShift == null) {
            alert("Please select shift to update");
        }
        else {
            crudMode = "update";
            $("#tabs").tabs("option", "active", 0);
            $(".form").show();
            fillForm(localShift);
            $("#submit").val("Update");
        }
        clearSelection();
    });

    $("#radio3").click(function () {
        crudMode = "delete";
        if (localShift != null) {
            doPost(localShift, deleteAction);
        }
        else {
            alert("Please select shift to delete");
        }
        localShift = null;
        clearSelection();
    });

    function doFetch() {
        var description = $("#description").val();
        var isActive = $("#isActive").is(":checked");
        var started = $("#started").val();
        var ended = $("#ended").val();
        if (crudMode == "add") {
            var shift = new Shift(null, description, isActive, started, ended, null, null);
        }
        else if (crudMode == "update") {
            var shift = new Shift(localShift.shiftId, description, isActive, started, ended, null, null)
        }
        return shift;
    }

    function doFetchByRow(row) {
        var shiftId = row["shiftId"];
        var description = row["description"];
        var isActive = row["isActive"];
        var started = row["started"]
        var ended = row["ended"];
        var startedString = row["startedString"];
        var endedString = row["endedString"];
        localShift = new Shift(shiftId, description, isActive, started, ended, startedString, endedString);
        return localShift;
    }

    function fillForm(shift) {
        $("#description").val(shift.description);
        $("#started").val(shift.startedString);
        $("#ended").val(shift.endedString);
        if (shift.isActive == "Yes") {
            $("#isActive").attr("checked", true);
        }
    }

    $("#form1").validate({
        rules: {
            description: {
                required: true
            },
            started: {
                required: true
            },
            ended: {
                required: true
            }
        },
        messages: {
            description: {
                required: "Please provide a description"
            },
            started: {
                required: "Please provide a start time"
            },
            ended: {
                required: "Please provide a end time"
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

    function doPost(shift, action) {
        $.ajax({
            type: 'POST',
            url: action,
            data: {
                'shiftId': shift.shiftId,
                'description': shift.description,
                'isActive': shift.isActive,
                'started': shift.started,
                'ended': shift.ended
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

    $("#list").jqGrid({
        url: fetchAction,
        datatype: "json",
        mtype: "POST",
        colNames: ["Shift ID", "Description", "Started", "Ended", "Started", "Ended", "Activated?", "Create By", "Modified By"],
        colModel: [
            { name: "shiftId", sortable: false, hidden: true },
            { name: "description", sortable: false },
            { name: "started", sortable: false, hidden: true },
            { name: "ended", sortable: false, hidden: true },
            { name: "startedString", sortable: false },
            { name: "endedString", sortable: false },
            { name: "isActive", sortable: false, formatter: "checkbox", value: "1:0", align: 'center' },
            { name: "createdBy.userId", sortable: false },
            { name: "modifiedBy.userId", sortable: false }
        ],
        pager: "#pager",
        rowNum: 5,
        rownumbers: true,
        rowList: [5, 10, 15],
        height: 'auto',
        width: '520',
        caption: "Shift Data",
        onSelectRow: function (id) {
            var row = $(this).getRowData(id);
            if (crudMode != null || crudMode != "add") {
                localShift = doFetchByRow(row);
            }
        }
    });

});