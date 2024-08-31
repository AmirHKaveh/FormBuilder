
function createForm() {
    $.get("/Index?handler=CreateForm", function (result) {

        $("#cu_div").html(result);
    });
}

function insertForm() {
    console.log($("#FormTitle").val());
    if ($("#FormTitle").val() == "") {
        alert("لطفا عنوان را وارد نمایید !");
        return false;
    }

    var form = {
        Title: $("#FormTitle").val(),
        Name: $("#FormName").val(),
    }
    $.ajax({
        method: 'POST',
        url: '/Index?handler=CreateForm',
        data: {
            form: form,
            __RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (result) {
            if (result.status == "ok") {
                alert("اطلاعات با موفقیت ثبت گردید");
                $("#FormTitle").val('');
                $("#FormName").val('');
                $("#FormId").val(result.id);
            }
            else if (result.status == "exist") {
                alert("فرم با این نام قبلا ثبت شده است");
            }
        },
        error: function (result) {
        }
    });
}

function insertField() {
    var frm = $("#fieldFrm").valid();
    var formId = $("#FormId").val();
    var key = [];
    var value = [];
    var options = [];

    if (frm == true) {
        if ($("#FormId").val() == 0 && $("#form_ddl").val() == "-1") {
            alert("فرمی ثبت نشده است !");
            return false;
        }
        if ($("#FormId").val() == 0)
            formId = $("#form_ddl").val();



        $(".txtKey").each(function () {
            if ($(this).val() != "" && $(this).val() != undefined) {
                key.push($(this).val());
            }
        });
        $(".txtValue").each(function () {
            if ($(this).val() != "" && $(this).val() != undefined) {
                value.push($(this).val());
            }
        });

        if (key.length != value.length) {
            alert("کلید و مقدار را بصورت صحیح وارد نمایید");
            return false;
        }

        for (var i = 0; i < key.length; i++) {
            options.push({ key: key[i], value: value[i] });
        }
        if (($("#FieldType").val() == 4) && options.length <= 0) {

            alert("برای فیلد خود آیتم را وارد نمایید");
            return false;
        }

        var isactive = $('#IsRequired:checked').length;
        var status = "";
        if (isactive === 1) { status = "true"; }
        else { status = "false"; }

        var formField = {
            FormId: formId,
            Title: $("#Title").val(),
            UniqueName: $("#UniqueName").val(),
            FieldType: $("#FieldType").val(),
            MaxLength: $("#MaxLength").val(),
            Options: JSON.stringify(options),
            IsRequired: status,
        }
        $.ajax({
            method: 'POST',
            url: '/Index?handler=CreateFormFields',
            data: {
                formField: formField,
                __RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            success: function (result) {
                if (result.status == "ok") {
                    getFormFields(formId)
                    alert("اطلاعات با موفقیت ثبت گردید");
                    $("#fieldFrm")[0].reset();
                }
                else if (result.status == "exist") {
                    alert("فیلد با این شناسه قبلا ثبت شده است");
                }
            },
            error: function (result) {
            }
        });
    }
    else {
        return false;
    }
}

function addNewItem() {
    var option = '<div class="row"><div class="col-md-5">' +
        '<input type = "text" class="form-control txtKey" placeholder = "کلید" /></div>' +
        '<div class="col-md-5">' +
        '<input type="text" class="form-control txtValue" placeholder="مقدار" /></div>' +
        '<div class="col-md-1">' +
        '<span class="fa fa-close text-danger btnRemoveItem"></span>' +
        '</div></div>';

    $("#options_div").append(option);
}

function getFormFields(formId) {
    $.get("/Index?handler=ViewAllFieldsOfForm", { formId: formId }, function (result) {

        $("#viewForm_div").html(result);
    });
}

function updateForm() {
    if ($("#Form_Title").val() == "") {
        alert("لطفا عنوان را وارد نمایید !");
        return false;
    }

    var form = {
        Id: $("#FormId").val(),
        Title: $("#Form_Title").val(),
        Name: $("#Form_Name").val(),
    }
    $.ajax({
        method: 'POST',
        url: '/Index?handler=EditForm',
        data: {
            form: form,
            __RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (result) {
            if (result.status == "ok") {
                alert("اطلاعات با موفقیت ویرایش گردید");
                $("#Form_Title").val('');
                $("#Form_Name").val('');
            }
            else if (result.status == "exist") {
                alert("فرم با این نام قبلا ثبت شده است");
            }
        },
        error: function (result) {
        }
    });
}

function updateField() {
    var frm = $("#fieldFrmEdit").valid();
    var formId = $("#FormId").val();
    var key = [];
    var value = [];
    var options = [];

    if (frm == true) {
        if ($("#FormId").val() == 0 && $("#form_ddl").val() == "-1") {
            alert("فرمی ثبت نشده است !");
            return false;
        }
        if ($("#FormId").val() == 0)
            formId = $("#form_ddl").val();



        $(".txtKey").each(function () {
            if ($(this).val() != "" && $(this).val() != undefined) {
                key.push($(this).val());
            }
        });
        $(".txtValue").each(function () {
            if ($(this).val() != "" && $(this).val() != undefined) {
                value.push($(this).val());
            }
        });

        if (key.length != value.length) {
            alert("کلید و مقدار را بصورت صحیح وارد نمایید");
            return false;
        }

        for (var i = 0; i < key.length; i++) {
            options.push({ key: key[i], value: value[i] });
        }
        if (($("#FieldType").val() == 4 || $("#FieldType").val() == "Selectbox") && options.length <= 0) {
            alert("برای فیلد خود آیتم را وارد نمایید");
            return false;
        }

        var isactive = $('#IsRequired:checked').length;
        var status = "";
        if (isactive === 1) { status = "true"; }
        else { status = "false"; }

        var formField = {
            Id: $("#Id").val(),
            FormId: formId,
            Title: $("#Title").val(),
            UniqueName: $("#UniqueName").val(),
            FieldType: $("#FieldType").val(),
            MaxLength: $("#MaxLength").val(),
            Options: JSON.stringify(options),
            IsRequired: status,
        }
        $.ajax({
            method: 'POST',
            url: '/Index?handler=EditFormFields',
            data: {
                formField: formField,
                __RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            success: function (result) {
                if (result.status == "ok") {
                    getFormFields(formId)
                    alert("اطلاعات با موفقیت ویرایش گردید");
                    $("#fieldFrmEdit")[0].reset();
                    createForm();
                }
                else if (result.status == "exist") {
                    alert("فیلد با این شناسه قبلا ثبت شده است");
                }
            },
            error: function (result) {
            }
        });
    }
    else {
        return false;
    }
}


$("#options_div").on('click', '.btnRemoveItem', function () {
    $(this).parent().parent().remove();
});

function isOptionable(thisValue) {
    if (thisValue == 4 || thisValue == "Selectbox") {
        $("#options").removeClass('d-none');
    }
    else {
        $("#options").addClass('d-none');
    }

}


function submitForm() {
    var formName = document.getElementById("Form1"); //$("#viewForm_div").children('form').attr('id')
    var data = "";

    //$("#" + formName + " :input").not(':input[type=button], :input[type=submit], :input[type=reset]').each(function () {
    //   // data.append($(this).attr('name'), $(this).val().trim());

    //    data += $(this).attr('name') + "=" + $(this).val().trim() + "&";
    //});



    var json = Array.from(new FormData(formName)).map(function (e, i) {
        if (typeof (e[1]) != "object") {
            this[e[0]] = e[1];
            return this;
        }
    }.bind({}))[0];

    var fdata = new FormData();

    var frmId = $('#formId').val();
    fdata.append("FormId", frmId);

    var names = "";
    $('input[type="file"]').each(function (a, b) {
        var fileInput = $('input[type="file"]')[a];
        if (fileInput.files.length > 0) {
            var file = fileInput.files[0];
            names += fileInput.name + "|" + file.name + "=";
            fdata.append("Files", file);
        }
    });
    fdata.append("FilesName", names);

    var str = JSON.stringify(json);

    fdata.append("FormValues", str);
    for (var pair of fdata.entries()) {
        console.log(pair[0] + ', ' + pair[1]);
    }
    var data = JSON.stringify(fdata);

    $.ajax({
        method: 'POST',
        url: '/Index?handler=SubmitForm',
        contentType: false,
        processData: false,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: fdata,
        success: function (result) {
            if (result == "ok") {
                alert("اطلاعات با موفقیت ویرایش گردید");
                $("#fieldFrmEdit")[0].reset();
                createForm();
            }
            else if (result.status == "exist") {
                alert("فیلد با این شناسه قبلا ثبت شده است");
            }
        },
        error: function (result) {
        }
    });
}

function getFormFieldsTitle(formId) {
    $.get("/Index?handler=FormFieldsTitle", { formId: formId }, function (result) {
        $("#viewAllFormFields").html(result);
    })
}