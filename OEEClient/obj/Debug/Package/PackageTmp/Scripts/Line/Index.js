$().ready(function () {

    var crudMode = "";
    var localLine;

    function Line(lineId, description, isActive, cAvailable, cPerformance, cQuality, wAvailable, wPerformance, wQuality) {
        this.lineId = lineId;
        this.description = description;
        this.isActive = isActive;
        this.cAvailable = cAvailable;
        this.cPerformance = cPerformance;
        this.cQuality = cQuality;
        this.wAvailable = wAvailable;
        this.wPerformance = wPerformance;
        this.wQuality = wQuality;
    }

    $("#radio1").click(function () {
        $("#tabs").tabs("option", "active", 0);
        $(".form").show();
        crudMode = "add";
        clearSelection();
        clearForm();
        $("#submit").val("Submit");
        localLine = null;
    });

    $("#radio2").click(function () {
        $("#lineId").attr("readonly", true);
        if (localLine == null) {
            alert("Please select line to update");
        }
        else {
            crudMode = "update";
            $("#tabs").tabs("option", "active", 0);
            $(".form").show();
            fillForm(localLine);
            $("#submit").val("Update");
        }
        clearSelection();
    });

    $("#radio3").click(function () {
        crudMode = "delete";
        if (localLine != null) {
            doPost(localLine, deleteAction);
        }
        else {
            alert("Please select line to delete");
        }
        localLine = null;
        clearSelection();
    });

    function doFetch() {
        var description = $("#description").val();
        var isActive = $("#isActive").is(":checked");
        var cAvailable = $("#cAvailable").val();
        var cPerformance = $("#cPerformance").val();
        var cQuality = $("#cQuality").val();
        var wAvailable = $("#wAvailable").val();
        var wPerformance = $("#wPerformance").val();
        var wQuality = $("#wQuality").val();
        if (crudMode == "add") {
            var line = new Line(null, description, isActive, cAvailable, cPerformance, cQuality, wAvailable, wPerformance, wQuality);
        }
        else if (crudMode == "update") {
            var line = new Line(localLine.lineId, description, isActive, cAvailable, cPerformance, cQuality, wAvailable, wPerformance, wQuality)
        }
        return line;
    }

    function doFetchByRow(row) {
        var lineId = row["lineId"];
        var description = row["description"];
        var isActive = row["isActive"];
        var cAvailable = row["cAvailable"];
        var cPerformance = row["cPerformance"];
        var cQuality = row["cQuality"];
        var wAvailable = row["wAvailable"];
        var wPerformance = row["wPerformance"];
        var wQuality = row["wQuality"];
        localLine = new Line(lineId, description, isActive, cAvailable, cPerformance, cQuality, wAvailable, wPerformance, wQuality);
        return localLine;
    }

    function fillForm(line) {
        $("#description").val(line.description);
        if (line.isActive == "Yes") {
            $("#isActive").attr("checked", true);
        }
        $("#cAvailable").val(line.cAvailable);
        $("#cPerformance").val(line.cPerformance);
        $("#cQuality").val(line.cQuality);
        $("#wAvailable").val(line.wAvailable);
        $("#wPerformance").val(line.wPerformance);
        $("#wQuality").val(line.wQuality);
    }

    $("#form1").validate({
        rules: {
            description: {
                required: true
            },
            cAvailable: {
                required: true
            },
            cPerformance: {
                required: true
            },
            cQuality: {
                required: true
            }
        },
        messages: {
            description: {
                required: "Please provide a description"
            },
            cAvailable: {
                required: "Please provide a available",
                range: [1, 100]
            },
            cPerformance: {
                required: "Please provide a performance",
                range: [1, 100]
            },
            cQuality: {
                required: "Please provide a quality",
                range: [1, 100]
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

    function doPost(line, action) {
        $.ajax({
            type: 'POST',
            url: action,
            data: {
                'lineId': line.lineId,
                'description': line.description,
                'isActive': line.isActive,
                'cAvailable' : line.cAvailable,
                'cPerformance' : line.cPerformance,
                'cQuality' : line.cQuality,
                'wAvailable': line.wAvailable,
                'wPerformance': line.wPerformance,
                'wQuality': line.wQuality
            },
            success: function (data) {
                alert(data.result + " " + data.msg);
                $("#list").jqGrid().setGridParam(
                    {
                        datatype:'json', 
                        page:1, 
                        url : fetchAction                    }
                ).trigger("reloadGrid");
                //$("#list").trigger("reloadGrid");
            }
        });
    }

    $("#list").jqGrid({
        url: fetchAction,
        datatype: "json",
        mtype: "POST",
        colNames: ["Line ID", "Description", "Available", "Performance", "Quality", "Low Available", "Low Performance", "Quality", "Activated?", "Create By", "Modified By"],
        colModel: [
            { name: "lineId", sortable: false, hidden: true },
            { name: "description", sortable: false },
            { name: "cAvailable", sortable: false },
            { name: "cPerformance", sortable: false },
            { name: "cQuality", sortable: false },
            { name: "wAvailable", sortable: false },
            { name: "wPerformance", sortable: false },
            { name: "wQuality", sortable: false },
            { name: "isActive", sortable: false, formatter: "checkbox", value: "1:0", align: 'center' },
            { name: "createdBy.userId", sortable: false, hidden:true },
            { name: "modifiedBy.userId", sortable: false, hidden: true }
        ],
        pager: "#pager",
        pagination: true,
        viewrecords:true,
        rowNum: 5,
        rownumbers: true,
        rowList: [5, 10, 15],
        height: 'auto',
        width: '520',
        caption: "Line Data",
        onSelectRow: function (id) {
            var row = $(this).getRowData(id);
            console.log(row["isActive"]);
            if (crudMode != null || crudMode != "add") {
                localLine = doFetchByRow(row);
            }
        }
    });

});