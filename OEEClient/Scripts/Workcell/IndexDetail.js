var localTagMap = null;
var tagMapId = null;
var crudDetailMode = "";

function TagMap(tagMapId, plcTag, isActive, machineId, tagType) {
    this.tagMapId = tagMapId;
    this.plcTag = plcTag;
    this.isActive = isActive;
    this.machineId = machineId;
    this.tagType = tagType;
}

function doFetch() {
    var machineId = vMachineId;
    var plcTag = $("#plcTag").val();
    var isActive = $("#isActive2").is(":checked");
    var tagType = $("#tagType").val();
    if (crudDetailMode == "add") {
        var tagMap = new TagMap(null, plcTag, isActive, machineId, tagType);
    }
    else if (crudDetailMode == "update") {
        var tagMap = new TagMap(localTagMap.tagMapId, plcTag, isActive, machineId, tagType);
    }
    localTagMap = tagMap;
    return localTagMap;
}

$(document).ready(function () {

    $("#radioset-detail").buttonset();
    $("#tabs-detail").tabs();
    $(".form-detail").hide();
    $("#tabs-detail").tabs("option", "active", 1);

    function clearSelection() {
        document.getElementById("radio4").checked = false;
        document.getElementById("radio5").checked = false;
        document.getElementById("radio6").checked = false;
    }

    function clearForm() {
        $('#tableFormDetail')
            .find(':radio, :checkbox').removeAttr('checked').end()
            .find('textarea, :text, :password, select').val('')
        return false;
    }

    function doFetchByRow(row) {
        var machineId = row["machineId.machineId"];
        var plcTag = row["plcTag"];
        var isActive = row["isActive"];
        var tagMapId = row["tagMapId"];
        var tagType = row["tagType"]
        localTagMap = new TagMap(tagMapId, plcTag, isActive, machineId, tagType);
        //console.log(localTagMap.tagMapId);
    }


    function fillForm(localTagMap) {
        vMachineId = localTagMap.machineId;
        $("#plcTag").val(localTagMap.plcTag);
        $("#tagType").val(localTagMap.tagType);
        if (localTagMap.isActive == "Yes") {
            $("#isActive2").attr("checked", true);
        }
    }

    $("#radio4").click(function () {
        $("#tabs-detail").tabs("option", "active", 0);
        $(".form-detail").show();
        crudDetailMode = "add";
        clearSelection();
        clearForm();
        $("#submit2").val("Submit");
        localTagMap = null;
    });

    $("#radio5").click(function () {
        if (localTagMap == null) {
            alert("Please select Tag to update");
        }
        else {
            crudDetailMode = "update";
            $("#tabs-detail").tabs("option", "active", 0);
            $(".form-detail").show();
            fillForm(localTagMap);
            $("#submit2").val("Update");
        }
        clearSelection();
    });

    $("#radio6").click(function () {
        crudDetailMode = "delete";
        if (localTagMap != null) {
            $.ajax({
                type: 'POST',
                url: deleteDetailAction,
                data: {
                    'tagMapId': localTagMap.tagMapId
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
                }
            });
        }
        else {
            alert("Please select Tag to delete");
        }
        localWorkcell = null;
        clearSelection();
    });

    $("#list-detail").jqGrid({
        url: fetchDetailAction,
        datatype: "json",
        mtype: "POST",
        colNames: ["Tag Map ID", "PLC Tag", "Tag Type", "Activated?", "Machine ID"],
        colModel: [
            { name: "tagMapId", sortable: false, hidden: true },
            { name: "plcTag", sortable: false },
            { name: "tagType", sortable: false },
            { name: "isActive", sortable: false, formatter: "checkbox", align: 'center' },
            { name: "machineId.machineId", hidden: true }
        ],
        pager: "#pager-detail",
        rowNum: 5,
        rownumbers: true,
        rowList: [5, 10, 15],
        height: 'auto',
        width: '520',
        emptyrecords: 'There is no data to show.',
        viewrecords: true,
        caption: "Tag Map Data",
        onSelectRow: function (id) {
            var row = $(this).getRowData(id);
            if (crudDetailMode != null || crudDetailMode != "add") {
                doFetchByRow(row);
            }
        }
    });


});

function doPost(tagMap, action) {
    $.ajax({
        type: 'POST',
        url: action,
        data: {
            'machineId': tagMap.machineId,
            'plcTag': tagMap.plcTag,
            'tagType' : tagMap.tagType,
            'isActive': tagMap.isActive,
            'tagMapId': tagMap.tagMapId
        },
        success: function (data) {
            alert(data.result + " " + data.msg);
            
        }
    });
}

function onSubmitClick() {
    if ($("#plcTag").val() != "") {
        if (crudDetailMode == "add") {
            doPost(doFetch(), addDetailAction);
        }
        else if (crudDetailMode == "update") {
            doPost(doFetch(), updateDetailAction);
        }
    }
    else {
        alert("Please fill Plc Tag");
    }
}