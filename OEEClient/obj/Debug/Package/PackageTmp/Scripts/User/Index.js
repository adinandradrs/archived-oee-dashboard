$(document).ready(function () {

    var crudMode = "";
    var localUser = null;

    function User(userUid, userId, password, firstName, lastName, isActive, groupId, userGroupId) {
        this.userUid = userUid,
        this.userId = userId;
        this.password = password;
        this.firstName = firstName;
        this.lastName = lastName;
        this.isActive = isActive;
        this.groupId = groupId;
        this.userGroupId = userGroupId;
    }

    $("#radio1").click(function () {
        $("#userId").attr("readonly", false);
        $("#tabs").tabs("option", "active", 0);
        $(".form").show();
        crudMode = "add";
        clearSelection();
        clearForm();
        $("#submit").val("Submit");
        localUser = null;
    });

    $("#radio2").click(function () {
        $("#userId").attr("readonly", true);
        if (localUser == null) {
            alert("Please select user to update");
        }
        else {
            crudMode = "update";
            $("#tabs").tabs("option", "active", 0);
            $(".form").show();
            fillForm(localUser);
            $("#submit").val("Update");
        }
        clearSelection();
    });

    $("#radio3").click(function () {
        crudMode = "delete";
        if (localUser != null) {
            doPost(localUser, deleteAction);
        }
        else {
            alert("Please select user to delete");
        }
        localUser = null;
        clearSelection();
    });

    function doFetch() {
        var userId = $("#userId").val();
        var firstName = $("#firstName").val();
        var lastName = $("#lastName").val();
        var password = $("#password").val();
        var isActive = $("#isActive").is(":checked");
        var groupId = $("#groupId").val();
        if (crudMode == "add") {
            var user = new User(null, userId, password, firstName, lastName, isActive, groupId, null);
        }
        else if (crudMode == "update") {
            var user = new User(localUser.userUid, userId, password, firstName, lastName, isActive, groupId, localUser.userGroupId);
        }
        return user;
    }

    function doFetchByRow(row) {
        var userUid = row["userId.userUid"];
        var userId = row["userId.userId"];
        var firstName = row["userId.firstName"];
        var lastName = row["userId.lastName"];
        var password = row["userId.password"];
        var isActive = row["userId.isActive"];
        var groupId = row["groupId.groupId"];
        var userGroupId = row["userGroupId"];
        localUser = new User(userUid, userId, password, firstName, lastName, isActive, groupId, userGroupId);
        return localUser;
    }

    function fillForm(user) {
        $("#userId").val(user.userId);
        $("#firstName").val(user.firstName);
        $("#lastName").val(user.lastName);
        $("#password").val(user.password);
        $("#groupId").val(user.groupId);
        if (user.isActive == "Yes") {
            $("#isActive").attr("checked", true);
        }
    }

    $("#form1").validate({
        rules: {
            userId: {
                required: true,
                minlength: 5
            },
            password: {
                required: true,
                minlength: 5
            },
            firstName: {
                required: true
            },
            lastName: {
                required: true
            }
        },
        messages: {
            userId: {
                required: "Please provide a User ID",
                minlength: "Your User ID must be at least 5 characters long"
            },
            password: {
                required: "Please provide a password",
                minlength: "Your password must be at least 5 characters long"
            },
            firstName: {
                required: "Please provide a first name"
            },
            lastName: {
                required: "Please provide a last name"
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

    function doPost(user, action) {
        $.ajax({
            type: 'POST',
            url: action,
            data: {
                'userUid': user.userUid,
                'userId': user.userId,
                'firstName': user.firstName,
                'lastName': user.lastName,
                'password': user.password,
                'isActive': user.isActive,
                'groupId': user.groupId,
                'userGroupId': user.userGroupId

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
        colNames: ["User UID", "User ID", "Password", "First Name", "Last Name", "Activated?", "Group ID", "Group", "User Group ID"],
        colModel: [
            { name: "userId.userUid", sortable: false, hidden: true },
            { name: "userId.userId", sortable: false },
            { name: "userId.password", sortable: false, hidden: true },
            { name: "userId.firstName", sortable: false },
            { name: "userId.lastName", sortable: false },
            { name: "userId.isActive", sortable: false, formatter: "checkbox", align:'center'},
            { name: "groupId.groupId", sortable: false, hidden: true },
            { name: "groupId.description", sortable: false },
            { name: "userGroupId", sortable: false, hidden: true }
        ],
        pager: "#pager",
        rowNum: 5,
        rownumbers: true,
        rowList: [5, 10, 15],
        height: 'auto',
        width: '520',
        grouping: true,
        groupingView: {
            groupField: ['groupId.description'],
            groupColumnShow: false,
            groupText: ['<center><b>{0} - {1} User(s)</b></center>']
        },
        caption: "User Data",
        onSelectRow: function (id) {
            var row = $(this).getRowData(id);
            if (crudMode != null || crudMode != "add") {
                localUser = doFetchByRow(row);
            }
        }
    });



});