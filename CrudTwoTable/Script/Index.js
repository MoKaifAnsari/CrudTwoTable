$(document).ready(function () {
    ShowStuDetail();
    reset();

    $("#sbtn").click(function () {
        InserUpdateStudentDerail();
    });
    $("#rbtn").click(function () {
        reset();
    });
});
function InserUpdateStudentDerail() {
    try {
        $.post("/Information/InsertUpdateStudentDetail", {
            sid: $("#sid").val(),
            fname: $("#txtfname").val().trim(),
            lname: $("#txtlname").val().trim(),
            username: $("#txtusername").val().trim(),
            cid: $("#cid").val(),
            city: $("#txtcity").val().trim(),
            state: $("#txtstate").val().trim(),
            zip: $("#txtzip").val().trim(),
        },
            function (data) {
                if (data.Message != "") {
                    alert(data.Message);
                    ShowStuDetail();
                }
                if (data.Status == "1" || data.Status == "2")
                {
                    reset();
                }
            });

    }
    catch (e) {
        alert("Error in InsertUpdateStudentDetail :" + e.Message);
    }
}
function reset() {
    $("#sid").val("0");
    $("#txtfname").val("");
    $("#txtlname").val("");
    $("#txtusername").val("");
    $("#cid").val("0");
    $("#txtcity").val("");
    $("#txtstate").val("");
    $("#txtzip").val("");
}
function ShowStuDetail() {
    try {
        $.post("/Information/ShowStudet", {
        },
            function (data) {
                if (data.Message != "") {
                    alert(data.Message);
                }
                if (data.Grid != "") {
                    $("#dvGrid").html(data.Grid);
                }
            });
    }
    catch (e) {
        alert("Error in ShowStudent :" + e.Message);
    }
}
function EditStuDetail(sid) {
    try {
        $.post("/Information/EditStudentDetail",
            { sid: sid },
            function (data) {
                if (data.Message != "") {
                    alert(data.Message);
                }
                if (data.Status == "1") {
                    $("#sid").val(data.sid);
                    $("#txtfname").val(data.fname);
                    $("#txtlname").val(data.lname);
                    $("#txtusername").val(data.username);
                    $("#cid").val(cid);
                    $("#txtcity").val(data.city);
                    $("#txtstate").val(data.state);
                    $("#txtzip").val(data.zip);
                }
            });
    } catch (e) {
        alert("Error in ShowStudentDetail: " + e.message)
    }
}
function DeleteStuDetail(sid) {
    try {
        $.post("/Information/DeleteStudent/",
            { sid: sid },
            function (data) {
                alert(data.Message);
                ShowStuDetail();
            },
        );
    }
    catch (e) {
        alert("Error in ShowStateMaster: " + e.message)
    }
}
